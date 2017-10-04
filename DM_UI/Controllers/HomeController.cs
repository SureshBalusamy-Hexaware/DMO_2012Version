using DM_BusinessService;
using DM_UI.App_Start;
using DM_UI.Models;
using System;
using System.Web.Mvc;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using DM_BusinessEntities;
using System.Configuration;
using System.IO;
namespace DM_UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMasterSetup _MS;
        private readonly IHXRConfigurationMS _configMS;

        public HomeController()
        {
            _MS = new MasterSetupService();
            _configMS = new HXRConfigurationMSService();
        }

        //View Bags
        private void ViewBags(UIProperties.MasterSetupMenu MenuId)
        {
            int ToolID = (int)UIProperties.Tools.MasterSetup;
            //ViewBag.SelectedMenu = SelectedTabId;
            UIProperties.MenuTable.SelectedMenuId = (int)MenuId;
            UIProperties.MenuTable.GetMenu(ToolID);

            if (UIProperties.Sessions.UserName != "Hexaware")
            {
                Menu mnu = (Menu)UIProperties.MenuTable.ToolMenu
                    .Where(r => r.MenuId == (int)UIProperties.MasterSetupMenu.DBDeployment).ToList()[0];
                UIProperties.MenuTable.ToolMenu.Remove(mnu);
            }
            ViewBag.ToolIconPath = ((dynamic)(UIProperties.Tool_Logos.GetToolLogo(ToolID))).Logo;
            ViewBag.Menu = UIProperties.MenuTable.ToolMenu;
            ViewBag.UserName = UIProperties.Sessions.UserName;
        }
        private void ViewDatas()
        {
            ViewData["ClientID"] = UIProperties.Sessions.Client.Client_ID;
            ViewData["ProjectID"] = UIProperties.Sessions.Client.project_ID;
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel user)
        {

            if (ModelState.IsValid)
            {

                //if (UIProperties.Sessions.Client == null)
                //{
                //    InitializeSession();
                //}
                //client_ID = Convert.ToString(UIProperties.Sessions.Client.Client_ID);

                string RoleName = string.Empty;
                string StatusCode = string.Empty, Message = string.Empty;

                if (_MS.ValidateUser(Convert.ToString(user.UserName).Trim(), Convert.ToString(user.Password).Trim(), ref RoleName, ref StatusCode, ref Message))
                {
                    FormsAuthentication.SetAuthCookie(Convert.ToString(user.UserName).Trim(), false);
                    UIProperties.Sessions.RoleName = RoleName;
                    UIProperties.Sessions.UserName = Convert.ToString(user.UserName).Trim();
                    InitializeSession();
                    CommonHelper.DirectoryCheck();
                    return RedirectToAction("Index");
                }
                else
                {

                    //  ModelState.AddModelError("", "UserName/Password are Invalid! Please contact administrator.");
                    ModelState.AddModelError("", Message);

                }
            }
            return View(user);
        }

        private void InitializeSession()
        {
            string StatusCode = string.Empty, Message = string.Empty;
            UIProperties.Sessions.Client = _configMS.GetClientDetails(UIProperties.Sessions.UserName, ref StatusCode, ref  Message);
        }

        public ActionResult Index()
        {
            string client_ID = string.Empty;
            string ProjectId = string.Empty;
            if (UIProperties.Sessions.Client == null)
            {
                InitializeSession();
            }
            client_ID = Convert.ToString(UIProperties.Sessions.Client.Client_ID);
            ProjectId = Convert.ToString(UIProperties.Sessions.Client.project_ID);

            //ViewBag.IsAdmin = Convert.ToString(UIProperties.Sessions.RoleName).ToLower() == "admin" ? true : false;
            ViewBag.IsAdmin = UIProperties.Sessions.Client.IsAdmin;


            var Tools = _MS.GetUserTools(client_ID, ProjectId, Convert.ToString(UIProperties.Sessions.UserName));

            return View(Tools.Where(r => r.ACTIVE_FLAG == 1).ToList());
        }

        public ActionResult MasterSetup()
        {

            ViewBags(UIProperties.MasterSetupMenu.ProjectSetup);
            var _Client = new DMOClientEntitiy();

            _Client.Client_ID = UIProperties.Sessions.Client.Client_ID;
            _Client.Client_Name = UIProperties.Sessions.Client.Client_Name;
            _Client.project_ID = UIProperties.Sessions.Client.project_ID;
            return View(_Client);

        }
        [HttpGet]
        public ActionResult DBDeployment(DBDeployment dbDeployment)
        {

            ViewBags(UIProperties.MasterSetupMenu.DBDeployment);
            var _Client = new DMOClientEntitiy();

            _Client.Client_ID = UIProperties.Sessions.Client.Client_ID;
            _Client.Client_Name = UIProperties.Sessions.Client.Client_Name;
            _Client.project_ID = UIProperties.Sessions.Client.project_ID;


            return View();


        }
        [HttpPost]
        public string ReadSqlFile()
        {
            IDASEM _dasem = new DASEMService();
            string filePath = string.Empty;
            string SqlFileLocation = ConfigurationManager.AppSettings["Path_DBDeployFolder"];
            string EncryptedFile = ConfigurationManager.AppSettings["EncryptedFile"];
            SqlFileLocation = AppDomain.CurrentDomain.BaseDirectory + @"\" + SqlFileLocation;

            var httpRequest = System.Web.HttpContext.Current.Request;
            System.IO.DirectoryInfo exportExcelInfo = new DirectoryInfo(SqlFileLocation);

            foreach (FileInfo file in exportExcelInfo.GetFiles())
            {
                file.Delete();
            }
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    filePath = SqlFileLocation + "\\" + Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    _dasem.EncryptFile(filePath, SqlFileLocation + "\\" + EncryptedFile);
                }
            }
            return "";
        }
        public ActionResult ProcessWorkFlow()
        {
            ViewBags(UIProperties.MasterSetupMenu.ProcessOrchestration);
            ViewData["toolTaskEntity"] = this.GetWorkflowProcess();
            return View();
        }

        [ActionName("ManageRole")]
        public ActionResult UserWizardWithRoles()
        {
            ViewBags(UIProperties.MasterSetupMenu.AssingRole);
            return View("UserWizardWithRoles");
        }

        [ActionName("ADDRole")]
        public ActionResult UserWizardRoles()
        {
            ViewBags(UIProperties.MasterSetupMenu.CreateRole);
            return View("UserWizardRoles");

        }


        [HttpPost]
        public ActionResult SaveClient(DMOClientEntitiy FormInputs)
        {

            FormInputs.ModifiedBy = UIProperties.Sessions.UserName;
            _MS.SaveClient(FormInputs);
            return RedirectToAction("MasterSetup");
        }

        [HttpGet]
        public JsonResult GetProjectDetail(string Client_ID, string UserName)
        {

            // var lstProject = _MS.GetProjects(Client_ID);
            var lstProject = _MS.GetProjects(UIProperties.Sessions.Client.Client_ID, UserName);
            var rows = new
            {
                rows = (
                    from pro in lstProject
                    select new
                    {
                        i = pro.ID,
                        cell = new string[] {
                             pro.ID.ToString(),
                            pro.Project_ID,
                            pro.Project_Description,
                            pro.Active_Flag.ToString()
                            
                      }
                    }).ToArray()
            };

            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string GetAllProjectDetail(string Client_ID,string SelectedProectId, string UserName)
        {
            if (Client_ID == null) Client_ID = UIProperties.Sessions.Client.Client_ID;
            if (UserName == null) UserName = UIProperties.Sessions.UserName;
            var lstProject = _MS.GetProjects(Client_ID, UserName);

            string _ddl = "<select>";
            foreach (var item in lstProject)
            {
                if (item.Project_ID == SelectedProectId)
                {
                    _ddl += "<option selected='selected' value='" + item.Project_ID + "'>" + item.Project_ID + "</option>";
                }
                else
                    _ddl += "<option value='" + item.Project_ID + "'>" + item.Project_ID + "</option>";
            }
            _ddl += "</select>";
            return _ddl;
        }

        [HttpPost]
        public string SaveProject(DMOProjectEntitiy PrjoectEntry)
        {
            PrjoectEntry.Client_ID = UIProperties.Sessions.Client.Client_ID;
            PrjoectEntry.Modified_By = UIProperties.Sessions.UserName;

            if (_MS.SaveProject(PrjoectEntry))
            {
                return "Saved successfully.";
            }
            else
            {
                return "Save failed.";
            }

        }

        [HttpPost]
        public string ActivateProject(DMOProjectEntitiy PrjoectEntry)
        {

            string StatusCode = string.Empty, Message = string.Empty;

            //PrjoectEntry.Client_ID = UIProperties.Sessions.Client.Client_ID;
            PrjoectEntry.Modified_By = UIProperties.Sessions.UserName;

            _MS.ActivateProject(PrjoectEntry, ref StatusCode, ref Message);

            return Message;

        }

        [HttpGet]
        public JsonResult GetUserDetail(string Client_ID, string Project_ID)
        {
            InitializeSession();

            //var lstUsers = _MS.GetUserDetails(Client_ID, Project_ID);
            var lstUsers = _MS.GetUserDetails(UIProperties.Sessions.Client.Client_ID, Project_ID);
            var rows = new
            {
                rows = (
                    from pro in lstUsers
                    select new
                    {
                        i = pro.User_ID,
                        cell = new string[] {
                            pro.User_ID.ToString(),
                            pro.Project_ID,
                            pro.User_name,
                            pro.email_ID, 
                            pro.ROLE_NAME,
                            pro.Active_Flag.ToString(),
                            
                      }
                    }).ToArray()
            };

            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string SaveUsers(DMOUserEntitiy UserEntry)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            UserEntry.Client_ID = UIProperties.Sessions.Client.Client_ID;
            //UserEntry.Project_ID = UIProperties.Sessions.Client.project_ID;
            UserEntry.Last_Modified_By = UIProperties.Sessions.UserName;

            bool mresult = false;
            if (UserEntry.User_ID > 0)
            {
                mresult = _MS.UpdateUser(UserEntry);
            }
            else
            {
                UserEntry.Password = "Password123";
                mresult = _MS.SaveUser(UserEntry, ref StatusCode, ref Message);
            }

            if (mresult)
            {
                return "Saved successfully.";
            }
            else
            {
                return Message;
            }
        }
        [HttpGet]
        public JsonResult GetUserTools(string Client_ID, string Project_ID, string UserName)
        {

            var lstUserTools = _MS.GetUserTools(Client_ID, Project_ID, UserName);
            var rows = new
            {
                rows = (
                    from pro in lstUserTools
                    select new
                    {
                        i = pro.ID,
                        cell = new string[] {
                            pro.ID.ToString(),
                            pro.Tool_Name,
                            pro.Tool_Description, 
                            pro.ACTIVE_FLAG.ToString()
                            
                      }
                    }).ToArray()
            };

            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveUserTools(List<UserToolEntity> UserToolEntry)
        {
            foreach (UserToolEntity obj in UserToolEntry)
            {
                _MS.UpdateTools(Convert.ToString(obj.ID), obj.ACTIVE_FLAG.ToString());
            }

            return Json("Saved successfully.", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string SaveWorkflowProcess(string workflowItem)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            var seq = 1;
            var lstItem = new List<ToolsEntity>();
            string[] items = workflowItem.Split(',');

            foreach (string item in items)
            {
                if (item == UIProperties.Tools.Automaton.ToString())
                    lstItem.Add(new ToolsEntity { ToolID = (int)UIProperties.Tools.Automaton, ToolName = item, Tool_TaskSequence = seq++ });
                else if (item == UIProperties.Tools.DataProfiler.ToString())
                    lstItem.Add(new ToolsEntity { ToolID = (int)UIProperties.Tools.DataProfiler, ToolName = item, Tool_TaskSequence = seq++ });
                else if (item == UIProperties.Tools.HexaRule.ToString())
                    lstItem.Add(new ToolsEntity { ToolID = (int)UIProperties.Tools.HexaRule, ToolName = item, Tool_TaskSequence = seq++ });
                else if (item == UIProperties.Tools.DataRecon.ToString())
                    lstItem.Add(new ToolsEntity { ToolID = (int)UIProperties.Tools.DataRecon, ToolName = item, Tool_TaskSequence = seq++ });
                else if (item == UIProperties.Tools.DIMAPLUS.ToString())
                    lstItem.Add(new ToolsEntity { ToolID = (int)UIProperties.Tools.DIMAPLUS, ToolName = item, Tool_TaskSequence = seq++ });
            }

            return _MS.SaveWorkflowProcess(UIProperties.Sessions.Client.Client_ID, UIProperties.Sessions.Client.project_ID, lstItem, UIProperties.Sessions.UserName, ref StatusCode, ref Message);

            // return "success";
        }

        private JsonResult GetWorkflowProcess()
        {
            string StatusCode = string.Empty, Message = string.Empty;

            string ClientID = UIProperties.Sessions.Client.Client_ID;
            string ProjectID = UIProperties.Sessions.Client.project_ID;

            List<ToolsEntity> toolTaskEntity = _MS.GetWorkflowProcess(ClientID, ProjectID, ref StatusCode, ref Message);

            if (toolTaskEntity != null)
                return Json(toolTaskEntity, JsonRequestBehavior.AllowGet);
            else
                return null;
        }

        [HttpGet]
        public ActionResult GetRoles(string Client_ID, string Project_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            int? RoleId = UIProperties.Sessions.Client.IsAdmin;

            if (RoleId == 1)
            {
                List<RoleEntity> roleEntity = _MS.GetRoleList(Client_ID, Project_ID, ref StatusCode, ref Message);

                var data = new
                {
                    total = 0,
                    page = 0,
                    records = 0,
                    rows = (
                        from item in roleEntity
                        select new
                        {
                            item.Role_ID,
                            cell = new string[] {
                                 item.Role_ID.ToString(),
                             item.Role_Name,
                         item.Role_Description
                            }
                        }).ToArray()
                };

                if (roleEntity != null)
                {                  
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                    return null;
            }
            else
            {
                ViewData["IsAdmin"] = "No";
                var data = new { Status = "0" };
                return Json(data, JsonRequestBehavior.AllowGet); ;
            }

        }

        [HttpGet]
        public string ddlRoles(string Client_ID, string Project_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            List<RoleEntity> roleEntity = _MS.GetRoleList(Client_ID, Project_ID, ref StatusCode, ref Message);
            string _ddl = "<select>";

            foreach (var item in roleEntity)
            {
                _ddl += "<option value='" + item.Role_ID + "'>" + item.Role_Name + "</option>";
            }
            _ddl += "</select>";
            return _ddl;

        }

        [HttpPost]
        public ActionResult CreateUpdateRole(RoleModel _role, string Command, string _roleId)
        {

            var query = from state in ModelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();

            string StatusCode = string.Empty, Message = string.Empty;
            string _ClientID = UIProperties.Sessions.Client.Client_ID;
            string _ProjectID = UIProperties.Sessions.Client.project_ID;

            if (Command == "Clear")
            {
                TempData["AlertMsg"] = "";
                return RedirectToAction("ADDRole");
            }
            if (ModelState.IsValid)
            {
                if (Command == "Save")
                {
                    _MS.Create_Update_Role(_ClientID, _ProjectID, _role.Role_Name, _role.Role_Desc, UIProperties.Sessions.UserName, ref StatusCode, ref Message);
                    if (Message == "Success") TempData["AlertMsg"] = Command + "successfully."; else TempData["AlertMsg"] = Message;
                    return RedirectToAction("ADDRole");
                }
                else if (Command == "Update")
                {
                    _MS.Create_Update_Role(_ClientID, _ProjectID, _role.Role_Name, _role.Role_Desc, UIProperties.Sessions.UserName, ref StatusCode, ref Message);
                    if (Message == "Success") TempData["AlertMsg"] = Command + "successfully."; else TempData["AlertMsg"] = Message;
                    return RedirectToAction("ADDRole");
                }
                else
                {
                    TempData["AlertMsg"] = "";
                    return RedirectToAction("ADDRole");
                }
            }
            else
            {
                TempData["AlertMsg"] = "";
                TempData["Command"] = Command == "Update" ? Command : "";
                ViewDatas();
                ViewBags(UIProperties.MasterSetupMenu.CreateRole);
                return View("UserWizardRoles");

            }
        }


        [HttpGet]
        public JsonResult GetRoleMenus(int role_id)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            List<RoleMenuEntity> roleMenuEntity = _MS.GetRoleMenuList(role_id, ref StatusCode, ref Message);

            var data = new
            {
                mydata = (
                    from item in roleMenuEntity
                    let pmenuid = item.Parent_Menu_ID
                    select new
                    {
                        id = item.Menu_ID,
                        menuname = item.Menu_Name,
                        menuid = Convert.ToString(item.Menu_ID),
                        pmenuid = Convert.ToString(pmenuid),
                        enbl = item.Active_Flag == true ? "1" : "0",
                        level = pmenuid == 0 ? "0" : "1",
                        parent = pmenuid == 0 ? "" : Convert.ToString(pmenuid),
                        isLeaf = pmenuid == 0 ? false : true,
                        expanded = false
                    }).ToArray()
            };

            if (roleMenuEntity != null)
                return Json(data, JsonRequestBehavior.AllowGet);
            else
                return null;
        }

        [HttpGet]
        public string UpdateRoleMenu(int role_id, string menu_list)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            _MS.UpdateRoleMenu(role_id, menu_list, ref StatusCode, ref Message);

            if (StatusCode == "0")
                Message = "Menu updated successfully.";

            return Message;
        }

        public ActionResult ChangePassword()
        {
            ChangePasswordViewModel _ChangePwdViewModel = new ChangePasswordViewModel();
            _ChangePwdViewModel.UserName = UIProperties.Sessions.UserName;
            ViewBag.ProjectId = new SelectList(new List<SelectListItem>() { 
                new SelectListItem() { Selected = true, Text = UIProperties.Sessions.Client.project_ID, Value = UIProperties.Sessions.Client.project_ID } 
            }, "Value", "Text");
            return View(_ChangePwdViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel _ChangePwdViewModel)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            if (ModelState.IsValid)
            {
                _ChangePwdViewModel.ProjectId = UIProperties.Sessions.Client.project_ID;
                _ChangePwdViewModel.UserName = UIProperties.Sessions.UserName;

                _MS.ChangePassword(
                    UIProperties.Sessions.Client.Client_ID
                    , UIProperties.Sessions.Client.project_ID
                    , _ChangePwdViewModel.OldPassword
                    , _ChangePwdViewModel.NewPassword
                    , _ChangePwdViewModel.UserName
                    , null
                    , "Password_Change"
                    , ref StatusCode
                    , ref Message);
                TempData["heading"] = "Change My Password";
                TempData["msg"] = Message;
                TempData["fColor"] = "black";
                TempData["StatusCode"] = StatusCode;
                switch (StatusCode)
                {
                    case "0":
                        TempData["fColor"] = "green";
                        break;
                    case "1":
                        TempData["fColor"] = "red";
                        break;
                    default:
                        TempData["fColor"] = "black";
                        break;
                }

                return RedirectToAction("Result");
                //return RedirectToAction("Result", new { heading = "Change My Password", msg = Message });


            }

            ViewBag.ProjectId = new SelectList(new List<SelectListItem>() { 
                new SelectListItem() { Selected = false, Text = UIProperties.Sessions.Client.project_ID, Value = UIProperties.Sessions.Client.project_ID } 
            }, "Value", "Text");
            _ChangePwdViewModel.UserName = UIProperties.Sessions.UserName;
            return View(_ChangePwdViewModel);
        }
        [AllowAnonymous]
        public ActionResult Result()
        {
            ViewBag.Response = TempData["msg"];
            ViewBag.Heading = TempData["heading"];
            ViewBag.color = TempData["fColor"];
            ViewBag.StatusCode = TempData["StatusCode"];
            return View();
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel _ForgotPasswordViewModel)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            if (ModelState.IsValid)
            {
                _MS.ChangePassword(
                    null
                    , null
                    , null
                    , _ForgotPasswordViewModel.NewPassword
                    , _ForgotPasswordViewModel.UserName
                    , _ForgotPasswordViewModel.Email
                    , "ForgotPassword"
                    , ref StatusCode
                    , ref Message);

                TempData["heading"] = "Forgot Password";
                TempData["msg"] = Message;
                TempData["fColor"] = "black";
                TempData["StatusCode"] = StatusCode;

                switch (StatusCode)
                {
                    case "0":
                        TempData["fColor"] = "green";
                        break;
                    case "1":
                        TempData["fColor"] = "red";
                        break;
                    default:
                        TempData["fColor"] = "black";
                        break;
                }

                return RedirectToAction("Result");
            }
            return View(_ForgotPasswordViewModel);
        }
    }
}
