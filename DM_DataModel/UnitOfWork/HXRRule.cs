using DM_BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_DataModel.UnitOfWork
{

    public class HXR_GET_RULE_VALIDATION_RESULTS1
    {
        public Nullable<long> number_errors { get; set; }
        public Nullable<long> Fail_Count { get; set; }
        public Nullable<long> pass_count { get; set; }
        public Nullable<long> total_records { get; set; }
        public Nullable<long> Run_Id { get; set; }

    }
    public class HXRRule : IDisposable
    {

        #region Private member variables...
        DM_MetaDataEntities _context = null;
        #endregion
        public HXRRule()
        {
            _context = new DM_MetaDataEntities();
        }

        #region Public Repository Creation properties...

        #endregion

        #region Public member methods....
        public List<string> GetMetaDataTableNames(string client_ID, string project_ID, string config_ID, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                //var result = _context.HXR_GET_TABLES_LIST_SP(client_ID, project_ID, database_IP, source_Target, database_Name, OutPut_status_Code, OutPut_message).ToList<string>();
                var result = _context.COMMON_GET_TABLE_LIST_SP(client_ID, project_ID, Convert.ToInt64(config_ID), OutPut_status_Code, OutPut_message).ToList<COMMON_GET_TABLE_LIST_SP_Result>();

                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                return result.Select(r => r.Table_Name).ToList<string>();
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
        public Dictionary<Int64, string> RunNumberByTable(string client_ID, string project_ID, string Tabel_Name, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {

                return _context.HXR_GET_RUN_ID_SP(client_ID, project_ID, Tabel_Name, OutPut_status_Code, OutPut_message)
                    .ToDictionary<HXR_GET_RUN_ID_SP_Result, Int64, string>(row => row.Run_ID, row => row.Run_Number);
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

        public Dictionary<string, string> RuleCriteriaByTable(string client_ID, string project_ID, string Table_Name, string Run_Id, ref string status_Code,
            ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                return _context.HXR_GET_ERROR_RULE_CATEGORY_SP(client_ID, project_ID, Table_Name, Convert.ToInt64(Run_Id), OutPut_status_Code, OutPut_message)
                    .ToDictionary<HXR_GET_ERROR_RULE_CATEGORY_SP_Result, string, string>(row => row.RuleCategory_ID.ToString(), row => row.RuleCategory_Name);
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
        public Dictionary<string, string> ErrorDataRuleByTable(string client_ID, string project_ID, string Table_Name, string Run_Id, string Rule_Category_Id,
           ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                return _context.HXR_GET_ERROR_RULE_SP(client_ID, project_ID, Table_Name, Convert.ToInt64(Run_Id), Convert.ToInt64(Rule_Category_Id), OutPut_status_Code, OutPut_message)
                    .ToDictionary<HXR_GET_ERROR_RULE_SP_Result, string, string>(row => row.Rule_id.ToString(), row => row.Rule_Name);
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
        public List<string> GetMetaDataColumnNamesByTableName(string client_ID, string project_ID, string database_IP, string source_Target, string database_Name, string config_ID, string Table_Name, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                //List<HXR_GET_TABLE_COLUMN_LIST_SP_Result> result = _context.HXR_GET_TABLE_COLUMN_LIST_SP(client_ID, project_ID, database_IP, source_Target, database_Name, Table_Name, OutPut_status_Code, OutPut_message).ToList<HXR_GET_TABLE_COLUMN_LIST_SP_Result>();
                List<COMMON_GET_TABLE_COLUMN_LIST_SP_Result> result = _context.COMMON_GET_TABLE_COLUMN_LIST_SP(client_ID, project_ID, database_IP, source_Target, database_Name, Convert.ToInt64(config_ID), Table_Name, OutPut_status_Code, OutPut_message).ToList<COMMON_GET_TABLE_COLUMN_LIST_SP_Result>();


                List<string> ColumnNames = result.Select(r => r.Column_Name + ":" + r.Data_Type).ToList();

                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                return ColumnNames;
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

        public List<HXR_GET_RULE_TYPE_SP_Result1> GetRuleType(Nullable<long> ruleType_ID, string ruleType_Name, string client_ID, string project_ID, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            try
            {
                var result = _context.HXR_GET_RULE_TYPE_SP(client_ID, project_ID, ruleType_ID, ruleType_Name, OutPut_status_Code, OutPut_message).ToList<HXR_GET_RULE_TYPE_SP_Result1>();

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

        public List<HXR_GET_RULE_SP_Result> GetRule(string client_ID, string project_ID, Nullable<long> rule_Id, string rule_Name, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            try
            {
                var result = _context.HXR_GET_RULE_SP(client_ID, project_ID, rule_Id, rule_Name, OutPut_status_Code, OutPut_message).ToList<HXR_GET_RULE_SP_Result>();

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

        public List<HXR_GET_RULE_ATTRIBUTE_FOR_DISPLAY_SP_Result> GetRuleAttributes(int page, int rows, string client_ID, string project_ID, string Table_Name,
            string Column_Name, Int64 Rule_TypeID, ref string status_Code, ref string message, ref long TotalRecords)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            var OutPut_TotalRecords = new ObjectParameter("TotalCount", typeof(long));
            try
            {
                Int64? _Rule_TypeID = null;
                if (Rule_TypeID > 0) _Rule_TypeID = Rule_TypeID;
                //var result = _context.HXR_GET_RULE_ATTRIBUTE_SP(client_ID, project_ID, Table_Name, null, null, OutPut_status_Code, OutPut_message).ToList<HXR_GET_RULE_ATTRIBUTE_SP_Result>();
                _context.Database.CommandTimeout = 120;
                var result = _context.HXR_GET_RULE_ATTRIBUTE_FOR_DISPLAY_SP(client_ID, project_ID, Table_Name, Column_Name,
                    _Rule_TypeID, page, rows, OutPut_status_Code, OutPut_message, OutPut_TotalRecords).ToList<HXR_GET_RULE_ATTRIBUTE_FOR_DISPLAY_SP_Result>();

                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                TotalRecords = Convert.ToInt64(OutPut_TotalRecords.Value);
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

        public void GenerateObjects(string client_ID, string project_ID, string table_name, long? config_ID, ref  string status_Code, ref string message)
        {

            string dynamicProc = "PRC_TO_CREATE_PROC";
            //string dynamicProc = "AHXR_Validate_Emp_Rules_SP";            
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;

            try
            {
                using (SqlConnection con = new SqlConnection(providerConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(dynamicProc, con))
                    {
                        cmd.CommandTimeout = 180;
                        if (con.State != ConnectionState.Open) con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Client_ID", client_ID);
                        cmd.Parameters.AddWithValue("@Project_ID", project_ID);
                        cmd.Parameters.AddWithValue("@Table_name", table_name);
                        cmd.Parameters.AddWithValue("@Config_ID", config_ID); ;
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        status_Code = cmd.Parameters["@Status_Code"].Value.ToString();
                        message = cmd.Parameters["@Message"].Value.ToString();
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
            }
            catch (Exception _e)
            {
                status_Code = "-1";
                message = "-1";
            }


            //var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            //var OutPut_message = new ObjectParameter("message", typeof(string));

            //try
            //{
            //    _context.PRC_TO_CREATE_PROC(client_ID, project_ID, table_name, config_ID, OutPut_status_Code, OutPut_message);
            //    status_Code = OutPut_status_Code.Value.ToString();
            //    message = OutPut_message.Value.ToString();
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

        }
        public void UpdateVersionNumber(string client_ID, string project_ID, string ActiveAttributeRuleId, string InActiveAttributeRuleId, string table_name, string updated_by, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                _context.HXR_ALLOCATE_RULE_ATTRIBUTE_SP(client_ID, project_ID, ActiveAttributeRuleId, InActiveAttributeRuleId,
                    table_name, updated_by, OutPut_status_Code, OutPut_message);

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

        }

        public void SavePreRuleAttribute(string client_ID, string project_ID, string rule_ID, Nullable<long> ruleType_ID, string error_Code, Nullable<long> ruleCategory_ID,
            string table_name, string column_Name, string default_value, string conditional_Clause, string rule_desc, string priority, string reference_Table, string reference_Column,
            string reference_Cond, string last_Modified_By, string data_steward, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                _context.HXR_INSERT_RULE_ATTRIBUTE_SP(
                    client_ID, project_ID, Convert.ToInt32(rule_ID), ruleType_ID, error_Code, ruleCategory_ID, table_name, column_Name, default_value,
                    conditional_Clause, rule_desc, priority, reference_Table, reference_Column, reference_Cond, data_steward, last_Modified_By, OutPut_status_Code, OutPut_message);
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

        }

        public void SaveUserRuleAttribute(string client_ID, string project_ID, string rule_ID, Nullable<long> ruleType_ID, string error_Code, Nullable<long> ruleCategory_ID,
            string table_name, string column_Name, string default_value, string conditional_Clause, string rule_desc, string priority, string reference_Table, string reference_Column,
            string reference_Cond, string last_Modified_By, string data_steward, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                _context.HXR_INSERT_RULE_ATTRIBUTE_SP(
                    client_ID, project_ID, Convert.ToInt32(rule_ID), ruleType_ID, error_Code, ruleCategory_ID, table_name, column_Name, default_value,
                    conditional_Clause, rule_desc, priority, reference_Table, reference_Column, reference_Cond, data_steward, last_Modified_By, OutPut_status_Code, OutPut_message);
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

        }

        public void UpdateRuleAttribute(string client_ID, string project_ID, Nullable<long> attribute_Rule_ID, string rule_ID, Nullable<long> ruleType_ID, 
            string IS_Predefined, string error_Code, Nullable<long> ruleCategory_ID, string column_Name, string default_value, string conditional_Clause, 
            string Rule_desc, string priority, string last_Modified_By,string Data_Steward, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                _context.HXR_UPDATE_RULE_ATTRIBUTE_SP(client_ID, project_ID, attribute_Rule_ID, rule_ID, ruleType_ID, IS_Predefined, error_Code, ruleCategory_ID, column_Name, default_value,
                    conditional_Clause, Rule_desc, priority, Data_Steward, last_Modified_By, OutPut_status_Code, OutPut_message);
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

        }
        public void ActivateRuleAttribute(string client_ID, string project_ID, string Attribute_Rule_ID,
            string Status, string updated_by, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                //_context.HXR_ACTIVATE_RULE_ATTRIBUTE_SP(client_ID, project_ID, Convert.ToInt64(Attribute_Rule_ID),
                //    Convert.ToInt64(Status), updated_by, OutPut_status_Code, OutPut_message);
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

        }

        public List<HXR_GET_ERROR_DESC_SP_Result> GetErrorDesc(string client_ID, string project_ID, string error_Code, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.HXR_GET_ERROR_DESC_SP(client_ID, project_ID, error_Code, OutPut_status_Code, OutPut_message).ToList<HXR_GET_ERROR_DESC_SP_Result>();

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
        public List<HXR_GET_RULE_CATEGORY_SP_Result> GetRuleCategory(Nullable<long> ruleCategory_ID, string ruleCategory_Name, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.HXR_GET_RULE_CATEGORY_SP(ruleCategory_ID, ruleCategory_Name, OutPut_status_Code, OutPut_message).ToList<HXR_GET_RULE_CATEGORY_SP_Result>();

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
        public List<HXR_VALIDATE_COLUMN_SP_Result> ValidateColumn(string client_ID, string project_ID, string IP_Address, string database_Name, string table_Name, string Source_Target, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.HXR_VALIDATE_COLUMN_SP(client_ID, project_ID, IP_Address, database_Name, table_Name, Source_Target, OutPut_status_Code,
                    OutPut_message).ToList<HXR_VALIDATE_COLUMN_SP_Result>();
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

        public void CheckKeyColumn(string client_ID, string project_ID, string table_Name, long? config_Id, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.CMN_CHECK_KEY_COLS_SP(client_ID, project_ID, table_Name, config_Id, OutPut_status_Code, OutPut_message);

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

        }

        public List<HXR_GET_RULE_FUNCTION_SP_Result> GetFunctionByDataType(string client_ID, string project_ID, string DataType, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.HXR_GET_RULE_FUNCTION_SP(client_ID, project_ID, DataType, OutPut_status_Code, OutPut_message).ToList<HXR_GET_RULE_FUNCTION_SP_Result>();
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

        public List<HXR_GET_RULE_VALIDATION_SP> GetUserDefineRuleValidation(string client_ID, string project_ID, string ToolID, long? RoleId,string TableName, string Query, ref  string status_Code, ref string message)
        {
            //var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            //var OutPut_message = new ObjectParameter("message", typeof(string));

            ////List<HXR_GET_RULE_VALIDATION_SP> RuleValidationResult =
            ////    _context.HXR_GET_RULE_VALIDATION_SP(client_ID, project_ID, ToolID, TableName, Query, OutPut_status_Code, OutPut_message).ToList<HXR_GET_RULE_VALIDATION_SP>();

            //try
            //{
            // var result = _context.HXR_GET_RULE_VALIDATION_SP(client_ID, project_ID, Convert.ToInt64(ToolID), TableName, Query, OutPut_status_Code, OutPut_message).ToList<HXR_GET_RULE_VALIDATION_SP>();                
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


            string Proc = "[HXR_GET_RULE_VALIDATION_SP]";
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;
            DataTable _dtValidation = new DataTable();

            List<HXR_GET_RULE_VALIDATION_SP> lstValidation;
            try
            {
                using (SqlConnection con = new SqlConnection(providerConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(Proc, con))
                    {
                        if (con.State != ConnectionState.Open) con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Client_ID", client_ID);
                        cmd.Parameters.AddWithValue("@Project_ID", project_ID);
                        cmd.Parameters.AddWithValue("@Tool_ID", ToolID);
                        cmd.Parameters.AddWithValue("@Role_ID",RoleId);
                        cmd.Parameters.AddWithValue("@Tablename", TableName);
                        cmd.Parameters.AddWithValue("@Query", Query);
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        //  cmd.ExecuteNonQuery();
                        using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
                        {
                            _da.Fill(_dtValidation);

                            lstValidation = new List<HXR_GET_RULE_VALIDATION_SP>();
                            lstValidation = _dtValidation.AsEnumerable()
                                .Select(r => new HXR_GET_RULE_VALIDATION_SP()
                                {
                                    VAL = r["VAL"].ToString()
                                }).ToList<HXR_GET_RULE_VALIDATION_SP>();
                        }

                        status_Code = cmd.Parameters["@Status_Code"].Value.ToString();
                        message = cmd.Parameters["@Message"].Value.ToString();
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
                return lstValidation;
            }
            catch (Exception _e)
            {
                status_Code = "-1";
                message = "-1";
                return null;
            }



        }

        public DataTable ExecuteDynamicProcedure(string client_ID, string project_ID, string table_name, string run_ID, string run_User, ref  string status_Code, ref string message)
        {
            string dynamicProc = "Validate_" + table_name.Replace('.','_') + "_Rules_SP";
            //string dynamicProc = "AHXR_Validate_Emp_Rules_SP";            
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;
            DataTable _dtErrorData = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(providerConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(dynamicProc, con))
                    {
                        if (con.State != ConnectionState.Open) con.Open();
                        cmd.CommandTimeout = 180;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Client_ID", client_ID);
                        cmd.Parameters.AddWithValue("@Project_ID", project_ID);
                        cmd.Parameters.AddWithValue("@Run_ID", run_ID);
                        cmd.Parameters.AddWithValue("@Run_User", run_User);
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Run_ID_OUTPUT", SqlDbType.Int).Direction = ParameterDirection.Output;
                        //cmd.ExecuteNonQueryAsync();
                        using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
                        {
                            _da.Fill(_dtErrorData);
                        }

                        status_Code = cmd.Parameters["@Status_Code"].Value.ToString();
                        message = cmd.Parameters["@Message"].Value.ToString();
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
                return _dtErrorData;
            }
            catch (SqlException _sql)
            {
                status_Code = "-1";
                //message = "Sql exception.";
                message = _sql.Message;
                return null;
            }
            catch (Exception _e)
            {
                status_Code = "-1";
                message = _e.Message;
                return null;
            }


        }

        public DataTable GetRuleValidationErrorData(string client_ID, string project_ID, long? config_ID, string table_name, int page_no, int recordsperpage, string run_ID,
            string Rule_cateogry_ID, string Rule_name, ref  string status_Code, ref string message, ref long TotalCount)
        {
            string dynamicProc = "[HXR_GET_RULE_VALIDATION_ERROR_DATA_SP]";
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;
            DataTable _dtErrorData = new DataTable();
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
                        cmd.Parameters.AddWithValue("@Rule_cateogry_ID", Rule_cateogry_ID);
                        cmd.Parameters.AddWithValue("@Rule_ID", Rule_name);
                        cmd.Parameters.AddWithValue("@Config_ID", config_ID);
                        cmd.Parameters.AddWithValue("@PageNo", page_no);
                        cmd.Parameters.AddWithValue("@RecordsPerPage", recordsperpage);
                        cmd.Parameters.AddWithValue("@Run_ID", run_ID);
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@TotalCount", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        //cmd.ExecuteNonQuery();
                        using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
                        {
                            _da.Fill(_dtErrorData);
                        }
                        status_Code = cmd.Parameters["@Status_Code"].Value.ToString();
                        message = cmd.Parameters["@Message"].Value.ToString();
                        TotalCount = Convert.ToInt64(cmd.Parameters["@TotalCount"].Value.ToString());
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
                return _dtErrorData;
            }
            catch (Exception _e)
            {
                status_Code = "-1";
                message = "-1";
                return null;
            }

        }

        public DataTable GetRuleValidationErrorData_Paging(string client_ID, string project_ID, string table_name, long? config_ID, long? pageNo, long? recordsPerPage, ref  string status_Code, ref string message)
        {
            string dynamicProc = "[HXR_GET_RULE_VALIDATION_ERROR_DATA_SP_PAGING]";
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;
            DataTable _dtErrorData = new DataTable();
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
                        cmd.Parameters.AddWithValue("@Config_ID", config_ID);
                        cmd.Parameters.AddWithValue("@PageNo", pageNo);
                        cmd.Parameters.AddWithValue("@RecordsPerPage", recordsPerPage);
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        //cmd.ExecuteNonQuery();
                        using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
                        {
                            _da.Fill(_dtErrorData);
                        }
                        status_Code = cmd.Parameters["@Status_Code"].Value.ToString();
                        message = cmd.Parameters["@Message"].Value.ToString();
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
                return _dtErrorData;
            }
            catch (Exception _e)
            {
                status_Code = "-1";
                message = "-1";
                return null;
            }

        }


        public List<HXR_GET_RULE_VALIDATION_RESULTS1> GetRuleValidationResults(string client_ID, string project_ID, string table_name, string run_ID, string run_User,
             long? ConfigID, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            string DynamicProc_StatusCode = string.Empty, DynamicProc_Message = string.Empty;
            DataTable _dtValidationResults = new DataTable();
            List<HXR_GET_RULE_VALIDATION_RESULTS1> lstValidationResults;
            try
            {

                _dtValidationResults = ExecuteDynamicProcedure(client_ID, project_ID, table_name, run_ID, run_User, ref DynamicProc_StatusCode, ref DynamicProc_Message);
                message = DynamicProc_Message;
                status_Code = DynamicProc_StatusCode;
                if (DynamicProc_Message == "SUCCESS")
                {
                    lstValidationResults = _dtValidationResults.AsEnumerable()
                       .Select(row => new HXR_GET_RULE_VALIDATION_RESULTS1()
                       {
                           pass_count = Convert.ToInt64(row["pass_count"].ToString()),
                           number_errors = Convert.ToInt64(row["number_errors"].ToString()),
                           Fail_Count = Convert.ToInt64(row["Fail_Count"].ToString()),
                           total_records = Convert.ToInt64(row["total_records"].ToString()),
                           Run_Id = Convert.ToInt64(row["run_id"].ToString())
                       }).ToList();
                    return lstValidationResults;
                }
                return null;

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
            catch (Exception e)
            {
                return null;
            }
        }
        public void UpdateSourceTable(string table_name, Nullable<long> Rule_cateogry_ID, Nullable<long> Rule_ID, string primary_key_col, string primary_key_value, string update_col, string update_val, string update_all,
            Nullable<long> run_id, Nullable<long> config_ID, ref  string status_Code, ref string message)
        {
            string dynamicProc = "HXR_UPDATE_SOURCE_TABLE_SP";
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
                        cmd.Parameters.AddWithValue("@Table_name", table_name);
                        cmd.Parameters.AddWithValue("@Rule_cateogry_ID", Rule_cateogry_ID);
                        cmd.Parameters.AddWithValue("@Rule_ID", Rule_ID);
                        cmd.Parameters.AddWithValue("@Primary_key_col", primary_key_col);
                        cmd.Parameters.AddWithValue("@Primary_key_value", primary_key_value);
                        cmd.Parameters.AddWithValue("@Update_col", update_col);
                        cmd.Parameters.AddWithValue("@Update_val", update_val);
                        cmd.Parameters.AddWithValue("@Update_All", update_all);
                        cmd.Parameters.AddWithValue("@Run_ID", run_id);
                        cmd.Parameters.AddWithValue("@Config_ID", config_ID);
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        status_Code = cmd.Parameters["@Status_Code"].Value.ToString();
                        message = cmd.Parameters["@Message"].Value.ToString();
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
        //public void UpdateSourceTable(string table_name, string primary_key_col, string primary_key_value, string update_col, string update_val, Nullable<long> config_ID, ref  string status_Code, ref string message)
        //{
        //    var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
        //    var OutPut_message = new ObjectParameter("message", typeof(string));

        //    try
        //    {
        //        var UpdateQuery = _context.PRC_TO_UPDATE_SOURCE_TABLE_SP(table_name, primary_key_col, primary_key_value, update_col, update_val, config_ID, OutPut_status_Code, OutPut_message).ToList<PRC_TO_UPDATE_SOURCE_TABLE_QUERY_SP>();

        //        status_Code = OutPut_status_Code.Value.ToString();
        //        message = OutPut_message.Value.ToString();

        //    }
        //    catch (DbEntityValidationException e)
        //    {

        //        var outputLines = new List<string>();
        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            outputLines.Add(string.Format(
        //                "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
        //                eve.Entry.Entity.GetType().Name, eve.Entry.State));
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
        //            }
        //        }
        //        System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

        //        throw e;
        //    }

        //}


        public DataTable GetRuleValidationErrorSampleData(long? config_ID, string table_name, string primary_key_column, string primary_key_value, ref  string status_Code, ref string message)
        {
            string dynamicProc = "[HXR_GET_SOURCE_ERROR_DATA_SP]";
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;
            DataTable _dtErrorData = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(providerConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(dynamicProc, con))
                    {
                        if (con.State != ConnectionState.Open) con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Table_name", table_name);
                        cmd.Parameters.AddWithValue("@Primary_key_col", primary_key_column);
                        cmd.Parameters.AddWithValue("@Primary_key_value", primary_key_value);
                        cmd.Parameters.AddWithValue("@Config_ID", config_ID);
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        //cmd.ExecuteNonQuery();
                        using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
                        {
                            _da.Fill(_dtErrorData);
                        }
                        status_Code = cmd.Parameters["@Status_Code"].Value.ToString();
                        message = cmd.Parameters["@Message"].Value.ToString();
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
                return _dtErrorData;
            }
            catch (Exception _e)
            {
                status_Code = "-1";
                message = "-1";
                return null;
            }

        }

        public DataTable GetMetaDataTableList(string ipAddres, string databaseName, ref  string status_Code, ref string message)
        {
            string dynamicProc = "[COMMON_GET_METADATA_TABLES_LIST_SP]";
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;
            DataTable _dtErrorData = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(providerConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(dynamicProc, con))
                    {
                        if (con.State != ConnectionState.Open) con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IP_Address", ipAddres);
                        cmd.Parameters.AddWithValue("@Database_Name", databaseName);
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        //cmd.ExecuteNonQuery();
                        using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
                        {
                            _da.Fill(_dtErrorData);
                        }
                        status_Code = cmd.Parameters["@Status_Code"].Value.ToString();
                        message = cmd.Parameters["@Message"].Value.ToString();
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
                return _dtErrorData;
            }
            catch (Exception _e)
            {
                status_Code = "-1";
                message = "-1";
                return null;
            }


        }





        #region Rule
        public void SaveRule(string client_ID, string project_ID, string rule_Name, string rule_Description, Nullable<System.DateTime> start_Date, Nullable<System.DateTime> end_Date, string conditional_Clause, string default_value, string last_Modified_By,
            ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            var OutPut_RuleId = new ObjectParameter("Rule_ID", typeof(int));
            try
            {
                _context.HXR_INSERT_RULE_SP(client_ID, project_ID, rule_Name, rule_Description, start_Date, end_Date, conditional_Clause, default_value, last_Modified_By,
                    OutPut_RuleId, OutPut_status_Code, OutPut_message);
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

        }

        public void UpdateRule(string client_ID, string project_ID, Nullable<long> rule_ID, string rule_Name, Nullable<long> active_Flag, string rule_Description,
            Nullable<System.DateTime> start_Date, Nullable<System.DateTime> end_Date, string conditional_Clause, string default_value, string last_Modified_By,
            ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                _context.HXR_UPDATE_RULE_SP(client_ID, project_ID, rule_ID, rule_Name, active_Flag, rule_Description, start_Date, end_Date, conditional_Clause, default_value,
                    last_Modified_By, OutPut_status_Code, OutPut_message);

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

        }

        #endregion


        #region RuleCategory
        public void SaveRuleCategory(string ruleCategory_Name, string ruleCategory_Desc, string inserted_by, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                _context.HXR_INSERT_RULE_CATEGORY_SP(ruleCategory_Name, ruleCategory_Desc, inserted_by, OutPut_status_Code, OutPut_message);
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

        }
        public void UpdateRuleCategory(Nullable<long> ruleCategory_ID, string ruleCategory_Name, string ruleCategory_Desc, Nullable<long> active_Flag, string last_Modified_By, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                _context.HXR_UPDATE_RULE_CATEGORY_SP(ruleCategory_ID, ruleCategory_Name, ruleCategory_Desc, active_Flag, last_Modified_By, OutPut_status_Code, OutPut_message);
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

        }

        #endregion

        #region RuleType
        public void SaveRuleType(string client_ID, string project_ID, string ruleType_Name, 
            string ruleType_Desc, string inserted_by, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                _context.HXR_INSERT_RULE_TYPE_SP(client_ID, project_ID, ruleType_Name, ruleType_Desc, inserted_by, OutPut_status_Code, OutPut_message);
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

        }
        public void UpdateRuleType(Nullable<long> ruleType_ID, string ruleType_Name, string ruleType_Desc, Nullable<long> active_Flag, string last_Modified_By, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                _context.HXR_UPDATE_RULE_TYPE_SP(ruleType_ID, ruleType_Name, active_Flag, ruleType_Desc, last_Modified_By, OutPut_status_Code, OutPut_message);
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

        }

        #endregion

        #region RuleError
        public void SaveRuleError(string client_ID, string project_ID, string errorCode, string errorDescription, string inserted_by, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                _context.HXR_INSERT_ERROR_SP(client_ID, project_ID, errorCode, errorDescription, inserted_by, OutPut_status_Code, OutPut_message);
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

        }
        public void UpdateRuleError(string client_ID, string project_ID, string errorCode, string errorDescription, Nullable<long> active_Flag, string last_Modified_By, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                _context.HXR_UPDATE_ERROR_SP(client_ID, project_ID, errorCode, errorDescription, active_Flag, last_Modified_By, OutPut_status_Code, OutPut_message);
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

        }

        #endregion
        public int InsertKeyColumns(string client_ID, string project_ID, long config_ID, string table_name, string key_column1, string key_column2, string key_column3, string key_column4, string key_column5, string created_by, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {

                //var result = _context.HXR_INSERT_KEY_COLUMN_SP(client_ID, project_ID, config_ID, table_name, key_column1, key_column2, key_column3, key_column4, key_column5, created_by, OutPut_status_Code, OutPut_message);
                var result = _context.CMN_INSERT_KEY_COLUMN_SP(client_ID, project_ID, config_ID, table_name, key_column1, key_column2, key_column3, key_column4, key_column5, created_by, OutPut_status_Code, OutPut_message);



                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                //return result;
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

        public List<CMN_GET_KEY_COLUMNS_SP_Result> GetKeyColumns(string client_ID, string project_ID, string table_name, Nullable<long> config_ID, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            try
            {
                //var result = _context.HXR_GET_KEY_COLUMNS_SP(client_ID, project_ID, table_name, OutPut_status_Code, OutPut_message).ToList<HXR_GET_KEY_COLUMNS_SP_Result>();
                var result = _context.CMN_GET_KEY_COLUMNS_SP(client_ID, project_ID, table_name, config_ID, OutPut_status_Code, OutPut_message).ToList<CMN_GET_KEY_COLUMNS_SP_Result>();

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



        public void Offline_Run(string client_ID, string project_ID, Nullable<long> tool_ID, Nullable<long> config_ID, string template_Name, string template_Path,
            string table_Name, string column_Name, string run_Status, string job_Description, string procedure_Name, string parameter_List, string created_by,
            ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                _context.CMN_INST_OFLINE_BATCH_JOBS_SP(client_ID, project_ID, tool_ID, config_ID, template_Name, template_Path, table_Name, column_Name, null, run_Status,
                    job_Description, procedure_Name, parameter_List, created_by, OutPut_status_Code, OutPut_message);
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

        }

        public List<CMN_GET_OFFLINE_JOB_STATS_SP_Result> GetOfflineStatus(HXROfflineStatusEntity offline, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            try
            {

                var result = _context.CMN_GET_OFFLINE_JOB_STATS_SP(offline.Client_ID, offline.Project_ID, offline.Tool_ID, OutPut_status_Code, OutPut_message).ToList<CMN_GET_OFFLINE_JOB_STATS_SP_Result>();

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
        public DataTable GetTableData(string client_ID, string project_ID, string config_ID, string table_name, string ColumnList, byte IsDistinct, ref string status_Code, ref string message)
        {
            string _Proc = "[COMMON_GET_TABLE_DATA_SP]";
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;
            DataTable _dtTableData = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(providerConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(_Proc, con))
                    {
                        if (con.State != ConnectionState.Open) con.Open();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Client_ID", client_ID);
                        cmd.Parameters.AddWithValue("@Project_ID", project_ID);
                        cmd.Parameters.AddWithValue("@Table_Name", table_name);
                        cmd.Parameters.AddWithValue("@Config_ID", config_ID);
                        cmd.Parameters.AddWithValue("@Column_name", ColumnList);
                        cmd.Parameters.AddWithValue("@IS_Distinct", IsDistinct);
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;


                        //cmd.ExecuteNonQuery();
                        using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
                        {
                            _da.Fill(_dtTableData);
                        }
                        status_Code = cmd.Parameters["@Status_Code"].Value.ToString();
                        message = cmd.Parameters["@Message"].Value.ToString();
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
                return _dtTableData;
            }
            catch (Exception _e)
            {
                status_Code = "-1";
                message = "-1";
                return null;
            }
        }
        public List<HXR_GET_RULE_ERROR_SUMMARY_SP_Result> GetRuleErrorSummary(string client_ID, string project_ID, string table_Name, string run_Number, long? rule_Category_Id, long? rule_ID, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            try
            {

                var result = _context.HXR_GET_RULE_ERROR_SUMMARY_SP(client_ID, project_ID, table_Name, run_Number, rule_Category_Id, rule_ID, OutPut_status_Code, OutPut_message).ToList<HXR_GET_RULE_ERROR_SUMMARY_SP_Result>();

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

        #endregion

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
