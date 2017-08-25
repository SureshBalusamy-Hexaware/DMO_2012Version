using AutoMapper;
using DM_BusinessEntities;
using DM_DataModel;
using DM_DataModel.UnitOfWork;
using Microsoft.DataDebugger.DataProfiling;
using Microsoft.SqlServer.Dts.Runtime;
using Microsoft.SqlServer.Dts.Tasks.DataProfilingTask;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace DM_BusinessService
{
    public class ProfilerService : IProfiler
    {
        private readonly Profiler _profiler;

        public ProfilerService()
        {
            _profiler = new Profiler();
        }

        public List<ProfilerEntity> GetProfilerTableList(string client_ID, string project_ID, long config_id, ref string status_Code, ref string message)
        {
            var ProfilerTable = _profiler.GetProfilerTableList(client_ID, project_ID, config_id, ref status_Code, ref message);

            if (ProfilerTable != null)
            {
                Mapper.CreateMap<COMMON_GET_TABLE_LIST_SP_Result, ProfilerEntity>();
                var _ProfilerTableModel = Mapper.Map<List<COMMON_GET_TABLE_LIST_SP_Result>, List<ProfilerEntity>>(ProfilerTable);
                return _ProfilerTableModel;
            }
            return null;
        }

        public List<ProfilerEntity> GetProfilerTemplateList(string client_ID, string project_ID, long? config_ID, ref string status_Code, ref string message)
        {
            var ProfilerTemplate = _profiler.GetProfilerTemplateList(client_ID, project_ID, config_ID, ref status_Code, ref message);

            if (ProfilerTemplate != null)
            {
                Mapper.CreateMap<PROFILER_GET_DISTINCT_TEMPLATES_SP_Result, ProfilerEntity>();
                var _ProfilerTableModel = Mapper.Map<List<PROFILER_GET_DISTINCT_TEMPLATES_SP_Result>, List<ProfilerEntity>>(ProfilerTemplate);
                return _ProfilerTableModel;
            }
            return null;
        }

        public List<ProfilerEntity> GetProfilerTableDetails(string client_ID, string project_ID, string table_name, string config_ID, ref string status_Code, ref string message)
        {
            var ProfilerTable = _profiler.GetProfilerTableDetails(client_ID, project_ID, table_name, config_ID, ref status_Code, ref message);

            if (ProfilerTable != null)
            {
                Mapper.CreateMap<PROFILER_GET_TABLE_DETAILS_SP_Result, ProfilerEntity>();
                var _ProfilerTableModel = Mapper.Map<List<PROFILER_GET_TABLE_DETAILS_SP_Result>, List<ProfilerEntity>>(ProfilerTable);
                return _ProfilerTableModel;
            }
            return null;
        }

        public List<ProfilerTemplateEntity> GetProfilerTemplateDetails(string client_ID, string project_ID, string template_name, long config_ID, ref string status_Code, ref string message)
        {
            //var ProfilerTable = _profiler.GetProfilerTableNames(client_ID, project_ID, source_Target, database_Name, ref status_Code, ref message);
            var obj = _profiler.GetProfilerTemplateDetails(client_ID, project_ID, template_name, config_ID, ref status_Code, ref message);

            if (obj != null)
            {
                Mapper.CreateMap<PROFILER_GET_TEMPALTE_DETAILS_SP_Result, ProfilerTemplateEntity>();
                var _ProfilerTableModel = Mapper.Map<List<PROFILER_GET_TEMPALTE_DETAILS_SP_Result>, List<ProfilerTemplateEntity>>(obj);
                return _ProfilerTableModel;
            }
            return null;
        }

        public List<ProfilerEntity> GetProfilerParameterRecordsValue(long tool_ID, ref string status_Code, ref string message)
        {
            //var ProfilerTable = _profiler.GetProfilerTableNames(client_ID, project_ID, source_Target, database_Name, ref status_Code, ref message);
            var obj = _profiler.GetProfilerParameterRecordsValue(tool_ID, ref status_Code, ref message);

            if (obj != null)
            {
                Mapper.CreateMap<PROFILER_GET_PARAMETER_VALUES_SP_Result, ProfilerEntity>();
                var _ProfilerTableModel = Mapper.Map<List<PROFILER_GET_PARAMETER_VALUES_SP_Result>, List<ProfilerEntity>>(obj);
                return _ProfilerTableModel;
            }
            return null;
        }

        public List<ProfileCodeRuleEntity> GetCodeRule(string client_ID, string project_ID, string table_name, string column_name, ref string status_Code, ref string message)
        {
            //var ProfilerTable = _profiler.GetProfilerTableNames(client_ID, project_ID, source_Target, database_Name, ref status_Code, ref message);
            var obj = _profiler.GetProfilerCodeRule(client_ID, project_ID, table_name, column_name, ref status_Code, ref message);

            if (obj != null)
            {
                Mapper.CreateMap<PROF_GET_PROFILER_RULE_SP_Result, ProfileCodeRuleEntity>();
                var _ProfilerCodeRule = Mapper.Map<List<PROF_GET_PROFILER_RULE_SP_Result>, List<ProfileCodeRuleEntity>>(obj);
                return _ProfilerCodeRule;
            }
            return null;
        }

        public List<string> GetTableCodeColumnList(string client_ID, string project_ID, long config_ID, ref string status_Code, ref string message)
        {
            List<string> _codeList = _profiler.GetTableCodeColumnList(client_ID, project_ID, config_ID, ref status_Code, ref message);
            return _codeList;
        }

        public DataTable GetProfilerTableSampleRecords(long? config_ID, string table_name, string column_list, string row_count, ref string status_Code, ref string message, ref long total_count, int pageno = 1, int rows = 1)
        {
            DataTable _dtData = _profiler.GetProfilerTableSampleRecords(config_ID, table_name, column_list, row_count, ref status_Code, ref message, ref total_count, pageno, rows);


            return _dtData;
        }

        public DataTable GetColumnCodeValues(string client_id, string project_id, long? config_ID, string table_name, string column_name, byte? isDistinct, ref  string status_Code, ref string message)
        {
            DataTable dt = _profiler.GetColumnCodeValues(client_id, project_id, config_ID, table_name, column_name, isDistinct, ref status_Code, ref message);

            return dt;
        }

        public DataTable GetBatchProfileStatus(int page, int rows, string client_id, string project_id)
        {
            DataTable dt = _profiler.GetBatchProfileStatus(page, rows, client_id, project_id);

            return dt;
        }

        public long UpdateProfileStatus(string client_ID, string project_ID, long? config_ID, string template_name, string table_name, string profile_type, string profile_desc, string profile_status, long rec_count, string profiled_by, long? profileID, ref string status_Code, ref string message)
        {
            var result = _profiler.UpdateProfileStatus(client_ID, project_ID, config_ID, template_name, table_name, profile_type, profile_desc, DateTime.Now, profile_status, rec_count, profiled_by, profileID, ref status_Code, ref message);

            return result;
        }

        public int InsertProfileTemplate(string client_ID, string project_ID, string template_name, string table_name, string column_name, string data_type, bool null__ratio_profile, bool statistics_Profile, bool value_Distribution_Profile, bool length_Distribution_Profile, bool pattern_Profile, bool candidate_Key_Profile, bool profiling_Status, long? config_ID, string created_by, ref string status_Code, ref string message)
        {
            var result = _profiler.InsertProfileTemplate(client_ID, project_ID, template_name, table_name, column_name, data_type, null__ratio_profile, statistics_Profile, value_Distribution_Profile, length_Distribution_Profile, pattern_Profile, candidate_Key_Profile, profiling_Status, config_ID, created_by, ref status_Code, ref message);

            return result;
        }

        public int InsertProfileData(ProfileData profile_data, string profiled_by, ref string status_Code, ref string message)
        {

            DataTable dt = ProfilerTemplateTable(profile_data, profiled_by);
            _profiler.InsertProfileTemplate(dt, ref status_Code, ref message);
            return 0;
        }

        public int CreateCodeRule(string client_ID, string project_ID, long? config_ID, string table_name, string column_name, string code_value, string created_by, ref string status_Code, ref string message)
        {
            var result = _profiler.CreateCodeRule(client_ID, project_ID, config_ID, table_name, column_name, code_value, created_by, ref status_Code, ref message);

            return result;
        }

        public string UpdateProfileXML(string tableName, string columnName, string[] removeComputeOptions, string templateName,
            string database_name, string server_ip, string srcUserName, string srcPwd, string client_id, string project_id, long tool_id,
            int row_count, string[] allColumns, long profileID, long? RoleId)
        {
            #region commented
            //XmlDocument profileInputDoc = new XmlDocument();

            //profileInputDoc.Load(xmlFile);

            //profileInputDoc = RemoveXmlns(profileInputDoc);

            //XmlNode removeNode = profileInputDoc.SelectSingleNode("/DataProfile/DataProfileInput/Requests");

            //foreach (string optionNode in removeComputeOptions)
            //{
            //    removeNode.RemoveChild(removeNode.SelectSingleNode(optionNode));
            //}

            //XmlNodeList nodeList = profileInputDoc.SelectNodes("/DataProfile/DataProfileInput/Requests");


            //foreach (XmlNode childNode in nodeList[0].ChildNodes)
            //{
            //    childNode.SelectSingleNode("Table").Attributes["Table"].Value = tableName;

            //    if (columnName == "ALL")
            //    {
            //        childNode.SelectSingleNode("Column").Attributes["IsWildCard"].Value = "true";

            //        if (childNode.SelectSingleNode("Column").Attributes["ColumnName"] != null)
            //            childNode.SelectSingleNode("Column").Attributes.Remove(childNode.SelectSingleNode("Column").Attributes["ColumnName"]);
            //    }
            //    else
            //    {
            //        childNode.SelectSingleNode("Column").Attributes["IsWildCard"].Value = "false";

            //        if (childNode.SelectSingleNode("Column").Attributes["ColumnName"] != null)
            //        {
            //            childNode.SelectSingleNode("Column").Attributes["ColumnName"].Value = columnName;
            //        }
            //        else
            //        {
            //            XmlAttribute attr = profileInputDoc.CreateAttribute("ColumnName");
            //            attr.Value = columnName;
            //            childNode.SelectSingleNode("Column").Attributes.SetNamedItem(attr);
            //        }
            //    }

            //}

            //XmlElement elm = profileInputDoc.DocumentElement;

            //XmlDeclaration dec = profileInputDoc.CreateXmlDeclaration("1.0", "utf-16", "");
            //profileInputDoc.InsertBefore(dec, elm);

            //profileInputDoc.Save(@"D:\Sample\ExecuteSSISPackage\SSIS package\profileInputModified.xml");


            //XmlDocument packageDoc = new XmlDocument();
            //packageDoc.Load(@"D:\Sample\ExecuteSSISPackage\SSIS package\DummyPackage.dtsx");

            //var nsmgr = new XmlNamespaceManager(packageDoc.NameTable);
            //nsmgr.AddNamespace("DTS", "www.microsoft.com/SqlServer/Dts");

            //XmlNode profileInputNode = packageDoc.SelectSingleNode("/DTS:Executable/DTS:Executable/DTS:ObjectData/DataProfilingTaskData/ProfileInput", nsmgr);
            ////profileInputNode.InnerXml = "<![CDATA[" + profileInputDoc.OuterXml + "]]>";
            //profileInputNode.InnerXml = profileInputNode.InnerXml.Replace("Table=\"AB_SOURCE\"", "Table=\"" + tableName + "\"");


            //packageDoc.Save(@"D:\Sample\ExecuteSSISPackage\SSIS package\DummyPackage - auto.dtsx");

            #endregion

            try
            {
                string viewname = _profiler.GetDynamicViewName(server_ip, database_name, tableName, string.Join(",", allColumns), row_count, RoleId);
                DataTable dt = _profiler.GetConfigDetails(client_id, project_id, "TARGET", tool_id, RoleId);
                string tgt_ip = "", tgt_db = "", tgt_db_user = "", tgt_db_pwd = "";

                if (dt.Rows.Count > 0)
                {
                    tgt_ip = dt.Rows[0]["Server_IP_Address"].ToString();
                    tgt_db = dt.Rows[0]["Schema_Name"].ToString();
                    tgt_db_user = dt.Rows[0]["Database_Name"].ToString();
                    tgt_db_pwd = dt.Rows[0]["Database_Password"].ToString();
                }

                string result = Wrapper.ExecutePackage(tableName, columnName, templateName, viewname, database_name, server_ip, srcUserName, srcPwd, tgt_ip, tgt_db, tgt_db_user, tgt_db_pwd, allColumns, removeComputeOptions, profileID, _profiler);

                if (!string.IsNullOrEmpty(viewname))
                    _profiler.DropView(server_ip, database_name, viewname);

                return result;
            }
            catch (Exception _e)
            {
                throw _e;
            }

        }

        public string OfflineProfiling(string client_id, string project_id, long tool_id, string templateName, string profileOutputXMLFile, long profileID)
        {
            DataTable dt = _profiler.GetConfigDetails(client_id, project_id, "TARGET", tool_id,1);
            string tgt_ip = "", tgt_db = "", tgt_db_user = "", tgt_db_pwd = "";

            if (dt.Rows.Count > 0)
            {
                tgt_ip = dt.Rows[0]["Server_IP_Address"].ToString();
                tgt_db = dt.Rows[0]["Schema_Name"].ToString();
                tgt_db_user = dt.Rows[0]["Database_Name"].ToString();
                tgt_db_pwd = dt.Rows[0]["Database_Password"].ToString();
            }

            #region get table name from ouput XML

            string tableName = "";
            string StatusCode = string.Empty, Message = string.Empty;
            string profileOutputXml = "";
            try
            {
                XmlDocument profileInputDoc = new XmlDocument();

                profileInputDoc.Load(profileOutputXMLFile);
                profileOutputXml = profileInputDoc.OuterXml;

                profileInputDoc = RemoveXmlns(profileInputDoc);

                XmlNodeList nodeList = profileInputDoc.SelectNodes("/DataProfile/DataProfileInput/Requests");


                foreach (XmlNode childNode in nodeList[0].ChildNodes)
                {
                    tableName = childNode.SelectSingleNode("Table").Attributes["Table"].Value;
                    if (!String.IsNullOrEmpty(tableName))
                    {
                        if (tableName.StartsWith("VW_"))
                        {
                            tableName = tableName.Replace("VW_", "");
                            tableName = tableName.Substring(0, tableName.Length - 15);
                        }
                        break;
                    }
                }
            }
            catch (System.IO.FileNotFoundException ex)
            {
                return "Could not find Profile Output XML file.";
            }
            #endregion


            string flowResult = Wrapper.ExecuteFlowTask(templateName, tableName, profileOutputXml, tgt_db, tgt_ip, tgt_db_user, tgt_db_pwd, profileID, _profiler);
            return flowResult;
        }

        private DataTable ProfilerTemplateTable(ProfileData _profileData, string _profiledBy)
        {
            DataTable dt = new DataTable();
            //dt.Columns.Add("Client_ID", Type.GetType("System.String"));
            //dt.Columns.Add("Project_ID", Type.GetType("System.String"));

            dt.Columns.Add("Profile_ID", typeof(long));
            dt.Columns.Add("Column_Name", Type.GetType("System.String"));
            dt.Columns.Add("Data_Type", Type.GetType("System.String"));
            dt.Columns.Add("Null_Ratio_Profile", typeof(byte));
            dt.Columns.Add("Statistics_Profile", typeof(byte));
            dt.Columns.Add("Value_Distribution_Profile", typeof(byte));
            dt.Columns.Add("Length_Distribution_Profile", typeof(byte));
            dt.Columns.Add("Pattern_Profile", typeof(byte));
            dt.Columns.Add("Candidate_Key_Profile", typeof(byte));
            dt.Columns.Add("Profiling_Status", typeof(byte));
            dt.Columns.Add("Create_Date", typeof(DateTime));
            dt.Columns.Add("Created_By", Type.GetType("System.String"));
            dt.Columns.Add("Modified_Date", typeof(DateTime));
            dt.Columns.Add("Modified_By", Type.GetType("System.String"));

            for (int i = 0; i < _profileData.ColumnList.Count; i++)
            {
                DataRow dr = dt.NewRow();
                //dr["Client_ID"] = _profileData.Client_ID;
                //dr["Project_ID"] = _profileData.Project_ID;

                dr["Profile_ID"] = _profileData.Profile_ID;
                dr["Column_Name"] = _profileData.ColumnList[i].Column_Name;
                dr["Data_Type"] = _profileData.ColumnList[i].Data_Type;
                dr["Null_Ratio_Profile"] = _profileData.Null_Ratio_Profile;
                dr["Statistics_Profile"] = _profileData.Statistics_Profile;
                dr["Value_Distribution_Profile"] = _profileData.Value_Distribution_Profile;
                dr["Length_Distribution_Profile"] = _profileData.Length_Distribution_Profile;
                dr["Pattern_Profile"] = _profileData.Pattern_Profile;
                dr["Candidate_Key_Profile"] = _profileData.Candidate_Key_Profile;
                dr["Profiling_Status"] = _profileData.Profiling_Status;
                dr["Create_Date"] = DateTime.Now;
                dr["Created_By"] = _profiledBy;
                dr["Modified_Date"] = DBNull.Value;
                dr["Modified_By"] = string.Empty;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        static XmlDocument RemoveXmlns(XmlDocument doc)
        {
            XDocument d;
            using (var nodeReader = new XmlNodeReader(doc))
                d = XDocument.Load(nodeReader);

            d.Root.Descendants().Attributes().Where(x => x.IsNamespaceDeclaration).Remove();

            foreach (var elem in d.Descendants())
                elem.Name = elem.Name.LocalName;

            var xmlDocument = new XmlDocument();
            using (var xmlReader = d.CreateReader())
                xmlDocument.Load(xmlReader);

            return xmlDocument;
        }
        
    }

    class Wrapper
    {
        public static string ExecutePackage(string tableName, string columnName, string templateName, string view_name, string database_name, string server_ip, string srcUserName, string srcPwd, string tgt_ip, string tgt_db, string tgtUserName, string tgtPwd, string[] selected_columns, string[] profileOptions, long profileID, Profiler _profiler)
        {
            try
            {
                // initialize SSIS application runtime
                //var app = new Microsoft.SqlServer.Dts.Runtime.Application();

                // load the package
                //var pkg = app.LoadPackage(pkgFile, null);

                Package pkg = new Package();

                pkg.Executables.Add(typeof(Microsoft.SqlServer.Dts.Tasks.DataProfilingTask.DataProfilingTask).AssemblyQualifiedName);

                // the package contains only one task. loading it.
                var task = pkg.Executables[0] as TaskHost;
                var dpt = task.InnerObject as DataProfilingTask;

                //Creating ADO.NET Connection to query table and profile table data
                ConnectionManager ConMgr;
                ConMgr = pkg.Connections.Add("ADO.NET");
                ConMgr.ConnectionString = "data source=" + server_ip + ";User id=" + srcUserName + "; pwd=" + srcPwd + ";Initial Catalog=" + database_name + ";";
                ConMgr.Name = "SSIS Connection Manager for ADO.NET";
                ConMgr.Description = "ADO NET connection to the Profile Task.";

                //Creating variable to assign profile output data
                pkg.Variables.Add("OutputString", false, "User", "");
                dpt.DestinationType = DataProfileDestinationType.Variable;
                dpt.Destination = "User::OutputString";

                //Clear all profile request before processing
                dpt.ProfileRequests.Clear();


                //// get connections
                //var connectionDB = pkg.Connections[0];
                ////var connectionDB1 = pkg.Connections[1];

                //connectionDB.ConnectionString = "data source=" + server_ip + ";User id=Sa; pwd=Password123;Initial Catalog=" + database_name + ";";
                ////connectionDB1.ConnectionString = "data source=" + server_ip + ";User id=Sa; pwd=Password123;Initial Catalog=" + database_name + ";";
                ////connectionFile.ConnectionString = PackageOutputPath;

                //if (!System.IO.File.Exists(pkgOutputXMLFile))
                //{
                //    string path = new System.IO.FileInfo(pkgOutputXMLFile).DirectoryName;
                //    if (!System.IO.Directory.Exists(path))
                //    {
                //        System.IO.Directory.CreateDirectory(path);
                //    }
                //}                


                // output file location. we'll create a temporary file.
                //pkg.Variables["FileDestination"].Value = pkgOutputXMLFile;
                //pkg.Variables["Column_Name"].Value = columnName;

                var connectionDB = pkg.Connections[0];
                string table_view = view_name.TrimStart('[').TrimEnd(']');


                if (!profileOptions.Contains("ColumnNullRatioProfileRequest"))
                {
                    // create new Column null ratio query
                    var colNullRatio = new Microsoft.DataDebugger.DataProfiling.ColumnNullRatioProfileRequest();

                    colNullRatio.RequestID = Guid.NewGuid().ToString();
                    colNullRatio.DataSourceID = connectionDB.ID;
                    colNullRatio.Table = new Microsoft.DataDebugger.DataProfiling.TableQName(table_view);
                    //if (columnName == "ALL")
                    colNullRatio.Column = Microsoft.DataDebugger.DataProfiling.ColumnParameter.WildCard;
                    //else
                    //    colNullRatio.Column = new Microsoft.DataDebugger.DataProfiling.ColumnParameter(columnName);
                    dpt.ProfileRequests.Add(colNullRatio);

                }

                if (!profileOptions.Contains("ColumnStatisticsProfileRequest"))
                {
                    // create new Column statistics query
                    var colStatistics = new Microsoft.DataDebugger.DataProfiling.ColumnStatisticsProfileRequest();

                    colStatistics.RequestID = Guid.NewGuid().ToString();
                    colStatistics.DataSourceID = connectionDB.ID;
                    colStatistics.Table = new Microsoft.DataDebugger.DataProfiling.TableQName(table_view);
                    //if (columnName == "ALL")
                    colStatistics.Column = Microsoft.DataDebugger.DataProfiling.ColumnParameter.WildCard;
                    //else
                    //    colStatistics.Column = new Microsoft.DataDebugger.DataProfiling.ColumnParameter(columnName);
                    dpt.ProfileRequests.Add(colStatistics);
                }

                if (!profileOptions.Contains("ColumnValueDistributionProfileRequest"))
                {
                    // create new Column value distribution query
                    var colValDistibution = new Microsoft.DataDebugger.DataProfiling.ColumnValueDistributionProfileRequest();

                    colValDistibution.RequestID = Guid.NewGuid().ToString();
                    colValDistibution.DataSourceID = connectionDB.ID;
                    colValDistibution.Table = new Microsoft.DataDebugger.DataProfiling.TableQName(table_view);
                    //if (columnName == "ALL")
                    colValDistibution.Column = Microsoft.DataDebugger.DataProfiling.ColumnParameter.WildCard;
                    //else
                    //    colValDistibution.Column = new Microsoft.DataDebugger.DataProfiling.ColumnParameter(columnName);
                    //colValDistibution.Option = ValueDistributionOptions.FrequentValues;
                    colValDistibution.Option = ValueDistributionOptions.AllValues;

                    colValDistibution.FrequentValueThreshold = 0.001;
                    dpt.ProfileRequests.Add(colValDistibution);

                }
                if (!profileOptions.Contains("ColumnPatternProfileRequest"))
                {
                    // create new Column pattern profile query
                    var colPattern = new Microsoft.DataDebugger.DataProfiling.ColumnPatternProfileRequest();

                    colPattern.RequestID = Guid.NewGuid().ToString();
                    colPattern.DataSourceID = connectionDB.ID;
                    colPattern.Table = new Microsoft.DataDebugger.DataProfiling.TableQName(table_view);
                    //if (columnName == "ALL")
                    colPattern.Column = Microsoft.DataDebugger.DataProfiling.ColumnParameter.WildCard;
                    //else
                    //    colPattern.Column = new Microsoft.DataDebugger.DataProfiling.ColumnParameter(columnName);
                    colPattern.MaxNumberOfPatterns = 10;
                    colPattern.PercentageDataCoverageDesired = 95;
                    colPattern.CaseSensitive = false;
                    //colPattern.Delimiters = "\t";
                    //colPattern.Symbols = ",.;:-\"'`~=&amp;/\\@!?()&lt;&gt;[]{}|#*^%";
                    dpt.ProfileRequests.Add(colPattern);
                }

                if (!profileOptions.Contains("ColumnLengthDistributionProfileRequest"))
                {
                    // create new Column length distribution query
                    var collendistribution = new Microsoft.DataDebugger.DataProfiling.ColumnLengthDistributionProfileRequest();

                    // set the id for request
                    collendistribution.RequestID = Guid.NewGuid().ToString();
                    // set the database id
                    collendistribution.DataSourceID = connectionDB.ID;
                    // select table
                    collendistribution.Table = new Microsoft.DataDebugger.DataProfiling.TableQName(table_view);
                    // add column(s)
                    //if (columnName == "ALL")
                    collendistribution.Column = Microsoft.DataDebugger.DataProfiling.ColumnParameter.WildCard;
                    //else
                    //    collendistribution.Column = new Microsoft.DataDebugger.DataProfiling.ColumnParameter(columnName);
                    // or use a single column with 
                    // keyprofileRequest.KeyColumns.Add(new Microsoft.DataDebugger.DataProfiling.ColumnParameter("CurrencyAlternateKey"));

                    // set profiling attributes
                    collendistribution.IgnoreLeadingSpace = true;
                    collendistribution.IgnoreTrailingSpace = true;

                    // add the request to request collection so it will be executed.
                    dpt.ProfileRequests.Add(collendistribution);
                }
                if (!profileOptions.Contains("CandidateKeyProfileRequest"))
                {
                    // create new Candidate Key Profile query
                    var keyprofileRequest = new Microsoft.DataDebugger.DataProfiling.CandidateKeyProfileRequest();
                    // set the id for request
                    keyprofileRequest.RequestID = Guid.NewGuid().ToString();
                    // set the database id
                    keyprofileRequest.DataSourceID = connectionDB.ID;
                    // select table
                    keyprofileRequest.Table = new Microsoft.DataDebugger.DataProfiling.TableQName(table_view);
                    // add column(s)

                    //if (columnName == "ALL")
                    keyprofileRequest.KeyColumns.Add(Microsoft.DataDebugger.DataProfiling.ColumnParameter.WildCard);
                    //else
                    //{
                    //    foreach (string col in selected_columns)
                    //    {
                    //        keyprofileRequest.KeyColumns.Add(new Microsoft.DataDebugger.DataProfiling.ColumnParameter(col));
                    //    }
                    //}



                    // set profiling attributes
                    keyprofileRequest.KeyStrengthThreshold = 0.95;
                    keyprofileRequest.MaxNumberOfViolations = 100;
                    keyprofileRequest.ThresholdSetting = Microsoft.DataDebugger.DataProfiling.StrengthThresholdSetting.Specified;

                    // add the request to request collection so it will be executed.
                    dpt.ProfileRequests.Add(keyprofileRequest);
                }

                if (!profileOptions.Contains("fun"))
                {
                    // create new Functional Dependency Profile query
                    //var funcDependencyProfileRequest = new Microsoft.DataDebugger.DataProfiling.FunctionalDependencyProfileRequest();
                    //funcDependencyProfileRequest.RequestID = Guid.NewGuid().ToString();
                    //funcDependencyProfileRequest.DataSourceID = connectionDB.ID;
                    //funcDependencyProfileRequest.Table = new Microsoft.DataDebugger.DataProfiling.TableQName(table_view);
                    //funcDependencyProfileRequest.DependentColumn = Microsoft.DataDebugger.DataProfiling.ColumnParameter.WildCard;
                    //funcDependencyProfileRequest.DeterminantColumns.Add(Microsoft.DataDebugger.DataProfiling.ColumnParameter.WildCard);
                    //funcDependencyProfileRequest.ThresholdSetting = StrengthThresholdSetting.Specified;
                    //funcDependencyProfileRequest.FDStrengthThreshold = 0.95;
                    //funcDependencyProfileRequest.MaxNumberOfViolations = 100;
                }
                //        break;

                //}


                // execute package
                //System.IO.File.Create("c:\\start_" + profileID + "_" + DateTime.Now.ToString("ddMMyyyy_HH_mm_ss_ms") + ".txt");

                var result = pkg.Execute();

                if (result == DTSExecResult.Success)
                {
                    string outputString = pkg.Variables["OutputString"].Value.ToString();
                    //XmlDocument doc = new XmlDocument();

                    // //load the xml out result and save to the file location
                    //doc.LoadXml(outputString);
                    //doc.Save(ConfigurationSettings.AppSettings["XMLFileLocation"]);
                    int length = outputString.IndexOf("<ProfileVersion>");
                    string replaceString = outputString.Substring(0, length); //ConfigurationSettings.AppSettings["replaceString"]; // "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<DataProfile xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"http://schemas.microsoft.com/sqlserver/2008/DataDebugger/\">";                    
                    string resultString = outputString.Replace(replaceString, "<DataProfile>");
                    string test = resultString;
                    //Execute Data flow task
                    string status_Code = string.Empty;
                    string message = string.Empty;
                    _profiler.ExecuteFlowTask(profileID, resultString, ref status_Code, ref message);

                    return message;

                    //string flowResult = ExecuteFlowTask(templateName, tableName, outputString, tgt_db, tgt_ip, tgtUserName, tgtPwd, profileID, _profiler);
                    ////System.IO.File.Create("c:\\stop_" + profileID + "_" + DateTime.Now.ToString("ddMMyyyy_HH_mm_ss_ms") + ".txt");                 
                    //return string.Format("{0}", flowResult);

                }
                else
                {

                    StringBuilder sb = new StringBuilder();
                    foreach (var error in pkg.Errors)
                    {
                        sb.AppendFormat("{0}: {1}\n", error.ErrorCode, error.Description);
                    }
                    return string.Format("Error: {0}", sb.ToString());
                }
            }
            catch (Exception ex)
            {
                return string.Format("Error: {0}", ex.Message);
            }
        }

        public static string ExecuteFlowTask(string templateName, string tableName, string profileOutputXML, string database_name, string server_ip, string tgtUserName, string tgtPwd, long profileID, Profiler _profiler)
        {

            string tgtConnection = "Data Source=" + server_ip + ";User ID=" + tgtUserName + ";pwd=" + tgtPwd + ";Initial Catalog=" + database_name + ";";
            return _profiler.LoadProfileOutputDatatToTable(profileOutputXML, profileID, templateName, tableName, tgtConnection);

            #region old code
            /*
            try
            {
                var app = new Microsoft.SqlServer.Dts.Runtime.Application();

                // load the package
                var pkg = app.LoadPackage(dataFlowPkgFile, null);

                pkg.Variables["User::XMLFilePath"].Value = profileOutputXMLFile;
                pkg.Variables["User::Template_Name"].Value = templateName;
                pkg.Variables["User::Table_Name"].Value = tableName;
                pkg.Variables["User::Column_Name"].Value = columnName;
                //enforce the Package not to validate the Task and objects.
                pkg.DelayValidation = true;

                // get connections
                var connectionDB = pkg.Connections[0];

                connectionDB.ConnectionString = "Data Source=" + server_ip + ";User ID=sa;pwd=Password123;Initial Catalog=" + database_name + ";Provider=SQLNCLI11;Persist Security Info=True;Application Name=SSIS-Variable-{C1C4FB5C-9AC5-4B94-ADDE-43A33DE1D8AD}172.25.121.105.DM_MetaData.sa;Auto Translate=False;";

                var result = pkg.Execute();

                if (result == DTSExecResult.Success)
                {
                    // return the result
                    return string.Format("Success: {0}", "Package executed Successfully.");
                }
                else
                {

                    StringBuilder sb = new StringBuilder();
                    foreach (var error in pkg.Errors)
                    {
                        sb.AppendFormat("{0}: {1}\n", error.ErrorCode, error.Description);
                    }
                    return string.Format("Error: {0}", sb.ToString());
                }
            }

            catch (Exception ex)
            {
                return string.Format("Error: {0}", ex.Message);
            }
            */
            #endregion
        }
    }
}
