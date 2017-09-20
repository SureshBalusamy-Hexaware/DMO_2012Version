using DM_BusinessEntities;
using DM_BusinessService;
using DM_UI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DM_UI.Controllers
{
    public class InfaGenController : Controller
    {
        private readonly IHXRConfigurationMS _configMS;

        public InfaGenController()
        {
            _configMS = new HXRConfigurationMSService();

        }
        //
        // GET: /InfaGen/

        //public ActionResult Configuration(string UserName)
        //{
        //    ViewBag.UserName = UserName;
        //    UIProperties.Sessions.UserName = UserName;
        //    ViewBag.SelectedMenu = "1";
        //    InitializeSession();
        //   // GetConfigBySourceTarget1("SOURCE");
        //    return View();
        //}
        public ActionResult InfaGenConfig(string UserName)
        {
            ViewBag.UserName = UserName;
            UIProperties.Sessions.UserName = UserName;
            ViewBag.SelectedMenu = "1";
            InitializeSession();
            // GetConfigBySourceTarget1("SOURCE");
            return View();
        }
        private void InitializeSession()
        {
            string StatusCode = string.Empty, Message = string.Empty;
            UIProperties.Sessions.Client = _configMS.GetClientDetails(UIProperties.Sessions.UserName,ref StatusCode, ref  Message);

            UIProperties.Sessions.ToolID = "8";
            ViewData["ClientID"] = UIProperties.Sessions.Client.Client_ID;
            ViewData["ProjectID"] = UIProperties.Sessions.Client.project_ID;
        }

        [HttpPost]
        public JsonResult GetConfigBySourceTarget(string SourceTarget)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            string ClientID = UIProperties.Sessions.Client.Client_ID;
            string ProjectID = UIProperties.Sessions.Client.project_ID;
            long ToolID ;
            long.TryParse(UIProperties.Sessions.ToolID, out ToolID);
            int? RoleId = UIProperties.Sessions.Client.Role_ID;
            HXRConfigurationMSEntity _configEntity = _configMS.GetConfigurationByType(ClientID, ProjectID, SourceTarget, ToolID,RoleId, ref StatusCode, ref Message);
            UIProperties.Sessions.ConfigEntity = _configEntity;
            if (_configEntity != null)
                return Json(_configEntity, JsonRequestBehavior.AllowGet);
            else return null;
        }


        public ActionResult CreateTemplate()
        {
            ViewBag.SelectedMenu = "2";
            return View();
        }
        public ActionResult ModifyTemplate()
        {
            ViewBag.SelectedMenu = "3";
            return View();
        }
        public ActionResult ReviewAndGenerate()
        {
            ViewBag.SelectedMenu = "4";
            return View();
        }

        #region Atuo Generated

        //
        // GET: /InfaGen/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /InfaGen/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /InfaGen/Create

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
        // GET: /InfaGen/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /InfaGen/Edit/5

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
        // GET: /InfaGen/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /InfaGen/Delete/5

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
        #endregion
    }
}
