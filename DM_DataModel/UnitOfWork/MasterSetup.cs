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
    public class MasterSetup : IDisposable
    {
        #region Private member variables...
        DM_MetaDataEntities _context = null;
        #endregion

        public MasterSetup()
        {
            _context = new DM_MetaDataEntities();
        }

        #region Public Repository Creation properties...

        #endregion

        #region Public member methods....

        public List<CMN_GET_TOOL_SP_Result> GetTools(string client_ID, string project_ID, string user_Name, ref string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));
            try
            {
                //var result = _context HXR_GET_TOOL_SP(client_ID, project_ID, user_Name, OutPut_status_Code, OutPut_message).ToList<HXR_GET_TOOL_SP_Result>();
                var result = _context.CMN_GET_TOOL_SP(client_ID, project_ID, user_Name, OutPut_status_Code, OutPut_message).ToList<CMN_GET_TOOL_SP_Result>();
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

        //public List<CMN_GET_TOOL_SP_Result> GetUserTools(string ClientID, string ProjectID, string UserName, ref string StatusCode, ref string Message)
        //{

        //    var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
        //    var OutPut_message = new ObjectParameter("message", typeof(string));

        //    var result = _context.CMN_GET_TOOL_SP(ClientID, ProjectID, UserName, OutPut_status_Code, OutPut_message).ToList<CMN_GET_TOOL_SP_Result>();

        //    StatusCode = OutPut_status_Code.Value.ToString();
        //    Message = OutPut_message.Value.ToString();

        //    return result;
        //}

        public int ValidateUser(string UserName, string Password, ref string StatusCode, ref string Message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            var result = _context.CMN_AUTHENTICATE_USER_SP(UserName, Password, OutPut_status_Code, OutPut_message);

            StatusCode = OutPut_status_Code.Value.ToString();
            Message = OutPut_message.Value.ToString();

            return result;

        }

        public List<CMN_GET_CLIENT_SP_Result> GetClientDetail(string UserName, ref string StatusCode, ref string Message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            var result = _context.CMN_GET_CLIENT_SP(UserName, OutPut_status_Code, OutPut_message).ToList<CMN_GET_CLIENT_SP_Result>();

            StatusCode = OutPut_status_Code.Value.ToString();
            Message = OutPut_message.Value.ToString();

            return result;

        }

        public List<CMN_GET_PROJECT_SP_Result> GetProjects(string ClientID, string UserName, ref string StatusCode, ref string Message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            // var result = _context.HXR_GET_PROJECT_SP(ClientID, OutPut_status_Code, OutPut_message).ToList<HXR_GET_PROJECT_SP_Result>();
            var result = _context.CMN_GET_PROJECT_SP(ClientID, UserName, OutPut_status_Code, OutPut_message).ToList<CMN_GET_PROJECT_SP_Result>();

            StatusCode = OutPut_status_Code.Value.ToString();
            Message = OutPut_message.Value.ToString();

            return result;
        }

        public void SaveClient(string ClientID, string ClientName, string LastModifiedBy, ref string StatusCode, ref string Message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            //_context.HXR_INSERT_CLIENT_SP(ClientID, ClientName, LastModifiedBy, OutPut_status_Code, OutPut_message);
            _context.CMN_INSERT_CLIENT_SP(ClientID, ClientName, LastModifiedBy, OutPut_status_Code, OutPut_message);

            StatusCode = OutPut_status_Code.Value.ToString();
            Message = OutPut_message.Value.ToString();


        }

        public void SaveProject(string ClientID, string ProjectID, string ProjectDescription, string ModifiedBy, ref string StatusCode, ref string Message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            //_context.HXR_INSERT_PROJECT_SP(ClientID, ProjectID, ProjectDescription, ModifiedBy, OutPut_status_Code, OutPut_message);
            _context.CMN_INSERT_PROJECT_SP(ClientID, ProjectID, ProjectDescription, ModifiedBy, OutPut_status_Code, OutPut_message);

            StatusCode = OutPut_status_Code.Value.ToString();
            Message = OutPut_message.Value.ToString();
        }

        public void ActivateProject(DMOProjectEntitiy _prj, ref string StatusCode, ref string Message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            _context.CMN_UPDATE_PROJECT_SP(_prj.Client_ID, _prj.Project_ID, _prj.Active_Flag, _prj.Modified_By, OutPut_status_Code, OutPut_message);

            StatusCode = OutPut_status_Code.Value.ToString();
            Message = OutPut_message.Value.ToString();
        }

        public List<CMN_GET_ALL_USER_DETAILS_SP_Result> GetUserDetails(string ClientID, string ProjectID, ref string StatusCode, ref string Message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            var _result = _context.CMN_GET_ALL_USER_DETAILS_SP(ClientID, ProjectID, OutPut_status_Code, OutPut_message).ToList<CMN_GET_ALL_USER_DETAILS_SP_Result>();

            StatusCode = OutPut_status_Code.Value.ToString();
            Message = OutPut_message.Value.ToString();

            return _result;
        }

        public void SaveUser(string ClientID, string ProjectID, string UserName, string EmailID, string Password, string RoleName, string LastModifiedBy, ref string StatusCode, ref string Message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            _context.CMN_INSERT_USER_SP(ClientID.Trim(), UserName.Trim(), EmailID.Trim(), ProjectID.Trim(), Password.Trim(), RoleName.Trim(),
                LastModifiedBy.Trim(), OutPut_status_Code, OutPut_message);

            StatusCode = OutPut_status_Code.Value.ToString();
            Message = OutPut_message.Value.ToString();
        }

        public void UpdateUser(string ClientID, string ProjectID, long UserID, string UserName, string EmailID, string RoleName, long ActiveFlag, string LastModifiedBy,
            ref string StatusCode, ref string Message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            //     _context.HXR_UPDATE_USER_SP(ClientID, ProjectID, UserID, EmailID, UserName, ActiveFlag, RoleName, LastModifiedBy, OutPut_status_Code, OutPut_message);
            _context.CMN_UPDATE_USER_SP(ClientID, ProjectID, UserID, EmailID, UserName, ActiveFlag, RoleName, LastModifiedBy, OutPut_status_Code, OutPut_message);

            StatusCode = OutPut_status_Code.Value.ToString();
            Message = OutPut_message.Value.ToString();
        }

        public void UpdateTools(long UserToolID, long ActiveFlag, ref string StatusCode, ref string Message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            _context.CMN_UPDATE_TOOL_SP(UserToolID, ActiveFlag, OutPut_status_Code, OutPut_message);

            StatusCode = OutPut_status_Code.Value.ToString();
            Message = OutPut_message.Value.ToString();
        }

        public void SaveWorkflowProcess(DataTable dtWorkFlowItem, ref string StatusCode, ref string Message)
        {
            string dynamicProc = "[CMN_INS_WORK_FLOW_SEQ_SP]";

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
                        cmd.Parameters.Add("@Temptable", SqlDbType.Structured).Value = dtWorkFlowItem;
                        cmd.Parameters.Add("@Status_Code", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 255).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        StatusCode = Convert.ToString(cmd.Parameters["@Status_Code"].Value);
                        Message = Convert.ToString(cmd.Parameters["@Message"].Value);
                    }
                    if (con.State != ConnectionState.Closed) con.Close();
                }
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        public List<CMN_GET_WORK_FLOW_SEQ_SP_Result> GetWorkflowProcess(string client_ID, string project_ID, ref string StatusCode, ref string Message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            var result = _context.CMN_GET_WORK_FLOW_SEQ_SP(client_ID, project_ID, OutPut_status_Code, OutPut_message).ToList<CMN_GET_WORK_FLOW_SEQ_SP_Result>();

            StatusCode = OutPut_status_Code.Value.ToString();
            Message = OutPut_message.Value.ToString();

            return result;

        }

        public void ChangePassword(string client_ID, string project_ID, string old_Password, string new_Password, string user_Name, string email_ID, string action, ref string StatusCode, ref string Message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            _context.CMN_CHANGE_PASSWORD_SP(client_ID, project_ID, old_Password, new_Password, user_Name, email_ID, action, OutPut_status_Code, OutPut_message);

            StatusCode = OutPut_status_Code.Value.ToString();
            Message = OutPut_message.Value.ToString();
        }

        public List<CMN_GET_ROLE_MENU_ITEMS_SP_Result> GetRoleMenuList(int role_id, ref string StatusCode, ref string Message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            var result = _context.CMN_GET_ROLE_MENU_ITEMS_SP(role_id, OutPut_status_Code, OutPut_message).ToList<CMN_GET_ROLE_MENU_ITEMS_SP_Result>();

            StatusCode = OutPut_status_Code.Value.ToString();
            Message = OutPut_message.Value.ToString();

            return result;
        }

        public void UpdateRoleMenu(int role_id, string menu_list, ref string StatusCode, ref string Message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            _context.CMN_UPDATE_ROLE_MENU_SP(role_id,menu_list, OutPut_status_Code, OutPut_message);

            StatusCode = OutPut_status_Code.Value.ToString();
            Message = OutPut_message.Value.ToString();
        }


        public List<CMN_GET_ROLE_SP_Result> GetRoleList(string clientid, string projectid, string roleid, ref string StatusCode, ref string Message)
        {

            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            var result = _context.CMN_GET_ROLE_SP(clientid, projectid, null, OutPut_status_Code, OutPut_message).ToList<CMN_GET_ROLE_SP_Result>();

            StatusCode = OutPut_status_Code.Value.ToString();
            Message = OutPut_message.Value.ToString();

            return result;
        }

        public void Create_Update_Role(string client_ID, string project_ID, string role_Name, string role_Desc, string inserted_by, ref  string status_Code, ref string message)
        {
            var OutPut_status_Code = new ObjectParameter("status_Code", typeof(string));
            var OutPut_message = new ObjectParameter("message", typeof(string));

            try
            {
                _context.CMN_INSERT_ROLE_SP(client_ID, project_ID, role_Name, role_Desc, "1", "0", OutPut_status_Code, OutPut_message);
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
