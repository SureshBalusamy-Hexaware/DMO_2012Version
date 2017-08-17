using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace DM_DataModel.UnitOfWork
{
    public class DataRecon : IDisposable
    {
        DM_MetaDataEntities _context = null;

        public DataRecon()
        {
            _context = new DM_MetaDataEntities();
        }

        public List<DRD_GET_SOURCE_TARGET_TABLE_COLUMNS_SP_Result> GetTableColumns(string client_ID, string project_ID, string srcTable_Name, string tgtTable_Name, string srcConfig_ID, string tgtConfig_ID, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.DRD_GET_SOURCE_TARGET_TABLE_COLUMNS_SP(client_ID, project_ID, srcTable_Name, tgtTable_Name, srcConfig_ID, tgtConfig_ID, OutPut_status_Code, OutPut_message).ToList<DRD_GET_SOURCE_TARGET_TABLE_COLUMNS_SP_Result>();


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

        public List<DRD_GET_SOURCE_TARGET_TABLE_TRANS_SP_Result> GetTransRuleData(string client_ID, string project_ID, string srcTable_Name, string tgtTable_Name, string source_Column, string target_Column, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.DRD_GET_SOURCE_TARGET_TABLE_TRANS_SP(client_ID, project_ID, srcTable_Name, tgtTable_Name, source_Column, target_Column, OutPut_status_Code, OutPut_message).ToList<DRD_GET_SOURCE_TARGET_TABLE_TRANS_SP_Result>();


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

        public List<COMMON_GET_TABLE_COLUMN_LIST_SP_Result> GetMetaDataColumnNamesByTableName(string client_ID, string project_ID, string database_IP, string source_Target, string database_Name, string config_ID, string Table_Name, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.COMMON_GET_TABLE_COLUMN_LIST_SP(client_ID, project_ID, database_IP, source_Target, database_Name, Convert.ToInt64(config_ID), Table_Name, OutPut_status_Code, OutPut_message).ToList<COMMON_GET_TABLE_COLUMN_LIST_SP_Result>();

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

        public List<COMMON_GET_KEY_COLUMNS_SP_Result> GetKeyColumns(string client_ID, string project_ID, string table_name, long tool_id, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            try
            {
                var result = _context.COMMON_GET_KEY_COLUMNS_SP(client_ID, project_ID, table_name, tool_id, OutPut_status_Code, OutPut_message).ToList<COMMON_GET_KEY_COLUMNS_SP_Result>();

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

        public List<DRD_GET_TEMPLATE_DETAILS_SP_Result> GetTemplateDetails(string client_ID, string project_ID, string template_name, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            try
            {
                var result = _context.DRD_GET_TEMPLATE_DETAILS_SP(client_ID, project_ID, template_name, OutPut_status_Code, OutPut_message).ToList<DRD_GET_TEMPLATE_DETAILS_SP_Result>();

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

        public List<string> GetTemplateNameList(string client_ID, string project_ID, Nullable<long> tool_ID, long? RoleId)
        {
            try
            {
                var result = _context.DRD_GET_DISTINCT_TEMPLATES_SP(client_ID, project_ID, tool_ID, RoleId).ToList<string>();

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

        public int LoadCompareDataToDOMTable(string client_ID, string project_ID, string srcTable_name, string tgtTable_name, string mapping_name, string srcConfig_ID, string tgtConfig_ID, string created_by, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.DRD_INSERT_DRD_DATA_MAPPING_DOM_SP(client_ID, project_ID, srcTable_name, tgtTable_name, mapping_name, srcConfig_ID, tgtConfig_ID, created_by, OutPut_status_Code, OutPut_message);

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

        public int UpdateAsCriticalColumnInDOMTable(string client_ID, string project_ID, string table_name, string columns_list, string created_by, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.DRD_UPDATE_DRD_DATA_MAPPING_DOM_SP(client_ID, project_ID, table_name, columns_list, created_by, OutPut_status_Code, OutPut_message);

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

        /*
        public int CompareData(string client_ID, string project_ID, string srcTable_name, string tgtTable_name, string mapping_name, long srcConfig_ID, long tgtConfig_ID, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                _context.Database.CommandTimeout = 180;
                var result = _context.DRD_DATA_COMPARE_SP(client_ID, project_ID, srcTable_name, tgtTable_name, srcConfig_ID, tgtConfig_ID, mapping_name, OutPut_status_Code, OutPut_message);

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
        }*/

        public int InsertKeyColumns(string client_ID, string project_ID, long config_ID, string table_name, string created_by, ref string status_Code, ref string message, string key_column1 = "", string key_column2 = "", string key_column3 = "", string key_column4 = "", string key_column5 = "")
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {

                //var result = _context.HXR_INSERT_KEY_COLUMN_SP(client_ID, project_ID, config_ID, table_name, key_column1, key_column2, key_column3, key_column4, key_column5, created_by, OutPut_status_Code, OutPut_message);
                var result = _context.CMN_INSERT_KEY_COLUMN_SP(client_ID, project_ID, config_ID, table_name, key_column1, key_column2, key_column3, key_column4, key_column5, created_by, OutPut_status_Code, OutPut_message);

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

        public void ValidateKeyColumns(string client_ID, string project_ID, string table_name, int Config_Id, string key_column1, string key_column2, string key_column3,
            string key_column4, string key_column5, ref string is_key_columns, ref string status_Code, ref string message)
        {
            string dynamicProc = "CMN_VALIDATE_KEY_COLUMN_SP";
            //string dynamicProc = "AHXR_Validate_Emp_Rules_SP";            
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
                        cmd.Parameters.AddWithValue("@Client_ID", client_ID);
                        cmd.Parameters.AddWithValue("@Project_ID", project_ID);
                        cmd.Parameters.AddWithValue("@Table_Name", table_name);
                        cmd.Parameters.AddWithValue("@Config_ID", Config_Id);
                        cmd.Parameters.AddWithValue("@Key_Column1", key_column1);
                        cmd.Parameters.AddWithValue("@Key_Column2", key_column2);
                        cmd.Parameters.AddWithValue("@Key_Column3", key_column3);
                        cmd.Parameters.AddWithValue("@Key_Column4", key_column4);
                        cmd.Parameters.AddWithValue("@Key_Column5", key_column5);
                        cmd.Parameters.Add("@Is_key_Columns", SqlDbType.VarChar, 150).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        status_Code = cmd.Parameters["@Status_Code"].Value.ToString();
                        message = cmd.Parameters["@Message"].Value.ToString();
                        is_key_columns = cmd.Parameters["@Is_key_columns"].Value.ToString();
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
            }
            catch (Exception _e)
            {
                status_Code = "-1";
                message = "-1";
            }

        }

        public void InsertReconcileData(DataTable dt, ref string status_Code, ref string message)
        {
            string dynamicProc = "[DRD_INSERT_TEMPLATE_SP]";

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

        public DataTable CompareData(string client_ID, string project_ID, string template_name, long tool_ID, long? RoleId, string modified_by, ref string status_Code, ref string message)
        {
            //var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            //var OutPut_message = new ObjectParameter("message", typeof(string));
            //try
            //{
            //    _context.Database.CommandTimeout = 180;
            //    var result = _context.DRD_DATA_COMPARE_SP(client_ID, project_ID, template_name, tool_ID, modified_by, OutPut_status_Code, OutPut_message);

            //    status_Code = OutPut_status_Code.Value.ToString();
            //    message = OutPut_message.Value.ToString();
            //    return result;
            //}
            //catch (DbEntityValidationException e)
            //{
            //    var outputLines = new List<string>();
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        outputLines.Add(string.Format(
            //            "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State));
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
            //        }
            //    }

            //    System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

            //    throw e;
            //}

            string dynamicProc = "[DRD_DATA_COMPARE_SP]";
            DataTable dt = new DataTable();
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;
            using (SqlConnection con = new SqlConnection(providerConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(dynamicProc, con))
                {
                    if (con.State != ConnectionState.Open) con.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.VarChar).Value = client_ID;
                    cmd.Parameters.Add("@Project_ID", SqlDbType.VarChar).Value = project_ID;
                    cmd.Parameters.Add("@Template_Name", SqlDbType.VarChar).Value = template_name;
                    cmd.Parameters.Add("@Tool_ID", SqlDbType.VarChar).Value = tool_ID;
                    cmd.Parameters.Add("@Role_ID", SqlDbType.BigInt).Value = RoleId;
                    cmd.Parameters.Add("@Run_User", SqlDbType.VarChar).Value = modified_by;
                    cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;


                    using (SqlDataAdapter a = new SqlDataAdapter(cmd))
                    {
                        a.Fill(dt);
                    }
                    status_Code = Convert.ToString(cmd.Parameters["@Status_Code"].Value);
                    message = Convert.ToString(cmd.Parameters["@Message"].Value);
                }
                if (con.State != ConnectionState.Closed) con.Close();
            }
            return dt;
        }

        public void ValidateExpression(string client_ID, string project_ID, long config_ID, string table_name, string Expression, ref string status_Code, ref string message)
        {
            string dynamicProc = "[CMN_VALIDATE_EXPRESSION_SP]";

            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;


            using (SqlConnection con = new SqlConnection(providerConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(dynamicProc, con))
                {
                    if (con.State != ConnectionState.Open) con.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.VarChar).Value = client_ID;
                    cmd.Parameters.Add("@Project_ID", SqlDbType.VarChar).Value = project_ID;
                    cmd.Parameters.Add("@Config_ID", SqlDbType.BigInt).Value = config_ID;
                    cmd.Parameters.Add("@Tablename", SqlDbType.VarChar).Value = table_name;
                    cmd.Parameters.Add("@Expression", SqlDbType.VarChar).Value = Expression;
                    cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    status_Code = Convert.ToString(cmd.Parameters["@Status_Code"].Value);
                    message = Convert.ToString(cmd.Parameters["@Message"].Value);
                }
                if (con.State != ConnectionState.Closed) con.Close();
            }

        }

        public List<COMMON_GET_META_DATA_SP_Result> GetMetaDataTableDetail(string client_ID, string project_ID, string Table_name, string connectionid, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                long? Connectid = 0;
                Connectid = Convert.ToInt64(connectionid);
                if (Connectid <= 0)
                {
                    Connectid = null;
                }
                string col = string.Empty;
                string configid = string.Empty;
                string sourcetarget = string.Empty;
                var result = _context.COMMON_GET_META_DATA_SP(client_ID, project_ID, Table_name, null, Connectid, OutPut_status_Code, OutPut_message).ToList<COMMON_GET_META_DATA_SP_Result>();
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

                throw e;
            }
        }

        public List<DRD_GET_DATARECON_REPORT_SP_Result> GetDetailErrorStatus(long runID, string columnName)
        {
            try
            {
                var result = _context.DRD_GET_DATARECON_REPORT_SP(runID, columnName).ToList<DRD_GET_DATARECON_REPORT_SP_Result>();
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
