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
    [Authorize]
    public class DashBoardController : Controller
    {
        //
        // GET: /DashBoard/
        private string _RedirectUrl = "~/Dashboard.aspx";

        public string RedirectUrl
        {
            get { return _RedirectUrl; }
            set { _RedirectUrl = value; }
        }
        private void ViewBags(UIProperties.HexaRuleMenu MenuId)
        {
            int ToolID = (int)UIProperties.Tools.HexaRule;            
            UIProperties.MenuTable.SelectedMenuId = (int)MenuId;
            UIProperties.MenuTable.GetMenu(ToolID);
            ViewBag.ToolIconPath = ((dynamic)(UIProperties.Tool_Logos.GetToolLogo(ToolID))).Logo;
            ViewBag.Menu = UIProperties.MenuTable.ToolMenu;
            ViewBag.UserName = UIProperties.Sessions.UserName;
        }
        private void ViewBags(UIProperties.DataProfilerMenu MenuId)
        {
            int ToolID = (int)UIProperties.Tools.DataProfiler;            
            UIProperties.MenuTable.SelectedMenuId = (int)MenuId;
            UIProperties.MenuTable.GetMenu(ToolID);
            ViewBag.ToolIconPath = ((dynamic)(UIProperties.Tool_Logos.GetToolLogo(ToolID))).Logo;
            ViewBag.Menu = UIProperties.MenuTable.ToolMenu;
            ViewBag.UserName = UIProperties.Sessions.UserName;
        }
        private void ViewBags(UIProperties.DataReconMenu MenuId)
        {
            int ToolID = (int)UIProperties.Tools.DataRecon;
            UIProperties.MenuTable.SelectedMenuId = (int)MenuId;
            UIProperties.MenuTable.GetMenu(ToolID);
            ViewBag.ToolIconPath = ((dynamic)(UIProperties.Tool_Logos.GetToolLogo(ToolID))).Logo;
            ViewBag.Menu = UIProperties.MenuTable.ToolMenu;
            ViewBag.UserName = UIProperties.Sessions.UserName;
        }
        public RedirectResult Index()
        {
            UIProperties.Sessions.ToolID = ((int)UIProperties.Tools.DashboardReports).ToString();
            return Redirect(RedirectUrl);
        }
        public RedirectResult DataProfiler()
        {
            UIProperties.Sessions.ToolID = ((int)UIProperties.Tools.DataProfiler).ToString();
            return Redirect(RedirectUrl);
        }
        public RedirectResult HexaRule()
        {
            UIProperties.Sessions.ToolID = ((int)UIProperties.Tools.HexaRule).ToString();
            return Redirect(RedirectUrl);
        }
        public ActionResult HexaRuleDashboard()
        {
            ViewBags(UIProperties.HexaRuleMenu.Dashboard);
            return View();
        }
        public ActionResult ProfilerDashboard()
        {
            ViewBags(UIProperties.DataProfilerMenu.Dashboard);            
            return View();
        }
        public ActionResult DataRecon()
        {
            ViewBags(UIProperties.DataReconMenu.Dashboard);
            return View();
        }
        //
        // GET: /DashBoard/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /DashBoard/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DashBoard/Create

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
        // GET: /DashBoard/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /DashBoard/Edit/5

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
        // GET: /DashBoard/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /DashBoard/Delete/5

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
