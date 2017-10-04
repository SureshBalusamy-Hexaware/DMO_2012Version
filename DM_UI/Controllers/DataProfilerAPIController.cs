using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DM_BusinessEntities;
using DM_BusinessService;
using DM_UI.App_Start;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
//using SAP.Middleware.Connector;
//using SPEED.SAPConnection;
//using SAPIntegration;

namespace DM_UI.Controllers
{
    public class DataProfilerAPIController : ApiController
    {
        private readonly IProfiler _profiler;
        //RfcDestination rfcDest = null;

        public DataProfilerAPIController()
        {
            
            _profiler = new ProfilerService();
            //SAPSystemConnect sapCfg = new SAPSystemConnect();

            //if (RfcDestinationManager.DestinationMonitors.Count == 0)
            //{
            //    RfcDestinationManager.RegisterDestinationConfiguration(sapCfg);
            //}
            //rfcDest = RfcDestinationManager.GetDestination("mySAPdestination");
        }

        [HttpGet] // api/DataProfilerAPI/GetProfilerTableNames
        public List<ProfilerEntity> GetProfilerTableNames(string client_ID, string project_ID, long config_Id)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            //return _profiler.GetProfilerTableNames(client_ID, project_ID, source_Target, database_Name, ref StatusCode, ref Message);
            return _profiler.GetProfilerTableList(client_ID, project_ID, config_Id, ref StatusCode, ref Message);
        }

        [HttpGet] // api/DataProfilerAPI/GetProfilerTemplateList
        public List<ProfilerEntity> GetProfilerTemplateList(string client_ID, string project_ID, long config_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            //return _profiler.GetProfilerTableNames(client_ID, project_ID, source_Target, database_Name, ref StatusCode, ref Message);
            return _profiler.GetProfilerTemplateList(client_ID, project_ID, config_ID, ref StatusCode, ref Message);
        }

        [HttpGet] // api/DataProfilerAPI/GetProfilerTableDetails
        public dynamic GetProfilerTableDetails(string client_ID, string project_ID, string table_name, string Config_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            var profilerTableDetails = _profiler.GetProfilerTableDetails(client_ID, project_ID, table_name, Config_ID, ref StatusCode, ref Message) as IEnumerable<ProfilerEntity>;


            var rows = new
            {
                total = 0,
                page = 0,
                records = 0,
                rows = (
                    from column in profilerTableDetails
                    select new
                    {
                        cell = new string[] {
                         column.Column_Name,
                         column.data_type,
                         column.Key_column
                      }
                    }).ToArray()
            };
            return rows;
        }

        [HttpGet]
        public string UpdateProfileStatus(string client_ID, string project_ID, long config_ID, string profile_type, string template_name, string table_name, long rec_count)
        {
            long _profileID = 0L;
            string StatusCode = string.Empty, Message = string.Empty;

            _profileID = _profiler.UpdateProfileStatus(client_ID, project_ID, config_ID, template_name, table_name, profile_type, string.Empty, "I", rec_count, UIProperties.Sessions.UserName, null, ref StatusCode, ref Message);

            if (_profileID <= 0 || StatusCode != "0")
                return Message;
            else
                return Convert.ToString(_profileID);
        }

        /*
        [HttpGet] //api/DataProfilerAPI/InsertProfileTemplate
        public string InsertProfileTemplate(string client_ID, string project_ID, string template_name, string table_name, string column_name,
            string data_type, bool null__ratio_profile, bool statistics_Profile, bool value_Distribution_Profile, bool length_Distribution_Profile, bool pattern_Profile, bool candidate_Key_Profile, bool profiling_Status, long config_ID, string created_by)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            _profiler.InsertProfileTemplate(client_ID, project_ID, template_name, table_name, column_name, data_type, null__ratio_profile, statistics_Profile, value_Distribution_Profile, length_Distribution_Profile, pattern_Profile, candidate_Key_Profile, profiling_Status, config_ID, created_by, ref StatusCode, ref Message);

            return Message;

            //return "Success:";
        }
        */

        [HttpPost]
        public string InsertProfileData(ProfileData profile_data)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            _profiler.InsertProfileData(profile_data, UIProperties.Sessions.UserName, ref StatusCode, ref Message);
            return "success";
        }

        [HttpGet] //api/DataProfilerAPI/DataProfiling
        public string DataProfiling(string client_ID, string project_ID, string template_name, string table_name, string profile_colum_name, [FromUri]string[] compute_options, string database_IP, string database_name, int row_count, string selected_columns, long profile_id)
        {
            try
            {
                string StatusCode = string.Empty, Message = string.Empty, profileStatus = string.Empty, profileDesc = string.Empty;
                string result = _profiler.UpdateProfileXML(table_name, profile_colum_name,
                    compute_options, template_name, database_name, database_IP, UIProperties.Sessions.ConfigEntity.DataBaseName,
                    UIProperties.Sessions.ConfigEntity.DataBasePassword, client_ID, project_ID, (int)UIProperties.Tools.DataProfiler,
                    row_count, selected_columns.Split(','), profile_id, UIProperties.Sessions.Client.Role_ID);

                if (result.Contains("Success"))
                {
                    profileStatus = "C";
                    profileDesc = "Completed";
                }
                else
                {
                    profileStatus = "E";
                    profileDesc = result;
                }

                _profiler.UpdateProfileStatus(client_ID, project_ID, Convert.ToInt64(UIProperties.Sessions.ConfigEntity.Config_ID), string.Empty, string.Empty, string.Empty, profileDesc, profileStatus, 0, UIProperties.Sessions.UserName, profile_id, ref StatusCode, ref Message);

                return result;

                //return "Success:";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        /*
        [HttpGet] //api/DataProfilerAPI/OfflineProfiling
        public string OfflineProfiling(string client_ID, string project_ID, string template_name, long profileID)
        {
            try
            {
                string StatusCode = string.Empty, Message = string.Empty, profileStatus = string.Empty, profileDesc = string.Empty;
                string result = _profiler.OfflineProfiling(client_ID, project_ID, (int)UIProperties.Tools.DataProfiler, template_name, System.Configuration.ConfigurationManager.AppSettings["PackageOutputFilePath"].ToString(), profileID);

                if (result.Contains("Success"))
                {
                    profileStatus = "C";
                    profileDesc = "Completed";
                }
                else
                {
                    profileStatus = "E";
                    profileDesc = result;
                }

                _profiler.UpdateProfileStatus(client_ID, project_ID, Convert.ToInt64(UIProperties.Sessions.ConfigEntity.Config_ID), string.Empty, string.Empty, string.Empty, profileDesc, profileStatus, 0, UIProperties.Sessions.UserName, profileID, ref StatusCode, ref Message);

                return result;
                
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        */

        [HttpGet] //api/DataProfilerAPI/GetProfilerParameterRecordsValue
        public List<ProfilerEntity> GetProfilerParameterRecordsValue()
        {
            string StatusCode = string.Empty, Message = string.Empty;

            return _profiler.GetProfilerParameterRecordsValue((long)UIProperties.Tools.DataProfiler, ref StatusCode, ref Message);
        }

        [HttpGet] //api/DataProfilerAPI/GetProfilerTemplateDetails
        public List<ProfilerTemplateEntity> GetProfilerTemplateDetails(string client_ID, string project_ID, string template_name, long config_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            return _profiler.GetProfilerTemplateDetails(client_ID, project_ID, template_name, config_ID, ref StatusCode, ref Message);
        }

        [HttpGet] //api/DataProfilerAPI/GetProfilerSampleData
        public object GetProfilerSampleDataColHeader(long config_ID, string table_name, string column_list, string row_count)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            long totalcount = 0;
            column_list = column_list.TrimEnd(',');

            DataTable dtRecord = _profiler.GetProfilerTableSampleRecords(config_ID, table_name, column_list, row_count, ref StatusCode, ref Message, ref totalcount);
            if (dtRecord == null) return Message;
            string[] Columns = dtRecord.Columns.Cast<DataColumn>().Select(r => r.ColumnName).ToArray();
            string _Columns = string.Join(",", Columns);

            //List<Dictionary<string, object>> rows1 = new List<Dictionary<string, object>>();
            //Dictionary<string, object> row;
            //foreach (DataRow dr in dtRecord.Rows)
            //{
            //    row = new Dictionary<string, object>();
            //    foreach (DataColumn col in dtRecord.Columns)
            //    {
            //        row.Add(col.ColumnName, dr[col]);
            //    }
            //    rows1.Add(row);
            //}
            //string json = JsonConvert.SerializeObject(dtRecord, new DataTableConverter());

            var data = new
            {
                ColNames = _Columns
                //rows1 = json
            };
            return data;
        }

        //[HttpGet]
        //public string SapIntegration(string column_list, string functionname)
        //{

        //    string value = "";
        //    string[] Splitrecord = column_list.Split('+');

        //    for (int i = 0; i < Splitrecord.Length - 1; i++)
        //    {
        //        string[] column_data = Splitrecord[i].Split(',');
        //        Customers customer = new Customers();
        //        if (functionname == "Create BussinessPartner")
        //        {
        //            value = value + "Business Partner Created" + customer.GetCustomerDetails(rfcDest, column_data) + ":";
        //        }
        //        if (functionname == "Create Contract")
        //        {
        //            value = "Contract Created " + customer.GetContract(rfcDest, column_data) + ":";
        //        }
        //    }
        //    return value;
        //}


        [HttpGet] //api/DataProfilerAPI/GetProfilerSampleData
        public object GetProfilerSampleData(int page, int rows, long config_ID, string table_name, string column_list, string row_count)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            long totalcount = 0;
            column_list = column_list.TrimEnd(',');

            DataTable dtRecord = _profiler.GetProfilerTableSampleRecords(config_ID, table_name, column_list, row_count, ref StatusCode, ref Message, ref totalcount, page, rows);

            string[] Columns = dtRecord.Columns.Cast<DataColumn>().Select(r => r.ColumnName).ToArray();
            string _Columns = string.Join(",", Columns);

            int _ColumnCount = dtRecord.Columns.Count;

            var jstr = new _JSON();
            jstr.total = Math.Ceiling(Convert.ToDouble(totalcount) / rows).ToString();
            jstr.page = page.ToString();
            jstr.records = totalcount.ToString();
            jstr.rows = new List<DM_BusinessEntities.rows>();

            int _rowIndex = 1;
            dtRecord.Rows.Cast<DataRow>().ToList().ForEach(datarow =>
            {
                string[] _r = new string[_ColumnCount];
                int _colIndex = 0;
                rows r = new DM_BusinessEntities.rows();

                dtRecord.Columns.Cast<DataColumn>().ToList().ForEach(column =>
                {
                    _r[_colIndex] = datarow[column].ToString();
                    _colIndex++;
                });
                r.id = _rowIndex.ToString();
                //r.cell = new List<string[]>();                   

                r.cell = _r;

                jstr.rows.Add(r);
                _rowIndex++;
            });

            return jstr;

            /* old method
            List<Dictionary<string, object>> rows1 = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dtRecord.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dtRecord.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows1.Add(row);
            }
            string json = JsonConvert.SerializeObject(dtRecord, new DataTableConverter());

            var data = new
            {
                ColNames = _Columns,
                rows1 = json
            };
            return data;
             
            */
        }

        [HttpGet] //api/DataProfilerAPI/GetTableCodeColumnList
        public object GetTableCodeColumnList(string client_ID, string project_ID, long config_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            List<string> _codeList = _profiler.GetTableCodeColumnList(client_ID, project_ID, config_ID, ref StatusCode, ref Message);
            return _codeList;
        }

        [HttpGet] //api/DataProfilerAPI/GetColumnCodeValue
        public dynamic GetColumnCodeValue(string client_ID, string project_ID, long config_ID, string table_name, string column_name)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            DataTable dt = _profiler.GetColumnCodeValues(client_ID, project_ID, config_ID, table_name, column_name, 1, ref StatusCode, ref Message);

            string json = JsonConvert.SerializeObject(dt, new DataTableConverter());

            if (dt != null)
            {
                var rows = new
                {
                    rows = (
                    from DataRow dr in dt.Rows
                    where !string.IsNullOrEmpty(Convert.ToString(dr[0]))
                    select new
                    {
                        cell = new string[] {
                       Convert.ToString(dr[0]),
                       Convert.ToString(dr[1])
                   }

                    }).ToArray()
                };

                return rows;
            }
            return null;
        }

        [HttpGet]
        public dynamic GetProfileBatchStatus(int page, int rows, string client_ID, string project_ID)
        {
            DataTable dt = _profiler.GetBatchProfileStatus(page, rows, client_ID, project_ID);

            string json = JsonConvert.SerializeObject(dt, new DataTableConverter());
            int TotalRecords = Convert.ToInt32(dt.Rows[0]["Total_Count"].ToString());

            var _rows = new
            {
                total = Math.Ceiling(Convert.ToDouble(TotalRecords) / rows).ToString(),
                page = page.ToString(),
                records = TotalRecords.ToString(),

                //total = Math.Ceiling(Convert.ToDouble(TotalRecords) / 10).ToString(),
                //page = "1",
                //records = TotalRecords.ToString(),

                rows = (
                from DataRow dr in dt.Rows
                select new
                {
                    cell = new string[] {
                        Convert.ToString(dr[0]),
                        Convert.ToString(dr[1]),
                        Convert.ToString(dr[2]),
                        Convert.ToString(dr[3]),
                        Convert.ToString(dr[4]),
                        Convert.ToString(dr[5]),
                        Convert.ToString(dr[6]),
                   }

                }).ToArray()
            };

            return _rows;
        }

        [HttpGet] //api/DataProfilerAPI/CreateCodeRule
        public string CreateCodeRule(string client_ID, string project_ID, long config_ID, string table_name, string column_name, string code_value)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            _profiler.CreateCodeRule(client_ID, project_ID, config_ID, table_name, column_name, code_value, UIProperties.Sessions.UserName, ref StatusCode, ref Message);

            return Message;
        }

        [HttpGet] //api/DataProfilerAPI/GetCodeRule
        public dynamic GetCodeRule(string client_ID, string project_ID, string table_name, string column_name)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            var codeRule = _profiler.GetCodeRule(client_ID, project_ID, table_name, column_name, ref StatusCode, ref Message);

            var rows = new
            {
                total = 0,
                page = 0,
                records = 0,
                rows = (
                    from column in codeRule
                    select new
                    {
                        cell = new string[] {
                         column.Table_Name,
                         column.Column_Name.TrimStart('[').TrimEnd(']'),
                         column.Conditional_Clause.TrimStart('(').TrimEnd(')'),
                         column.Create_Date
                      }
                    }).ToArray()
            };
            return rows;
        }


    }
}

//namespace SAPIntegration
//{
//    public class Customers
//    {
//        protected string CustomerNo;
//        protected string CustomerName;
//        protected string Address;
//        protected string City;
//        protected string StateProvince;
//        protected string CountryCode;
//        protected string PostalCode;
//        protected string Region;
//        protected string Industry;
//        protected string District;
//        protected string SalesOrg;
//        protected string DistributionChannel;
//        protected string Division;

//        public string GetContract(RfcDestination destination, string[] column_data)
//        {
//            #region Contract

//            string Contract = "";
//            try
//            {
//                RfcRepository repo = destination.Repository;

//                IRfcFunction contractDetails = repo.CreateFunction("BAPI_CONTRACT_CREATEFROMDATA");

//                //iMPORT
//                RfcStructureMetadata headermetaData = repo.GetStructureMetadata("BAPISDHD1");
//                IRfcStructure structheader = headermetaData.CreateStructure();
//                structheader.SetValue("DOC_TYPE", column_data[0].ToString());
//                structheader.SetValue("SALES_ORG", column_data[1].ToString());
//                structheader.SetValue("DISTR_CHAN", "0" + column_data[2].ToString());
//                structheader.SetValue("DIVISION", "0" + column_data[3].ToString());
//                structheader.SetValue("SALES_GRP", column_data[4].ToString());
//                structheader.SetValue("SALES_OFF", column_data[5].ToString());
//                structheader.SetValue("PURCH_DATE", DateFormat(column_data[6].Split('/')));
//                structheader.SetValue("NAME", column_data[7].ToString());
//                structheader.SetValue("INCOTERMS1", column_data[8].ToString());
//                structheader.SetValue("INCOTERMS2", column_data[9].ToString());
//                structheader.SetValue("PMNTTRMS", "000" + column_data[10].ToString());
//                structheader.SetValue("PRICE_DATE", DateFormat(column_data[11].Split('/')));
//                structheader.SetValue("DOC_DATE", DateFormat(column_data[12].Split('/')));
//                structheader.SetValue("SHIP_COND", "0" + column_data[13].ToString());
//                structheader.SetValue("BILL_DATE", DateFormat(column_data[14].Split('/')));
//                structheader.SetValue("CURRENCY", column_data[15].ToString());
//                contractDetails.SetValue("CONTRACT_HEADER_IN", structheader);

//                RfcStructureMetadata headerxmetaData = repo.GetStructureMetadata("BAPISDHD1X");
//                IRfcStructure structheaderx = headerxmetaData.CreateStructure();
//                structheaderx.SetValue("DOC_TYPE", "X");
//                structheaderx.SetValue("SALES_ORG", "X");
//                structheaderx.SetValue("DISTR_CHAN", "X");
//                structheaderx.SetValue("DIVISION", "X");
//                structheaderx.SetValue("SALES_GRP", "X");
//                structheaderx.SetValue("SALES_OFF", "X");
//                structheaderx.SetValue("PURCH_DATE", "X");
//                structheaderx.SetValue("NAME", "X");
//                structheaderx.SetValue("INCOTERMS1", "X");
//                structheaderx.SetValue("INCOTERMS2", "X");
//                structheaderx.SetValue("PMNTTRMS", "X");
//                structheaderx.SetValue("PRICE_DATE", "X");
//                structheaderx.SetValue("DOC_DATE", "X");
//                structheaderx.SetValue("SHIP_COND", "X");
//                structheaderx.SetValue("BILL_DATE", "X");
//                structheaderx.SetValue("CURRENCY", "X");
//                contractDetails.SetValue("CONTRACT_HEADER_INX", structheaderx);


//                //TABLES

//                RfcStructureMetadata CONTRACTITESM = repo.GetStructureMetadata("BAPISDITM");
//                IRfcStructure structcontractitesm = CONTRACTITESM.CreateStructure();
//                structcontractitesm.SetValue("ITM_NUMBER", "000010");
//                structcontractitesm.SetValue("MATERIAL", "000000000000000" + column_data[16].ToString());
//                structcontractitesm.SetValue("TARGET_QTY", "1000000");
//                structcontractitesm.SetValue("ITEM_CATEG", column_data[18].ToString());
//                IRfcTable tblcontractitems = contractDetails.GetTable("CONTRACT_ITEMS_IN");
//                tblcontractitems.Append(structcontractitesm);
//                contractDetails.SetValue("CONTRACT_ITEMS_IN", tblcontractitems);

//                RfcStructureMetadata CONTRACTITESMx = repo.GetStructureMetadata("BAPISDITMX");
//                IRfcStructure structcontractitesmx = CONTRACTITESMx.CreateStructure();
//                structcontractitesmx.SetValue("ITM_NUMBER", "000010");
//                structcontractitesmx.SetValue("MATERIAL", "X");
//                structcontractitesmx.SetValue("TARGET_QTY", "X");
//                structcontractitesmx.SetValue("ITEM_CATEG", "X");
//                IRfcTable tblcontractitemsx = contractDetails.GetTable("CONTRACT_ITEMS_INX");
//                tblcontractitemsx.Append(structcontractitesmx);
//                contractDetails.SetValue("CONTRACT_ITEMS_INX", tblcontractitemsx);

//                RfcStructureMetadata CONTRACTPARTNER = repo.GetStructureMetadata("BAPIPARNR");
//                IRfcStructure structcontractpartner = CONTRACTPARTNER.CreateStructure();
//                structcontractpartner.SetValue("PARTN_ROLE", column_data[19]);
//                structcontractpartner.SetValue("PARTN_NUMB", "0000000" + column_data[20]);
//                IRfcTable tblcontractpartner = contractDetails.GetTable("CONTRACT_PARTNERS");
//                tblcontractpartner.Append(structcontractpartner);
//                contractDetails.SetValue("CONTRACT_PARTNERS", tblcontractpartner);

//                RfcStructureMetadata conditions = repo.GetStructureMetadata("BAPICOND");
//                IRfcStructure structconditions = conditions.CreateStructure();
//                structconditions.SetValue("ITM_NUMBER", "000010");
//                structconditions.SetValue("COND_TYPE", column_data[21]);
//                structconditions.SetValue("CURRENCY", column_data[22]);
//                IRfcTable tblconditions = contractDetails.GetTable("CONTRACT_CONDITIONS_IN");
//                tblconditions.Append(structconditions);
//                contractDetails.SetValue("CONTRACT_CONDITIONS_IN", tblconditions);

//                RfcStructureMetadata conditionsx = repo.GetStructureMetadata("BAPICONDX");
//                IRfcStructure structconditionsx = conditionsx.CreateStructure();
//                structconditionsx.SetValue("ITM_NUMBER", "000010");
//                structconditionsx.SetValue("COND_TYPE", "X");
//                structconditionsx.SetValue("CURRENCY", "X");
//                IRfcTable tblconditionsx = contractDetails.GetTable("CONTRACT_CONDITIONS_INX");
//                tblconditionsx.Append(structconditionsx);
//                contractDetails.SetValue("CONTRACT_CONDITIONS_INX", tblconditionsx);

//                RfcStructureMetadata contractdata = repo.GetStructureMetadata("BAPICTR");
//                IRfcStructure structcontract = contractdata.CreateStructure();
//                structcontract.SetValue("ITM_NUMBER", "000010");
//                structcontract.SetValue("CON_ST_DAT", DateFormat(column_data[23].Split('/')));
//                structcontract.SetValue("CON_EN_DAT", DateFormat(column_data[24].Split('/')));
//                IRfcTable tblcontract = contractDetails.GetTable("CONTRACT_DATA_IN");
//                tblcontract.Append(structcontract);
//                contractDetails.SetValue("CONTRACT_DATA_IN", tblcontract);

//                RfcStructureMetadata contractdataX = repo.GetStructureMetadata("BAPICTRX");
//                IRfcStructure structcontractX = contractdataX.CreateStructure();
//                structcontractX.SetValue("ITM_NUMBER", "000010");
//                structcontractX.SetValue("CON_ST_DAT", "X");
//                structcontractX.SetValue("CON_EN_DAT", "X");
//                IRfcTable tblcontractx = contractDetails.GetTable("CONTRACT_DATA_INX");
//                tblcontractx.Append(structcontractX);
//                contractDetails.SetValue("CONTRACT_DATA_INX", tblcontractx);

//                IRfcFunction COMMITcontractDetails = repo.CreateFunction("BAPI_TRANSACTION_COMMIT");


//                RfcSessionManager.BeginContext(destination);
//                contractDetails.Invoke(destination);
//                COMMITcontractDetails.Invoke(destination);
//                RfcSessionManager.EndContext(destination);

//                Contract = contractDetails.GetValue("SALESDOCUMENT").ToString();
//            }
//            catch (RfcCommunicationException e)
//            {

//            }
//            catch (RfcLogonException e)
//            {
//                // user could not logon...
//            }
//            catch (RfcAbapRuntimeException e)
//            {
//                // serious problem on ABAP system side...
//            }
//            catch (RfcAbapBaseException e)
//            {
//                // The function module returned an ABAP exception, an ABAP message
//                // or an ABAP class-based exception...
//            }
//            catch (Exception ex)
//            {

//            }
//            return Contract;


//            #endregion
//        }
//        public string DateFormat(string[] BirthDateSeperation)
//        {
//            string year = BirthDateSeperation[2].Substring(0, 4);
//            string Month = BirthDateSeperation[0].ToString().Length == 1 ? "0" + BirthDateSeperation[0].ToString() : BirthDateSeperation[0].ToString();
//            string Day = BirthDateSeperation[1].ToString().Length == 1 ? "0" + BirthDateSeperation[1].ToString() : BirthDateSeperation[1].ToString();
//            return year + Month + Day;

//        }
//        public string GetCustomerDetails(RfcDestination destination, string[] column_data)
//        {

//            string businesspartner = "";
//            try
//            {

//                RfcRepository repo = destination.Repository;

//                #region Partner

//                IRfcFunction orderDetails = repo.CreateFunction("BAPI_BUPA_CREATE_FROM_DATA");

//                orderDetails.SetValue("PARTNERCATEGORY", column_data[0].ToString());
//                orderDetails.SetValue("PARTNERGROUP", "000" + column_data[1].ToString());


//                RfcStructureMetadata metaData = repo.GetStructureMetadata("BAPIBUS1006_CENTRAL");
//                IRfcStructure structPartners = metaData.CreateStructure();
//                structPartners.SetValue("TITLE_KEY", "0001");
//                orderDetails.SetValue("CENTRALDATA", structPartners);


//                RfcStructureMetadata am1 = repo.GetStructureMetadata("BAPIBUS1006_CENTRAL_PERSON");
//                IRfcStructure person = am1.CreateStructure();
//                person.SetValue("FIRSTNAME", column_data[2].ToString());
//                person.SetValue("LASTNAME", column_data[3].ToString());
//                person.SetValue("SEX", column_data[4].ToString());
//                person.SetValue("BIRTHDATE", DateFormat(column_data[5].Split('/')));
//                orderDetails.SetValue("CENTRALDATAPERSON", person);


//                RfcStructureMetadata am2 = repo.GetStructureMetadata("BAPIBUS1006_ADDRESS");
//                IRfcStructure ADD = am2.CreateStructure();
//                ADD.SetValue("CITY", column_data[9].ToString());
//                ADD.SetValue("POSTL_COD1", column_data[10].ToString());
//                ADD.SetValue("STREET", column_data[8].ToString());
//                ADD.SetValue("HOUSE_NO", column_data[6].ToString());
//                ADD.SetValue("HOUSE_NO2", column_data[7].ToString());
//                ADD.SetValue("COUNTRY", column_data[11].ToString());
//                ADD.SetValue("REGION", column_data[12].ToString());
//                ADD.SetValue("EXTADDRESSNUMBER", "123");
//                orderDetails.SetValue("ADDRESSDATA", ADD);

//                RfcStructureMetadata PHONE = repo.GetStructureMetadata("BAPIADTEL");
//                IRfcStructure structPartners1 = PHONE.CreateStructure();
//                structPartners1.SetValue("TELEPHONE", column_data[13].ToString());
//                structPartners1.SetValue("EXTENSION", column_data[14].ToString());
//                IRfcTable tblPartner1 = orderDetails.GetTable("TELEFONDATA");
//                tblPartner1.Append(structPartners1);
//                orderDetails.SetValue("TELEFONDATA", tblPartner1);


//                RfcStructureMetadata FAXDATA = repo.GetStructureMetadata("BAPIADFAX");
//                IRfcStructure structfaxdata = FAXDATA.CreateStructure();
//                structfaxdata.SetValue("FAX", column_data[15].ToString());
//                structfaxdata.SetValue("EXTENSION", column_data[16].ToString());
//                IRfcTable tblfaxdata = orderDetails.GetTable("FAXDATA");
//                tblfaxdata.Append(structfaxdata);
//                orderDetails.SetValue("FAXDATA", tblfaxdata);

//                RfcStructureMetadata EMAILDATA = repo.GetStructureMetadata("BAPIADSMTP");
//                IRfcStructure structemaildata = EMAILDATA.CreateStructure();
//                structemaildata.SetValue("E_MAIL", column_data[17].ToString());
//                IRfcTable tblemaildata = orderDetails.GetTable("E_MAILDATA");
//                tblemaildata.Append(structemaildata);
//                orderDetails.SetValue("E_MAILDATA", tblemaildata);


//                IRfcFunction COMMITDetails = repo.CreateFunction("BAPI_TRANSACTION_COMMIT");
//                COMMITDetails.SetValue("WAIT", "X");

//                RfcSessionManager.BeginContext(destination);
//                orderDetails.Invoke(destination);
//                COMMITDetails.Invoke(destination);
//                RfcSessionManager.EndContext(destination);

//                businesspartner = orderDetails.GetValue("BUSINESSPARTNER").ToString();



//                #endregion


//                GC.Collect();
//                GC.WaitForPendingFinalizers();


//            }
//            catch (RfcCommunicationException e)
//            {

//            }
//            catch (RfcLogonException e)
//            {
//                // user could not logon...
//            }
//            catch (RfcAbapRuntimeException e)
//            {
//                // serious problem on ABAP system side...
//            }
//            catch (RfcAbapBaseException e)
//            {
//                // The function module returned an ABAP exception, an ABAP message
//                // or an ABAP class-based exception...
//            }
//            catch (Exception ex)
//            {

//            }
//            return businesspartner;

//        }
//    }


//    class SAPSystemConnect : IDestinationConfiguration
//    {

//        public bool ChangeEventsSupported()
//        {
//            return true;
//        }
//        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;


//        public RfcConfigParameters GetParameters(string destinationName)
//        {

//            RfcConfigParameters parms = new RfcConfigParameters();

//            if (destinationName.Equals("mySAPdestination"))
//            {
//                parms.Add(RfcConfigParameters.AppServerHost, "172.25.121.226");
//                parms.Add(RfcConfigParameters.SystemNumber, "06");
//                parms.Add(RfcConfigParameters.User, "33937");
//                parms.Add(RfcConfigParameters.Password, "welcome456");
//                parms.Add(RfcConfigParameters.Client, "700");
//                parms.Add(RfcConfigParameters.Language, "EN");
//                parms.Add(RfcConfigParameters.PoolSize, "5");
//                parms.Add(RfcConfigParameters.PeakConnectionsLimit, "10");
//                parms.Add(RfcConfigParameters.IdleTimeout, "600");

//            }
//            return parms;
//        }

//    }
//}
