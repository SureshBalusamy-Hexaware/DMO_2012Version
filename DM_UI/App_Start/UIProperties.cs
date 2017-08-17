using DM_BusinessEntities;
using DM_UI.Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DM_UI.App_Start
{
    public class UIProperties
    {
        public static class Sessions
        {
            public static HXRGetClientEntity Client
            {
                get { return HttpContext.Current.Session["Client"] != null ? (HXRGetClientEntity)HttpContext.Current.Session["Client"] : null; }
                set { HttpContext.Current.Session["Client"] = value; }
            }
            public static HXRConfigurationMSEntity ConfigEntity
            {
                get { return HttpContext.Current.Session["ConfigEntity"] != null ? (HXRConfigurationMSEntity)HttpContext.Current.Session["ConfigEntity"] : null; }
                set { HttpContext.Current.Session["ConfigEntity"] = value; }
            }
            public static HXRTargetConfigurationMSEntity TargetConfigEntity
            {
                get { return HttpContext.Current.Session["TargetConfigEntity"] != null ? (HXRTargetConfigurationMSEntity)HttpContext.Current.Session["TargetConfigEntity"] : null; }
                set { HttpContext.Current.Session["TargetConfigEntity"] = value; }
            }
            public static string ConfigID
            {
                get { return HttpContext.Current.Session["ConfigID"] != null ? HttpContext.Current.Session["ConfigID"].ToString() : string.Empty; }
                set { HttpContext.Current.Session["ConfigID"] = value; }
            }
            public static string ToolID
            {
                get { return HttpContext.Current.Session["ToolID"] != null ? HttpContext.Current.Session["ToolID"].ToString() : string.Empty; }
                set { HttpContext.Current.Session["ToolID"] = value; }
            }
            public static string UserName
            {
                get { return HttpContext.Current.Session["userid"] != null ? HttpContext.Current.Session["userid"].ToString() : string.Empty; }
                set { HttpContext.Current.Session["userid"] = value; }
            }
            public static string RoleName
            {
                get { return HttpContext.Current.Session["RoleName"] != null ? HttpContext.Current.Session["RoleName"].ToString() : string.Empty; }
                set { HttpContext.Current.Session["RoleName"] = value; }
            }

        }
        public static class Roles
        {
            public static string Admin
            {
                get { return ConfigurationManager.AppSettings["RoleAdmin"]; }
            }
            public static string User
            {
                get { return ConfigurationManager.AppSettings["RoleUser"]; }
            }

        }
        //Last tool id 40
        public enum Tools
        {
            Automaton = 1,
            DART = 2,
            DashboardReports = 3,
            DataRecon = 4,
            DIMA = 5,
            DIMAPLUS = 6,
            HexaRule = 7,
            InfaGen = 8,
            SQLPARSER = 9,
            DataProfiler = 11,
            IConvert = 12,
            MasterSetup = 13 //-.63
            //MasterSetup = 13 //.54
        }

        public enum AutomatonMenu
        {
            Configuration = 101,
            CreateTemplate = 102,
            CreateCustomTemplate = 103,
            CopyTemplate = 104,
            ReviewAndExecute = 105,
            DataManagement = 106,
            Upload = 107,
            Download = 108,
            BusinessName = 37,
            ETLScheduler = 109,
            UploadExcelFile = 110

        }
        public enum DataReconMenu
        {
            Configuration = 401,
            Reconciliation = 402,
            Report = 403,
            Dashboard = 404,
        }
        public enum DIMAPLUSMenu
        {
            //Configuration = 601,
            //Criteria = 604,
            //CopySlicedData = 605,
            //Reports = 607,
            //PurgeSlicedData = 606,
            //DataMasking = 603,
            //DataManagement = 602,
            //Upload = 641,
            //Download = 642,
            //BusinessName = 643

            Configuration = 601,
            DataManagement = 602,
            UploadExcelFile = 603,
            Upload = 604,
            Download = 605,
            BusinessName = 606,
            DataMasking = 607,
            Criteria = 608,
            CopySlicedData = 609,
            PurgeSlicedData = 610,
            Reports = 611
        }
        public enum HexaRuleMenu
        {
            //Configuration = 16,
            //RuleSetup = 17,
            //Rule = 18,
            //RuleType = 19,
            //RuleCategory = 20,
            //Keycolumns = 21,
            //Error = 22,
            //PreDefinedRule = 23,
            //CreateRule = 24,
            //RuleAllocation = 25,
            //ProcessandUpdates = 45,
            //CorrectionandReProcess = 46,
            //Report = 27,
            //Dashboard = 28,

            Configuration = 701,
            RuleSetup = 702,
            Rule = 703,
            RuleType = 704,
            RuleCategory = 705,
            Keycolumns = 706,
            Error = 707,
            PreDefinedRule = 708,
            CreateRule = 709,
            RuleAllocation = 710,
            ProcessandUpdates = 714,
            CorrectionandReProcess = 715,
            Report = 712,
            Dashboard = 713

        }
        public enum DataProfilerMenu
        {
            //Configuration = 29,
            //LoadData = 30,
            //Profile = 31,
            //OfflineProfile = 32,
            //CodeRuleManagement = 33,
            //Report = 34,
            //Dashboard = 36

            Configuration = 1101,
            LoadData = 30,
            Profile = 1103,
            OfflineProfile = 1102,
            CodeRuleManagement = 1105,
            Report = 1106,
            Dashboard = 1107
        }

        public enum MasterSetupMenu
        {
            //ProjectSetup = 38,
            //ProcessOrchestration = 39,
            //ManageRole = 44,
            //DBDeployment = 49
            ProjectSetup = 1303,
            ProcessOrchestration = 1302,
            ManageRole = 1304,
            DBDeployment = 1301,
            CreateRole = 1305,
            AssingRole = 1306
        }


        public static class Tool_Logos
        {
            private static string Automaton { get { return "Images/automation.png"; } }
            private static string DART { get { return "Images/DART.png"; } }
            private static string DashboardReports { get { return "Images/DashBoard.png"; } }
            private static string DataRecon { get { return "Images/DataRecon.png"; } }
            private static string DIMA { get { return "Images/DIMA.png"; } }
            private static string DASEM { get { return "Images/dasem.png"; } }
            private static string HexaRule { get { return "Images/HexaRule.png"; } }
            private static string InfaGen { get { return "Images/InfaGen.png"; } }
            private static string SQLPARSER { get { return "Images/sql.png"; } }
            private static string DataProfiler { get { return "Images/dataprofiler.png"; } }
            private static string MasterSetup { get { return "Images/mastersetup.png"; } }
            public static object GetToolLogo(int ToolId)
            {
                switch (ToolId)
                {
                    case (int)Tools.Automaton:
                        return new { Logo = Automaton, PostBackUrl = "Automaton/Configuration" };
                    case (int)Tools.DART:
                        return new { Logo = DART, PostBackUrl = "DART/Configuration" };
                    case (int)Tools.DashboardReports:
                        return new { Logo = DashboardReports, PostBackUrl = "DashBoard.aspx" };
                    case (int)Tools.DataRecon:
                        return new { Logo = DataRecon, PostBackUrl = "DataRecon/Configuration" };
                    case (int)Tools.DIMA:
                        return new { Logo = DIMA, PostBackUrl = "DIMA/Configuration" };
                    case (int)Tools.DIMAPLUS:
                        return new { Logo = DASEM, PostBackUrl = "DIMAPLUS/Configuration" };
                    case (int)Tools.HexaRule:
                        return new { Logo = HexaRule, PostBackUrl = "HexaRule/Index" };
                    case (int)Tools.InfaGen:
                        return new { Logo = InfaGen, PostBackUrl = "InfaGen/Configuration" };
                    case (int)Tools.SQLPARSER:
                        return new { Logo = SQLPARSER, PostBackUrl = "SQLPARSER/Configuration" };
                    case (int)Tools.DataProfiler:
                        return new { Logo = DataProfiler, PostBackUrl = "DataProfiler/Configuration" };
                    case (int)Tools.MasterSetup:
                        return new { Logo = MasterSetup, PostBackUrl = "Home/MasterSetup" };
                    default:
                        return new { Logo = DashboardReports, PostBackUrl = "DashBoard.aspx" };
                }
            }
        }
        public enum DataType
        {
            Text = 1,
            Number = 2,
            Date = 3
        }
        public enum Hexarule_RptCategory
        {
            MAINDATA,
            RULEREPORTS,
            DATAREPORTS
        }
        public static string DownloadPath
        {
            get { return ConfigurationManager.AppSettings["DownloadPath"]; }
        }
        public static string GetDataType(string _sqlDataType)
        {
            string _Type = string.Empty;
            switch (_sqlDataType.ToUpper().Trim())
            {
                case "NVARCHAR":
                case "CHAR":
                case "VARCHAR":
                case "TEXT":
                    _Type = UIProperties.DataType.Text.ToString();
                    break;
                default:
                    _Type = string.Empty;
                    break;

            }
            return _Type;
        }
        public static object ConvertToJQGridRows(System.Data.DataTable dt, int page, int rows)
        {

            long TotalCount = 0;
            int _ColumnCount = 0;

            if (dt.Rows.Count > 0 && dt.Columns.Contains("TotalRecords"))
            {
                long.TryParse(dt.Rows[0]["TotalRecords"].ToString(), out TotalCount);

                dt.Columns.Remove("TotalRecords");
                dt.AcceptChanges();
            }
            _ColumnCount = dt.Columns.Count;

            var json = new _JSON();
            json.total = Math.Ceiling(Convert.ToDouble(TotalCount) / rows).ToString();
            json.page = page.ToString();
            json.records = TotalCount.ToString();
            json.rows = new List<DM_BusinessEntities.rows>();
            int _rowIndex = 1;
            dt.Rows.Cast<System.Data.DataRow>().ToList().ForEach(datarow =>
            {

                string[] _r = new string[_ColumnCount];
                int _colIndex = 0;
                rows r = new DM_BusinessEntities.rows();

                dt.Columns.Cast<System.Data.DataColumn>().ToList()
                    .ForEach(column =>
                    {

                        _r[_colIndex] = datarow[column].ToString();
                        _colIndex++;
                    });
                r.id = _rowIndex.ToString();
                r.cell = _r;
                json.rows.Add(r);
                _rowIndex++;
            });

            return json;
        }
        public static class MenuTable
        {
            public static List<Menu> ToolMenu { get; set; }
            public static int SelectedMenuId { get; set; }
            public static void GetMenu(int _ToolId)
            {
                ToolMenu = new List<Menu> { 
new Menu { ToolId = ((int)Tools.Automaton), MenuId = 101, ParentMenuId = 0,     MenuOrderId = 1,    MenuName = "Configuration",             Controller="Automaton", Action = "Configuration",                          ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%;" } },
new Menu { ToolId = ((int)Tools.Automaton), MenuId = 102, ParentMenuId = 111,   MenuOrderId = 21,   MenuName = "DataType Template",         Controller="Automaton", Action = "CreateTemplate",                         ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="margin: 1px !important; text-align: left; cursor: pointer; width: 99%;" } },
new Menu { ToolId = ((int)Tools.Automaton),	MenuId = 103, ParentMenuId = 111,   MenuOrderId = 22,	MenuName = "Transformation Template",   Controller="Automaton", Action = "CreateCustomTemplate",            ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%; margin: 1px !important; text-align: left; cursor: pointer; width: 99%;" } },
new Menu { ToolId = ((int)Tools.Automaton),	MenuId = 104, ParentMenuId = 0,     MenuOrderId = 4,	MenuName = "Copy Template",             Controller="Automaton", Action = "CopyTemplate",                             ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%;" } },
new Menu { ToolId = ((int)Tools.Automaton),	MenuId = 105, ParentMenuId = 0,     MenuOrderId = 5,	MenuName = "Review & Execute",          Controller="Automaton", Action = "ReviewAndGenerate",                      ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%;" } },
new Menu { ToolId = ((int)Tools.Automaton),	MenuId = 106, ParentMenuId = 0,     MenuOrderId = 6,	MenuName = "Data Management",           Controller="Automaton", Action = null,                                     ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%;" } },
new Menu { ToolId = ((int)Tools.Automaton),	MenuId = 107, ParentMenuId = 111,   MenuOrderId = 23,	MenuName = "Upload File Template",          Controller="Automaton", Action = "ExcelTemplate",                          ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%; margin: 1px !important; text-align: left; cursor: pointer; width: 99%;" } },
new Menu { ToolId = ((int)Tools.Automaton),	MenuId = 108, ParentMenuId = 106,   MenuOrderId = 62,	MenuName = "Download",                  Controller="Automaton", Action = "Downloads",                              ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%; margin: 1px !important; text-align: left; cursor: pointer; width: 99%;" } },
new Menu { ToolId = ((int)Tools.Automaton),	MenuId = 109, ParentMenuId = 0,     MenuOrderId = 7,	MenuName = "Scheduler",                 Controller="Automaton", Action = "ETLScheduler",                         ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%;" } },
new Menu { ToolId = ((int)Tools.Automaton),	MenuId = 110, ParentMenuId = 106,   MenuOrderId = 61,  	MenuName = "Upload Excel File",         Controller="Automaton", Action = "UploadExcel",                          ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%; margin: 1px !important; text-align: left; cursor: pointer; width: 99%;" } },
new Menu { ToolId = ((int)Tools.Automaton),	MenuId = 111, ParentMenuId = 0,     MenuOrderId = 2,  	MenuName = "Template Management",       Controller="Automaton", Action = null,                          ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%; margin: 1px !important; cursor: pointer;" } },

//new Menu { ToolId = ((int)Tools.Automaton),	MenuId = 47,	MenuName = "Scheduler",           Controller="Automaton", Action = "ETLScheduler",                       ParentMenuId = 0,  ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%;" } },
//new Menu { ToolId = ((int)Tools.Automaton),	MenuId = 37,	MenuName = "Business Name",          Controller="Automaton", Action = "BusinessName",                         ParentMenuId = 6,  ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%; margin: 1px !important; text-align: left; cursor: pointer; width: 99%;" } },

                     
new Menu { ToolId = ((int)Tools.DataRecon),	MenuId = 401, ParentMenuId = 0, MenuOrderId = 1,	    MenuName = "Configuration",         Controller="DataRecon", Action = "Configuration",                 ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false } },
new Menu { ToolId = ((int)Tools.DataRecon),	MenuId = 402, ParentMenuId = 0, MenuOrderId = 2,	    MenuName = "Data Reconciliation",   Controller="DataRecon", Action = "Reconciliation",                ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false } },
new Menu { ToolId = ((int)Tools.DataRecon),	MenuId = 403, ParentMenuId = 0, MenuOrderId = 3,	    MenuName = "Report",                Controller="DataRecon", Action = "Report",                        ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false } },
new Menu { ToolId = ((int)Tools.DataRecon),	MenuId = 404, ParentMenuId = 0, MenuOrderId = 4,	    MenuName = "Dashboard",             Controller="DashBoard", Action = "DataRecon",                        ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false } },



new Menu { ToolId = ((int)Tools.DIMAPLUS),	MenuId = 601, ParentMenuId = 0, MenuOrderId = 1,	MenuName = "Configuration",         Controller="DIMAPLUS", Action = "Index",                                  ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 15%;" } },
new Menu { ToolId = ((int)Tools.DIMAPLUS),	MenuId = 602, ParentMenuId = 0, MenuOrderId = 2,	MenuName = "Data Management",       Controller="DIMAPLUS", Action = null,                                     ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%;" } },
new Menu { ToolId = ((int)Tools.DIMAPLUS),	MenuId = 603, ParentMenuId = 602, MenuOrderId = 21,	MenuName = "Upload Excel File",                Controller="DIMAPLUS", Action = "UploadExcel",                          ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%; margin: 1px !important; text-align: left; cursor: pointer; width: 99%;" } },
//new Menu { ToolId = ((int)Tools.DIMAPLUS),	MenuId = 604, ParentMenuId = 602, MenuOrderId = 22,	MenuName = "Upload File Template",                Controller="DIMAPLUS", Action = "ExcelTemplate",                          ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%; margin: 1px !important; text-align: left; cursor: pointer; width: 99%;" } },
new Menu { ToolId = ((int)Tools.DIMAPLUS),	MenuId = 605, ParentMenuId = 602, MenuOrderId = 23, MenuName = "Download",              Controller="DIMAPLUS", Action = "Downloads",                              ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%; margin: 1px !important; text-align: left; cursor: pointer; width: 99%;" } },
new Menu { ToolId = ((int)Tools.DIMAPLUS),	MenuId = 606, ParentMenuId = 602, MenuOrderId = 24, MenuName = "Business Name",         Controller="DIMAPLUS", Action = "BusinessName",                           ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%; margin: 1px !important; text-align: left; cursor: pointer; width: 99%;" } },
new Menu { ToolId = ((int)Tools.DIMAPLUS),	MenuId = 607, ParentMenuId = 0,  MenuOrderId = 3, MenuName = "Data Masking",          Controller="DIMAPLUS", Action = "DataMasking",                       ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 10%;" } },
new Menu { ToolId = ((int)Tools.DIMAPLUS),	MenuId = 608, ParentMenuId = 0,	MenuOrderId = 4, MenuName = "Slice Criteria",        Controller="DIMAPLUS", Action = "Criteria",                               ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 10%;" } },
new Menu { ToolId = ((int)Tools.DIMAPLUS),	MenuId = 609, ParentMenuId = 0,	MenuOrderId = 5, MenuName = "Copy Sliced Data",      Controller="DIMAPLUS", Action = "Copy",                                  ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 15%;" } },                   
new Menu { ToolId = ((int)Tools.DIMAPLUS),	MenuId = 610, ParentMenuId = 0,	MenuOrderId = 6, MenuName = "Purge Sliced Data",     Controller="DIMAPLUS", Action = "Purge",                                  ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 15%;" } },                    
new Menu { ToolId = ((int)Tools.DIMAPLUS),	MenuId = 611, ParentMenuId = 0,	MenuOrderId = 7, MenuName = "Reports",               Controller="DIMAPLUS", Action = "Reports",                                ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 15%;" } },
                     

new Menu { ToolId = ((int)Tools.HexaRule),	MenuId = 701, ParentMenuId = 0, MenuOrderId = 0,	MenuName = "Configuration",         Controller="Hexarule", Action = "Index",        ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 10%;" } },
new Menu { ToolId = ((int)Tools.HexaRule),	MenuId = 702, ParentMenuId = 0, MenuOrderId = 0,	MenuName = "RuleSetup",             Controller="Hexarule", Action = null,           ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%;" } },
new Menu { ToolId = ((int)Tools.HexaRule),	MenuId = 703, ParentMenuId = 702, MenuOrderId = 21,	MenuName = "Rule",                  Controller="Hexarule", Action = "Rule",         ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="margin: 1px !important; text-align: left; cursor: pointer; width: 99%;" } },
new Menu { ToolId = ((int)Tools.HexaRule),	MenuId = 704, ParentMenuId = 702, MenuOrderId = 22, 	MenuName = DM_en_US.lblRuleType,              Controller="Hexarule", Action = "RuleType",                      ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="margin: 1px !important; text-align: left; cursor: pointer; width: 99%;" } },
new Menu { ToolId = ((int)Tools.HexaRule),	MenuId = 705, ParentMenuId = 702, MenuOrderId = 23,	MenuName = DM_en_US.lblRuleCategory,          Controller="Hexarule", Action = "RuleCategory",                  ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="margin: 1px !important; text-align: left; cursor: pointer; width: 99%;" } },
new Menu { ToolId = ((int)Tools.HexaRule),	MenuId = 706, ParentMenuId = 702, MenuOrderId = 24,	MenuName = "Key columns",           Controller="Hexarule", Action = "HXRKeyColumns",                 ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="margin: 1px !important; text-align: left; cursor: pointer; width: 99%;" } },
new Menu { ToolId = ((int)Tools.HexaRule),	MenuId = 707, ParentMenuId = 702, MenuOrderId = 25,	MenuName = "Error",                 Controller="Hexarule", Action = "RuleError",                     ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="margin: 1px !important; text-align: left; cursor: pointer; width: 99%;" } },
new Menu { ToolId = ((int)Tools.HexaRule),	MenuId = 708, ParentMenuId = 0, MenuOrderId = 0,	MenuName = "Pre-Defined Rule",      Controller="Hexarule", Action = "HXRPreDefine",                   ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 11%;" } },
new Menu { ToolId = ((int)Tools.HexaRule),	MenuId = 709, ParentMenuId = 0, MenuOrderId = 0,	MenuName = "Create Rule",           Controller="Hexarule", Action = "HXRUserDefine",                  ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 11%;" } },
new Menu { ToolId = ((int)Tools.HexaRule),	MenuId = 710, ParentMenuId = 0, MenuOrderId = 0,	MenuName = "Rule Allocation",       Controller="Hexarule", Action = "RuleAllocation",              ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 11%;" } },
new Menu { ToolId = ((int)Tools.HexaRule),	MenuId = 711, ParentMenuId = 0, MenuOrderId = 0,	MenuName = "Rule Process",   Controller="Hexarule", Action = null,                   ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%;" } },
new Menu { ToolId = ((int)Tools.HexaRule),	MenuId = 712, ParentMenuId = 0, MenuOrderId = 0,	MenuName = "Report",                Controller="DashBoard", Action = "HexaRule",                      ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 12%;" } },
//new Menu { ToolId = ((int)Tools.HexaRule),	MenuId = 28,	MenuName = "Dashboard",             Controller=null, Action = "http://172.25.69.49:8090/views/BIADataQualityDashboard/DataQualityDashboard??:embed=yes&:toolbar=top", ParentMenuId = 0, ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%;" } },
new Menu { ToolId = ((int)Tools.HexaRule),	MenuId = 713, ParentMenuId = 0, MenuOrderId = 0,	MenuName = "Dashboard",             Controller="DashBoard", Action = "HexaRuleDashboard",  ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 14%;" } },
new Menu { ToolId = ((int)Tools.HexaRule),	MenuId = 714, ParentMenuId = 711, MenuOrderId = 111,	MenuName = "Process and Updates",   Controller="Hexarule", Action = "HXRShowRules",                   ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="margin: 1px !important; text-align: left; cursor: pointer; width: 99%;" } },
new Menu { ToolId = ((int)Tools.HexaRule),	MenuId = 715, ParentMenuId = 711, MenuOrderId = 112,	MenuName = "Correction and Re-Process",   Controller="Hexarule", Action = "CorrectionAndReProcess",                   ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="margin: 1px !important; text-align: left; cursor: pointer; width: 99%;" } },

                                                                    

                    
new Menu { ToolId = ((int)Tools.DataProfiler),	MenuId = 1101, ParentMenuId = 0, MenuOrderId = 1,	MenuName = "Configuration",         Controller="DataProfiler", Action = "Configuration",          ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 12%;" } },
new Menu { ToolId = ((int)Tools.DataProfiler),	MenuId = 1102, ParentMenuId = 0, MenuOrderId = 2,	MenuName = "Offline Profile",       Controller="DataProfiler", Action = "ExcelTemplate",          ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 12%;" } },
new Menu { ToolId = ((int)Tools.DataProfiler),	MenuId = 1103, ParentMenuId = 0, MenuOrderId = 3,	MenuName = "Profile",               Controller="DataProfiler", Action = "DataProfiling",          ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 12%;" } },
new Menu { ToolId = ((int)Tools.DataProfiler),	MenuId = 1104, ParentMenuId = 0, MenuOrderId = 4,	MenuName = "Offline Profile",       Controller="DataProfiler", Action = "OfflineProfiling",      ActiveFlag = 0, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 12%;" } },
new Menu { ToolId = ((int)Tools.DataProfiler),	MenuId = 1105, ParentMenuId = 0, MenuOrderId = 5,	MenuName = "Code Rule Management",  Controller="DataProfiler", Action = "CodeRuleManagement",    ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 22%;" } },
new Menu { ToolId = ((int)Tools.DataProfiler),	MenuId = 1106, ParentMenuId = 0, MenuOrderId = 6,	MenuName = "Report",                Controller="DashBoard", Action = "DataProfiler",          ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 12%;" } },
//new Menu { ToolId = ((int)Tools.DataProfiler),	MenuId = 36,	MenuName = "Dashboard",             Controller=null, Action = "http://172.25.69.49:8090/#/views/Profiler_Master_Dashboard/Profiler_Summary?:embed=yes&:toolbar=top", ParentMenuId = 0, ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 12%;" } },
new Menu { ToolId = ((int)Tools.DataProfiler),	MenuId = 1107, ParentMenuId = 0, MenuOrderId = 7,	MenuName = "Dashboard",             Controller="DashBoard", Action = "ProfilerDashboard", ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 12%;" } },
                                                                                                                 

new Menu { ToolId = ((int)Tools.MasterSetup),	MenuId = 1301, ParentMenuId = 0, MenuOrderId = 1,	MenuName = "Configuration",                Controller="Home", Action = "DBDeployment",           ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 12%;" } },
new Menu { ToolId = ((int)Tools.MasterSetup),	MenuId = 1302, ParentMenuId = 0, MenuOrderId = 2,	MenuName = "Process Orchestration", Controller="Home", Action = "ProcessWorkFlow",           ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 12%;" } },
new Menu { ToolId = ((int)Tools.MasterSetup),	MenuId = 1303, ParentMenuId = 0, MenuOrderId = 3,	MenuName = "Project Setup",         Controller="Home", Action = "MasterSetup",           ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 12%;" } },
new Menu { ToolId = ((int)Tools.MasterSetup),	MenuId = 1304, ParentMenuId = 0, MenuOrderId = 4,	MenuName = "Manage Role",           Controller="Home", Action = null,           ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 12%;" } },
new Menu { ToolId = ((int)Tools.MasterSetup),   MenuId = 1305, ParentMenuId = 1304, MenuOrderId = 1,MenuName = "Create Role",           Controller="Home", Action = "ADDRole",           ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 99%; text-align: left;" } },
new Menu { ToolId = ((int)Tools.MasterSetup),   MenuId = 1306, ParentMenuId = 1304, MenuOrderId = 2,MenuName = "Assign Role",           Controller="Home", Action = "ManageRole",           ActiveFlag = 1, HtmlAttribute= new htmlAttr { Selected = false, Style="width: 99%; text-align: left;"  }},


                };

                ToolMenu.Where(r => r.MenuId == SelectedMenuId).ToList().ForEach(r => r.HtmlAttribute.Selected = true);
                ToolMenu = ToolMenu.Where(r => r.ToolId == _ToolId && r.ActiveFlag == 1).OrderBy(r => r.MenuOrderId).ToList();
                //ToolMenu=ToolMenu.OrderBy(r => r.MenuOrderId).ToList();
            }
        }
    }
}