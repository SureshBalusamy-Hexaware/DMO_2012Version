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

namespace DM_UI.Controllers
{
    public class DataProfilerAPIController : ApiController
    {
        private readonly IProfiler _profiler;


        public DataProfilerAPIController()
        {
            _profiler = new ProfilerService();
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


            return new { _codeList = _codeList, statuscode = StatusCode, message = Message };
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
