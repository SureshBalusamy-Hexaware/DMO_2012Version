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
    public class DIMAPLUSController : Controller
    {
        private readonly IHXRConfigurationMS _configMS;

        public DIMAPLUSController()
        {
            _configMS = new HXRConfigurationMSService();
        }
        private void ViewBags(UIProperties.DIMAPLUSMenu MenuId)
        {
            int ToolID = (int)UIProperties.Tools.DIMAPLUS;

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
        // GET: /DIMAPLUS/

        public ActionResult Index()
        {
            TempData["ToolId"] = (int)UIProperties.Tools.DIMAPLUS;
            TempData["MenuId"] = (int)UIProperties.DIMAPLUSMenu.Configuration;

            return RedirectToAction("Index", "Configuration");
        }
        public ActionResult BusinessName()
        {
            return RedirectToAction("BusinessName", "Automaton",
            new { ToolName = "DASEM" });

        }
        public ActionResult Criteria()
        {

            HXRConfigurationMSEntity _configEntity = UIProperties.Sessions.ConfigEntity;
            ViewData["ConfigID"] = _configEntity.Config_ID;

            ViewDatas();
            ViewBags(UIProperties.DIMAPLUSMenu.Criteria);
            return View();
        }
        public ActionResult CriteriaDetail()
        {
            ViewBags(UIProperties.DIMAPLUSMenu.Criteria);
            return View();
        }
        public ActionResult Copy()
        {
            ViewBags(UIProperties.DIMAPLUSMenu.CopySlicedData);
            return View();
        }
        public ActionResult Reports()
        {
            ViewBags(UIProperties.DIMAPLUSMenu.Reports);
            return View();
        }
        public ActionResult Integration()
        {
            ViewDatas();
            ViewBags(UIProperties.DIMAPLUSMenu.Integration);
            return View();
        }

        public ActionResult ReportRunIDDetail(int TemplateID, int RunID)
        {
            ViewBag.TemplateID = TemplateID;
            ViewBag.RunID = RunID;
            ViewBags(UIProperties.DIMAPLUSMenu.Reports);
            return View();
        }
        public ActionResult ReportDetail()
        {
            ViewBags(UIProperties.DIMAPLUSMenu.Reports);
            return View();
        }
        public ActionResult Purge()
        {
            ViewBags(UIProperties.DIMAPLUSMenu.PurgeSlicedData);
            return View();
        }
        public ActionResult DataMasking()
        {
            ViewDatas();
            ViewBags(UIProperties.DIMAPLUSMenu.DataMasking);

            return View();
        }
        public ActionResult UploadExcel()
        {
            return RedirectToAction("UploadExcel", "Automaton", new { ToolName = "DASEM" });
        }
        public ActionResult ExcelTemplate()
        {
            return RedirectToAction("ExcelTemplate", "Automaton",
            new { ToolName = "DASEM" });
        }

        public ActionResult Downloads()
        {
            return RedirectToAction("Downloads", "Automaton",
            new { ToolName = "DASEM" });
        }

    }
}
