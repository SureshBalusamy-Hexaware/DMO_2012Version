using DM_BusinessService;
using DM_UI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DM_BusinessEntities;
using DM_UI.Models;

namespace DM_UI.Controllers
{
    public class HexaruleController : Controller
    {
        private readonly IHXRConfigurationMS _configMS;
        private readonly IRules _Irules;

        public HexaruleController()
        {
            _configMS = new HXRConfigurationMSService();
            _Irules = new HXRRuleService();

        }
        //ViewBags
        private void ViewBags(UIProperties.HexaRuleMenu MenuId)
        {
            int ToolID = (int)UIProperties.Tools.HexaRule;
            UIProperties.MenuTable.SelectedMenuId = (int)MenuId;
            UIProperties.MenuTable.GetMenu(ToolID);
            ViewBag.ToolIconPath = ((dynamic)(UIProperties.Tool_Logos.GetToolLogo(ToolID))).Logo;
            ViewBag.Menu = UIProperties.MenuTable.ToolMenu;
            ViewBag.UserName = UIProperties.Sessions.UserName;
        }
        private void ViewDatas()
        {
            ViewData["ClientID"] = UIProperties.Sessions.Client.Client_ID;
            ViewData["ProjectID"] = UIProperties.Sessions.Client.project_ID;
        }
        //
        // GET: /Hexarule/
        public ActionResult Index()
        {
            TempData["ToolId"] = (int)UIProperties.Tools.HexaRule;
            TempData["MenuId"] = (int)UIProperties.HexaRuleMenu.Configuration;

            return RedirectToAction("Index", "Configuration");
        }
        public ActionResult Rule()
        {
            ViewDatas();
            ViewBags(UIProperties.HexaRuleMenu.Rule);
            return View();
        }
        public ActionResult RuleType()
        {
            ViewDatas();
            ViewBags(UIProperties.HexaRuleMenu.RuleType);
            return View();
        }
        public ActionResult RuleCategory()
        {
            ViewDatas();
            ViewBags(UIProperties.HexaRuleMenu.RuleCategory);
            return View();
        }
        public ActionResult HXRKeyColumns()
        {
            ViewDatas();
            HXRConfigurationMSEntity _configEntity = UIProperties.Sessions.ConfigEntity;
            if (_configEntity == null) return View("HXRKeyColumns");
            ViewData["DatabaseIP"] = _configEntity.ServerIPAddress;
            ViewData["SourceTarget"] = _configEntity.SourceTarget;
            ViewData["DatabaseName"] = _configEntity.SchemaName;
            ViewData["ConfigID"] = UIProperties.Sessions.ConfigEntity.Config_ID;

            ViewBags(UIProperties.HexaRuleMenu.Keycolumns);

            return View("HXRKeyColumns");
        }
        public ActionResult RuleError()
        {
            ViewDatas();
            ViewBags(UIProperties.HexaRuleMenu.Error);

            return View();
        }
        //public ActionResult HygieneRule()mput
        //{
        //    ViewDatas();
        //    HXRConfigurationMSEntity _configEntity = UIProperties.Sessions.ConfigEntity;
        //    ViewData["DatabaseIP"] = _configEntity.ServerIPAddress;
        //    ViewData["SourceTarget"] = _configEntity.SourceTarget;
        //    ViewData["DatabaseName"] = _configEntity.SchemaName;
        //    ViewData["ConfigID"] = _configEntity.Config_ID;
        //    ViewBags(UIProperties.HexaRuleMenu.PreDefinedRule);
        //    return View();
        //}
        public ActionResult HXRPreDefine()
        {
            ViewDatas();
            HXRConfigurationMSEntity _configEntity = UIProperties.Sessions.ConfigEntity;
            ViewData["DatabaseIP"] = _configEntity.ServerIPAddress;
            ViewData["SourceTarget"] = _configEntity.SourceTarget;
            ViewData["DatabaseName"] = _configEntity.SchemaName;
            ViewData["ConfigID"] = _configEntity.Config_ID;
            ViewBags(UIProperties.HexaRuleMenu.PreDefinedRule);
            return View("HXRPreDefineRule");
        }
        public ActionResult HXRUserDefine()
        {
            ViewDatas();
            HXRConfigurationMSEntity _configEntity = UIProperties.Sessions.ConfigEntity;
            ViewData["DatabaseIP"] = _configEntity.ServerIPAddress;
            ViewData["SourceTarget"] = _configEntity.SourceTarget;
            ViewData["DatabaseName"] = _configEntity.SchemaName;
            ViewData["ConfigID"] = UIProperties.Sessions.ConfigEntity.Config_ID;
            ViewBags(UIProperties.HexaRuleMenu.CreateRule);
            return View("HXRUserDefineRule");
        }
        //public ActionResult HXRRuleAllocation()
        //{
        //    ViewDatas();
        //    HXRConfigurationMSEntity _configEntity = UIProperties.Sessions.ConfigEntity;
        //    ViewData["DatabaseIP"] = _configEntity.ServerIPAddress;
        //    ViewData["SourceTarget"] = _configEntity.SourceTarget;
        //    ViewData["DatabaseName"] = _configEntity.SchemaName;
        //    ViewData["ConfigID"] = _configEntity.Config_ID;
        //    ViewBags(UIProperties.HexaRuleMenu.RuleAllocation);
        //    return View("HXRRuleAllocation");
        //}
        public ActionResult HXRShowRules()
        {
            ViewDatas();
            HXRConfigurationMSEntity _configEntity = UIProperties.Sessions.ConfigEntity;
            ViewData["DatabaseIP"] = _configEntity.ServerIPAddress;
            ViewData["SourceTarget"] = _configEntity.SourceTarget;
            ViewData["DatabaseName"] = _configEntity.SchemaName;
            ViewData["ConfigID"] = UIProperties.Sessions.ConfigEntity.Config_ID;
            ViewBags(UIProperties.HexaRuleMenu.ProcessandUpdates);
            return View("HXRShowRules");
        }
        public ActionResult CorrectionAndReProcess()
        {
            ViewDatas();
            HXRConfigurationMSEntity _configEntity = UIProperties.Sessions.ConfigEntity;
            ViewData["DatabaseIP"] = _configEntity.ServerIPAddress;
            ViewData["SourceTarget"] = _configEntity.SourceTarget;
            ViewData["DatabaseName"] = _configEntity.SchemaName;
            ViewData["ConfigID"] = UIProperties.Sessions.ConfigEntity.Config_ID;
            ViewBags(UIProperties.HexaRuleMenu.CorrectionandReProcess);
            return View();
        }
        public ActionResult RuleAllocation()
        {
            ViewDatas();
            HXRConfigurationMSEntity _configEntity = UIProperties.Sessions.ConfigEntity;
            ViewData["DatabaseIP"] = _configEntity.ServerIPAddress;
            ViewData["SourceTarget"] = _configEntity.SourceTarget;
            ViewData["DatabaseName"] = _configEntity.SchemaName;
            ViewData["ConfigID"] = _configEntity.Config_ID;
            ViewBags(UIProperties.HexaRuleMenu.RuleAllocation);
            return View("RuleAllocation");
        }


        public JsonResult GetConfigBySourceTarget(string SourceTarget)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            string ClientID = UIProperties.Sessions.Client.Client_ID;
            string ProjectID = UIProperties.Sessions.Client.project_ID;
            long ToolID = (int)UIProperties.Tools.HexaRule;
            int? RoleId = UIProperties.Sessions.Client.Role_ID;

            HXRConfigurationMSEntity _configEntity = _configMS.GetConfigurationByType(ClientID, ProjectID, SourceTarget, ToolID, RoleId, ref StatusCode, ref Message);
            UIProperties.Sessions.ConfigEntity = _configEntity;

            if (UIProperties.Sessions.ConfigEntity != null)
                ViewData["ConfigID"] = UIProperties.Sessions.ConfigEntity.Config_ID;
            else
                ViewData["ConfigID"] = "0";

            if (_configEntity != null)
                return Json(_configEntity, JsonRequestBehavior.AllowGet);
            else return null;
        }

        #region RuleSetup
        [HttpPost]
        public ActionResult RuleSave(RuleModel _rule, string Command, string _ruleId)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            string _ClientID = UIProperties.Sessions.Client.Client_ID;
            string _ProjectID = UIProperties.Sessions.Client.project_ID;

            if (Command == "Clear")
            {
                TempData["AlertMsg"] = "";
                return RedirectToAction("Rule");
            }
            if (ModelState.IsValid)
            {
                if (Command == "Save")
                {
                    //Save functionality   
                    _Irules.SaveRule(_ClientID, _ProjectID, _rule.Rule_Name.Trim(), _rule.Rule_Name.Trim(), DateTime.Now, Convert.ToDateTime("2049-12-31 00:00:00.000"),
                        _rule.Conditional_Clause.Trim(), _rule.Default_value.Trim(), UIProperties.Sessions.UserName, ref StatusCode, ref Message);
                    if (Message == "Success") TempData["AlertMsg"] = "Saved successfully."; else TempData["AlertMsg"] = Message;
                    return RedirectToAction("Rule");
                }
                else if (Command == "Update")
                {
                    //Update call
                    _Irules.UpdateRule(_ClientID, _ProjectID, Convert.ToInt32(_ruleId), _rule.Rule_Name, 1, _rule.Rule_Name,
                        DateTime.Now, Convert.ToDateTime("2049-12-31 00:00:00.000"),
                    _rule.Conditional_Clause, _rule.Default_value, UIProperties.Sessions.UserName,
                    ref StatusCode, ref Message);
                    //RuleUpdate("edit", Convert.ToInt32(_ruleId), _rule.Rule_Name, _rule.Conditional_Clause, _rule.Default_value);
                    if (Message == "Success") TempData["AlertMsg"] = "Updated successfully."; else TempData["AlertMsg"] = Message;
                    return RedirectToAction("Rule");
                }
                else
                {
                    TempData["AlertMsg"] = "";
                    return RedirectToAction("Rule");
                }
            }
            else
            {
                TempData["AlertMsg"] = "";
                TempData["Command"] = Command == "Update" ? Command : "";
                ViewDatas();
                ViewBags(UIProperties.HexaRuleMenu.Rule);
                return View("Rule");
            }

        }
        [HttpPost]
        public ActionResult RuleCategorySave(RuleCategoryModel _ruleCategory, string Command, string _ruleCategoryId)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            string _ClientID = UIProperties.Sessions.Client.Client_ID;
            string _ProjectID = UIProperties.Sessions.Client.project_ID;
            if (Command == "Clear")
            {
                TempData["AlertMsg"] = "";
                return RedirectToAction("RuleCategory");
            }
            if (ModelState.IsValid)
            {
                if (Command == "Save")
                {
                    //Save functionality   
                    _Irules.SaveRuleCategory(_ruleCategory.RuleCategory_Name, _ruleCategory.RuleCategory_Desc, UIProperties.Sessions.UserName, ref StatusCode, ref Message);
                    if (Message == "Success") TempData["AlertMsg"] = "Saved successfully."; else TempData["AlertMsg"] = Message;
                    return RedirectToAction("RuleCategory");
                }
                else if (Command == "Update")
                {
                    //Update call
                    _Irules.UpdateRuleCategory(Convert.ToInt64(_ruleCategoryId), _ruleCategory.RuleCategory_Name, _ruleCategory.RuleCategory_Desc, 1,
                        UIProperties.Sessions.UserName, ref StatusCode, ref Message);
                    //RuleUpdate("edit", Convert.ToInt32(_ruleId), _rule.Rule_Name, _rule.Conditional_Clause, _rule.Default_value);
                    if (Message == "Success") TempData["AlertMsg"] = "Updated successfully."; else TempData["AlertMsg"] = Message;
                    return RedirectToAction("RuleCategory");
                }
                else
                {
                    TempData["AlertMsg"] = "";
                    return RedirectToAction("RuleCategory");
                }
            }
            else
            {
                TempData["AlertMsg"] = "";
                TempData["Command"] = Command == "Update" ? Command : "";
                ViewDatas();
                ViewBags(UIProperties.HexaRuleMenu.RuleCategory);
                return View("RuleCategory");
            }
            //if (Command == "Save")
            //{
            //    if (ModelState.IsValid)
            //    {
            //        //Save functionality   
            //        _Irules.SaveRuleCategory(_ruleCategory.RuleCategory_Name, _ruleCategory.RuleCategory_Desc, UIProperties.Sessions.UserName, ref StatusCode, ref Message);
            //        if (Message == "Success")
            //            TempData["AlertMsg"] = "Saved successfully.";
            //        else
            //            TempData["AlertMsg"] = Message;

            //        return RedirectToAction("RuleCategory");

            //    }
            //    else
            //    {
            //        TempData["AlertMsg"] = "";
            //        ViewDatas();
            //        //ViewBags("4");
            //        ViewBags(UIProperties.HexaRuleMenu.RuleCategory);
            //        return View("RuleCategory");
            //    }
            //}
            //else
            //{
            //    TempData["AlertMsg"] = "";
            //    return RedirectToAction("RuleCategory");
            //}
        }
        [HttpPost]
        public ActionResult RuleTypeSave(RuleTypeModel _ruleType, string Command, string _ruleTypeId)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            string _ClientID = UIProperties.Sessions.Client.Client_ID;
            string _ProjectID = UIProperties.Sessions.Client.project_ID;

            if (Command == "Clear")
            {
                TempData["AlertMsg"] = "";
                return RedirectToAction("RuleType");
            }
            if (ModelState.IsValid)
            {
                if (Command == "Save")
                {
                    //Save functionality   
                    _Irules.SaveRuleType(_ClientID, _ProjectID, _ruleType.RuleType_Name.Trim(), _ruleType.RuleType_Desc.Trim(), UIProperties.Sessions.UserName, ref StatusCode, ref Message);
                    if (Message == "Success") TempData["AlertMsg"] = "Saved successfully."; else TempData["AlertMsg"] = Message;
                    return RedirectToAction("RuleType");
                }
                else if (Command == "Update")
                {
                    //Update call
                    _Irules.UpdateRuleType(Convert.ToInt64(_ruleTypeId), _ruleType.RuleType_Name.Trim(), _ruleType.RuleType_Desc.Trim(), 1,
                     UIProperties.Sessions.UserName, ref StatusCode, ref Message);

                    if (Message == "Success") TempData["AlertMsg"] = "Updated successfully."; else TempData["AlertMsg"] = Message;
                    return RedirectToAction("RuleType");
                }
                else
                {
                    TempData["AlertMsg"] = "";
                    return RedirectToAction("RuleType");
                }
            }
            else
            {
                TempData["AlertMsg"] = "";
                TempData["Command"] = Command == "Update" ? Command : "";
                ViewDatas();
                ViewBags(UIProperties.HexaRuleMenu.RuleType);
                return View("RuleType");
            }




            //if (Command == "Save")
            //{
            //    if (ModelState.IsValid)
            //    {
            //        //Save functionality                      
            //        _Irules.SaveRuleType(_ruleType.RuleType_Name.Trim(), _ruleType.RuleType_Desc.Trim(), UIProperties.Sessions.UserName, ref StatusCode, ref Message);
            //        if (Message == "Success")
            //            TempData["AlertMsg"] = "Saved successfully.";
            //        else
            //            TempData["AlertMsg"] = Message;

            //        return RedirectToAction("RuleType");

            //    }
            //    else
            //    {
            //        TempData["AlertMsg"] = "";
            //        ViewDatas();
            //        //ViewBags("3");
            //        ViewBags(UIProperties.HexaRuleMenu.RuleType);
            //        return View("RuleType");
            //    }
            //}
            //else
            //{
            //    TempData["AlertMsg"] = "";
            //    return RedirectToAction("RuleType");
            //}

        }
        [HttpPost]
        public ActionResult RuleErrorSave(RuleErrorModel _ruleError, string Command, string _ruleErrorCode)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            if (Command == "Clear")
            {
                TempData["AlertMsg"] = "";
                return RedirectToAction("RuleError");
            }
            if (ModelState.IsValid)
            {
                if (Command == "Save")
                {
                    //Save functionality   
                    _Irules.SaveRuleError(UIProperties.Sessions.Client.Client_ID, UIProperties.Sessions.Client.project_ID, _ruleError.Error_Code.Trim(),
                        _ruleError.Error_Description, UIProperties.Sessions.UserName, ref StatusCode, ref Message);
                    if (Message == "Success") TempData["AlertMsg"] = "Saved successfully."; else TempData["AlertMsg"] = Message;
                    return RedirectToAction("RuleError");
                }
                else if (Command == "Update")
                {
                    //Update call
                    _Irules.UpdateRuleError(UIProperties.Sessions.Client.Client_ID, UIProperties.Sessions.Client.project_ID, _ruleErrorCode.Trim(), _ruleError.Error_Description, 1,
                    UIProperties.Sessions.UserName, ref StatusCode, ref Message);
                    //RuleUpdate("edit", Convert.ToInt32(_ruleId), _rule.Rule_Name, _rule.Conditional_Clause, _rule.Default_value);
                    if (Message == "Success") TempData["AlertMsg"] = "Updated successfully."; else TempData["AlertMsg"] = Message;
                    return RedirectToAction("RuleError");
                }
                else
                {
                    TempData["AlertMsg"] = "";
                    return RedirectToAction("RuleError");
                }
            }
            else
            {
                TempData["AlertMsg"] = "";
                TempData["Command"] = Command == "Update" ? Command : "";
                ViewDatas();
                ViewBags(UIProperties.HexaRuleMenu.Error);
                return View("RuleError");
            }


            //if (Command == "Save")
            //{
            //    if (ModelState.IsValid)
            //    {
            //        //Save functionality   
            //        _Irules.SaveRuleError(UIProperties.Sessions.Client.Client_ID, UIProperties.Sessions.Client.project_ID, _ruleError.Error_Code.Trim(),
            //            _ruleError.Error_Description, UIProperties.Sessions.UserName, ref StatusCode, ref Message);
            //        if (Message == "Success")
            //            TempData["AlertMsg"] = "Saved successfully.";
            //        else
            //            TempData["AlertMsg"] = Message;

            //        return RedirectToAction("RuleError");

            //    }
            //    else
            //    {
            //        TempData["AlertMsg"] = "";
            //        //ViewData["ClientID"] = UIProperties.Sessions.Client.Client_ID;
            //        //ViewData["ProjectID"] = UIProperties.Sessions.Client.project_ID;
            //        ViewDatas();
            //        //ViewBags("6");
            //        ViewBags(UIProperties.HexaRuleMenu.Error);
            //        return View("RuleError");
            //    }
            //}
            //else
            //{
            //    TempData["AlertMsg"] = "";
            //    return RedirectToAction("RuleError");
            //}

        }

        public string RuleCategoryUpdate(string oper, int? CategoryId, string RuleCategory, string CategoryDescription)
        {
            string StatusCode = string.Empty, Message = string.Empty, _Result = string.Empty;
            if (oper == "add")
            {

            }
            else if (oper == "edit")
            {
                //_Irules.UpdateRuleCategory(CategoryId, RuleCategory, CategoryDescription, 1,
                //    UIProperties.Sessions.UserName, ref StatusCode, ref Message);
                //if (Message == "Success") _Result = "Updated successfully."; else _Result = Message;
            }
            else if (oper == "del")
            {
                _Irules.UpdateRuleCategory(CategoryId, RuleCategory, CategoryDescription, 0,
                    UIProperties.Sessions.UserName, ref StatusCode, ref Message);
                //if (Message == "Success") _Result = "Deleted successfully."; else _Result = Message;
                if (Message == "Success") _Result = "Deleted successfully."; else _Result = Message;
            }
            return _Result;

        }
        public string RuleUpdate(string oper, int? RuleID, string RuleName, string ConditionalClause, string Defaultvalue)
        {
            string StatusCode = string.Empty, Message = string.Empty, _Result = string.Empty;
            string ClientId = UIProperties.Sessions.Client.Client_ID;
            string ProjectId = UIProperties.Sessions.Client.project_ID;

            if (oper == "add")
            {

            }
            else if (oper == "edit")
            {
                //_Irules.UpdateRule(ClientId, ProjectId, RuleID, RuleName, 1, RuleName, DateTime.Now, Convert.ToDateTime("2049-12-31 00:00:00.000"),
                //    ConditionalClause, Defaultvalue, UIProperties.Sessions.UserName,
                //    ref StatusCode, ref Message);
                //if (Message == "Success") _Result = "Updated successfully."; else _Result = Message;

            }
            else if (oper == "del")
            {
                _Irules.UpdateRule(ClientId, ProjectId, RuleID, RuleName, 0, RuleName, DateTime.Now, Convert.ToDateTime("2049-12-31 00:00:00.000"),
                    ConditionalClause, Defaultvalue, UIProperties.Sessions.UserName, ref StatusCode, ref Message);
                if (Message == "Success") _Result = "Deleted successfully."; else _Result = Message;
                //if (Message == "Success")
                //{
                //    TempData["AlertMsg"] = "Deleted successfully.";
                //}
                //else { TempData["AlertMsg"] = Message; }
                //TempData["AlertMsg"] = Message;

            }
            return _Result;

        }
        public string RuleTypeUpdate(string oper, int? TypeId, string RuleType, string TypeDescription)
        {
            string StatusCode = string.Empty, Message = string.Empty, _Result = string.Empty;
            if (oper == "add")
            {

            }
            else if (oper == "edit")
            {
                //_Irules.UpdateRuleType(TypeId, RuleType, TypeDescription, 1,
                //    UIProperties.Sessions.UserName, ref StatusCode, ref Message);
                //if (Message == "Success") _Result = "Updated successfully."; else _Result = "Updating failed.";

            }
            else if (oper == "del")
            {
                _Irules.UpdateRuleType(TypeId, RuleType, TypeDescription, 0,
                    UIProperties.Sessions.UserName, ref StatusCode, ref Message);
                //if (Message == "Success") _Result = "Deleted successfully."; else _Result = "Deleting failed.";
                if (Message == "Success") _Result = "Deleted successfully."; else _Result = Message;

            }
            return _Result;

        }
        public string RuleErrorUpdate(string oper, string Error_code, string errorDescription)
        {
            string StatusCode = string.Empty, Message = string.Empty, _Result = string.Empty;

            if (oper == "edit")
            {
                //_Irules.UpdateRuleError(UIProperties.Sessions.Client.Client_ID, UIProperties.Sessions.Client.project_ID, Error_code, errorDescription, 1,
                //    UIProperties.Sessions.UserName, ref StatusCode, ref Message);
                //if (Message == "Success") _Result = "Updated successfully."; else _Result = Message;

            }
            else if (oper == "del")
            {
                _Irules.UpdateRuleError(UIProperties.Sessions.Client.Client_ID, UIProperties.Sessions.Client.project_ID, Error_code, errorDescription, 0,
                    UIProperties.Sessions.UserName, ref StatusCode, ref Message);
                //if (Message == "Success") _Result = "Deleted successfully."; else _Result = Message;
                if (Message == "Success") _Result = "Deleted successfully."; else _Result = Message;

            }
            return _Result;

        }
        #endregion

        [HttpGet]
        public ActionResult HXRReport()
        {
            // ViewBag.SelectedMenu = "7";
            // ViewBag.UserName = UIProperties.Sessions.UserName;
            string toolid = UIProperties.Sessions.ToolID;
            return Redirect("../Dashboard.aspx?ToolID=" + toolid + "");

        }

        private void InitializeSession()
        {
            string StatusCode = string.Empty, Message = string.Empty;
            UIProperties.Sessions.Client = _configMS.GetClientDetails(UIProperties.Sessions.UserName, ref StatusCode, ref  Message);

            UIProperties.Sessions.ToolID = "7";
            ViewData["ClientID"] = UIProperties.Sessions.Client.Client_ID;
            ViewData["ProjectID"] = UIProperties.Sessions.Client.project_ID;
        }


        //
        // GET: /Hexarule/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        //
        // GET: /Hexarule/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Hexarule/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Hexarule/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Hexarule/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Hexarule/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Hexarule/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
