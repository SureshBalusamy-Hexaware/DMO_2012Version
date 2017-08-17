using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Microsoft.SqlServer.Dts.Tasks.DataProfilingTask;
using Microsoft.DataDebugger.DataProfiling;

namespace DM_DataModel.UnitOfWork
{
    public class Profiler : IDisposable
    {
        DM_MetaDataEntities _context = null;

        public Profiler()
        {
            _context = new DM_MetaDataEntities();
        }

        public List<COMMON_GET_TABLE_LIST_SP_Result> GetProfilerTableList(string client_ID, string project_ID, long config_ID, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.COMMON_GET_TABLE_LIST_SP(client_ID, project_ID, config_ID, OutPut_status_Code, OutPut_message).ToList<COMMON_GET_TABLE_LIST_SP_Result>();

                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                return result;
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }


        }

        public List<PROFILER_GET_TABLE_DETAILS_SP_Result> GetProfilerTableDetails(string client_ID, string project_ID, string table_name, string config_ID, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.PROFILER_GET_TABLE_DETAILS_SP(client_ID, project_ID, table_name, config_ID, OutPut_status_Code, OutPut_message).ToList<PROFILER_GET_TABLE_DETAILS_SP_Result>();


                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                return result;
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }


        }

        public List<PROFILER_GET_PARAMETER_VALUES_SP_Result> GetProfilerParameterRecordsValue(Nullable<long> tool_ID, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.PROFILER_GET_PARAMETER_VALUES_SP(tool_ID, OutPut_status_Code, OutPut_message).ToList<PROFILER_GET_PARAMETER_VALUES_SP_Result>();

                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                return result;
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }


        }

        public List<PROFILER_GET_DISTINCT_TEMPLATES_SP_Result> GetProfilerTemplateList(string client_ID, string project_ID, Nullable<long> config_ID, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.PROFILER_GET_DISTINCT_TEMPLATES_SP(client_ID, project_ID, config_ID, OutPut_status_Code, OutPut_message).ToList<PROFILER_GET_DISTINCT_TEMPLATES_SP_Result>();

                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                return result;
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }
        }

        public List<PROFILER_GET_TEMPALTE_DETAILS_SP_Result> GetProfilerTemplateDetails(string client_ID, string project_ID, string template_name, long config_ID, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.PROFILER_GET_TEMPALTE_DETAILS_SP(client_ID, project_ID, template_name, config_ID, OutPut_status_Code, OutPut_message).ToList<PROFILER_GET_TEMPALTE_DETAILS_SP_Result>();

                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                return result;
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }


        }

        public List<string> GetTableCodeColumnList(string client_ID, string project_ID, Nullable<long> config_ID, ref string status_Code, ref string message)
        {
            List<string> list = new List<string>();
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            try
            {
                ObjectResult<string> objresult = _context.PROFILER_GET_COLUMN_LIST_SP(client_ID, project_ID, config_ID, OutPut_status_Code, OutPut_message);
                list = objresult.ToList();
                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
            }
            catch (DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }
            return list;
        }

        public Int64 UpdateProfileStatus(string client_ID, string project_ID, Nullable<long> config_ID, string template_name, string table_name, string profile_type, string profile_description, Nullable<DateTime> start_time, string profile_status, long rec_count, string profiled_by, Nullable<long> profileID, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {

                var result = _context.PROFILER_INSERT_STATUS_SP(client_ID, project_ID, config_ID, template_name, table_name, profile_type, profile_description, start_time, profile_status, rec_count, profiled_by, profileID, OutPut_status_Code, OutPut_message).ToList<PROFILER_INSERT_STATUS_SP_Result>();


                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();

                return result[0].ProfileID ?? -1;
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        public int InsertProfileTemplate(string client_ID, string project_ID, string template_name, string table_name, string column_name, string data_type, Nullable<bool> null__ratio_profile, Nullable<bool> statistics_Profile, Nullable<bool> value_Distribution_Profile, Nullable<bool> length_Distribution_Profile, Nullable<bool> pattern_Profile, Nullable<bool> candidate_Key_Profile, Nullable<bool> profiling_Status, Nullable<long> config_ID, string created_by, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {

                //var result = _context.PROFILER_INSERT_TEMPLATE_SP(client_ID, project_ID, template_name, table_name, column_name, data_type, null__ratio_profile, statistics_Profile, value_Distribution_Profile, length_Distribution_Profile, pattern_Profile, candidate_Key_Profile, profiling_Status, config_ID, created_by, OutPut_status_Code, OutPut_message);


                //status_Code = OutPut_status_Code.Value.ToString();
                //message = OutPut_message.Value.ToString();
                return 0;
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }
        }

        public void InsertProfileTemplate(DataTable dt, ref string status_Code, ref string message)
        {
            string dynamicProc = "[PROFILER_INSERT_TEMPLATE_SP]";

            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(providerConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(dynamicProc, con))
                    {
                        if (con.State != ConnectionState.Open) con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Temptable", SqlDbType.Structured).Value = dt;
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        status_Code = Convert.ToString(cmd.Parameters["@Status_Code"].Value);
                        message = Convert.ToString(cmd.Parameters["@Message"].Value);
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        public int CreateCodeRule(string client_ID, string project_ID, Nullable<long> config_ID, string table_name, string column_name, string code_value, string created_by, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("Status_code", typeof(string));
            var OutPut_message = new ObjectParameter("Message", typeof(string));

            try
            {

                var result = _context.PROFILER_CREATE_RULES_SP(client_ID, project_ID, config_ID, table_name, column_name, code_value, created_by, OutPut_status_Code, OutPut_message);

                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                return result;
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }


        }

        public DataTable GetProfilerTableSampleRecords(long? config_ID, string table_name, string column_list, string row_count, ref  string status_Code, ref string message, ref long totalCount, int pageno, int noOfRecord)
        {
            string dynamicProc = "[PROFILER_GET_TABLE_COLUMN_VALUES_SP]";
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;
            DataTable _dtSampleData = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(providerConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(dynamicProc, con))
                    {
                        if (con.State != ConnectionState.Open) con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Config_ID", config_ID);
                        cmd.Parameters.AddWithValue("@Table_Name", table_name);
                        cmd.Parameters.AddWithValue("@Column_list", column_list);
                        cmd.Parameters.AddWithValue("@Row_Count", row_count);
                        cmd.Parameters.AddWithValue("@PageNo", pageno);
                        cmd.Parameters.AddWithValue("@RecordsPerPage", noOfRecord);
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@TotalCount", SqlDbType.BigInt, 255).Direction = ParameterDirection.Output;

                        //cmd.ExecuteNonQuery();
                        using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
                        {
                            _da.Fill(_dtSampleData);
                        }
                        status_Code = cmd.Parameters["@Status_Code"].Value.ToString();
                        message = cmd.Parameters["@Message"].Value.ToString();
                        totalCount = Convert.ToInt64(cmd.Parameters["@TotalCount"].Value.ToString());
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
                return _dtSampleData;
            }
            catch (Exception _e)
            {
                //status_Code = "-1";
                //message = "-1";
                return null;
            }
        }

        public DataTable GetConfigDetails(string client_id, string project_id, string source_target, long? tool_id, long? Role_ID)
        {

            string dynamicProc = "[COMMON_GET_CONFIG_DETAILS_SP]";
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;
            DataTable _dtSampleData = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(providerConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(dynamicProc, con))
                    {
                        if (con.State != ConnectionState.Open) con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Client_ID", client_id);
                        cmd.Parameters.AddWithValue("@Project_ID", project_id);
                        cmd.Parameters.AddWithValue("@Source_Target", source_target);
                        cmd.Parameters.AddWithValue("@Tool_ID", tool_id);
                        cmd.Parameters.AddWithValue("@Role_ID", Role_ID);
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
                        {
                            _da.Fill(_dtSampleData);
                        }
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
                return _dtSampleData;
            }
            catch (Exception _e)
            {
                return null;
            }
        }

        public DataTable GetColumnCodeValues(string client_id, string project_id, long? config_ID, string table_name, string column_name, byte? isDistinct, ref  string status_Code, ref string message)
        {
            string dynamicProc = "[COMMON_GET_TABLE_DATA_SP]";
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;
            DataTable _dtData = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(providerConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(dynamicProc, con))
                    {
                        if (con.State != ConnectionState.Open) con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Client_ID", client_id);
                        cmd.Parameters.AddWithValue("@Project_ID", project_id);
                        cmd.Parameters.AddWithValue("@Config_ID", config_ID);
                        cmd.Parameters.AddWithValue("@Table_Name", table_name);
                        cmd.Parameters.AddWithValue("@Column_name", column_name);
                        cmd.Parameters.AddWithValue("@IS_Distinct", isDistinct);
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        //cmd.ExecuteNonQuery();
                        using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
                        {
                            _da.Fill(_dtData);
                        }
                        status_Code = cmd.Parameters["@Status_Code"].Value.ToString();
                        message = cmd.Parameters["@Message"].Value.ToString();
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
                return _dtData;
            }
            catch (Exception _e)
            {
                status_Code = "-1";
                message = "-1";
                return null;
            }

        }

        public DataTable GetBatchProfileStatus(int page, int rows, string client_id, string project_id)
        {
            string dynamicProc = "[PROF_GET_BATCH_STATUS_SP]";
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;
            DataTable _dtData = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(providerConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(dynamicProc, con))
                    {
                        if (con.State != ConnectionState.Open) con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Page_no", page);
                        cmd.Parameters.AddWithValue("@No_of_rows", rows);
                        cmd.Parameters.AddWithValue("@Client_ID", client_id);
                        cmd.Parameters.AddWithValue("@Project_ID", project_id);
                        using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
                        {
                            _da.Fill(_dtData);
                        }
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
                return _dtData;
            }
            catch (Exception _e)
            {
                return null;
            }

        }

        public string GetDynamicViewName(string server_ip, string database_name, string table_name, string column_list, int row_count, long? RoleId)
        {
            string view_name;
            string dynamicProc = "[PROFILER_CREATE_DYNAMIC_VIEW_SP]";

            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(providerConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(dynamicProc, con))
                    {
                        if (con.State != ConnectionState.Open) con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TableName", table_name);
                        cmd.Parameters.AddWithValue("@Role_ID", RoleId);
                        cmd.Parameters.AddWithValue("@ColumnList", column_list);
                        cmd.Parameters.AddWithValue("@RowCount", row_count);
                        //cmd.Parameters.AddWithValue("@Source_DB_IP_Address", server_ip);
                        cmd.Parameters.AddWithValue("@Source_DB_Name", database_name);
                        cmd.Parameters.Add("@ViewName", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        view_name = cmd.Parameters["@ViewName"].Value.ToString();
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
                return view_name;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        public void DropView(string server_ip, string database_name, string view_name)
        {
            string dynamicProc = "[PROFILER_DROP_DYNAMIC_VIEW_SP]";

            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(providerConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(dynamicProc, con))
                    {
                        if (con.State != ConnectionState.Open) con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ViewName", view_name);
                        //cmd.Parameters.AddWithValue("@Source_DB_IP_Address", server_ip);
                        cmd.Parameters.AddWithValue("@Source_DB_Name", database_name);
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        //view_name = cmd.Parameters["@Status_Code"].Value.ToString();
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
            }
            catch (Exception _e)
            {
                //return null;
            }
        }

        public string LoadProfileOutputDatatToTable(string profileOutputData, long _profileId, string _templateName, string _tableName, string tgtConn)
        {
            double rowCount;
            long lenItemCount = 0;
            long nullItemCount = 0;
            long valueItemCount = 0;
            long statItemCount = 0;
            long patternItemCount = 0;
            long keyItemCount = 0;
            long profileId = _profileId;
            string templateName = _templateName;

            ProfilerOutput dsProfileOutput = new ProfilerOutput();

            ProfilerOutput.ColumnLengthDistributionProfileRow lenDistRow;
            ProfilerOutput.LengthDistributionItemRow lenItemRow;

            ProfilerOutput.ColumnNullRatioProfileRow nullProfileRow;
            ProfilerOutput.ColumnNullRatioProfile_ColumnRow nullColRow;

            ProfilerOutput.ColumnValueDistributionProfileRow valDistRow;
            ProfilerOutput.ValueDistributionItemRow valItemRow;

            ProfilerOutput.ColumnPatternProfileRow patternProfileRow;
            ProfilerOutput.PatternDistributionItemRow patternItemRow;

            ProfilerOutput.ColumnStatisticsProfileRow statProfileRow;

            ProfilerOutput.CandidateKeyProfileRow keyProfileRow;

            ProfilerOutputTableAdapters.ColumnLengthDistributionProfileTableAdapter taLenDist = new ProfilerOutputTableAdapters.ColumnLengthDistributionProfileTableAdapter();
            ProfilerOutputTableAdapters.LengthDistributionItemTableAdapter taLenItem = new ProfilerOutputTableAdapters.LengthDistributionItemTableAdapter();

            ProfilerOutputTableAdapters.ColumnNullRatioProfileTableAdapter taNullProfile = new ProfilerOutputTableAdapters.ColumnNullRatioProfileTableAdapter();
            ProfilerOutputTableAdapters.ColumnNullRatioProfile_ColumnTableAdapter taNullColumn = new ProfilerOutputTableAdapters.ColumnNullRatioProfile_ColumnTableAdapter();

            ProfilerOutputTableAdapters.ColumnValueDistributionProfileTableAdapter taValDist = new ProfilerOutputTableAdapters.ColumnValueDistributionProfileTableAdapter();
            ProfilerOutputTableAdapters.ValueDistributionItemTableAdapter taValItem = new ProfilerOutputTableAdapters.ValueDistributionItemTableAdapter();

            ProfilerOutputTableAdapters.ColumnPatternProfileTableAdapter taPatternProfile = new ProfilerOutputTableAdapters.ColumnPatternProfileTableAdapter();
            ProfilerOutputTableAdapters.PatternDistributionItemTableAdapter taPatternItem = new ProfilerOutputTableAdapters.PatternDistributionItemTableAdapter();

            ProfilerOutputTableAdapters.ColumnStatisticsProfileTableAdapter taStatProfile = new ProfilerOutputTableAdapters.ColumnStatisticsProfileTableAdapter();

            ProfilerOutputTableAdapters.CandidateKeyProfileTableAdapter taKeyProfile = new ProfilerOutputTableAdapters.CandidateKeyProfileTableAdapter();

            string targetConnection = tgtConn;

            #region Assignning connection string for adapter

            taLenDist.Connection.ConnectionString = targetConnection;
            taLenItem.Connection.ConnectionString = targetConnection;
            taNullProfile.Connection.ConnectionString = targetConnection;
            taNullColumn.Connection.ConnectionString = targetConnection;
            taValDist.Connection.ConnectionString = targetConnection;
            taValItem.Connection.ConnectionString = targetConnection;
            taPatternProfile.Connection.ConnectionString = targetConnection;
            taPatternItem.Connection.ConnectionString = targetConnection;
            taStatProfile.Connection.ConnectionString = targetConnection;
            taKeyProfile.Connection.ConnectionString = targetConnection;

            #endregion

            DataProfileXmlSerializer serializer = new DataProfileXmlSerializer();

            try
            {

                DataProfile profiles = serializer.Deserialize(new System.IO.StringReader(profileOutputData));

                foreach (Profile p in profiles.DataProfileOutput.Profiles)
                {
                    if (p is ColumnLengthDistributionProfile)
                    {
                        long maxCount = 0L;
                        lenItemCount++;
                        ColumnLengthDistributionProfile profile = p as ColumnLengthDistributionProfile;

                        rowCount = profile.Table.RowCount;

                        lenDistRow = dsProfileOutput.ColumnLengthDistributionProfile.NewColumnLengthDistributionProfileRow();

                        lenDistRow.Profile_Id = profileId;
                        lenDistRow.TemplateName = templateName;
                        lenDistRow.TableName = _tableName;

                        lenDistRow.ColumnLengthDistributionProfile_Id = lenItemCount;
                        lenDistRow.LengthDistribution_Id = lenItemCount;

                        lenDistRow.DataSource = profile.Table.DataSource;
                        lenDistRow.Database = profile.Table.Database;
                        lenDistRow.RowCount = profile.Table.RowCount;

                        lenDistRow.ColumnName = profile.Column.Name;

                        lenDistRow.MinLength = (byte)profile.MinLength;
                        lenDistRow.MaxLength = (byte)profile.MaxLength;

                        lenDistRow.IgnoreLeadingSpace = profile.IgnoreLeadingSpace;
                        lenDistRow.IgnoreTrailingSpace = profile.IgnoreTrailingSpace;

                        dsProfileOutput.ColumnLengthDistributionProfile.AddColumnLengthDistributionProfileRow(lenDistRow);
                        taLenDist.Update(dsProfileOutput.ColumnLengthDistributionProfile);

                        int num = 0;
                        int count = profile.LengthDistribution.Count;
                        while (num < count)
                        {
                            ColumnLengthDistributionItem item = profile.LengthDistribution[num];
                            maxCount = Math.Max(maxCount, item.Count);
                            num++;
                            int length = item.Length;
                            long recordCount = item.Count;
                            double percentag = (((double)item.Count) / rowCount);

                            lenItemRow = dsProfileOutput.LengthDistributionItem.NewLengthDistributionItemRow();

                            lenItemRow.Profile_Id = profileId;
                            lenItemRow.LengthDistribution_Id = lenItemCount;
                            lenItemRow.Count = item.Count;
                            lenItemRow.Length = item.Length;
                            lenItemRow.Percentage = (decimal)(percentag * 100);
                            dsProfileOutput.LengthDistributionItem.AddLengthDistributionItemRow(lenItemRow);
                            taLenItem.Update(dsProfileOutput.LengthDistributionItem);
                        }
                        dsProfileOutput.AcceptChanges();
                    }

                    else if (p is ColumnNullRatioProfile)
                    {
                        nullItemCount++;
                        ColumnNullRatioProfile nullProfile = p as ColumnNullRatioProfile;

                        nullProfileRow = dsProfileOutput.ColumnNullRatioProfile.NewColumnNullRatioProfileRow();
                        nullProfileRow.Profile_Id = profileId;
                        nullProfileRow.TemplateName = templateName;
                        nullProfileRow.TableName = _tableName;
                        nullProfileRow.DataSource = nullProfile.Table.DataSource;
                        nullProfileRow.Database = nullProfile.Table.Database;
                        nullProfileRow.RowCount = nullProfile.Table.RowCount;
                        nullProfileRow.ColumnNullRatioProfile_Id = nullItemCount;
                        dsProfileOutput.ColumnNullRatioProfile.AddColumnNullRatioProfileRow(nullProfileRow);
                        taNullProfile.Update(dsProfileOutput.ColumnNullRatioProfile);

                        nullColRow = dsProfileOutput.ColumnNullRatioProfile_Column.NewColumnNullRatioProfile_ColumnRow();
                        nullColRow.Profile_Id = profileId;
                        nullColRow.ColumnNullRatioProfile_Id = nullItemCount;
                        nullColRow.ColumnName = nullProfile.Column.Name;
                        nullColRow.MaxLength = nullProfile.Column.MaxLength;
                        nullColRow.IsNullable = nullProfile.Column.IsNullable;
                        nullColRow.NullCount = nullProfile.NullCount;
                        nullColRow.NullPercentage = Convert.ToDecimal(this.GetNullPercentage(nullProfile) * 100);
                        dsProfileOutput.ColumnNullRatioProfile_Column.AddColumnNullRatioProfile_ColumnRow(nullColRow);
                        taNullColumn.Update(dsProfileOutput.ColumnNullRatioProfile_Column);

                        dsProfileOutput.AcceptChanges();
                    }

                    else if (p is ColumnValueDistributionProfile)
                    {
                        valueItemCount++;
                        ColumnValueDistributionProfile valueProfile = p as ColumnValueDistributionProfile;

                        valDistRow = dsProfileOutput.ColumnValueDistributionProfile.NewColumnValueDistributionProfileRow();
                        valDistRow.Profile_Id = profileId;
                        valDistRow.TemplateName = templateName;
                        valDistRow.TableName = _tableName;
                        valDistRow.DataSource = valueProfile.Table.DataSource;
                        valDistRow.Database = valueProfile.Table.Database;
                        valDistRow.ColumnName = valueProfile.Column.Name;
                        valDistRow.NumberOfDistinctValues = valueProfile.NumDistinctValues;
                        valDistRow.RowCount = valueProfile.Table.RowCount;
                        valDistRow.ColumnValueDistributionProfile_Id = valueItemCount;
                        valDistRow.ValueDistribution_Id = valueItemCount;
                        dsProfileOutput.ColumnValueDistributionProfile.AddColumnValueDistributionProfileRow(valDistRow);
                        taValDist.Update(dsProfileOutput.ColumnValueDistributionProfile);

                        foreach (ColumnValueDistributionItem item in valueProfile.ValueDistribution)
                        {
                            valItemRow = dsProfileOutput.ValueDistributionItem.NewValueDistributionItemRow();

                            valItemRow.Profile_Id = profileId;
                            valItemRow.ValueDistribution_Id = valueItemCount;

                            valItemRow.Value = Convert.ToString(item.Value);
                            valItemRow.Count = item.Count;
                            valItemRow.Percentage = Convert.ToDecimal((((double)item.Count) / valueProfile.Table.RowCount) * 100);

                            dsProfileOutput.ValueDistributionItem.AddValueDistributionItemRow(valItemRow);
                            taValItem.Update(dsProfileOutput.ValueDistributionItem);
                        }

                        dsProfileOutput.AcceptChanges();

                        //using (SqlBulkCopy bulkCopy = new SqlBulkCopy(targetConnection))
                        //{
                        //    bulkCopy.DestinationTableName = "dbo.ColumnValueDistributionProfile";
                        //    try
                        //    {
                        //        // Write from the source to the destination.
                        //        bulkCopy.WriteToServer(dsProfileOutput.Tables["ColumnValueDistributionProfile"]);
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        throw ex;
                        //        //   Console.WriteLine(ex.Message);
                        //    }

                        //    bulkCopy.DestinationTableName = "dbo.ValueDistributionItem";
                        //    try
                        //    {
                        //        // Write from the source to the destination.
                        //        bulkCopy.WriteToServer(dsProfileOutput.Tables["ValueDistributionItem"]);
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        throw ex;
                        //        // Console.WriteLine(ex.Message);
                        //    }
                        //}
                    }

                    else if (p is ColumnStatisticsProfile)
                    {
                        statItemCount++;
                        ColumnStatisticsProfile statProfile = p as ColumnStatisticsProfile;

                        statProfileRow = dsProfileOutput.ColumnStatisticsProfile.NewColumnStatisticsProfileRow();

                        statProfileRow.Profile_Id = profileId;
                        statProfileRow.ColumnStatisticsProfile_Id = statItemCount;
                        statProfileRow.TemplateName = templateName;
                        statProfileRow.TableName = _tableName;
                        statProfileRow.RowCount = statProfile.Table.RowCount;
                        statProfileRow.DataSource = statProfile.Table.DataSource;
                        statProfileRow.Database = statProfile.Table.Database;

                        statProfileRow.ColumnName = (statProfile.Column == null) ? string.Empty : statProfile.Column.Name;
                        statProfileRow.MinValue = statProfile.MinValue.ToString();
                        statProfileRow.MaxValue = statProfile.MaxValue.ToString();
                        statProfileRow.Mean = Convert.ToDecimal(statProfile.IsMeanNull ? 0M + string.Empty : statProfile.Mean.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
                        statProfileRow.StdDev = Convert.ToDecimal(statProfile.IsStdDevNull ? 0M + string.Empty : statProfile.StdDev.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

                        dsProfileOutput.ColumnStatisticsProfile.AddColumnStatisticsProfileRow(statProfileRow);
                        taStatProfile.Update(dsProfileOutput.ColumnStatisticsProfile);
                        dsProfileOutput.AcceptChanges();
                    }

                    else if (p is ColumnPatternProfile)
                    {
                        patternItemCount++;
                        ColumnPatternProfile patternProfile = p as ColumnPatternProfile;

                        patternProfileRow = dsProfileOutput.ColumnPatternProfile.NewColumnPatternProfileRow();

                        patternProfileRow.Profile_Id = profileId;
                        patternProfileRow.TemplateName = templateName;
                        patternProfileRow.TableName = _tableName;
                        patternProfileRow.RowCount = patternProfile.Table.RowCount;
                        patternProfileRow.DataSource = patternProfile.Table.DataSource;
                        patternProfileRow.Database = patternProfile.Table.Database;

                        patternProfileRow.ColumnName = patternProfile.Column.Name;
                        patternProfileRow.ColumnPatternProfileRequest_Id = patternItemCount;
                        patternProfileRow.TopRegexPatterns_Id = patternItemCount;

                        dsProfileOutput.ColumnPatternProfile.AddColumnPatternProfileRow(patternProfileRow);
                        taPatternProfile.Update(patternProfileRow);

                        foreach (PatternDistributionItem item in patternProfile.TopRegexPatterns)
                        {
                            patternItemRow = dsProfileOutput.PatternDistributionItem.NewPatternDistributionItemRow();

                            patternItemRow.Profile_Id = profileId;
                            patternItemRow.TopRegexPatterns_Id = patternItemCount;

                            patternItemRow.Frequency = item.Frequency;
                            patternItemRow.RegexText = item.RegexText;

                            dsProfileOutput.PatternDistributionItem.AddPatternDistributionItemRow(patternItemRow);
                            taPatternItem.Update(patternItemRow);
                        }

                        dsProfileOutput.AcceptChanges();
                    }

                    else if (p is CandidateKeyProfile)
                    {
                        keyItemCount++;
                        CandidateKeyProfile keyProfile = p as CandidateKeyProfile;

                        keyProfileRow = dsProfileOutput.CandidateKeyProfile.NewCandidateKeyProfileRow();

                        keyProfileRow.Profile_Id = profileId;
                        keyProfileRow.TemplateName = templateName;
                        keyProfileRow.TableName = _tableName;
                        keyProfileRow.Database = keyProfile.Table.Database;
                        keyProfileRow.DataSource = keyProfile.Table.DataSource;

                        keyProfileRow.ColumnName = this.GetKeyColumnsFormattedString(keyProfile);
                        keyProfileRow.KeyStrength = (keyProfile.IsExactKey ? 1.0M : ((decimal)keyProfile.KeyStrength)) * 100;

                        keyProfileRow.CandidateKeyProfile_Id = keyItemCount;

                        dsProfileOutput.CandidateKeyProfile.AddCandidateKeyProfileRow(keyProfileRow);
                        taKeyProfile.Update(keyProfileRow);

                        dsProfileOutput.AcceptChanges();
                    }

                }

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private double GetNullPercentage(ColumnNullRatioProfile profile)
        {
            if (((profile != null) && (profile.Table != null)) && (profile.NullCount >= 0L))
            {
                if (profile.NullCount == 0L)
                {
                    return 0.0;
                }
                if ((profile.Table.RowCount > 0L) && (profile.NullCount <= profile.Table.RowCount))
                {
                    return (((double)profile.NullCount) / ((double)profile.Table.RowCount));
                }
            }
            return double.NaN;
        }

        private string GetKeyColumnsFormattedString(CandidateKeyProfile keyProfile)
        {
            return FormatColumnInfoCollectionSorted(keyProfile.KeyColumns);
        }

        private string FormatColumnInfoCollectionSorted(ColumnInfoCollection columns)
        {
            string[] array = new string[columns.Count];
            for (int i = 0; i < columns.Count; i++)
            {
                array[i] = columns[i].Name;
            }
            Array.Sort<string>(array);
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            for (int j = 0; j < array.Length; j++)
            {
                builder.Append(array[j]);
                if (j < (array.Length - 1))
                {
                    builder.Append(", ");
                }
            }
            return builder.ToString();
        }

        public List<PROF_GET_PROFILER_RULE_SP_Result> GetProfilerCodeRule(string client_id, string project_id, string table_name, string column_name, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.PROF_GET_PROFILER_RULE_SP(client_id, project_id, table_name, column_name, OutPut_status_Code, OutPut_message).ToList<PROF_GET_PROFILER_RULE_SP_Result>();

                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                return result;
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }


        }

        public string ExecuteFlowTask(long profileID, string xml, ref  string status_Code, ref string message)
        {

            //string profilerInsertProfAuditSP = "[PROFILER_INSERT_PROF_AUDIT_SP]";

            string updateQuery = "UPDATE [DM_MetaData].[dbo].[PROFILER_STATUS_MS] SET [Profile_XML] = '" + (xml.Replace("'", "''")) + "' WHERE Profile_ID = " + profileID.ToString();

            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(providerConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        if (con.State != ConnectionState.Open) con.Open();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.ExecuteNonQuery();
                        message = "Success";
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
            }
            catch (Exception _e)
            {
                throw _e;
            }

            return "Success";

            //try
            //{
            //    using (SqlConnection con = new SqlConnection(providerConnectionString))
            //    {
            //        using (SqlCommand cmd = new SqlCommand(profilerInsertProfAuditSP, con))
            //        {
            //            if (con.State != ConnectionState.Open) con.Open();
            //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //            cmd.Parameters.Add("@Temptable", SqlDbType.Structured).Value = dt;
            //            cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
            //            cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

            //            cmd.ExecuteNonQuery();

            //            status_Code = Convert.ToString(cmd.Parameters["@Status_Code"].Value);
            //            message = Convert.ToString(cmd.Parameters["@Message"].Value);
            //        }
            //        if (con.State != ConnectionState.Closed) con.Close();
            //    }
            //}
            //catch (Exception _e)
            //{
            //    throw _e;
            //}        
        }

        #region Implementing IDiosposable...
        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion
        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
