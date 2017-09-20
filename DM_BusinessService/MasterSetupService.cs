using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM_DataModel.UnitOfWork;
using DM_DataModel;
using AutoMapper;
using DM_BusinessEntities;
using System.Data;

namespace DM_BusinessService
{
    public class MasterSetupService : IMasterSetup
    {
        private readonly MasterSetup _MasterSetup;

        public MasterSetupService()
        {
            _MasterSetup = new MasterSetup();
        }

        public List<DM_BusinessEntities.UserToolEntity> GetTools(string client_ID, string project_ID, string user_Name, ref string status_Code, ref string message)
        {
            var _UserTools = _MasterSetup.GetTools(client_ID, project_ID, user_Name, ref status_Code, ref message);

            if (_UserTools != null)
            {
                Mapper.CreateMap<CMN_GET_TOOL_SP_Result, UserToolEntity>();
                var _UserToolsModel = Mapper.Map<List<CMN_GET_TOOL_SP_Result>, List<UserToolEntity>>(_UserTools);
                return _UserToolsModel;
            }
            return null;
        }

        public bool ValidateUser(string userName, string password, ref string RoleName, ref string StatusCode, ref string Message)
        {
            bool IsSuccess = false;
            // string StatusCode = "0", Message = "NOT AUTHENTICATED";
            var lst = _MasterSetup.ValidateUser(userName, password, ref StatusCode, ref Message);
            if (Message == "AUTHENTICATED")
            {
                //RoleName = lst[0].Role_Name;
                IsSuccess = true;
            }


            return IsSuccess;
        }

        public List<UserToolEntity> GetUserTools(string ClientID, string ProjectID, string UserName)
        {

            string StatusCode = string.Empty;
            string Message = string.Empty;

            var lst = _MasterSetup.GetTools(ClientID, ProjectID, UserName, ref StatusCode, ref Message);

            Mapper.CreateMap<CMN_GET_TOOL_SP_Result, UserToolEntity>();
            var _Tools = Mapper.Map<List<CMN_GET_TOOL_SP_Result>, List<UserToolEntity>>(lst);


            return _Tools;

        }

        public DMOClientEntitiy GetClientDetail(string UserName)
        {
            string StatusCode = "0", Message = "";
            var lst = _MasterSetup.GetClientDetail(UserName, ref StatusCode, ref Message);

            if (lst.Count <= 0)
            {
                return null;
            }
            var lstobj = lst.FirstOrDefault();
            var _Client = new DMOClientEntitiy();
            if (lstobj != null)
            {
                _Client.Client_ID = lstobj.Client_ID;
                _Client.Client_Name = lstobj.Client_Name;
                _Client.project_ID = lstobj.Project_ID;
            }

            return _Client;
        }

        public List<DMOProjectEntitiy> GetProjects(string ClientID, string UserName)
        {

            string StatusCode = string.Empty;
            string Message = string.Empty;
            var lst = _MasterSetup.GetProjects(ClientID, UserName, ref StatusCode, ref Message);

            Mapper.CreateMap<CMN_GET_PROJECT_SP_Result, DMOProjectEntitiy>();
            var _Projects = Mapper.Map<List<CMN_GET_PROJECT_SP_Result>, List<DMOProjectEntitiy>>(lst);

            return _Projects;

        }

        public bool SaveProject(DMOProjectEntitiy PrjoectEntry)
        {
            string StatusCode = "0", Message = "";
            _MasterSetup.SaveProject(PrjoectEntry.Client_ID, PrjoectEntry.Project_ID, PrjoectEntry.Project_Description, PrjoectEntry.Modified_By, ref StatusCode, ref Message);

            if (Message == "Success")
                return true;
            else
                return false;
        }

        public bool SaveClient(DMOClientEntitiy FormInputs)
        {

            string StatusCode = "0", Message = "";
            FormInputs.Client_ID = "1001";
            _MasterSetup.SaveClient(FormInputs.Client_ID, FormInputs.Client_Name, FormInputs.ModifiedBy, ref  StatusCode, ref  Message);

            if (Message == "")
                return true;
            else
                return false;

        }

        public List<DMOUserEntitiy> GetUserDetails(string ClientID, string ProjectID)
        {
            string StatusCode = "0", Message = "";
            var lst = _MasterSetup.GetUserDetails(ClientID, ProjectID, ref StatusCode, ref Message);
            Mapper.CreateMap<CMN_GET_ALL_USER_DETAILS_SP_Result, DMOUserEntitiy>();
            var _Users = Mapper.Map<List<CMN_GET_ALL_USER_DETAILS_SP_Result>, List<DMOUserEntitiy>>(lst);

            return _Users;


        }

        public bool SaveUser(DMOUserEntitiy _objUser, ref string StatusCode, ref string Message)
        {
            //string StatusCode = "0", Message = "";

            _MasterSetup.SaveUser(_objUser.Client_ID, _objUser.Project_ID, _objUser.User_name, _objUser.email_ID, _objUser.Password, _objUser.ROLE_NAME, _objUser.Last_Modified_By, ref StatusCode, ref Message);

            if (Message == "Success")
                return true;
            else
                return false;


        }

        public bool UpdateUser(DMOUserEntitiy _objUser)
        {
            string StatusCode = "0", Message = "";
            _MasterSetup.UpdateUser(_objUser.Client_ID, _objUser.Project_ID, _objUser.User_ID, _objUser.User_name, _objUser.email_ID, _objUser.ROLE_NAME, _objUser.Active_Flag, _objUser.Last_Modified_By, ref StatusCode, ref Message);

            if (Message == "Success")
                return true;
            else
                return false;

        }

        public void UpdateTools(string UserToolID, string ActiveFlag)
        {

            string StatusCode = "0", Message = "";
            _MasterSetup.UpdateTools(Convert.ToInt64(UserToolID), Convert.ToInt64(ActiveFlag), ref StatusCode, ref Message);

        }

        public void ActivateProject(DMOProjectEntitiy Prj, ref string status_Code, ref string message)
        {
            _MasterSetup.ActivateProject(Prj, ref status_Code, ref message);
        }

        public string SaveWorkflowProcess(string client_ID, string project_ID, List<ToolsEntity> workflowItem, string created_By, ref string StatusCode, ref string Message)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Client_ID", typeof(String));
            dt.Columns.Add("Project_ID", typeof(String));
            dt.Columns.Add("Task_Name", typeof(String));
            dt.Columns.Add("Tool_ID", typeof(long));
            dt.Columns.Add("Task_Sequence", typeof(int));
            dt.Columns.Add("Created_By", typeof(String));

            try
            {
                foreach (ToolsEntity item in workflowItem)
                {
                    DataRow dr = dt.NewRow();

                    dr["Client_ID"] = client_ID;
                    dr["Project_ID"] = project_ID;
                    dr["Task_Name"] = item.ToolName;
                    dr["Tool_ID"] = item.ToolID;
                    dr["Task_Sequence"] = item.Tool_TaskSequence;
                    dr["Created_By"] = created_By;

                    dt.Rows.Add(dr);
                }

                _MasterSetup.SaveWorkflowProcess(dt, ref StatusCode, ref Message);

                if (StatusCode == "0")
                    return "Save workflow process completed successfully.";
                else
                    return "Data reconcillation failed. Error: " + Message;
            }
            catch (Exception _e)
            {
                return "Save workflow process failed. Error: " + _e.Message;
            }

        }

        public List<ToolsEntity> GetWorkflowProcess(string client_ID, string project_ID, ref string status_Code, ref string message)
        {

            var lst = _MasterSetup.GetWorkflowProcess(client_ID, project_ID, ref status_Code, ref message);

            Mapper.CreateMap<CMN_GET_WORK_FLOW_SEQ_SP_Result, ToolsEntity>();
            var _workflowItem = Mapper.Map<List<CMN_GET_WORK_FLOW_SEQ_SP_Result>, List<ToolsEntity>>(lst);

            return _workflowItem;

        }

        public void ChangePassword(string client_ID, string project_ID, string old_Password, string new_Password, string user_Name, string email_ID, string action, ref string StatusCode, ref string Message)
        {
             _MasterSetup.ChangePassword(client_ID, project_ID, old_Password, new_Password, user_Name, email_ID, action, ref StatusCode, ref Message);
        }

        public List<RoleEntity> GetRoleList(string client_ID, string project_ID, ref string status_Code, ref string message)
        {
            var listRole = _MasterSetup.GetRoleList(client_ID, project_ID, "", ref status_Code, ref message);

            if (listRole != null)
            {
                Mapper.CreateMap<CMN_GET_ROLE_SP_Result1, RoleEntity>();
                var _roleroleItem = Mapper.Map<List<CMN_GET_ROLE_SP_Result1>, List<RoleEntity>>(listRole);
                return _roleroleItem;
            }
            return null;

        }

        public void Create_Update_Role(string client_ID, string project_ID, string role_Name, string role_Desc, string inserted_by, ref  string status_Code, ref string message)
        {
            _MasterSetup.Create_Update_Role(client_ID, project_ID, role_Name, role_Desc, inserted_by, ref status_Code, ref message);
        }

        public List<RoleMenuEntity> GetRoleMenuList(int role_id, ref string status_Code, ref string message)
        {
            var lst = _MasterSetup.GetRoleMenuList(role_id, ref status_Code, ref message);

            Mapper.CreateMap<CMN_GET_ROLE_MENU_ITEMS_SP_Result, RoleMenuEntity>();
            var _roleMenuItem = Mapper.Map<List<CMN_GET_ROLE_MENU_ITEMS_SP_Result>, List<RoleMenuEntity>>(lst);

            return _roleMenuItem;
        }

        public void UpdateRoleMenu(int role_id, string menu_list, ref string StatusCode, ref string Message)
        {
            try
            {
                _MasterSetup.UpdateRoleMenu(role_id, menu_list, ref StatusCode, ref Message);                
            }
            catch(Exception ex)
            {
                StatusCode = "-1";
                Message = ex.InnerException.Message;
            }
        }
    }
}
