using DM_BusinessEntities;
using DM_BusinessService;
using DM_UI.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DM_UI.Controllers
{
    public class DIMAController : Controller
    {
        private readonly IHXRConfigurationMS _configMS;
        private readonly IDIMA _DIMA;
        //   private readonly IDIMA _dima;
        public DIMAController()
        {
            _configMS = new HXRConfigurationMSService();
            // _dima = new DIMAService();
            _DIMA = new DIMAService();
        }
        public ActionResult DIMAConfig()
        {

            return RedirectToAction("Index", "Configuration",
               new { _Layout = "_LayoutDIMA", ToolID = (int)UIProperties.Tools.DIMA, MenuId = 1 });

            //if (UserName != null && UserName != string.Empty)
            //{
            //    ViewBag.UserName = UserName;
            //    UIProperties.Sessions.UserName = UserName;
            //}

            //InitializeSession();
            //ViewBag.SelectedMenu = "1";
            //return View();

            //ViewBag.UserName = UserName;
            //UIProperties.Sessions.UserName = UserName;

            //InitializeSession();

            ////ViewData["HXRConfigurationMSEntity"] = GetConfigBySourceTarget("SOURCE");
            //ViewData["HXRTgtConfigurationMSEntity"] = GetConfigBySourceTarget("TARGET");
            //ViewData["HXRSrcConfigurationMSEntity"] = GetConfigBySourceTarget("SOURCE");

            //ViewBag.SelectedMenu = "1";
            //ViewData["ToolID"] = (int)UIProperties.Tools.DIMA;
            //return View("HXRConfiguration", "_LayoutDIMA");
        }
        private void InitializeSession()
        {
            string StatusCode = string.Empty, Message = string.Empty;
            UIProperties.Sessions.Client = _configMS.GetClientDetails(UIProperties.Sessions.UserName, ref StatusCode, ref  Message);

            UIProperties.Sessions.ToolID = "8";
            ViewData["ClientID"] = UIProperties.Sessions.Client.Client_ID;
            ViewData["ProjectID"] = UIProperties.Sessions.Client.project_ID;
        }

        [HttpGet]
        public JsonResult GetConfigBySourceTarget(string SourceTarget)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            string ClientID = UIProperties.Sessions.Client.Client_ID;
            string ProjectID = UIProperties.Sessions.Client.project_ID;
            long ToolID = Convert.ToInt64(UIProperties.Tools.DIMA);
            int? RoleId = UIProperties.Sessions.Client.Role_ID;

            HXRConfigurationMSEntity _configEntity = _configMS.GetConfigurationByType(ClientID, ProjectID, SourceTarget, ToolID, RoleId, ref StatusCode, ref Message);
            UIProperties.Sessions.ConfigEntity = _configEntity;
            if (_configEntity != null)
                return Json(_configEntity, JsonRequestBehavior.AllowGet);
            else return null;
        }

        public ActionResult Upload()
        {
            ViewBag.SelectedMenu = "2";
            ViewBag.UserName = UIProperties.Sessions.UserName;
            return View();
        }
        [HttpPost]
        public ActionResult PostedFile(HttpPostedFileBase file, bool ClearDatabase)

        //, bool ClearDatabase
        {
            string StatusCode = string.Empty, Message = string.Empty;

            if (file != null)// file.FileName != null && file.FileName != string.Empty)
            {
                var fileName = Path.GetFileName(file.FileName);
                string filePath = Path.GetFullPath(file.FileName);

                //string _conStr = ConfigurationManager.ConnectionStrings["DIMAconstr"].ConnectionString;
                var builder = new EntityConnectionStringBuilder(
                    System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
                string _conStr = builder.ProviderConnectionString;

                if (ClearDatabase)
                {
                    _DIMA.TruncateDIMAMappings(UIProperties.Sessions.Client.Client_ID, UIProperties.Sessions.Client.project_ID, ref StatusCode, ref Message);
                }
                SaveDIMA(UIProperties.Sessions.Client.Client_ID, UIProperties.Sessions.Client.project_ID, _conStr, filePath, ref StatusCode, ref  Message);
                _DIMA.UpdateDIMAMappings(ref StatusCode, ref Message);
            }
            return RedirectToAction("Upload");
        }
        private DataSet dsExcel { get; set; }
        public void ReadExcel(string FilePath, ref string status_Code, ref string message)
        {
            OleDbConnection Econ;
            string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", FilePath);
            string Query = string.Format("Select [Repository Name],[Folder name],[Mapping Name],[Table Name],[Table Type] FROM [{0}]", "Sheet1$");
            try
            {
                Econ = new OleDbConnection(constr);
                OleDbCommand Ecom = new OleDbCommand(Query, Econ);
                //Econ.Open();
                dsExcel = new DataSet();
                OleDbDataAdapter oda = new OleDbDataAdapter(Query, Econ);
                //Econ.Close();
                oda.Fill(dsExcel);

            }
            catch (Exception _e)
            {

            }
        }
        public void SaveDIMA(string client_ID, string project_ID, string sqlconn, string FilePath, ref string status_Code, ref string message)
        {


            SqlConnection con = new SqlConnection(sqlconn);
            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            string DestinationTable = "DIMA_MAPPING_MS";
            string Status_Code = string.Empty, Message = string.Empty;

            ReadExcel(FilePath, ref Status_Code, ref Message);
            DataTable Exceldt = dsExcel.Tables[0];



            DataColumn Client_ID = new DataColumn("Client_ID", typeof(string));
            Client_ID.DefaultValue = UIProperties.Sessions.Client.Client_ID;
            Exceldt.Columns.Add(Client_ID);

            DataColumn Project_ID = new DataColumn("Project_ID", typeof(string));
            Project_ID.DefaultValue = UIProperties.Sessions.Client.project_ID;
            Exceldt.Columns.Add(Project_ID);

            DataColumn Modified_by = new DataColumn("Modified_by", typeof(string));
            Modified_by.DefaultValue = UIProperties.Sessions.UserName == string.Empty ? "Hexaware" : UIProperties.Sessions.UserName;
            Exceldt.Columns.Add(Modified_by);

            DataColumn Create_Date = new DataColumn("Create_Date", typeof(string));
            Create_Date.DefaultValue = DateTime.Now;
            Exceldt.Columns.Add(Create_Date);

            objbulk.DestinationTableName = DestinationTable;
            objbulk.ColumnMappings.Add("Repository Name", "Repository_Name");
            objbulk.ColumnMappings.Add("Folder name", "Folder_Name");
            objbulk.ColumnMappings.Add("Mapping Name", "Mapping_Name");
            objbulk.ColumnMappings.Add("Table Name", "Table_Name");
            objbulk.ColumnMappings.Add("Table Type", "Table_TypeID");
            objbulk.ColumnMappings.Add("Client_ID", "Client_ID");
            objbulk.ColumnMappings.Add("Project_ID", "Project_ID");
            objbulk.ColumnMappings.Add("Create_Date", "Create_Date");
            objbulk.ColumnMappings.Add("Modified_by", "Modified_by");

            con.Open();
            objbulk.WriteToServer(Exceldt);
            con.Close();
        }

        #region AutoGenerated
        //
        // GET: /DIMA/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /DIMA/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /DIMA/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DIMA/Create

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
        // GET: /DIMA/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /DIMA/Edit/5

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
        // GET: /DIMA/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /DIMA/Delete/5

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
