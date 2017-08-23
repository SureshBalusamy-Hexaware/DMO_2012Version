using DM_BusinessEntities;
using DM_BusinessService;
using DM_UI.App_Start;
using System.IO;
using Microsoft.SqlServer.Dts.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Microsoft.SqlServer.Dts;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using ClosedXML.Excel;

namespace DM_UI.Controllers
{
    public class AutomatonController : Controller
    {

        private readonly IHXRConfigurationMS _configMS;
        private readonly IAutomaton _autoMS;

        public AutomatonController()
        {
            _configMS = new HXRConfigurationMSService();
            _autoMS = new HXRAutomatonService();
        }

        public ActionResult Configuration()
        {
            TempData["ToolId"] = (int)UIProperties.Tools.Automaton;
            TempData["MenuId"] = (int)UIProperties.AutomatonMenu.Configuration;

            return RedirectToAction("Index", "Configuration");

        }

        public ActionResult BusinessName(string ToolName)
        {
            ViewData["ToolID"] = UIProperties.Sessions.ToolID;
            ViewDatas();
            if (ToolName == "DASEM")
                ViewBags(UIProperties.DIMAPLUSMenu.BusinessName);
            else
                ViewBags(UIProperties.AutomatonMenu.BusinessName);

            ViewData["SourceConfigID"] = GetDBConfiguID("SOURCE");

            return View("BusinessName");
        }


        private void ViewBags(UIProperties.DIMAPLUSMenu MenuId)
        {
            int ToolID = (int)UIProperties.Tools.DIMAPLUS;

            UIProperties.MenuTable.SelectedMenuId = ((int)MenuId);
            UIProperties.MenuTable.GetMenu(ToolID);
            ViewBag.ToolIconPath = ((dynamic)(UIProperties.Tool_Logos.GetToolLogo(ToolID))).Logo;
            ViewBag.Menu = UIProperties.MenuTable.ToolMenu;
            ViewBag.UserName = UIProperties.Sessions.UserName;
        }
        private void ViewBags(UIProperties.AutomatonMenu MenuId)
        {
            int ToolID = (int)UIProperties.Tools.Automaton;

            UIProperties.MenuTable.SelectedMenuId = ((int)MenuId);
            UIProperties.MenuTable.GetMenu(ToolID);
            ViewBag.ToolIconPath = ((dynamic)(UIProperties.Tool_Logos.GetToolLogo(ToolID))).Logo;
            ViewBag.Menu = UIProperties.MenuTable.ToolMenu;
            ViewBag.UserName = UIProperties.Sessions.UserName;
        }
        private void ViewBags(UIProperties.DataProfilerMenu MenuId)
        {
            int ToolID = (int)UIProperties.Tools.DataProfiler;

            UIProperties.MenuTable.SelectedMenuId = ((int)MenuId);
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

        public ActionResult CreateTemplate()
        {
            ViewData["ToolID"] = UIProperties.Sessions.ToolID;
            ViewDatas();
            ViewBags(UIProperties.AutomatonMenu.CreateTemplate);
            return View();
        }

        public ActionResult CreateCustomTemplate()
        {
            ViewBag.SelectedMenu = "6";
            ViewData["ToolID"] = UIProperties.Sessions.ToolID;
            ViewDatas();
            ViewBags(UIProperties.AutomatonMenu.CreateCustomTemplate);

            return View();
        }


        public ActionResult CopyTemplate()
        {

            ViewData["ToolID"] = UIProperties.Sessions.ToolID;
            ViewDatas();
            ViewBags(UIProperties.AutomatonMenu.CopyTemplate);

            ViewData["SourceConnectionID"] = GetDBConfiguID("SOURCE");
            ViewData["TargetConnectionID"] = GetDBConfiguID("TARGET");
            return View();
        }


        public ActionResult ReviewAndGenerate()
        {
            ViewData["ToolID"] = UIProperties.Sessions.ToolID;
            ViewDatas();
            ViewBags(UIProperties.AutomatonMenu.ReviewAndExecute);
            return View();
        }
        public ActionResult UploadExcel(string ToolName)
        {
            ViewData["ToolID"] = UIProperties.Sessions.ToolID;
            ViewData["ConfigID"] = GetDBConfiguID("SOURCE");
            ViewDatas();

            if (ToolName == "DataProfiler")
                ViewBags(UIProperties.DataProfilerMenu.LoadData);
            else if (ToolName == "DASEM")
                ViewBags(UIProperties.DIMAPLUSMenu.UploadExcelFile);
            else
                ViewBags(UIProperties.AutomatonMenu.UploadExcelFile);

            return View();
        }

        public ActionResult ExcelTemplate(string ToolName)
        {
            ViewData["ToolID"] = UIProperties.Sessions.ToolID;
            ViewData["ConfigID"] = GetDBConfiguID("SOURCE");
            ViewDatas();

            if (ToolName == "DataProfiler")
                ViewBags(UIProperties.DataProfilerMenu.LoadData);
            else if (ToolName == "DASEM")
                ViewBags(UIProperties.DIMAPLUSMenu.Upload);
            else
                ViewBags(UIProperties.AutomatonMenu.Upload);

            return View("ExcelTemplate");
        }

        public ActionResult Downloads(string ToolName)
        {
            ViewDatas();

            if (ToolName == "DASEM")
                ViewBags(UIProperties.DIMAPLUSMenu.Download);
            else
                ViewBags(UIProperties.AutomatonMenu.Download);

            if (UIProperties.Sessions.TargetConfigEntity == null) return View();
            ViewData["tgtConfigID"] = UIProperties.Sessions.TargetConfigEntity.Config_ID.ToString();
            ViewData["ToolID"] = UIProperties.Sessions.ToolID;

            return View();
        }

        public ActionResult DownloadFile(string TableName, string type)
        {

            string StatusCode = string.Empty, Message = string.Empty;
            string _ConfigId = UIProperties.Sessions.TargetConfigEntity.Config_ID.ToString();
            string _ClientId = UIProperties.Sessions.Client.Client_ID;
            string _ProjectId = UIProperties.Sessions.Client.project_ID;
            string _ColumnList = "ALL";
            string _TableName = TableName;

            DataTable dt = _autoMS.GetTableData(_ClientId, _ProjectId, _ConfigId, _TableName, _ColumnList, 0, ref StatusCode, ref Message);

            if (type == "xml")
            {
                DataSet dS = new DataSet();
                dS.DataSetName = "ROOT";
                dt.TableName = _TableName;
                dS.Tables.Add(dt);
                StringWriter sw = new StringWriter();
                dS.WriteXml(sw, XmlWriteMode.IgnoreSchema);
                string s = sw.ToString();

                string attachment = "attachment; filename=" + _TableName + ".xml";
                Response.ClearContent();
                Response.ContentType = "application/xml";
                Response.AddHeader("content-disposition", attachment);
                Response.Write(s);
                Response.End();
            }
            else if (type == "csv")
            {
                string csv = string.Empty;
                foreach (DataColumn column in dt.Columns)
                {
                    //Add the Header row for CSV file.
                    csv += column.ColumnName + ',';
                }

                //Add new line.
                csv += "\r\n";

                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        //Add the Data rows.
                        csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                    }

                    //Add new line.
                    csv += "\r\n";
                }

                //Download the CSV file.
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + _TableName + ".csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(csv);
                Response.Flush();
                Response.End();
            }
            else
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "DataSheet");
                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Style.Font.Bold = true;
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //Response.AddHeader("content-disposition", "attachment;filename= " + _TableName + ".xlsx");
                    //Response.AddHeader("content-disposition", "attachment;filename= " + _TableName + ".xls");
                    Response.AddHeader("content-disposition", "attachment;filename= " + _TableName + "." + type);

                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            return RedirectToAction("Downloads");
        }

        [ActionName("Download")]
        public ActionResult DownloadFile(string fId, string fName, string fFormat)
        {
            int _ToolId = 0;
            string contentType = "application/" + fFormat;
            string fldrPath = UIProperties.DownloadPath;
            string filePath = fldrPath + fId + "_" + fName + "." + fFormat;
        
            if (!System.IO.File.Exists(filePath))
            {
                ViewBag.FileNotFound = "Yes";
                ViewDatas();
                _ToolId = Convert.ToInt16(UIProperties.Sessions.ToolID);

                if (_ToolId == (int)UIProperties.Tools.DIMAPLUS)
                    ViewBags(UIProperties.DIMAPLUSMenu.Download);
                else
                    ViewBags(UIProperties.AutomatonMenu.Download);

                if (UIProperties.Sessions.TargetConfigEntity == null) return View();
                ViewData["tgtConfigID"] = UIProperties.Sessions.TargetConfigEntity.Config_ID.ToString();


                //return View("Downloads");
                if (_ToolId == (int)UIProperties.Tools.DIMAPLUS)
                    return RedirectToAction("Downloads", new { ToolName = "DASEM" });
                else
                    return RedirectToAction("Downloads", "Automaton", new { ToolName = "Automaton" });

            }
            return File(filePath, contentType, fName + "." + fFormat);
        }


        [HttpPost]
        public JsonResult GetSrcList()
        {
            string msg = string.Empty;
            return Json(ViewBag.sourcetable, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GenerateXML(string Template_ID, string TemplateType)
        {
            string[] msg = new string[2];
            //string Generated_SSIS_PkgLocation = ConfigurationManager.AppSettings["Generatexml_SSIS_PkgLocation"];
            string Generated_SSIS_PkgLocation = ConfigurationManager.AppSettings["transformation_pkg"];
            //string Generatexml_Save_PkgLocation = ConfigurationManager.AppSettings["Generatexml_Save_PkgLocation"];
            string Generatexml_Save_PkgLocation = ConfigurationManager.AppSettings["generatexml_transformation"];

            if (System.IO.File.Exists(Generated_SSIS_PkgLocation))
            {
                Package pkg;
                Application app;
                DTSExecResult pkgResults;
                app = new Application();
                pkg = app.LoadPackage(Generated_SSIS_PkgLocation, null);
                pkg.Variables["Client_ID"].Value = UIProperties.Sessions.Client.Client_ID;
                pkg.Variables["Project_ID"].Value = UIProperties.Sessions.Client.project_ID;
                pkg.Variables["Template_Id"].Value = Template_ID;
                pkg.Variables["PackageSaveLocation"].Value = Generatexml_Save_PkgLocation;
                pkg.Variables["MetaDataConn"].Value = CommonHelper.GetADOConnectionString();
                //pkg.Connections[0].ConnectionString = CommonHelper.GetCurrentConnectionString();
                pkgResults = pkg.Execute();
                if (pkgResults == DTSExecResult.Success)
                    msg[0] = "Xml generated successfully.";
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (var error in pkg.Errors)
                    {
                        sb.AppendFormat("{0}: {1}\n", error.ErrorCode, error.Description);
                    }
                    msg[0] = string.Format("Error: {0}", sb.ToString());
                }


            }
            msg[1] = Generatexml_Save_PkgLocation;
            return Json(msg, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult FileUpload_GenerateXML(string Template_ID)
        {
            try
            {
                string[] msg = new string[2];
                //string Generated_SSIS_PkgLocation = ConfigurationManager.AppSettings["Generatexml_SSIS_PkgLocation"];
                //string Generatexml_Save_PkgLocation = ConfigurationManager.AppSettings["Generatexml_Save_PkgLocation"];

                string Generated_SSIS_PkgLocation = ConfigurationManager.AppSettings["dynamicupload_pkg"];
                string Generatexml_Save_PkgLocation = ConfigurationManager.AppSettings["generatexml_fileupload"];

                //string AutomatonSSISpath = ConfigurationManager.AppSettings["AutomatonSSISpath"];
                //if (System.IO.File.Exists(AutomatonSSISpath))
                //{
                Package pkg;
                Application app;
                DTSExecResult pkgResults;
                app = new Application();
                pkg = app.LoadPackage(Generated_SSIS_PkgLocation, null);
                //pkg.Variables["Client_ID"].Value = UIProperties.Sessions.Client.Client_ID;
                //pkg.Variables["Project_ID"].Value = UIProperties.Sessions.Client.project_ID;


                pkg.Variables["Generated_SSIS_PkgLocation"].Value = Generatexml_Save_PkgLocation; //  AutomatonSSISpath;                
                pkg.Variables["MetaDataConn"].Value = CommonHelper.GetADOConnectionString();
                pkg.Variables["Template_ID"].Value = Template_ID;

                //pkg.Connections[0].ConnectionString = CommonHelper.GetCurrentConnectionString();
                pkgResults = pkg.Execute();
                if (pkgResults == DTSExecResult.Success)
                    msg[0] = "Xml generated successfully.";
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (var error in pkg.Errors)
                    {
                        sb.AppendFormat("{0}: {1}\n", error.ErrorCode, error.Description);
                    }
                    msg[0] = string.Format("Error: {0}", sb.ToString());
                }


                //}
                msg[1] = Generatexml_Save_PkgLocation;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult GenerateMasterAutomatonXML(string Template_ID)
        {
            string[] msg = new string[2];

            //string Generated_SSIS_PkgLocation = ConfigurationManager.AppSettings["Generatexml_SSIS_MasterPkgLocation"];
            //string Generatexml_Save_PkgLocation = ConfigurationManager.AppSettings["Generatexml_Save_PkgLocation"];

            string Generated_SSIS_PkgLocation = ConfigurationManager.AppSettings["master_pkg"];
            string Generatexml_Save_PkgLocation = ConfigurationManager.AppSettings["Generatexml_Save_PkgLocation"];

            if (System.IO.File.Exists(Generated_SSIS_PkgLocation))
            {
                Package pkg;
                Application app;
                DTSExecResult pkgResults;
                app = new Application();
                pkg = app.LoadPackage(Generated_SSIS_PkgLocation, null);
                pkg.Variables["Client_ID"].Value = UIProperties.Sessions.Client.Client_ID;
                pkg.Variables["Project_ID"].Value = UIProperties.Sessions.Client.project_ID;
                pkg.Variables["Template_Id"].Value = Template_ID;
                pkg.Variables["PackageSaveLocation"].Value = Generatexml_Save_PkgLocation;
                pkg.Variables["MetaDataConn"].Value = CommonHelper.GetADOConnectionString();
                pkgResults = pkg.Execute();
                if (pkgResults == DTSExecResult.Success)
                    msg[0] = "Xml generated successfully.";
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (var error in pkg.Errors)
                    {
                        sb.AppendFormat("{0}: {1}\n", error.ErrorCode, error.Description);
                    }
                    msg[0] = string.Format("Error: {0}", sb.ToString());
                }


            }
            msg[1] = Generatexml_Save_PkgLocation;
            return Json(msg, JsonRequestBehavior.AllowGet);

        }

        //[HttpPost]
        //public JsonResult GenerateDRTemplate(string Template_ID)
        //{
        //   return Json(Template_ID, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public void GenerateDRTemplate(string Template_ID)
        //{
        //    _autoMS.GenerateDRTemplate(Template_ID);
        //}


        [HttpPost]
        public JsonResult RunPackage(string TemplateName, string TemplateType)
        {
            string[] msg = new string[2];
            //string Generatexml_Save_PkgLocation = Path.Combine(ConfigurationManager.AppSettings["Generatexml_Save_PkgLocation"], TemplateName + ".dtsx");
            string Generatexml_Save_PkgLocation = string.Empty;

            if (TemplateType == "Transformation" || TemplateType == "DataType")
                Generatexml_Save_PkgLocation = Path.Combine(ConfigurationManager.AppSettings["generatexml_transformation"], TemplateName + ".dtsx");
            else if (TemplateType == "Master")
                Generatexml_Save_PkgLocation = ConfigurationManager.AppSettings["master_pkg"];
            else if (TemplateType == "FileUpload")
                Generatexml_Save_PkgLocation = Path.Combine(ConfigurationManager.AppSettings["generatexml_fileupload"], TemplateName + ".dtsx");

            if (System.IO.File.Exists(Generatexml_Save_PkgLocation))
            {
                Package pkg;
                Application app;
                DTSExecResult pkgResults;
                app = new Application();
                pkg = app.LoadPackage(Generatexml_Save_PkgLocation, null);


                if (pkg.Connections.Count > 1)
                {
                    for (int i = 0; i < pkg.Connections.Count; i++)
                    {
                        string Type = "SOURCE";
                        if (pkg.Connections[i].Name.ToUpper().Contains("TARGET"))
                        {
                            Type = "TARGET";
                        }
                        string Password = GetDataBasePassword(Type);
                        if (!string.IsNullOrWhiteSpace(Password))
                        {
                            pkg.Connections[i].ConnectionString = pkg.Connections[i].ConnectionString + "Password=" + Password + ";";
                        }
                    }
                }



                long RunID = 0;

                string Client_ID = UIProperties.Sessions.Client.Client_ID;
                string project_ID = UIProperties.Sessions.Client.project_ID;
                string UserName = UIProperties.Sessions.UserName;
                string StatusCode = string.Empty, Message = string.Empty;

                //Start Run Audit log

                _autoMS.LogRunAudit(Client_ID, project_ID, TemplateName, UserName, null, ref RunID, UIProperties.Sessions.Client.Role_ID, ref StatusCode, ref Message);

                pkgResults = pkg.Execute();
                if (pkgResults == DTSExecResult.Success)
                    msg[0] = "Package Run successfully.";
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (var error in pkg.Errors)
                    {
                        sb.AppendFormat("{0}: {1}\n", error.ErrorCode, error.Description);
                    }
                    msg[0] = string.Format("Error: {0}", sb.ToString());
                }


                //End Run Audit log
                _autoMS.LogRunAudit(Client_ID, project_ID, TemplateName, UserName, RunID, ref RunID, UIProperties.Sessions.Client.Role_ID, ref StatusCode, ref Message);


            }
            else
            {

                msg[0] = "Package is not available.";
            }


            return Json(msg, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult CheckETLThresholdLimit(long Template_ID)
        {
            string Client_ID = UIProperties.Sessions.Client.Client_ID;
            string project_ID = UIProperties.Sessions.Client.project_ID;
            string StatusCode = string.Empty, Message = string.Empty;

            _autoMS.CheckETLThresholdLimit(Client_ID, project_ID, Template_ID, ref StatusCode, ref Message);

            return Json(new { StatusCode = StatusCode, Message = Message }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetConfigBySourceTarget(string SourceTarget)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            string ClientID = UIProperties.Sessions.Client.Client_ID;
            string ProjectID = UIProperties.Sessions.Client.project_ID;
            long ToolID;
            long.TryParse(UIProperties.Sessions.ToolID, out ToolID);
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

        [HttpPost]
        public JsonResult CopyOldtoNewTemplate(string Client_ID, string Project_ID, long Template_ID, string NewTemplateName)
        {
            long New_Template_ID = 0;
            string message = string.Empty;
            string status_Code = string.Empty;
            string CreatedBy = UIProperties.Sessions.UserName;

            _autoMS.CopyTemplate(Client_ID, Project_ID, Template_ID, NewTemplateName, "", ref New_Template_ID, ref status_Code, ref message);

            return Json((new
           {
               New_Template_ID = New_Template_ID,
               Message = message == "Success" ? "Template copied successfully" : message,

           }), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult _ReadExcelSheetName()
        {

            string filePath = string.Empty;
            //string ExcelFileLocation = ConfigurationManager.AppSettings["ExcelFileLocation"];
            string ExcelFileLocation = ConfigurationManager.AppSettings["ExcelFileLocation"];

            var httpRequest = System.Web.HttpContext.Current.Request;
            System.IO.DirectoryInfo exportExcelInfo = new DirectoryInfo(ExcelFileLocation);
            foreach (FileInfo file in exportExcelInfo.GetFiles())
            {
                file.Delete();
            }

            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    filePath = ExcelFileLocation + "\\" + Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(filePath);
                }
            }


            DataTable dt = new DataTable();
            if (Path.GetExtension(filePath).ToUpper() == ".XLSX")
            {

                dt.Columns.Add("TABLE_NAME", typeof(string));
                using (XLWorkbook wb = new XLWorkbook(filePath))
                {
                    foreach (IXLWorksheet worksheet in wb.Worksheets)
                    {
                        dt.Rows.Add(worksheet.Name);
                        //Console.WriteLine(worksheet.Name); // outputs the current worksheet name.
                        // do the thing you want to do on each individual worksheet.
                    }
                }
            }
            else
            {
                dt = CommonHelper.ReadExcelSheetName(filePath, Path.GetExtension(filePath), "Yes");
            }
            //  

            if (dt.Rows.Count < 0)
            {
                return null;
            }
            var query = (from p in dt.AsEnumerable()
                         select new
                         {
                             SheetName = p.Field<string>("TABLE_NAME").Replace("'", ""),
                         }).ToList();

            return Json(query, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        public string ReadExcelSheetName()
        {
            string filePath = string.Empty;
            string ExcelFileLocation = ConfigurationManager.AppSettings["FlatFileLocation"];
            var httpRequest = System.Web.HttpContext.Current.Request;
            try
            {

                System.IO.DirectoryInfo exportExcelInfo = new DirectoryInfo(ExcelFileLocation);

                //foreach (FileInfo file in exportExcelInfo.GetFiles())
                //{                    
                //    file.Delete();
                //}

                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        filePath = ExcelFileLocation + "\\" + Path.GetFileName(postedFile.FileName);
                        postedFile.SaveAs(filePath);
                    }
                }
                return filePath;




                //DataTable dt = new DataTable();
                //if (Path.GetExtension(filePath).ToUpper() == ".XLSX")
                //{

                //    dt.Columns.Add("TABLE_NAME", typeof(string));
                //    using (XLWorkbook wb = new XLWorkbook(filePath))
                //    {
                //        foreach (IXLWorksheet worksheet in wb.Worksheets)
                //        {
                //            dt.Rows.Add(worksheet.Name);
                //            //Console.WriteLine(worksheet.Name); // outputs the current worksheet name.
                //            // do the thing you want to do on each individual worksheet.
                //        }
                //    }
                //}
                //else
                //{
                //    dt = CommonHelper.ReadExcelSheetName(filePath, Path.GetExtension(filePath), "Yes");
                //}
                ////  

                //if (dt.Rows.Count < 0)
                //{
                //    return null;
                //}
                //var query = (from p in dt.AsEnumerable()
                //             select new
                //             {
                //                 SheetName = p.Field<string>("TABLE_NAME").Replace("'", ""),
                //             }).ToList();

                //return Json(query, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                // return Json(ex.Message, JsonRequestBehavior.AllowGet);
                return ex.Message;
            }
        }

        [HttpPost]
        public JsonResult ImportData()
        {

            string filePath = string.Empty;
            string ExcelFileLocation = ConfigurationManager.AppSettings["ExcelFileLocation"];
            var httpRequest = System.Web.HttpContext.Current.Request;


            var mMessage = "Package failed";
            var mTotalRecords = "0";
            long mBatchID = 0;
            var mTableName = "";


            var sheetNames = httpRequest.Form.GetValues("SheetName").FirstOrDefault();
            if (sheetNames == null)
            {
                sheetNames = string.Empty;
            }


            System.IO.DirectoryInfo exportExcelInfo = new DirectoryInfo(ExcelFileLocation);
            foreach (FileInfo file in exportExcelInfo.GetFiles())
            {
                file.Delete();
            }

            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    filePath = ExcelFileLocation + Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(filePath);
                }
            }

            if (Path.GetExtension(filePath).ToLower() == ".xml")
            {
                var fileNamePath = ExcelFileLocation + Path.GetFileNameWithoutExtension(filePath) + ".csv";
                DataSet ds = new DataSet();
                ds.ReadXml(filePath);

                if (ds.Tables.Count > 0)
                {
                    ds.Tables[0].ToCSV(fileNamePath);
                }

                filePath = fileNamePath;
            }


            mTableName = Path.GetFileNameWithoutExtension(filePath);


            int mmTotalRecords = 0;
            int Source_Column_Count = 0;

            if (!string.IsNullOrWhiteSpace(sheetNames))
            {
                foreach (string mmSheetName in sheetNames.Split('~'))
                {
                    var mdt = CommonHelper.ReadExcelData(filePath, Path.GetExtension(filePath), "Yes", mmSheetName);
                    mmTotalRecords += mdt.Rows.Count;
                    Source_Column_Count = mdt.Columns.Count;
                }
            }

            mMessage = ExecuteExcelPackage(filePath, mTableName, sheetNames, Convert.ToString(Source_Column_Count), Convert.ToString(mmTotalRecords), ref mBatchID);

            var lst = new
            {
                PackageMessage = mMessage,
                TotalRecords = mTotalRecords,
                BatchID = mBatchID,
                TableName = mTableName,
                FileName = Path.GetFileName(filePath),
                SheetName = sheetNames,
            };

            return Json(lst, JsonRequestBehavior.AllowGet);


        }



        public JsonResult SourceAction(string Template_Id, string Template_Name, string Connection_ID, string Table_Name, string Field_Name, string Field_Seq_No, string Field_Type, string Data_Type, string Data_Precision, string Data_Scale, string Field_Data, string Key_column, string update_all, string oper, int? id)
        {
            string StatusCode = string.Empty, Message = string.Empty, _Result = string.Empty;
            List<SourceEntity> SourceEntity = new List<SourceEntity>();
            string ClientID = UIProperties.Sessions.Client.Client_ID;
            string ProjectID = UIProperties.Sessions.Client.project_ID;

            if (oper == "add" || oper == "edit")
            {
                SourceEntity.Add(new SourceEntity()
                {
                    Client_ID = ClientID,
                    Project_ID = ProjectID,
                    Connection_ID = Connection_ID,
                    Template_ID = Template_Id,
                    Table_Name = Table_Name,
                    Template_Name = Template_Name,
                    Field_Seq_No = Field_Seq_No,
                    Field_Data = Field_Data,
                    Field_Name = Field_Name,
                    Field_Data_Type = Data_Type,
                    Field_Prec = Data_Precision,
                    Field_Scale = Data_Scale,
                    Field_Key = Key_column,
                    Field_Type = Field_Type,
                    Row_ID = id != null ? Convert.ToString(id) : null,
                    Create_Date = DateTime.Now,
                    Modified_Date = DateTime.Now,
                    Created_By = UIProperties.Sessions.ToolID

                });


                if (update_all == "Y")
                {
                    _autoMS.ModifySourceGrid(SourceEntity, ref StatusCode, ref Message);
                }
                else
                {
                    _autoMS.SaveSourceGrid(SourceEntity, ref StatusCode, ref Message);
                }


                if (Message == "Success") _Result = "Saved successfully."; else _Result = "failed to save record.";

            }
            else if (oper == "del")
            {
                _autoMS.DeleteSourceGrid(Convert.ToInt64(id), ref StatusCode, ref Message);
                if (Message == "Success") _Result = "Deleted successfully."; else _Result = "Deleting failed.";
            }

            return Json((new
            {
                Message = _Result,

            }), JsonRequestBehavior.AllowGet);

        }
        public JsonResult TargetAction(string Template_Id, string Template_Name, string Connection_ID, string Table_Name, string Field_Name, string Field_Seq_No, string Field_Type, string Data_Type, string Data_Precision, string Data_Scale, string Field_Data, string Key_column, string update_all, string oper, int? id)
        {
            string StatusCode = string.Empty, Message = string.Empty, _Result = string.Empty;
            List<TargetEntity> targetEntity = new List<TargetEntity>();
            string ClientID = UIProperties.Sessions.Client.Client_ID;
            string ProjectID = UIProperties.Sessions.Client.project_ID;

            if (oper == "add" || oper == "edit")
            {
                targetEntity.Add(new TargetEntity()
                {
                    Client_ID = ClientID,
                    Project_ID = ProjectID,
                    Template_ID = Template_Id,
                    Table_Name = Table_Name,
                    Template_Name = Template_Name,
                    Field_Seq_No = Field_Seq_No,
                    Field_Data = Field_Data,
                    Field_Name = Field_Name,
                    Field_Data_Type = Data_Type,
                    Field_Prec = Data_Precision,
                    Field_Scale = Data_Scale,
                    Field_Key = Key_column,
                    Field_Type = Field_Type,
                    Row_ID = id != null ? Convert.ToString(id) : null,
                    Create_Date = DateTime.Now,
                    Modified_Date = DateTime.Now,
                    Created_By = UIProperties.Sessions.ToolID

                });


                if (update_all == "Y")
                {
                    _autoMS.ModifyTargetGrid(targetEntity, ref StatusCode, ref Message);
                }
                else
                {
                    _autoMS.SaveTargetGrid(targetEntity, ref StatusCode, ref Message);
                }



                if (Message == "Success") _Result = "Saved successfully."; else _Result = "failed to save record.";

            }
            else if (oper == "del")
            {
                _autoMS.DeleteTargetGrid(Convert.ToInt64(id), ref StatusCode, ref Message);
                if (Message == "Success") _Result = "Deleted successfully."; else _Result = "Deleting failed.";
            }

            return Json((new
            {
                Message = _Result,

            }), JsonRequestBehavior.AllowGet);

        }
        public JsonResult TransformationAction(string Template_Id, string Template_Name, string Table_Name, string Field_Name, string Field_Data_Type, string Field_Length, string Trans_Type, string Trans_Name, string Trans_Order, string Trans_Rule, string oper, int? id)
        {
            string StatusCode = string.Empty, Message = string.Empty, _Result = string.Empty;

            var transformEntity = new List<TransformEntity>();
            string ClientID = UIProperties.Sessions.Client.Client_ID;
            string ProjectID = UIProperties.Sessions.Client.project_ID;

            if (oper == "add" || oper == "edit")
            {
                transformEntity.Add(new TransformEntity()
                {
                    Client_ID = ClientID,
                    Project_ID = ProjectID,
                    Template_ID = Template_Id,
                    Table_Name = Table_Name,
                    Template_Name = Template_Name,
                    Trans_Order = Trans_Order,
                    Trans_Type = Trans_Type,
                    Field_Name = Field_Name,
                    Trans_Name = Trans_Name,
                    Field_Data_Type = Field_Data_Type,
                    Field_Length = Field_Length,
                    Trans_Rule = Trans_Rule,
                    Trans_ID = id == null ? "0" : Convert.ToString(id),
                    Create_Date = DateTime.Now,
                    Modified_Date = DateTime.Now,
                    Modified_by = UIProperties.Sessions.ToolID
                });

                _autoMS.SaveTransGrid(transformEntity, ref StatusCode, ref Message);

                if (Message == "Success") _Result = "Saved successfully."; else _Result = "failed to save record.";

            }
            else if (oper == "del")
            {
                _autoMS.DeleteTransGrid(Convert.ToInt64(id), ref StatusCode, ref Message);
                if (Message == "Success") _Result = "Deleted successfully."; else _Result = "deleting failed.";
            }


            return Json((new
            {
                Message = _Result,

            }), JsonRequestBehavior.AllowGet);

        }

        [NonAction]
        private string ExecuteExcelPackage(string FilePath, string TableName, string SheetNames, string Source_Column_Count, string TotalRecords, ref long BatchID)
        {
            string msg = "success";

            string pkgLocation = ConfigurationManager.AppSettings["AutomatonSSISpath"];
            string Generated_SSIS_PkgLocation = ConfigurationManager.AppSettings["Generated_SSIS_PkgLocation"];
            string ExcelFileLocation = ConfigurationManager.AppSettings["ExcelFileLocation"];

            if (System.IO.File.Exists(pkgLocation))
            {
                Package pkg;
                Application app;
                DTSExecResult pkgResults;
                app = new Application();
                pkg = app.LoadPackage(pkgLocation, null);

                pkg.Variables["Client_ID"].Value = UIProperties.Sessions.Client.Client_ID;
                pkg.Variables["Project_ID"].Value = UIProperties.Sessions.Client.project_ID;
                pkg.Variables["Config_ID"].Value = GetDBConfiguID("SOURCE");
                pkg.Variables["Conn_String"].Value = GetSourceDBConnectionString();
                pkg.Variables["MetaDataConnString"].Value = CommonHelper.GetCurrentConnectionString();
                pkg.Variables["ExcelFile"].Value = Path.GetFileName(FilePath);
                pkg.Variables["ExcelFileLocation"].Value = ExcelFileLocation;
                pkg.Variables["ExcelSheets"].Value = SheetNames;
                pkg.Variables["Generated_SSIS_PkgLocation"].Value = Generated_SSIS_PkgLocation;

                //Start Run Audit log

                string Client_ID = UIProperties.Sessions.Client.Client_ID;
                string project_ID = UIProperties.Sessions.Client.project_ID;
                string ToolID = UIProperties.Sessions.ToolID;
                string UserName = UIProperties.Sessions.UserName;
                string StatusCode = string.Empty, Message = string.Empty;
                long RunID = 0;
                string Target_TableName = Path.GetFileNameWithoutExtension(FilePath);
                string Source_TableName = Path.GetFileNameWithoutExtension(FilePath);



                _autoMS.LogExcelRunAudit(Client_ID, project_ID, ToolID, Source_TableName, Source_Column_Count, TotalRecords, null, "i", Target_TableName, UserName, null,
                    ref RunID, UIProperties.Sessions.Client.Role_ID,ref StatusCode, ref Message);

                pkgResults = pkg.Execute();
                if (pkgResults == DTSExecResult.Failure)
                {
                    string sErr = "";
                    foreach (var err in pkg.Errors)
                    {
                        sErr += err.Description;
                    }
                    msg = "Package failed. " + sErr;

                    _autoMS.LogExcelRunAudit(Client_ID, project_ID, ToolID, Source_TableName, Source_Column_Count, TotalRecords, null, "E", Target_TableName, UserName, RunID,
                        ref RunID, UIProperties.Sessions.Client.Role_ID, ref StatusCode, ref Message);
                }
                else
                {
                    BatchID = Convert.ToInt64(pkg.Variables["Batch_ID"].Value);

                    _autoMS.LogExcelRunAudit(Client_ID, project_ID, ToolID, Source_TableName, Source_Column_Count, TotalRecords, BatchID, "C", Target_TableName,
                        UserName, RunID, ref RunID, UIProperties.Sessions.Client.Role_ID, ref StatusCode, ref Message);
                }


            }

            return msg;

        }

        [NonAction]
        private string GetSourceDBConnectionString()
        {

            string ConnectingString = string.Empty;
            long ToolID = 0;
            string ClientID = UIProperties.Sessions.Client.Client_ID;
            string ProjectID = UIProperties.Sessions.Client.project_ID;
            long.TryParse(UIProperties.Sessions.ToolID, out ToolID);
            int? RoleId = UIProperties.Sessions.Client.Role_ID;
            string StatusCode = string.Empty, Message = string.Empty;
            HXRConfigurationMSEntity _configEntity = _configMS.GetConfigurationByType(ClientID, ProjectID, "SOURCE", ToolID, RoleId, ref StatusCode, ref Message);
            if (_configEntity != null)
            {
                ConnectingString = CommonHelper.BuildConnnectionString(_configEntity.ServerIPAddress, _configEntity.DataBaseName, _configEntity.DataBasePassword, _configEntity.SchemaName);
            }
            return ConnectingString;

        }

        [NonAction]
        private string GetDBConfiguID(string Type)
        {

            string ConnectingString = string.Empty;
            long ToolID = 0;
            string ClientID = UIProperties.Sessions.Client.Client_ID;
            string ProjectID = UIProperties.Sessions.Client.project_ID;
            long.TryParse(UIProperties.Sessions.ToolID, out ToolID);
            int? RoleId = UIProperties.Sessions.Client.Role_ID;
            string StatusCode = string.Empty, Message = string.Empty;
            HXRConfigurationMSEntity _configEntity = _configMS.GetConfigurationByType(ClientID, ProjectID, Type, ToolID, RoleId, ref StatusCode, ref Message);
            if (_configEntity != null)
            {
                return Convert.ToString(_configEntity.Config_ID);
            }
            else
            {
                return "0";
            }

        }

        [NonAction]
        private string GetDataBasePassword(string Type)
        {

            string ConnectingString = string.Empty;
            long ToolID = 0;
            string ClientID = UIProperties.Sessions.Client.Client_ID;
            string ProjectID = UIProperties.Sessions.Client.project_ID;
            long.TryParse(UIProperties.Sessions.ToolID, out ToolID);
            int? RoleId = UIProperties.Sessions.Client.Role_ID;
            string StatusCode = string.Empty, Message = string.Empty;
            HXRConfigurationMSEntity _configEntity = _configMS.GetConfigurationByType(ClientID, ProjectID, Type, ToolID, RoleId, ref StatusCode, ref Message);
            if (_configEntity != null)
            {
                return _configEntity.DataBasePassword;
            }

            return string.Empty;

        }


        public string ParameterUpdate(string oper, int? Parameter_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty, _Result = string.Empty;
            _autoMS.DeleteParameter(Convert.ToInt64(Parameter_ID), UIProperties.Sessions.UserName, ref StatusCode, ref Message);
            return _Result;

        }


        [HttpPost]
        public JsonResult RunOfflinePackage(string TemplateID, string TemplateName, string TemplateType)
        {
            string[] msg = new string[2];
            string StatusCode = string.Empty, Message = string.Empty;

            // string TemplatePath = Path.Combine(ConfigurationManager.AppSettings["Generatexml_Save_PkgLocation"], TemplateName + ".dtsx");
            string TemplatePath = string.Empty;

            if (TemplateType == "Transformation" || TemplateType == "DataType")
                TemplatePath = Path.Combine(ConfigurationManager.AppSettings["generatexml_transformation"], TemplateName + ".dtsx");
            else if (TemplateType == "Master")
                TemplatePath = ConfigurationManager.AppSettings["master_pkg"];
            else if (TemplateType == "FileUpload")
                TemplatePath = Path.Combine(ConfigurationManager.AppSettings["generatexml_fileupload"], TemplateName + ".dtsx");

            string Client_ID = UIProperties.Sessions.Client.Client_ID;
            string project_ID = UIProperties.Sessions.Client.project_ID;
            string UserName = UIProperties.Sessions.UserName;
            string ToolID = UIProperties.Sessions.ToolID;
            string RunStatus = "I";
            if (System.IO.File.Exists(TemplatePath))
            {

                _autoMS.InsertOfflineBatchJobs(Client_ID, project_ID, Convert.ToInt64(ToolID), TemplateName, TemplatePath, UserName, RunStatus, "", null, ref StatusCode, ref Message);

                msg[0] = "successfully offline job created";
            }
            else
            {

                msg[0] = "Package is not available.";
            }


            return Json(msg, JsonRequestBehavior.AllowGet);

        }

        [ActionName("DownloadPackage")]
        public ActionResult DownloadPackage(string TemplateID, string TemplateName)
        {

            string filePath = Path.Combine(ConfigurationManager.AppSettings["Generatexml_Save_PkgLocation"], TemplateName + ".dtsx");
            string contentType = "application/xml";
            if (!System.IO.File.Exists(filePath))
            {
                ViewBag.FileNotFound = "Yes";
                ViewDatas();
                ViewBags(UIProperties.AutomatonMenu.ReviewAndExecute);
                return View("ReviewAndGenerate");
            }
            var fname = Path.GetFileName(filePath);
            return File(filePath, contentType, fname);
        }

        [ActionName("DownloadDocument")]
        public ActionResult DownloadDocument(string TemplateID, string TemplateName)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            string designTemplate = Path.Combine(ConfigurationManager.AppSettings["DesignDocFolder"], "Automaton_ETL_Design_template.docx");

            _autoMS.GenerateDesignDocument(
                UIProperties.Sessions.Client.Client_ID
                , UIProperties.Sessions.Client.project_ID
                , Convert.ToInt64("" + UIProperties.Sessions.ToolID)
                , Convert.ToInt64("" + TemplateID)
                , TemplateName, designTemplate, UIProperties.Sessions.Client.Role_ID, ref StatusCode, ref Message);

            string filePath = Path.Combine(ConfigurationManager.AppSettings["DesignGeneratedDocFolder"], TemplateName + ".docx");
            string contentType = "application/xml";
            if (!System.IO.File.Exists(filePath))
            {
                ViewBag.FileNotFound = "Yes";
                ViewDatas();
                ViewBags(UIProperties.AutomatonMenu.ReviewAndExecute);
                return View("ReviewAndGenerate");
            }
            var fname = Path.GetFileName(filePath);
            return File(filePath, contentType, fname);
        }

        public ActionResult ETLScheduler()
        {
            ViewData["ToolID"] = UIProperties.Sessions.ToolID;
            ViewDatas();
            ViewBags(UIProperties.AutomatonMenu.ETLScheduler);
            return View();
        }

        public string UpdateSchedule(string oper, long Job_ID)
        {
            string _Result = string.Empty;
            if (oper == "del")
            {
                _Result = this.UpdateScheduleTransformation(Job_ID, null, 'Y').Data as string;
            }
            _Result = _Result.ToLower().Contains("success") ? "Record deleted successfully" : _Result;
            return _Result;

        }

        [HttpPost]
        public JsonResult UpdateScheduleTransformation(long Job_ID, string schedule_Date, char isDelete)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            Nullable<DateTime> schdate = null;
            if (schedule_Date != null) { schdate = Convert.ToDateTime(schedule_Date); }
            _autoMS.UpdateScheduleTransformation(Job_ID, schdate, isDelete, ref StatusCode, ref Message);

            return Json(Message, JsonRequestBehavior.AllowGet);
        }

        public void WriteErrorLog(string Message)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            string _filePath = Path.GetFullPath(Path.Combine(filePath, @"ErrorLog.txt"));

            if (!System.IO.File.Exists(_filePath))
                System.IO.File.Create(_filePath);
            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {
                writer.WriteLine("Date :" + DateTime.Now.ToString() +
                    " Message :" + Message + "<br/>" + Environment.NewLine + "" + Environment.NewLine);
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }

        }
        [ActionName("GenerateReconcile")]
        public dynamic GenerateReconcile(string Template_ID)
        {
            string Client_ID = UIProperties.Sessions.Client.Client_ID;
            string project_ID = UIProperties.Sessions.Client.project_ID;
            string StatusCode = string.Empty, Message = string.Empty;

            _autoMS.GenerateReconcile(Client_ID, project_ID, Template_ID, UIProperties.Sessions.Client.Role_ID, UIProperties.Sessions.Client.User_ID.ToString(), ref StatusCode, ref Message);

            //return Json(new { StatusCode = StatusCode, Message = Message }, JsonRequestBehavior.AllowGet);
            return new { StatusCode = StatusCode, Message = Message };

        }

    }
}
