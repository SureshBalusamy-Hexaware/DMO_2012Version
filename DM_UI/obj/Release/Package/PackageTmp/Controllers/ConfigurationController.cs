using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DM_BusinessService;
using DM_UI.App_Start;
using DM_BusinessEntities;
using DM_UI.Models;

namespace DM_UI.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly IHXRConfigurationMS _configMS;
        public ConfigurationController()
        {
            _configMS = new HXRConfigurationMSService();
        }


        //
        // GET: /Configuration/


        public ActionResult Index()
        {

            try
            {
                int ToolID = (int)TempData["ToolId"];
                int MenuId = (int)TempData["MenuId"];
                int? RoleId = UIProperties.Sessions.Client.IsAdmin;

                ViewBag.ToolIconPath = ((dynamic)(UIProperties.Tool_Logos.GetToolLogo(ToolID))).Logo;
                UIProperties.MenuTable.SelectedMenuId = MenuId;
                UIProperties.MenuTable.GetMenu(ToolID);
                ViewBag.Menu = UIProperties.MenuTable.ToolMenu;

                InitializeSession(ToolID);
                ViewBag.UserName = Convert.ToString(UIProperties.Sessions.UserName);
                UIProperties.Sessions.ToolID = ToolID.ToString();

                // if (ToolID != ((int)UIProperties.Tools.HexaRule) && ToolID != ((int)UIProperties.Tools.DataProfiler))
                ViewData["HXRTgtConfigurationMSEntity"] = GetConfigBySourceTarget("TARGET", ToolID);

                ViewData["HXRSrcConfigurationMSEntity"] = GetConfigBySourceTarget("SOURCE", ToolID);

                //ViewBag.SelectedMenu = MenuId.ToString();
                ViewData["ToolID"] = ToolID;
                if (RoleId == 1)
                {
                    ViewData["IsAdmin"] = "Yes";
                }
                else
                {
                    ViewData["IsAdmin"] = "No";
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Home");
            }
            return View("HXRConfiguration");
        }
        private void InitializeSession(int ToolID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            UIProperties.Sessions.Client = _configMS.GetClientDetails(UIProperties.Sessions.UserName, ref StatusCode, ref  Message);

            UIProperties.Sessions.ToolID = ToolID.ToString();
            ViewData["ClientID"] = UIProperties.Sessions.Client.Client_ID;
            ViewData["ProjectID"] = UIProperties.Sessions.Client.project_ID;
        }
        public JsonResult GetConfigBySourceTarget(string SourceTarget, int _ToolID)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            string ClientID = UIProperties.Sessions.Client.Client_ID;
            string ProjectID = UIProperties.Sessions.Client.project_ID;
            int ToolID = Convert.ToInt16(UIProperties.Sessions.ToolID);//_ToolID
            int RoleId = Convert.ToInt16(UIProperties.Sessions.Client.Role_ID);
            HXRConfigurationMSEntity _configEntity = _configMS.GetConfigurationByType(ClientID, ProjectID, SourceTarget, ToolID, RoleId, ref StatusCode, ref Message);
            //if (_configEntity == null) return null;
            if (_configEntity == null)
            {
                _configEntity = new HXRConfigurationMSEntity()
                {
                    Project = new HXRProjectMSEntity()
                    {
                        Client = new HXRClientMSEntity()
                    }
                };
                return Json(_configEntity, JsonRequestBehavior.AllowGet);
            }
            UIProperties.Sessions.ConfigEntity = _configEntity;
            if (UIProperties.Sessions.ConfigEntity != null)
                ViewData["ConfigID"] = UIProperties.Sessions.ConfigEntity.Config_ID;
            else

                ViewData["ConfigID"] = "0";


            if (SourceTarget == "TARGET")
            {
                HXRTargetConfigurationMSEntity _tgtConfigEntity = new HXRTargetConfigurationMSEntity()
                {
                    Config_ID = _configEntity.Config_ID
                };
                UIProperties.Sessions.TargetConfigEntity = _tgtConfigEntity;
            }


            if (_configEntity != null)
                return Json(_configEntity, JsonRequestBehavior.AllowGet);
            else return null;
        }



    }
}
