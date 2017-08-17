using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DM_BusinessEntities;
using DM_BusinessService;
using DM_UI.App_Start;

namespace DM_UI.Controllers
{
    public class DataReconController : Controller
    {

        private readonly IHXRConfigurationMS _configMS;

        public DataReconController()
        {
            _configMS = new HXRConfigurationMSService();
        }

        private void ViewBags(UIProperties.DataReconMenu MenuId)
        {
            int ToolID = (int)UIProperties.Tools.DataRecon;
            //ViewBag.SelectedMenu = SelectedTabId;
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

        [HttpGet]
        public JsonResult GetConfigBySourceTarget(string SourceTarget)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            string ClientID = UIProperties.Sessions.Client.Client_ID;
            string ProjectID = UIProperties.Sessions.Client.project_ID;
            long ToolID = (long)UIProperties.Tools.DataRecon;
            int? RoleId = UIProperties.Sessions.Client.Role_ID;

            HXRConfigurationMSEntity _configEntity = _configMS.GetConfigurationByType(ClientID, ProjectID, SourceTarget, ToolID, RoleId, ref StatusCode, ref Message);
            UIProperties.Sessions.ConfigEntity = _configEntity;

            if (SourceTarget == "TARGET")
            {
                HXRTargetConfigurationMSEntity _tgtConfigEntity = new HXRTargetConfigurationMSEntity()
                    {
                        Config_ID = _configEntity.Config_ID
                    };

                UIProperties.Sessions.TargetConfigEntity = _tgtConfigEntity;
            }

            ViewData["ConfigID"] = UIProperties.Sessions.ConfigEntity.Config_ID;

            if (_configEntity != null)
                return Json(_configEntity, JsonRequestBehavior.AllowGet);
            else return null;
        }

        public ActionResult Configuration()
        {
            TempData["ToolId"] = (int)UIProperties.Tools.DataRecon;
            TempData["MenuId"] = (int)UIProperties.DataReconMenu.Configuration;

            return RedirectToAction("Index", "Configuration");
        }

        public ActionResult Reconciliation()
        {
            //ViewData["ClientID"] = UIProperties.Sessions.Client.Client_ID;
            //ViewData["ProjectID"] = UIProperties.Sessions.Client.project_ID;
            HXRConfigurationMSEntity _configEntity = UIProperties.Sessions.ConfigEntity;
            ViewData["DatabaseIP"] = _configEntity.ServerIPAddress;
            ViewData["SourceTarget"] = _configEntity.SourceTarget;
            ViewData["DatabaseName"] = _configEntity.SchemaName;
            ViewData["ConfigId"] = _configEntity.Config_ID;

            HXRTargetConfigurationMSEntity _tgtConfigEntity = UIProperties.Sessions.TargetConfigEntity;
            ViewData["TgtConfigId"] = _tgtConfigEntity.Config_ID;
            ViewData["TgtDatabaseIP"] = _tgtConfigEntity.ServerIPAddress;
            ViewData["TgtDatabaseName"] = _tgtConfigEntity.SchemaName;

            //ViewBag.SelectedMenu = "2";
            //ViewBag.UserName = UIProperties.Sessions.UserName;
            ViewDatas();
            ViewBags(UIProperties.DataReconMenu.Reconciliation);
            return View("ReconciliationNew");
        }

        public ActionResult Report()
        {
            //ViewBag.SelectedMenu = "3";
            //ViewBag.UserName = UIProperties.Sessions.UserName;
            ViewDatas();
            ViewBags(UIProperties.DataReconMenu.Report);
            return Redirect("../Dashboard.aspx?ToolID=");// + (int)UIProperties.Tools.DataRecon);
        }
    }
}
