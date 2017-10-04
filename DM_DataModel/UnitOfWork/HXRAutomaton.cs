using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using DM_BusinessEntities;

namespace DM_DataModel.UnitOfWork
{
    public class HXRAutomaton
    {
        #region Private member variables...
        DM_MetaDataEntities _context = null;
        #endregion
        public HXRAutomaton()
        {
            _context = new DM_MetaDataEntities();
        }

        public List<string> GetMetaDataTableNames(string client_ID, string project_ID, string config_ID, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {

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


                throw e;
            }


        }
        public List<string> GetMetaDataConnectionList(string client_ID, string project_ID, long Tool_ID, string Sourcetarget, ref string Source_code, long? RoleId, ref string Message)
        {
            List<string> list = new List<string>();
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            try
            {
                ObjectResult<ETL_GET_CONN_STRING_SP_Result> objresult = _context.ETL_GET_CONN_STRING_SP(client_ID, project_ID, Tool_ID, RoleId, Sourcetarget, null, OutPut_status_Code, OutPut_message);
                list = objresult.Select(r => r.Server_IP_Address + ":" + r.Source_Target + "," + r.Config_ID).ToList();
                Source_code = OutPut_status_Code.Value.ToString();
                Message = OutPut_message.Value.ToString();
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
            return list;
        }
        public List<string> GetMetaDataTableNameList(string client_ID, string project_ID, string source_Target, long Tool_Id, long? RoleId, ref string status_Code, ref string message)
        {
            List<string> list = new List<string>();
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            try
            {
                ObjectResult<ETL_GET_SOURCE_TARGET_TABLE_SP_Result> objresult = _context.ETL_GET_SOURCE_TARGET_TABLE_SP(client_ID, project_ID, RoleId, source_Target, Tool_Id, OutPut_status_Code, OutPut_message);
                list = objresult.Select(r => r.Table_Name + ":" + r.Database_name).ToList();
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


                throw e;
            }
            return list;
        }
        public List<string> GetTransformationDesc(string client_ID, string project_ID, long Tool_Id, ref string status_Code, ref string message)
        {
            List<string> list = new List<string>();
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            try
            {
                var objresult = _context.ETL_GET_TRANS_METADATA("AUTOMATON", OutPut_status_Code, OutPut_message);
                list = objresult.Select(r => r.Trans_Type + ":" + r.Trans_Description).ToList();
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


                throw e;
            }
            return list;
        }
        public List<string> GetTransSourceTargetTable(string client_ID, string project_ID, string TemplateName, string type, ref string status_Code, ref string message)
        {
            List<string> list = new List<string>();
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            try
            {
                ObjectResult<ETL_GET_TEMPLATE_TABLE_SP_Result> objresult = _context.ETL_GET_TEMPLATE_TABLE_SP(client_ID, project_ID, TemplateName, type, OutPut_status_Code, OutPut_message);
                list = objresult.Select(r => r.Table_Name + ":" + r.Table_Name).ToList();
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


                throw e;
            }
            return list;
        }
        public List<string> GetTemplateSourceTableList(string client_ID, string project_ID, string TemplateName, ref string status_Code, ref string message)
        {
            List<string> list = new List<string>();
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            try
            {
                ObjectResult<ETL_GET_SOURCE_SP_Result> objresult = _context.ETL_GET_SOURCE_SP(client_ID, project_ID, null, null, TemplateName, null, null, null, null, null, null, null, null, null, null, null, OutPut_status_Code, OutPut_message);
                list = objresult.Select(r => r.Business_name).Distinct().ToList();
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


                throw e;
            }
            return list;
        }
        public List<string> GetTemplateTargetTableList(string client_ID, string project_ID, string TemplateName, string type, ref string status_Code, ref string message)
        {
            List<string> list = new List<string>();
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            try
            {
                ObjectResult<ETL_GET_TEMPLATE_TABLE_SP_Result> objresult = _context.ETL_GET_TEMPLATE_TABLE_SP(client_ID, project_ID, TemplateName, type, OutPut_status_Code, OutPut_message);
                list = objresult.Select(r => r.Table_Name + ":" + r.Table_Name).ToList();
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


                throw e;
            }
            return list;
        }
        public List<string> GetTemplateTransTableList(string client_ID, string project_ID, string TemplateName, string type, ref string status_Code, ref string message)
        {
            List<string> list = new List<string>();
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            try
            {
                ObjectResult<ETL_GET_TEMPLATE_TABLE_SP_Result> objresult = _context.ETL_GET_TEMPLATE_TABLE_SP(client_ID, project_ID, type, TemplateName, OutPut_status_Code, OutPut_message);
                list = objresult.Select(r => r.Table_Name + ":" + r.Table_Name).ToList();
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


                throw e;
            }
            return list;
        }
        public List<string> GetSourceTargetColumnsByTableName(string client_ID, string project_ID, string TemplateName, string Table_Name, string type, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            List<string> list = new List<string>();
            try
            {
                List<ETL_GET_TEMPLATE_TABLE_COLUMNS_SP_Result> objresult = _context.ETL_GET_TEMPLATE_TABLE_COLUMNS_SP(client_ID, project_ID, TemplateName, Table_Name, OutPut_status_Code, OutPut_message).ToList<ETL_GET_TEMPLATE_TABLE_COLUMNS_SP_Result>();
                //var result = _context.HXR_GET_TABLE_COLUMN_LIST_SP(client_ID, project_ID, null, null, null, Table_Name, OutPut_status_Code, OutPut_message).ToList<string>();
                list = objresult.Select(r => r.Column_Name + ":" + r.Column_Name).ToList();
                message = OutPut_message.Value.ToString();
                return list;
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
        public List<CMN_GET_ENTITY_LIST_SP_Result> GetEntityList(string client_ID, string project_ID, long Config_ID, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {

                //var result = _context.CMN_GET_ENTITY_LIST_SP(client_ID, project_ID, Config_ID ,OutPut_status_Code, OutPut_message).ToList<CMN_GET_ENTITY_LIST_SP_Result>();
                var result = _context.CMN_GET_ENTITY_LIST_SP(client_ID, project_ID, Config_ID, OutPut_status_Code, OutPut_message)
                    .ToList<CMN_GET_ENTITY_LIST_SP_Result>();
                var Index = 1;
                if (result.Count > 1 && result.Where(t => t.Ordinal_Position > 0).Count() <= 0)
                    foreach (var item in result)
                    {
                        item.Ordinal_Position = Index;
                        Index++;
                    }

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
        public List<CMN_GET_ENTITY_COLS_SP_Result> GetEntityColList(string client_ID, string project_ID, Nullable<long> entity_ID, long Config_ID, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {

                var result = _context.CMN_GET_ENTITY_COLS_SP(client_ID, project_ID, entity_ID, Config_ID, OutPut_status_Code, OutPut_message).ToList<CMN_GET_ENTITY_COLS_SP_Result>();

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
        public void SaveEntityData(DataTable dt, ref string status_Code, ref string message)
        {
            string dynamicProc = "[CMN_UPDATE_ENTITY_DETAILS_SP]";

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

        public void SaveEntityColData(DataTable dt, ref string status_Code, ref string message)
        {
            string dynamicProc = "[CMN_UPDATE_ENTITY_ATTRS_SP]";

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
        public List<ETL_GET_COLUMN_DATA_TYPE_SP_Result> GetTransTableData(string client_ID, string project_ID, string TemplateName, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                string col = string.Empty;
                string configid = string.Empty;
                string sourcetarget = string.Empty;
                var result = _context.ETL_GET_COLUMN_DATA_TYPE_SP(client_ID, project_ID, TemplateName, OutPut_status_Code, OutPut_message).ToList<ETL_GET_COLUMN_DATA_TYPE_SP_Result>();
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
        public List<string> GetTemplateList(string client_ID, string project_ID, string Type, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            List<string> list = new List<string>();
            try
            {
                string col = string.Empty;
                string configid = string.Empty;
                string sourcetarget = string.Empty;
                List<ETL_GET_SOURCE_TEMPLATE_LIST_SP_Result> objresult = _context.ETL_GET_SOURCE_TEMPLATE_LIST_SP(client_ID, project_ID, Type, OutPut_status_Code, OutPut_message).ToList<ETL_GET_SOURCE_TEMPLATE_LIST_SP_Result>();
                list = objresult.Select(r => r.Template_Name + ":" + r.Template_ID).Distinct().ToList();
                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                return list;
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
        public List<ETL_GET_SOURCE_TEMPLATE_SP_Result> GetSourceTemplateList(string client_ID, string project_ID, string Template_ID, string Template_Name, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                string col = string.Empty;
                string configid = string.Empty;
                string sourcetarget = string.Empty;
                var result = _context.ETL_GET_SOURCE_TEMPLATE_SP(client_ID, project_ID, Template_ID, Template_Name, OutPut_status_Code, OutPut_message).ToList<ETL_GET_SOURCE_TEMPLATE_SP_Result>();
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

        public List<ETL_GET_TEMPLATE_SOURCE_TARGET_TABLE_SP_Result> GetTemplateSourceTargetTableList(string client_ID, string project_ID, string Template_ID, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                string col = string.Empty;
                string configid = string.Empty;
                string sourcetarget = string.Empty;
                var result = _context.ETL_GET_TEMPLATE_SOURCE_TARGET_TABLE_SP(client_ID, project_ID, Convert.ToInt64(Template_ID), OutPut_status_Code, OutPut_message).ToList<ETL_GET_TEMPLATE_SOURCE_TARGET_TABLE_SP_Result>();
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

        public List<ETL_GET_TARGET_TEMPLATE_SP_Result> GetTargetTemplateList(string client_ID, string project_ID, string Template_ID, string Template_Name, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                string col = string.Empty;
                string configid = string.Empty;
                string sourcetarget = string.Empty;
                var result = _context.ETL_GET_TARGET_TEMPLATE_SP(client_ID, project_ID, Template_ID, Template_Name, OutPut_status_Code, OutPut_message).ToList<ETL_GET_TARGET_TEMPLATE_SP_Result>();
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
        public List<ETL_GET_TRANS_TEMPLATE_SP_Result> GetTransTemplateList(string client_ID, string project_ID, string Template_ID, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                string col = string.Empty;
                string configid = string.Empty;
                string sourcetarget = string.Empty;
                var result = _context.ETL_GET_TRANS_TEMPLATE_SP(client_ID, project_ID, Template_ID, OutPut_status_Code, OutPut_message).ToList<ETL_GET_TRANS_TEMPLATE_SP_Result>();

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
        public string SaveSourceGrid(string Client_ID, string Project_ID, string Connection_ID, long Template_ID, string Template_Name, string Table_Name,
            string Field_Seq_No, string Field_Name, string Field_Prec, string Field_Scale, string Field_Key, string Field_Data_Type, string description,
            string FieldType, string datefeed, string Createdby, ref string Source_code, ref string Message)
        {
            string tempid = "";
            try
            {
                var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
                var OutPut_message = new ObjectParameter("message", typeof(string));

                long? Connectid = 0;
                long? FieldSeqNo = 0;
                Connectid = Convert.ToInt64(Connection_ID);
                FieldSeqNo = Convert.ToInt64(Field_Seq_No);
                if (Connectid <= 0)
                {
                    Connectid = null;
                }
                if (FieldSeqNo <= 0)
                {
                    FieldSeqNo = null;
                }
                _context.ETL_INSERT_SOURCE_SP(Client_ID, Project_ID, Connectid, Template_Name, Table_Name, FieldSeqNo, Field_Name, Field_Prec, Field_Scale, Field_Key,
                    Field_Data_Type, description, FieldType, datefeed, Createdby, Template_ID, OutPut_status_Code, OutPut_message);


                Source_code = OutPut_status_Code.Value.ToString();
                Message = OutPut_message.Value.ToString();



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
            return tempid;
        }
        public void SaveTargetGrid(string Client_ID, string Project_ID, string Connection_ID, string Template_ID, string Template_Name, string Table_Name, string Field_Seq_No, string Field_Name, string Field_Prec, string Field_Scale, string Field_Key, string Field_Data_Type, string Description, string Field_data, string createdby, string modifyby, ref string Source_code, ref string Message)
        {
            try
            {
                var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
                var OutPut_message = new ObjectParameter("message", typeof(string));
                long? Connectid = 0;
                long? FieldSeqNo = 0;
                Connectid = Convert.ToInt64(Connection_ID);
                FieldSeqNo = Convert.ToInt64(Field_Seq_No);
                if (Connectid <= 0)
                {
                    Connectid = null;
                }
                if (FieldSeqNo <= 0)
                {
                    FieldSeqNo = null;
                }
                _context.ETL_INSERT_TARGET_SP(Client_ID, Project_ID, Connectid, Template_ID, Template_Name, Table_Name, FieldSeqNo, Field_Name, Field_Prec, Field_Scale, Field_Key, Field_Data_Type, Description, Field_data, createdby, modifyby, OutPut_status_Code, OutPut_message);

                Source_code = OutPut_status_Code.Value.ToString();
                Message = OutPut_message.Value.ToString();
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
        public void SaveTransformGrid(string Client_ID, string Project_ID, string Template_Name, string Table_Name, string Column_Name, string Data_Type, string tran_Name, string Trans_type, string Field_length, string Trans_order, string Trans_rule, string Template_Id, string createdby, ref string Source_code, ref string Message)
        {
            try
            {
                var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
                var OutPut_message = new ObjectParameter("message", typeof(string));
                int? Trorder = 0;
                Trorder = Convert.ToInt32(Trans_order);
                if (Trorder <= 0)
                {
                    Trorder = null;
                }
                long? FiSeqNo = 0;
                FiSeqNo = Convert.ToInt64(Trans_order);
                if (FiSeqNo <= 0)
                {
                    FiSeqNo = null;
                }
                _context.ETL_INSERT_TRANS_SP(Client_ID, Project_ID, FiSeqNo, Table_Name, tran_Name, Column_Name, Trans_rule, Data_Type, Field_length, Trans_type, Trorder, "Transition", Template_Id, Template_Name, createdby, OutPut_status_Code, OutPut_message);
                Source_code = OutPut_status_Code.Value.ToString();
                Message = OutPut_message.Value.ToString();
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
        public void TransAdd(string Client_ID, string Project_ID, string Tool_ID, string TemplateName, string TransName, string TransType, string TransTable, string tblcolumn, string TransRule, ref string Source_code, ref string Message)
        {
            try
            {
                var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
                var OutPut_message = new ObjectParameter("message", typeof(string));
                long? ToolID = 0;
                ToolID = Convert.ToInt64(Tool_ID);
                if (ToolID <= 0)
                {
                    ToolID = null;
                }
                _context.ETL_INSERT_ADDTRANS_SP(Client_ID, Project_ID, ToolID, TemplateName, TransName, TransType, TransTable, tblcolumn, TransRule, OutPut_status_Code, OutPut_message);
                Source_code = OutPut_status_Code.Value.ToString();
                Message = OutPut_message.Value.ToString();
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
        public void ModifySourceGrid(string Client_ID, string Project_ID, long? RowID, string Table_Name, string Connection_ID, string Template_ID, string Template_Name, string Field_Seq_No, string Field_Name, string Description, string Field_Data_Type, string Field_Prec, string Field_Scale, string Field_Key, string Field_Type, string Field_Data, string BusinessName, string Modifyby, ref string Source_code, ref string Message)
        {
            try
            {
                var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
                var OutPut_message = new ObjectParameter("message", typeof(string));
                long? Connectid = 0;
                long? FieldSeqNo = 0;
                Connectid = Convert.ToInt64(Connection_ID);
                FieldSeqNo = Convert.ToInt64(Field_Seq_No);
                if (Connectid <= 0)
                {
                    Connectid = null;
                }
                if (FieldSeqNo <= 0)
                {
                    FieldSeqNo = null;
                }
                _context.ETL_UPDATE_SOURCE_SP(Client_ID, Project_ID, RowID, Table_Name, Connectid, Template_ID, Template_Name, FieldSeqNo, Field_Name, Description, Field_Data_Type, Field_Prec, Field_Scale, Field_Key, Field_Type, Field_Data, BusinessName, Modifyby, OutPut_status_Code, OutPut_message);

                Source_code = OutPut_status_Code.Value.ToString();
                Message = OutPut_message.Value.ToString();
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
        public string ModifyTargetGrid(string Client_ID, string Project_ID, long? RowID, string Table_Name, string Connection_ID, string Template_ID, string Template_Name, string Field_Seq_No, string Field_Name, string Description, string Field_Data_Type, string Field_Prec, string Field_Scale, string Field_Key, string Field_Type, string Field_Data, string BusinessName, string Modifyby, ref string Source_code, ref string Message)
        {
            string tempid = "";
            try
            {
                var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
                var OutPut_message = new ObjectParameter("message", typeof(string));
                var OutTemplateId = new ObjectParameter("Tempalte_ID", typeof(string));
                long? Connectid = 0;
                long? FieldSeqNo = 0;
                Connectid = Convert.ToInt64(Connection_ID);
                FieldSeqNo = Convert.ToInt64(Field_Seq_No);
                if (Connectid <= 0)
                {
                    Connectid = null;
                }
                if (FieldSeqNo <= 0)
                {
                    FieldSeqNo = null;
                }

                _context.ETL_UPDATE_TARGET_SP(Client_ID, Project_ID, RowID, Table_Name, Connectid, Template_ID, Template_Name, FieldSeqNo, Field_Name, Description, Field_Data_Type, Field_Prec, Field_Scale, Field_Key, Field_Type, Field_Data, BusinessName, Modifyby, OutPut_status_Code, OutPut_message);

                Source_code = OutPut_status_Code.Value.ToString();
                Message = OutPut_message.Value.ToString();


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
            return tempid;
        }
        public string ModifyTransGrid(string Client_ID, string Project_ID, long Trans_ID, string Trans_Name, string Trans_Field, string Trans_Rule, string Field_Type, string Field_Length, string Trans_Type, string Trans_Order, string SourceName, string table_name, string Field_name, string Modifyby, ref string Source_code, ref string Message)
        {
            string tempid = "";
            try
            {
                var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
                var OutPut_message = new ObjectParameter("message", typeof(string));


                int? Transorder = 0;
                //long? FieldSeqNo = 0;
                //FieldSeqNo = Convert.ToInt64(Field_Seq_No);
                //if (FieldSeqNo <= 0)
                //{
                //    FieldSeqNo = null;
                //}
                Transorder = Convert.ToInt32(Trans_Order);
                if (Transorder <= 0)
                {
                    Transorder = null;
                }
                _context.ETL_UPDATE_TRANS_SP(Client_ID, Project_ID, Trans_ID, Trans_Name, Trans_Field, Trans_Rule, Field_Type, Field_Length, Trans_Type, Transorder, table_name, Field_name, "transition", Modifyby, OutPut_status_Code, OutPut_message);

                Source_code = OutPut_status_Code.Value.ToString();
                Message = OutPut_message.Value.ToString();
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
            return tempid;
        }
        public void DeleteSourceGrid(long RowID, ref string Source_code, ref string Message)
        {

            try
            {
                var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
                var OutPut_message = new ObjectParameter("message", typeof(string));
                _context.ETL_DELETE_SOURCE_SP(RowID, OutPut_status_Code, OutPut_message);
                Source_code = OutPut_status_Code.Value.ToString();
                Message = OutPut_message.Value.ToString();
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
        public void DeleteTargetGrid(long RowID, ref string Source_code, ref string Message)
        {

            try
            {
                var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
                var OutPut_message = new ObjectParameter("message", typeof(string));
                _context.ETL_DELETE_TARGET_SP(RowID, OutPut_status_Code, OutPut_message);
                Source_code = OutPut_status_Code.Value.ToString();
                Message = OutPut_message.Value.ToString();
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
        public void DeleteTransGrid(long Trans_ID, ref string Source_code, ref string Message)
        {

            try
            {
                var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
                var OutPut_message = new ObjectParameter("message", typeof(string));
                _context.ETL_DELETE_TRANS_SP(Trans_ID, OutPut_status_Code, OutPut_message);
                Source_code = OutPut_status_Code.Value.ToString();
                Message = OutPut_message.Value.ToString();
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
        public DataTable GetBatchData(string client_ID, string project_ID, long? config_ID, string table_name, string BatchID, int page_no, int recordsperpage, ref  string status_Code, ref string message, ref long TotalCount)
        {
            string dynamicProc = "[COMMON_GET_BATCH_DATA_SP]";
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
                        cmd.Parameters.AddWithValue("@Client_ID", client_ID);
                        cmd.Parameters.AddWithValue("@Project_ID", project_ID);
                        cmd.Parameters.AddWithValue("@Config_ID", config_ID);
                        cmd.Parameters.AddWithValue("@Batch_ID ", BatchID);
                        cmd.Parameters.AddWithValue("@Table_Name", table_name);
                        //cmd.Parameters.AddWithValue("@PageNo", page_no);
                        //cmd.Parameters.AddWithValue("@RecordsPerPage", recordsperpage); 
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                        //cmd.Parameters.Add("@TotalCount", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        //cmd.ExecuteNonQuery();
                        using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
                        {
                            _da.Fill(_dtData);
                        }
                        status_Code = cmd.Parameters["@Status_Code"].Value.ToString();
                        message = cmd.Parameters["@Message"].Value.ToString();
                        //TotalCount = Convert.ToInt64(cmd.Parameters["@TotalCount"].Value.ToString());
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
        public List<ETL_GET_SOURCE_TEMPLATE_LIST_SP_Result> GetTemplateNameList(string client_ID, string project_ID, string Type, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            List<string> list = new List<string>();
            try
            {
                string col = string.Empty;
                string configid = string.Empty;
                string sourcetarget = string.Empty;
                List<ETL_GET_SOURCE_TEMPLATE_LIST_SP_Result> objresult = _context.ETL_GET_SOURCE_TEMPLATE_LIST_SP(client_ID, project_ID, Type, OutPut_status_Code, OutPut_message).ToList<ETL_GET_SOURCE_TEMPLATE_LIST_SP_Result>();
                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                return objresult;
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
        public void CopyTemplate(string client_ID, string project_ID, long OldTemplateID, string NewTemplateName, string CreatedBy, ref long New_Template_ID, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            var Output_NewTemplate_ID = new ObjectParameter("New_Template_ID", typeof(long));
            List<string> list = new List<string>();
            try
            {
                string col = string.Empty;

                _context.ATMTN_COPY_TEMPLATE_SP(client_ID, project_ID, OldTemplateID, NewTemplateName, CreatedBy, Output_NewTemplate_ID, OutPut_status_Code, OutPut_message);

                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                New_Template_ID = Convert.ToInt64(Output_NewTemplate_ID.Value);

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
        public List<ETL_GET_COLUMN_DETAIL_SP_Result> GetColumnDetail(string client_ID, string project_ID, string TemplateName, string TableName, string FieldName, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                string col = string.Empty;
                string configid = string.Empty;
                string sourcetarget = string.Empty;
                var result = _context.ETL_GET_COLUMN_DETAIL_SP(client_ID, project_ID, TemplateName, TableName, FieldName, OutPut_status_Code, OutPut_message).ToList<ETL_GET_COLUMN_DETAIL_SP_Result>();
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
        public List<ATMTN_GET_TBL_EXPORT_STATUS_SP_Result> GetExportStatus(AutomatonExportStatus _s, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                string col = string.Empty;
                string configid = string.Empty;
                string sourcetarget = string.Empty;
                var result = _context.ATMTN_GET_TBL_EXPORT_STATUS_SP(_s.Client_ID, _s.Project_ID, _s.Config_ID, _s.Run_User, OutPut_status_Code, OutPut_message).ToList<ATMTN_GET_TBL_EXPORT_STATUS_SP_Result>();


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

            //string _Proc = "[COMMON_GET_TABLE_DATA_SP]";
            //var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            //string providerConnectionString = builder.ProviderConnectionString;
            //DataTable _dtTableData = new DataTable();
            //try
            //{
            //    using (SqlConnection con = new SqlConnection(providerConnectionString))
            //    {
            //        using (SqlCommand cmd = new SqlCommand(_Proc, con))
            //        {
            //            if (con.State != ConnectionState.Open) con.Open();
            //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //            cmd.Parameters.AddWithValue("@Client_ID", client_ID);
            //            cmd.Parameters.AddWithValue("@Project_ID", project_ID);
            //            cmd.Parameters.AddWithValue("@Table_Name", table_name);
            //            cmd.Parameters.AddWithValue("@Config_ID", config_ID);
            //            cmd.Parameters.AddWithValue("@No_Of_Rows", NumberOfRows);
            //            cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
            //            cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;


            //            //cmd.ExecuteNonQuery();
            //            using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
            //            {
            //                _da.Fill(_dtTableData);
            //            }
            //            status_Code = cmd.Parameters["@Status_Code"].Value.ToString();
            //            message = cmd.Parameters["@Message"].Value.ToString();                        
            //        }
            //        if (con.State != ConnectionState.Closed) con.Close();
            //    }
            //    return _dtTableData;
            //}
            //catch (Exception _e)
            //{
            //    status_Code = "-1";
            //    message = "-1";
            //    return null;
            //}

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
        public void SubmitDownloadRequest(AutomatonExportStatus _s, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {

                var result = _context.ATMTN_REQUEST_TBL_EXPORT_SP(_s.Client_ID, _s.Project_ID, _s.Config_ID, _s.Table_Name, _s.Folder_Path, _s.Output_Format, _s.Run_User,
                OutPut_status_Code, OutPut_message);

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


                throw e;
            }
        }


        public void LogRunAudit(string client_ID, string project_ID, string TemplateName, string Modified_by, long? Run_ID, ref long Run_ID_OUTPUT, long? RoleId, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            var OutPut_Run_ID = new ObjectParameter("Run_ID_OUTPUT", typeof(long));

            try
            {
                _context.ATMTN_INSERT_RUN_AUDIT_SP(client_ID, project_ID, TemplateName, Modified_by, Run_ID, OutPut_Run_ID, RoleId, OutPut_status_Code, OutPut_message);

                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                Run_ID_OUTPUT = Convert.ToInt64(OutPut_Run_ID.Value);


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
        public void LogExcelRunAudit(string client_ID, string project_ID, string Tool_ID, string Source_Table_Name, string Source_Column_count, string Source_Record_Count, 
            long? Batch_ID, string Upload_status, string Target_Table_name, string Modified_by, long? Run_ID,ref long Run_ID_OUTPUT, long? RoleId,
            ref string status_Code, ref string message)
        {

            string _Proc = "[CMN_EXCL_UPLOAD_AUDIT_SP]";
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
                        cmd.Parameters.AddWithValue("@Source_Table_Name", Source_Table_Name);
                        cmd.Parameters.AddWithValue("@Target_Table_name", Target_Table_name);
                        cmd.Parameters.AddWithValue("@Source_Column_count", Source_Column_count);
                        cmd.Parameters.AddWithValue("@Source_Record_Count", Source_Record_Count);
                        cmd.Parameters.AddWithValue("@Batch_ID", Batch_ID);
                        cmd.Parameters.AddWithValue("@Upload_status", Upload_status);
                        cmd.Parameters.AddWithValue("@Tool_ID", Tool_ID);
                        cmd.Parameters.AddWithValue("@Role_ID", RoleId);
                        cmd.Parameters.AddWithValue("@Modified_by", Modified_by);
                        cmd.Parameters.AddWithValue("@Run_ID", Run_ID);
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Run_ID_OUTPUT", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        status_Code = Convert.ToString(cmd.Parameters["@Status_Code"].Value);
                        message = Convert.ToString(cmd.Parameters["@Message"].Value);
                        Run_ID_OUTPUT = Convert.ToInt64(cmd.Parameters["@Run_ID_OUTPUT"].Value);
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

        public DataTable Get_MASK_TABLE_COLUMNS(string client_ID, string project_ID, string table_name, long? config_ID, DataTable Temptable, ref  string status_Code, ref string message)
        {

            string dynamicProc = "[CMN_MASK_TABLE_COLUMNS_SP]";
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
                        cmd.Parameters.AddWithValue("@Client_ID", client_ID);
                        cmd.Parameters.AddWithValue("@Project_ID", project_ID);
                        cmd.Parameters.AddWithValue("@Table_Name", table_name);
                        cmd.Parameters.AddWithValue("@Config_id", config_ID);
                        cmd.Parameters.AddWithValue("@Temptable", Temptable);
                        //cmd.Parameters.AddWithValue("@Column_List_query", Column_List_query);
                        //cmd.Parameters.AddWithValue("@Column_List", Column_List);
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

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

        public List<CMN_GET_DATA_MASK_TYPE_SP_Result> GetMaskTypes(string column_Type, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {

                var result = _context.CMN_GET_DATA_MASK_TYPE_SP(column_Type, OutPut_status_Code, OutPut_message).ToList<CMN_GET_DATA_MASK_TYPE_SP_Result>();

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


        public void AddTemplateParameter(string client_ID, string project_ID, string tool_ID, long template_ID, string templateName, string parameterName, string parameterValue, string PacakageSaveLocation, string Createdby, ref string status_Code, ref string Message)
        {
            try
            {
                var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
                var OutPut_message = new ObjectParameter("message", typeof(string));

                _context.ETL_INSERT_VARIABLE_PARAM_SP(client_ID, project_ID, template_ID, templateName, parameterName, parameterValue, PacakageSaveLocation, Convert.ToInt64(tool_ID), Createdby, OutPut_status_Code, OutPut_message);
                status_Code = OutPut_status_Code.Value.ToString();
                Message = OutPut_message.Value.ToString();
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
        public void UpdateTemplateParameter(long ParameterID, string parameterName, string parameterValue, string ModifiedBy, ref string status_Code, ref string Message)
        {
            try
            {
                var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
                var OutPut_message = new ObjectParameter("message", typeof(string));
                _context.ETL_UPDT_VARIABLE_PARAM_SP(ParameterID, parameterName, parameterValue, ModifiedBy, OutPut_status_Code, OutPut_message);
                status_Code = OutPut_status_Code.Value.ToString();
                Message = OutPut_message.Value.ToString();
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
        public void DeleteTemplateParameter(long ParameterID, string ModifiedBy, ref string status_Code, ref string Message)
        {
            try
            {
                var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
                var OutPut_message = new ObjectParameter("message", typeof(string));
                _context.ETL_DELTE_VARIABLE_PARAM_SP(ParameterID, ModifiedBy, OutPut_status_Code, OutPut_message);
                status_Code = OutPut_status_Code.Value.ToString();
                Message = OutPut_message.Value.ToString();
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
        public List<ETL_GET_VARIABLE_PARAM_SP_Result> GetTemplateParameterList(string client_ID, string project_ID, string templateName, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                string col = string.Empty;
                string configid = string.Empty;
                string sourcetarget = string.Empty;
                var result = _context.ETL_GET_VARIABLE_PARAM_SP(client_ID, project_ID, templateName, OutPut_status_Code, OutPut_message).ToList<ETL_GET_VARIABLE_PARAM_SP_Result>();

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

        public long InsertTemplate(string client_ID, string project_ID, string TemplateName, string Created_By, string Type, ref string status_Code, ref string message)
        {
            long Tempalte_ID = 0;
            string _Proc = "[ETL_INSERT_TEMPLATE_SP]";
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
                        cmd.Parameters.AddWithValue("@Template_Name", TemplateName);
                        cmd.Parameters.AddWithValue("@Created_By", Created_By);
                        cmd.Parameters.AddWithValue("@Type", Type);

                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Tempalte_ID", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        status_Code = Convert.ToString(cmd.Parameters["@Status_Code"].Value);
                        message = Convert.ToString(cmd.Parameters["@Message"].Value);
                        Tempalte_ID = Convert.ToInt64(cmd.Parameters["@Tempalte_ID"].Value);
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }

            }
            catch (Exception _e)
            {
                status_Code = "-1";
                message = "-1";
            }


            return Tempalte_ID;

        }

        public void InsertOfflineBatchJobs(string client_ID, string project_ID, long? ToolID, string TemplateName, string TemplatePath, string Modified_by, string RunStatus, string JobDescription, Nullable<DateTime> scheduled_date, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                _context.CMN_INST_OFLINE_BATCH_JOBS_SP(client_ID, project_ID, ToolID, null, TemplateName, TemplatePath, null, null, scheduled_date, RunStatus, JobDescription, null, null, Modified_by, OutPut_status_Code, OutPut_message);

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


                throw e;
            }

        }

        public DataTable GetAutomatonBatchStatus(string client_id, string project_id, long? ToolID, ref string status_Code, ref string message)
        {
            string dynamicProc = "[CMN_GET_OFFLINE_JOB_STATS_SP]";
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
                        cmd.Parameters.AddWithValue("@Tool_ID", ToolID);
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
                        {
                            _da.Fill(_dtData);
                        }

                        status_Code = Convert.ToString(cmd.Parameters["@Status_Code"].Value);
                        message = Convert.ToString(cmd.Parameters["@Message"].Value);
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

        public List<CMN_GET_METADATA_BUSNAME_SP_Result> GetMetaDataTableBusinessName(string client_ID, string project_ID, string Table_name, string connectionid, ref string status_Code, ref string message)
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
                var result = _context.CMN_GET_METADATA_BUSNAME_SP(client_ID, project_ID, Table_name, null, Convert.ToInt64(Connectid), OutPut_status_Code, OutPut_message).ToList<CMN_GET_METADATA_BUSNAME_SP_Result>();
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

        public List<CMN_GUESS_DATA_TYPE_SP_Result> GetEntitySuggestedAttr(string client_id, string project_id, string table_name, string column_name, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {

                var result = _context.CMN_GUESS_DATA_TYPE_SP(client_id, project_id, table_name, column_name, OutPut_status_Code, OutPut_message).ToList<CMN_GUESS_DATA_TYPE_SP_Result>();
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

        public List<CMN_GET_COL_DATA_TYPES_SP_Result> GetTargetDBDataTypes(string client_id, string project_id, Nullable<long> config_ID, ref string status_Code,
            ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {

                var result = _context.CMN_GET_COL_DATA_TYPES_SP(client_id, project_id, config_ID, OutPut_status_Code, OutPut_message).ToList<CMN_GET_COL_DATA_TYPES_SP_Result>();

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

        public int GetTargetDBtblClmnCount(string client_ID, string project_ID, Nullable<long> config_ID, string table_Name,
            ref Nullable<long> clmnCnt, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            var OutPut_Clmn_Cnt = new ObjectParameter("column_Count", typeof(int));
            long count = 0;

            try
            {

                var result = _context.CMN_GET_TABLE_COL_COUNT_SP(client_ID, project_ID, config_ID, table_Name, OutPut_Clmn_Cnt, OutPut_status_Code, OutPut_message);

                status_Code = OutPut_status_Code.Value.ToString();
                message = OutPut_message.Value.ToString();
                long.TryParse(OutPut_Clmn_Cnt.Value.ToString(), out count);
                clmnCnt = count;

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

        public int DateColumnCheck(string client_ID, string project_ID, string table_Name, string column_name, Nullable<long> config_ID, string columnType, ref string status_Code,
            ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            try
            {
                var result = _context.CMN_CHECK_COLUMN_DATA_TYPE_SP(client_ID, project_ID, table_Name, column_name, config_ID, columnType, OutPut_status_Code, OutPut_message);

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

                //throw e;
                return -1;
            }
        }

        public DataTable GetDesignMasterDetails(string client_id, string project_id, long tool_id, long template_id, long? RoleId, ref string status_Code, ref string message)
        {
            string dynamicProc = "[ETL_GET_DESIGN_MASTER_DTLS_SP]";
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
                        cmd.Parameters.AddWithValue("@Tool_ID", tool_id);
                        cmd.Parameters.AddWithValue("@Template_ID", template_id);
                        cmd.Parameters.AddWithValue("@Role_ID", RoleId);
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
                        {
                            _da.Fill(_dtData);
                        }

                        status_Code = Convert.ToString(cmd.Parameters["@Status_Code"].Value);
                        message = Convert.ToString(cmd.Parameters["@Message"].Value);
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

        public DataSet GetDesignTableAttributeDetails(string client_id, string project_id, long tool_id, long template_id, long? RoleId, ref string status_Code, ref string message)
        {
            string dynamicProc = "[ETL_GET_TABLE_ATTRIBUTE_DTLS_SP]";
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;
            DataSet _dsData = new DataSet();
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
                        cmd.Parameters.AddWithValue("@Tool_ID", tool_id);
                        cmd.Parameters.AddWithValue("@Template_ID", template_id);
                        cmd.Parameters.AddWithValue("@Role_ID", RoleId);
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
                        {
                            _da.Fill(_dsData);
                        }

                        status_Code = Convert.ToString(cmd.Parameters["@Status_Code"].Value);
                        message = Convert.ToString(cmd.Parameters["@Message"].Value);
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
                return _dsData;
            }
            catch (Exception _e)
            {
                return null;
            }
        }

        public List<CMN_GET_SCHEDULED_JOBS_SP_Result> GetScheduledTransformation(string client_ID, string project_ID, Nullable<long> tool_ID, string trans_type, ref string status_Code, ref string message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                string col = string.Empty;
                string configid = string.Empty;
                string sourcetarget = string.Empty;
                var result = _context.CMN_GET_SCHEDULED_JOBS_SP(client_ID, project_ID, tool_ID, trans_type, OutPut_status_Code, OutPut_message).ToList<CMN_GET_SCHEDULED_JOBS_SP_Result>();

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

        public string UpdateScheduleTransformation(long jobID, Nullable<DateTime> scheduleDate, char isDel, ref string status_Code, ref string message)
        {
            string dynamicProc = "[CMN_UPDATE_SCHEDULED_JOBS_SP]";
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
                        cmd.Parameters.AddWithValue("@Offline_Job_ID", jobID);
                        cmd.Parameters.AddWithValue("@Schedule_date", scheduleDate);
                        cmd.Parameters.AddWithValue("@Is_delete", isDel);
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        status_Code = Convert.ToString(cmd.Parameters["@Status_Code"].Value);
                        message = Convert.ToString(cmd.Parameters["@Message"].Value);
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }

                return message;

            }
            catch (Exception _e)
            {
                return _e.Message;
            }
        }




        public void SaveFileUploadTemplate(AutomatonFileUploadTemplateEntity entity, ref string Template_ID, ref string StatusCode, ref string Message)
        {
            string dynamicProc = "[ETL_INSERT_TEMPLATE_SP]";
            //string Template_ID = string.Empty;
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
                        cmd.Parameters.AddWithValue("@Client_ID", entity.Client_ID);
                        cmd.Parameters.AddWithValue("@Project_ID", entity.Project_ID);
                        cmd.Parameters.AddWithValue("@Template_Name", entity.Template_Name);
                        cmd.Parameters.AddWithValue("@Created_By", entity.Created_By);
                        cmd.Parameters.AddWithValue("@Type ", entity.Type);
                        cmd.Parameters.Add("@Tempalte_ID", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        StatusCode = Convert.ToString(cmd.Parameters["@Status_Code"].Value);
                        Message = Convert.ToString(cmd.Parameters["@Message"].Value);
                        Template_ID = Convert.ToString(cmd.Parameters["@Tempalte_ID"].Value);
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }


                if (StatusCode == "0")
                {
                    dynamicProc = "[ETL_INSERT_FILE_UPLOAD_TEMPLATE_SP]";
                    using (SqlConnection con = new SqlConnection(providerConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(dynamicProc, con))
                        {
                            if (con.State != ConnectionState.Open) con.Open();
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Template_ID", Template_ID);
                            cmd.Parameters.AddWithValue("@Source_Name", entity.Source_Name);
                            cmd.Parameters.AddWithValue("@File_Location", entity.File_Location);
                            cmd.Parameters.AddWithValue("@File_Name", entity.File_Name);
                            cmd.Parameters.AddWithValue("@Prefix_Filename", entity.Prefix_FileName);
                            cmd.Parameters.AddWithValue("@Batch_Portion", entity.Batch_Portion);
                            cmd.Parameters.AddWithValue("@Batch_Name_Values", entity.Batch_Name_Values);
                            cmd.Parameters.AddWithValue("@File_Type", entity.File_Type);
                            cmd.Parameters.AddWithValue("@File_Delimiter", entity.File_Delimiter);
                            cmd.Parameters.AddWithValue("@Data_Starting_Line", entity.Data_Starting_Line);
                            cmd.Parameters.AddWithValue("@Target_Table", entity.Target_Table);
                            cmd.Parameters.AddWithValue("@Header_Row", entity.Header_Row);
                            cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                            cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();

                            StatusCode = Convert.ToString(cmd.Parameters["@Status_Code"].Value);
                            Message = Convert.ToString(cmd.Parameters["@Message"].Value);
                        }
                        if (con.State != ConnectionState.Closed) con.Close();
                    }
                }

            }
            catch (Exception _e)
            {
                Message = _e.Message;
            }
        }

        public void CheckETLThresholdLimit(string client_ID, string project_ID, Nullable<long> template_ID, ref string StatusCode, ref string Message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                _context.ETL_GET_THRESH_LIMIT_SP(client_ID, project_ID, template_ID, OutPut_status_Code, OutPut_message);

                StatusCode = OutPut_status_Code.Value.ToString();
                Message = OutPut_message.Value.ToString();

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
        public void GenerateReconcile(string client_ID, string project_ID, string template_ID, int RoleId, string UpdatedBy, ref string StatusCode, ref string Message)
        {
            var OutPut_status_Code = new ObjectParameter("Status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("Message", typeof(string));

            try
            {
                //_context.DRD_INSERT_AUTOMATON_TEMPLATE_SP(client_ID, project_ID, template_ID, RoleId, UpdatedBy, OutPut_status_Code, OutPut_message);
                _context.DRD_INS_AUTOMATON_TEMPLATE_SP(client_ID, project_ID, template_ID, RoleId, UpdatedBy, OutPut_status_Code, OutPut_message);

                StatusCode = OutPut_status_Code.Value.ToString();
                Message = OutPut_message.Value.ToString();

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
        public List<ETL_GET_SOURCE_TARGET_MAPPING_DTLS_SP_Result> GetMetaDataTableDetailAutomatic(string client_ID, string project_ID, long? Template_ID, long? Role_Id, string Table_name, string flagtype, string connectionid, ref string status_Code, ref string message)
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
                var result = _context.ETL_GET_SOURCE_TARGET_MAPPING_DTLS_SP(client_ID, project_ID, Template_ID, Role_Id, Table_name, flagtype, OutPut_status_Code, OutPut_message).ToList<ETL_GET_SOURCE_TARGET_MAPPING_DTLS_SP_Result>();
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


    }

}
