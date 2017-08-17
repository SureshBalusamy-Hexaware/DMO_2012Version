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
using System.Security.Cryptography;
using System.IO;

namespace DM_DataModel.UnitOfWork
{
    public class DASEM : IDisposable
    {
        #region Private member variables...
        DM_MetaDataEntities _context = null;
        #endregion
        public DASEM()
        {
            _context = new DM_MetaDataEntities();
        }
        private string ConnectionString()
        {
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            return builder.ProviderConnectionString;

        }
        #region Public Repository Creation properties...

        #endregion

        #region Public member methods....

        public List<DASM_GET_TEMPLATE_SP_Result> GetAllTemplates(string client_ID, string project_ID, long? RoleId, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.DASM_GET_TEMPLATE_SP(client_ID, project_ID, RoleId, OutPut_status_Code, OutPut_message).ToList<DASM_GET_TEMPLATE_SP_Result>();

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

        public List<DASM_GET_SLICING_RUNID_SP_Result> GetRunIDList(string client_ID, string project_ID, long? Template_ID, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.DASM_GET_SLICING_RUNID_SP(client_ID, project_ID, Template_ID, OutPut_status_Code, OutPut_message).ToList<DASM_GET_SLICING_RUNID_SP_Result>();

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

        //JA:20170313 : 
        public List<DASM_GET_MASKING_TEMPLATE_SP_Result> GetAllMaskingTemplates(string client_ID, string project_ID, long? RoleId, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.DASM_GET_MASKING_TEMPLATE_SP(client_ID, project_ID, RoleId, OutPut_status_Code, OutPut_message)
                    .ToList<DASM_GET_MASKING_TEMPLATE_SP_Result>();

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
        public void SaveMaskingTemplate(DataTable Masking, ref string StatusCode, ref string Message)
        {
            string Proc = "[DASM_INS_MASKING_TEMPLATE_SP]";
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(Proc, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Temptable", Masking);
                        cmd.Parameters.Add("@Template_ID_out", SqlDbType.VarChar, 10).Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add("@Status_code", SqlDbType.VarChar, 10).Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                        if (con.State != ConnectionState.Open) con.Open();
                        cmd.ExecuteNonQuery();

                        StatusCode = cmd.Parameters["@Status_code"].Value.ToString();
                        Message = cmd.Parameters["@Message"].Value.ToString();

                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }

            }
            catch (Exception e)
            {

            }
        }

        public List<string> GetSlicingColumns(string client_ID, string project_ID, string constraint_Type, long config_ID, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.DASM_GET_SLICING_COL_LIST_SP(client_ID, project_ID, constraint_Type, config_ID, OutPut_status_Code,
                    OutPut_message).ToList<string>();

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

        public List<string> GetSlicingColumnValues(string client_ID, string project_ID, string Column_name, int Config_ID, ref string StatusCode, ref string Message)
        {

            string Proc = "[DIMPLS_GET_SLICING_COL_VALUE_SP]";
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;

            DataTable dtSlicingCols;
            using (SqlConnection con = new SqlConnection(providerConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Proc, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Client_ID", client_ID);
                    cmd.Parameters.AddWithValue("@Project_ID", project_ID);
                    cmd.Parameters.AddWithValue("@Column_name", Column_name);
                    cmd.Parameters.AddWithValue("@Config_ID", Config_ID);
                    cmd.Parameters.Add("@Status_code", SqlDbType.VarChar, 10).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        dtSlicingCols = new DataTable();
                        da.Fill(dtSlicingCols);
                    }
                    StatusCode = cmd.Parameters["@Status_code"].Value.ToString();
                    Message = cmd.Parameters["@Message"].Value.ToString();
                }
            }
            return dtSlicingCols.AsEnumerable().Select(r => r[Column_name].ToString()).ToList<string>();
        }

        public DataTable GetCriteria(string Type, ref string StatusCode, ref string Message)
        {
            //string Proc = "[DIMPLS_GET_CRITERIA_TYPE_COLUMN_SP]";
            string Proc = "[DASM_GET_CRITERIA_TYP_COL_SP]";
            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;

            DataTable dtCriteria;
            using (SqlConnection con = new SqlConnection(providerConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Proc, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", Type);

                    cmd.Parameters.Add("@Status_code", SqlDbType.VarChar, 10).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        dtCriteria = new DataTable();
                        da.Fill(dtCriteria);
                    }
                    StatusCode = cmd.Parameters["@Status_code"].Value.ToString();
                    Message = cmd.Parameters["@Message"].Value.ToString();
                }
            }
            return dtCriteria;
        }

        public DataTable GetCriteria(int page, int rows, string client_ID, string project_ID, string Object_Type, string Template_Name, long config_ID, string Column_name,
            string SlicingValue, int ToolId, string Expression, ref string StatusCode, ref string Message)
        {
            //string Proc = "[DIMPLS_GET_CRITERIA_TYPE_SP]";
            string Proc = "[DASM_GET_CRITERIA_TYPE_SP]";

            var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
            string providerConnectionString = builder.ProviderConnectionString;

            DataTable dtCriteria;
            using (SqlConnection con = new SqlConnection(providerConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(Proc, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Client_ID", client_ID);
                    cmd.Parameters.AddWithValue("@Project_ID", project_ID);
                    cmd.Parameters.AddWithValue("@Object_Type", Object_Type);
                    cmd.Parameters.AddWithValue("@Template_Name", Template_Name);
                    cmd.Parameters.AddWithValue("@Tool_ID", ToolId);
                    cmd.Parameters.AddWithValue("@Columname", Column_name);
                    cmd.Parameters.AddWithValue("@Config_ID", config_ID);
                    cmd.Parameters.AddWithValue("@Slicingvalue", SlicingValue);
                    cmd.Parameters.AddWithValue("@Expression", Expression);
                    //cmd.Parameters.AddWithValue("@PageNo", page);
                    //cmd.Parameters.AddWithValue("@RecordsPerPage", rows);
                    cmd.Parameters.Add("@Status_code", SqlDbType.VarChar, 10).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        dtCriteria = new DataTable();
                        da.Fill(dtCriteria);
                    }
                    StatusCode = cmd.Parameters["@Status_code"].Value.ToString();
                    Message = cmd.Parameters["@Message"].Value.ToString();
                }
            }
            return dtCriteria;
        }

        public void SaveUpdateCriteria(DIMAPLUSCriteriaEntity Criteria, ref string StatusCode, ref string Message)
        {
            //var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            //var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                _context.HXR_UPDATE_CRITERIA_SP(Criteria.ClientId, Criteria.ProjectId, Criteria.ObjectType, Criteria.Objects, Criteria.SlicingField,
                    Criteria.SlicingValue, Convert.ToInt64(Criteria.Criteria));

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

        public void SaveTemplate(DataTable Criteria, ref string StatusCode, ref string Message)
        {
            string Proc = "[DASM_INSERT_TEMPLATE_SP]";
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(Proc, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Temptable", Criteria);
                        cmd.Parameters.Add("@Status_code", SqlDbType.VarChar, 10).Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                        if (con.State != ConnectionState.Open) con.Open();
                        cmd.ExecuteNonQuery();

                        StatusCode = cmd.Parameters["@Status_code"].Value.ToString();
                        Message = cmd.Parameters["@Message"].Value.ToString();

                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }

            }
            catch (Exception e)
            {

            }
        }

        public void UpdateCriteriaSourceDelete(DIMAPLUSCriteriaEntity Criteria, ref string StatusCode, ref string Message)
        {
            //var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            //var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                _context.HXR_UPDATE_CRITERIA_DELETEFLAG_SP(Criteria.ClientId, Criteria.ProjectId, Criteria.ObjectType, Criteria.Objects, Criteria.SlicingField,
                    Criteria.SlicingValue, Criteria.SourceDelete);

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

        public void PurgeData(DIMAPLUSCriteriaEntity Criteria, ref string StatusCode, ref string Message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                //_context.HXR_UP_PURGE_DATA_SP(Criteria.ClientId, Criteria.ProjectId, Criteria.Objects, Criteria.SlicingField, Criteria.SlicingValue,
                //     Convert.ToInt16(Criteria.SourceDelete), OutPut_status_Code, OutPut_message);

                _context.DASM_UP_PURGE_DATA_SP(Criteria.ClientId, Criteria.ProjectId, Criteria.Template_ID, Criteria.Run_ID, Criteria.Tool_ID, Criteria.RoleId, OutPut_status_Code, OutPut_message);
                //_context.DASM_INSERT_AUDIT_LOG_SP(Criteria., projectId, Convert.ToInt32(toolId), template_Id, "COPYDATA", UserName, OutPut_status_Code, OutPut_message);

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
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);
                throw e;
            }
        }
        public List<HXR_GET_CRITERIA_ALL_SP_Result> GetAllCriteria(int page, int rows, string client_ID, string project_ID, ref string status_Code, ref string message)
        {
            //var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            //var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.HXR_GET_CRITERIA_ALL_SP(client_ID, project_ID, page, rows).ToList<HXR_GET_CRITERIA_ALL_SP_Result>();

                //status_Code = OutPut_status_Code.Value.ToString();
                //message = OutPut_message.Value.ToString();

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
        public List<DASM_GET_CRI_OBJ_COUNT_SP_Result> GetSourceObjectDetails(string client_ID, string project_ID, long? template_ID, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                //var result = _context.HXR_GET_PARAMETERCOUNT_SP(client_ID, project_ID, OutPut_status_Code,OutPut_message).ToList<HXR_GET_PARAMETERCOUNT_SP_Result>();
                var result = _context.DASM_GET_CRI_OBJ_COUNT_SP(client_ID, project_ID, template_ID, OutPut_status_Code, OutPut_message).ToList<DASM_GET_CRI_OBJ_COUNT_SP_Result>();

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
        public List<DASM_GET_TARGETSERVER_SP_Result> GetTargetServerDetails(string client_ID, string project_ID, long? RoleId, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                var result = _context.DASM_GET_TARGETSERVER_SP(client_ID, project_ID, RoleId, OutPut_status_Code,
                    OutPut_message).ToList<DASM_GET_TARGETSERVER_SP_Result>();

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
        public List<DASM_GET_AUDITREPORT_SP_Result> GetAuditReport(string client_ID, string project_ID, long? Template_ID, ref string StatusCode, ref string Message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));


            //var result = _context.HXR_GET_AUDITREPORT_SP(client_ID, project_ID, OutPut_status_Code,
            //    OutPut_message).ToList<HXR_GET_AUDITREPORT_SP_Result>();
            var result = _context.DASM_GET_AUDITREPORT_SP(client_ID, project_ID, Template_ID, OutPut_status_Code,
              OutPut_message).ToList<DASM_GET_AUDITREPORT_SP_Result>();

            StatusCode = OutPut_status_Code.Value.ToString();
            Message = OutPut_message.Value.ToString();

            return result;

        }
        public List<DASM_GET_TRANSFERRESULT_SP_Result> GetTransferResultReport(string client_ID, string project_ID, long? Template_ID, long? Run_ID, ref string StatusCode, ref string Message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));


            //var result = _context.HXR_GET_TRANSFERRESULT_SP(client_ID, project_ID, OutPut_status_Code,
            //    OutPut_message).ToList<HXR_GET_TRANSFERRESULT_SP_Result>();

            var result = _context.DASM_GET_TRANSFERRESULT_SP(client_ID, project_ID, Template_ID, Run_ID, OutPut_status_Code,
                OutPut_message).ToList<DASM_GET_TRANSFERRESULT_SP_Result>();

            StatusCode = OutPut_status_Code.Value.ToString();
            Message = OutPut_message.Value.ToString();

            return result;

        }
        public bool CopySlicedData(string client_ID, string project_ID, string ToolID, long? template_ID, long? RoleId, string UserName,
            ref string message, ref string status_Code)
        {

            string Source_constring = string.Empty;
            string Target_constring = string.Empty, _error = string.Empty;

            GetSourceAndTargetConnectionString(client_ID, project_ID, ToolID, RoleId, ref Source_constring, ref Target_constring);

            if (string.IsNullOrWhiteSpace(Source_constring) || string.IsNullOrWhiteSpace(Target_constring))
            {
                return false;
            }
            if (ScriptGenerator(client_ID, project_ID, ToolID, RoleId, Target_constring, template_ID, ref _error))
            {
                var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
                var OutPut_message = new ObjectParameter("message", typeof(string));
                //_context.HXR_UP_TRANSFERPROCESS_SP(client_ID, project_ID, Convert.ToInt64(ToolID), OutPut_status_Code, OutPut_message);


                LoadTable(client_ID, project_ID, ToolID, Source_constring, Target_constring, template_ID, RoleId, UserName, ref message, ref status_Code);

                _context.DASM_UP_TRANSFERPROCESS_SP(client_ID, project_ID, Convert.ToInt64(ToolID), template_ID, RoleId, UserName, OutPut_status_Code, OutPut_message);
            }
            else
            {
                message = _error;
                return false;
            }
            return true;
        }
        public bool ScriptGenerator(string client_ID, string project_ID, string ToolID, long? RoleId, string TargetConnectionString, long? template_ID, ref string error)
        {
            UnitOfWork u = new UnitOfWork();

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            //var lst = _context.HXR_GET_OBJECTNAME_SP(client_ID, project_ID, OutPut_status_Code, OutPut_message).ToList<HXR_GET_OBJECTNAME_SP_Result>();
            var lst = _context.DASM_GET_OBJECTNAME_SP(client_ID, project_ID, template_ID, OutPut_status_Code, OutPut_message).ToList<DASM_GET_OBJECTNAME_SP_Result>();

            if (lst == null)
            {
                return false;
            }


            try
            {
                foreach (var obj in lst)
                {
                    var OutPut_status_Code1 = new ObjectParameter("status_Code", typeof(string));
                    var OutPut_message1 = new ObjectParameter("message", typeof(string));

                    SqlDataReader sqlReaderSchema;
                    using (SqlConnection cons = new SqlConnection(ConnectionString()))
                    {
                        cons.Open();
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1 = new SqlCommand("[DASM_GET_SCRIPTDIFINATION_SP]", cons);
                        cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@Client_ID", client_ID);
                        cmd1.Parameters.AddWithValue("@Project_ID", project_ID);
                        cmd1.Parameters.AddWithValue("@type", obj.Object_Type);
                        cmd1.Parameters.AddWithValue("@objectname", obj.Object_Name);
                        cmd1.Parameters.AddWithValue("@Tool_ID", ToolID);
                        cmd1.Parameters.AddWithValue("@Role_ID", RoleId);
                        cmd1.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd1.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        sqlReaderSchema = cmd1.ExecuteReader();


                        while (sqlReaderSchema.Read())
                        {
                            SqlCommand dmd = new SqlCommand();

                            try
                            {


                                if (obj.Object_Type.ToUpper() != "TABLES")
                                {
                                    using (SqlConnection destinationConnection = new SqlConnection("" + TargetConnectionString + ""))
                                    {
                                        destinationConnection.Open();
                                        dmd = new SqlCommand("" + Convert.ToString(sqlReaderSchema[0]) + "", destinationConnection);
                                        dmd.ExecuteNonQuery();
                                        dmd = new SqlCommand("" + Convert.ToString(sqlReaderSchema[1]) + "", destinationConnection);
                                        dmd.ExecuteNonQuery();
                                    }

                                }
                                else
                                {
                                    using (SqlConnection destinationConnection = new SqlConnection("" + TargetConnectionString + ""))
                                    {
                                        destinationConnection.Open();
                                        dmd = new SqlCommand("" + Convert.ToString(sqlReaderSchema[0]) + "", destinationConnection);
                                        dmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            catch (Exception __ex)
                            {
                                error = __ex.Message;
                                u.WriteErrorLog(__ex, "sql script:" + dmd.CommandText);
                                return false;
                            }
                        }

                    }
                }
            }
            catch (Exception _ex)
            {
                error = _ex.Message;
                u.WriteErrorLog(_ex);
                return false;
            }


            return true;

        }
        public bool LoadTable(string client_ID, string project_ID, string ToolID, string SourceConnectionString,
            string TargetConnectionString, long? template_Id, long? RoleId, string UserName, ref string message, ref string status_code)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));


            //var lst = _context.HXR_FX_GET_CRITERIAMASTER_SP(client_ID, project_ID, "Tables", OutPut_status_Code, OutPut_message).ToList<HXR_FX_GET_CRITERIAMASTER_SP_Result>();
            //var lst = _context.DASM_FX_GET_CRITERIAMASTER_SP(client_ID, project_ID, "Tables", template_Id, OutPut_status_Code, OutPut_message).ToList<DASM_FX_GET_CRITERIAMASTER_SP_Result>();
            string Proc = "[DASM_FX_GET_CRITERIAMASTER_SP]";
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(Proc, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Status_code", SqlDbType.VarChar, 10).Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                        cmd.Parameters.AddWithValue("@Client_ID", client_ID);
                        cmd.Parameters.AddWithValue("@Project_ID", project_ID);
                        cmd.Parameters.AddWithValue("@type", "Tables");
                        cmd.Parameters.AddWithValue("@Teamplate_ID", template_Id);
                        cmd.Parameters.AddWithValue("@Role_ID", RoleId);
                        //if (con.State != ConnectionState.Open) con.Open();
                        //cmd.ExecuteNonQuery();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {

                            da.Fill(dt);
                        }


                        status_code = cmd.Parameters["@Status_code"].Value.ToString();
                        message = cmd.Parameters["@Message"].Value.ToString();

                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }

            }
            catch (Exception e)
            {

            }
            //var lst1 = new List<DASM_FX_GET_CRITERIAMASTER_SP_Result>();

            var lst = dt.AsEnumerable().Select(r => new DASM_FX_GET_CRITERIAMASTER_Res()
          {
              Object_Type = r["Object_Type"].ToString(),
              Object_Name = r["Object_Name"].ToString(),
              Slicing_Field = r["Slicing_Field"].ToString(),
              Slicing_Value = r["Slicing_Value"].ToString(),
              Sx_Flag = Convert.ToInt16(r["Sx_Flag"].ToString()),
              Tx_Flag = Convert.ToInt16(r["Tx_Flag"].ToString()),
              Fx_Flag = Convert.ToInt16(r["Fx_Flag"].ToString()),
              Source_Rec_Count = Convert.ToInt64(r["Source_Rec_Count"].ToString()),
              Target_Rec_Count = Convert.ToInt64(r["Target_Rec_Count"].ToString()),
              Create_Date = Convert.ToDateTime(r["Create_Date"].ToString()),
              ExtractionDate = Convert.ToDateTime(r["ExtractionDate"].ToString()),
              Template_ID = Convert.ToInt64(r["Template_ID"].ToString()),
              Criteria_ID = Convert.ToInt64(r["Criteria_ID"]),
              Client_ID = r["Client_ID"].ToString(),
              Project_ID = r["Project_ID"].ToString(),
              Expression_Code = r["Expression_Code"].ToString(),
              SQL_STMT = r["SQL_STMT"].ToString()
          }).ToList();





            if (lst == null || lst.Count <= 0)
            {

                return false;
            }

            foreach (var obj in lst)
            {
                Load_data(obj.Client_ID, obj.Project_ID, obj.Criteria_ID, obj.Object_Name, obj.Slicing_Field, obj.Expression_Code, obj.Slicing_Value, SourceConnectionString,
                    TargetConnectionString, template_Id, ToolID, UserName, obj.SQL_STMT);
            }
            if (lst.Count > 0)
            {
                _context.DASM_INSERT_AUDIT_LOG_SP(lst[0].Client_ID, lst[0].Project_ID, Convert.ToInt32(ToolID), template_Id, RoleId, "COPYDATA", UserName, OutPut_status_Code, OutPut_message);
                message = OutPut_message.Value.ToString();
                status_code = OutPut_status_Code.Value.ToString();
            }
            //else
            //{
            //    message = OutPut_message.Value.ToString();
            //    status_code = OutPut_status_Code.Value.ToString();
            //}
            return true;

        }
        public string Load_data(string clientId, string projectId, long? Criteria_ID, string objectname, string Slicefield, string condition, string Slicevalue,
            string SourceConnectionString, string TargetConnectionString, long? template_Id, string toolId, string UserName, string sqlStatement)
        {
            SqlCommand cmd = new SqlCommand();
            SqlCommand con = new SqlCommand();
            SqlCommand cmdtr = new SqlCommand();
            UnitOfWork u = new UnitOfWork();

            int rcount = 0;
            using (SqlConnection sourceConnection = new SqlConnection("" + SourceConnectionString + ""))
            {
                sourceConnection.Open();

                //string sqlQuery = "SELECT * FROM " + objectname + " Where " + Slicefield + condition + " ''" + Slicevalue + "'";                
                //sqlQuery = "DECLARE @PRocedure_Encrypted nvarchar(max);" + System.Environment.NewLine + " DECLARE @Stmt_to_create nvarchar(max); " + System.Environment.NewLine + "OPEN SYMMETRIC KEY HXR_SymmetricKey DECRYPTION BY CERTIFICATE HXR_Certificate; " + System.Environment.NewLine + "SET @PRocedure_Encrypted =  EncryptByKey (Key_GUID('HXR_SymmetricKey'),'" + sqlQuery + "'') " + System.Environment.NewLine + "SET @Stmt_to_create =  DecryptByKey(@PRocedure_Encrypted) " + System.Environment.NewLine + "CLOSE SYMMETRIC KEY HXR_SymmetricKey; " + System.Environment.NewLine + " EXEC SP_EXECUTESQL @statement = @Stmt_to_create ";


                //cmd = new SqlCommand("SELECT * FROM " + objectname + " Where " + Slicefield + condition + " '" + Slicevalue + "'", sourceConnection);
                cmd = new SqlCommand(sqlStatement, sourceConnection);
                //cmd = new SqlCommand(sqlQuery, sourceConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                using (SqlConnection destinationConnection = new SqlConnection("" + TargetConnectionString + ""))
                {
                    destinationConnection.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection))
                    {
                        bulkCopy.DestinationTableName = "" + objectname + "";

                        try
                        {
                            bulkCopy.WriteToServer(reader);

                        }
                        catch (Exception ex)
                        {
                            u.WriteErrorLog(ex);
                            return ex.Message;
                        }
                        finally
                        {

                            reader.Close();
                        }

                    }

                    //------- code for to get target record count ------------ 
                    //cmdtr = new SqlCommand("SELECT Count(*) As No FROM " + objectname + " Where " + Slicefield + condition + " '" + Slicevalue + "'", destinationConnection);
                    //cmdtr = new SqlCommand(sqlStatement, sourceConnection);
                    //SqlDataReader readercount = cmdtr.ExecuteReader();

                    //if (readercount.HasRows)
                    //{
                    //    while (readercount.Read())
                    //    {
                    //        rcount = Convert.ToInt32(readercount[0].ToString());
                    //    }
                    //}

                    destinationConnection.Close();

                }

                sourceConnection.Close();

            }

            using (SqlConnection Connection = new SqlConnection(ConnectionString()))
            {
                Connection.Open();
                //con = new SqlCommand("Update HXR_CRITERIA_MS Set Fx_Flag=1,Target_Nos='" + rcount + "' Where [Object_Name]='"
                //    + objectname + "' And Slicing_Field='" + Slicefield + "' And Slicing_Value='" + Slicevalue + "' And Sx_Flag=1 And ISNULL(Tx_Flag,0)=0", Connection);

                con = new SqlCommand("Update DASEM_CRITERIA_MS Set Fx_Flag = 1 Where Criteria_ID = " + Criteria_ID + " And Sx_Flag = 1 And ISNULL(Tx_Flag,0) = 0", Connection);
                con.ExecuteNonQuery();

                //con = new SqlCommand("UPDATE DASEM_TRANS_AUDIT_LOG SET Target_Rec_Count = " + rcount + ", Process_End_Time = GetDate() WHERE Criteria_ID = " + Criteria_ID + "", Connection);
                //con.ExecuteNonQuery();

                var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
                var OutPut_message = new ObjectParameter("message", typeof(string));
                //_context.DASM_INSERT_AUDIT_LOG_SP(clientId, projectId, Criteria_ID, Convert.ToInt32(toolId), rcount, UserName, OutPut_status_Code, OutPut_message);
                //_context.DASM_INSERT_AUDIT_LOG_SP(clientId, projectId, Convert.ToInt32(toolId), template_Id, "COPYDATA", UserName, OutPut_status_Code, OutPut_message);


                Connection.Close();
            }
            return string.Empty;

        }
        public void StopSqlProfiler()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CMN_STRAT_DEPLOY_SP";
                    if (con.State == ConnectionState.Closed) con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public bool DBDeployment(string client_ID, string project_ID, string ToolID, string _ConnectionString,
            string EncryptedFile, string DecryptedFile, ref string message, ref string status_code)
        {
            try
            {
                UnitOfWork u = new UnitOfWork();
                StopSqlProfiler();

                string Source_constring = _ConnectionString;
                string Target_constring = string.Empty;
                //GetSourceAndTargetConnectionString(client_ID, project_ID, ToolID, ref Source_constring, ref Target_constring);
                using (SqlConnection sourceConnection = new SqlConnection("" + Source_constring + ""))
                {
                    //if (System.IO.File.Exists(path))
                    {
                        //if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\temp"))
                        //    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\temp");

                        // string path = AppDomain.CurrentDomain.BaseDirectory + @"\temp\myscript.dll";
                        // string decryptPath = AppDomain.CurrentDomain.BaseDirectory + @"\temp\myscript_decrypt.dll";

                        DecryptFile(EncryptedFile, DecryptedFile);

                        if (System.IO.File.Exists(DecryptedFile))
                        {
                            string script = "";
                            SqlCommand cmd = new SqlCommand();
                            script = System.IO.File.ReadAllText(DecryptedFile);
                            script.Replace("'", "''");
                            sourceConnection.Open();


                            cmd.Connection = sourceConnection;

                            cmd.CommandType = CommandType.Text;
                            //cmd.CommandText = script;                                                
                            var sqlqueries = script.Split(new[] { "\r\nGO\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var query in sqlqueries)
                            {
                                try
                                {
                                    cmd.CommandText = query;
                                    cmd.ExecuteNonQuery();
                                }
                                catch (Exception e1)
                                {
                                    //throw e1;
                                    u.WriteErrorLog(e1);
                                    //break;
                                    message = e1.Message;
                                    sourceConnection.Close();
                                    return false;
                                }
                            }
                            sourceConnection.Close();
                            if (File.Exists(DecryptedFile))
                                File.Delete(DecryptedFile);

                            if (File.Exists(EncryptedFile))
                                File.Delete(EncryptedFile);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                //throw ex;
                message = ex.Message;
                return false;
            }

        }

        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        public void EncryptFile(string file, string fileEncrypted)
        {
            //string file = @"C:\Users\36988\Desktop\script.sql";
            string password = "abcd1234";

            byte[] bytesToBeEncrypted = File.ReadAllBytes(file);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            //string fileEncrypted = @"C:\Users\36988\Desktop\script_encrypted.sql";

            File.WriteAllBytes(fileEncrypted, bytesEncrypted);
        }

        public void DecryptFile(string fileEncrypted, string fileDecrypted)
        {
            //string fileEncrypted = @"C:\Users\36988\Desktop\script_encrypted.sql";
            string password = "abcd1234";

            byte[] bytesToBeDecrypted = File.ReadAllBytes(fileEncrypted);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            //string file = @"C:\Users\36988\Desktop\script_decrypted.sql";
            File.WriteAllBytes(fileDecrypted, bytesDecrypted);
        }


        public void GetSourceAndTargetConnectionString(string client_ID, string project_ID, string ToolID, long? RoleId, ref string Source_constring, ref string Target_constring)
        {

            string StatusCode = string.Empty;
            string Message = string.Empty;
            string Proc = "[HXR_GET_CONSTRING_SP]";
            DataTable dt;
            using (SqlConnection con = new SqlConnection(ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(Proc, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Client_ID", client_ID);
                    cmd.Parameters.AddWithValue("@Project_ID", project_ID);
                    cmd.Parameters.AddWithValue("@Tool_ID", ToolID);
                    cmd.Parameters.AddWithValue("@Role_ID", RoleId);
                    cmd.Parameters.Add("@Status_code", SqlDbType.VarChar, 10).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        dt = new DataTable();
                        da.Fill(dt);
                    }
                    StatusCode = cmd.Parameters["@Status_code"].Value.ToString();
                    Message = cmd.Parameters["@Message"].Value.ToString();

                    if (dt.Rows.Count > 0)
                    {
                        Source_constring = Convert.ToString(dt.Rows[0][0]);
                        Target_constring = Convert.ToString(dt.Rows[0][1]);
                    }
                }
            }


        }


        public List<DASM_GET_DELETELIST_SP_Result> GetDeleteList(int page, int rows, string client_ID, string project_ID, long? TemplateId, long? Run_ID, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                //var result = _context.HXR_GET_DELETELIST_SP(client_ID, project_ID, page, rows, OutPut_status_Code, OutPut_message)
                //    .ToList<HXR_GET_DELETELIST_SP_Result>();
                var result = _context.DASM_GET_DELETELIST_SP(client_ID, project_ID, TemplateId, Run_ID, page, rows, OutPut_status_Code, OutPut_message)
                  .ToList<DASM_GET_DELETELIST_SP_Result>();

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


        public DataTable GetObjectDetails(DIMAPLUSCriteriaEntity _params, ref string status_Code, ref string message)
        {

            string StatusCode = string.Empty;
            string Message = string.Empty;
            string Proc = "[DASM_GET_CRI_OBJ_DETL_SP]";
            DataTable dt;
            using (SqlConnection con = new SqlConnection(ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(Proc, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Client_ID", _params.ClientId);
                    cmd.Parameters.AddWithValue("@Project_ID", _params.ProjectId);
                    cmd.Parameters.AddWithValue("@Object_Type", _params.ObjectType);
                    cmd.Parameters.AddWithValue("@Template_Name", _params.Template);
                    cmd.Parameters.AddWithValue("@Config_ID", _params.ConfigId);
                    cmd.Parameters.Add("@Status_code", SqlDbType.VarChar, 10).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        dt = new DataTable();
                        da.Fill(dt);
                    }
                    StatusCode = cmd.Parameters["@Status_code"].Value.ToString();
                    Message = cmd.Parameters["@Message"].Value.ToString();
                }
            }
            return dt;


        }
        public void CheckCopyCount(string client_ID, string project_ID, long? template_ID, ref string message, ref string status_Code)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            _context.DASM_CHECK_DUPLICATE_RUN_SP(client_ID, project_ID, template_ID, OutPut_status_Code, OutPut_message);
            status_Code = OutPut_status_Code.Value.ToString();
            message = OutPut_message.Value.ToString();
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
