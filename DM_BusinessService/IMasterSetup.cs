using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM_BusinessEntities;
using System.Data;

namespace DM_BusinessService
{
    public interface IMasterSetup
    {
        List<UserToolEntity> GetTools(string client_ID, string project_ID, string user_Name, ref string status_Code, ref string message);
        DMOClientEntitiy GetClientDetail(string UserName);
        System.Collections.Generic.List<DMOProjectEntitiy> GetProjects(string ClientID, string UserName);
        System.Collections.Generic.List<DMOUserEntitiy> GetUserDetails(string ClientID, string ProjectID);
        System.Collections.Generic.List<DM_BusinessEntities.UserToolEntity> GetUserTools(string ClientID, string ProjectID, string UserName);
        bool SaveClient(DMOClientEntitiy FormInputs);
        bool SaveProject(DMOProjectEntitiy PrjoectEntry);
        void ActivateProject(DMOProjectEntitiy PrjoectEntry, ref string StatusCode, ref string Message);
        bool SaveUser(DMOUserEntitiy _objUser, ref string StatusCode, ref string Message);
        void UpdateTools(string UserToolID, string ActiveFlag);
        bool UpdateUser(DMOUserEntitiy _objUser);
        bool ValidateUser(string userName, string password, ref string RoleName, ref string StatusCode, ref string Message);
        string SaveWorkflowProcess(string client_ID, string project_ID, List<ToolsEntity> workflowItem, string Created_By, ref string StatusCode, ref string Message);
        List<ToolsEntity> GetWorkflowProcess(string client_ID, string project_ID, ref string StatusCode, ref string Message);
        void ChangePassword(string client_ID, string project_ID, string old_Password, string new_Password, string user_Name, string email_ID, string action, ref string StatusCode, ref string Message);
        List<RoleMenuEntity> GetRoleMenuList(int role_id, ref string status_Code, ref string message);
        void UpdateRoleMenu(int role_id, string menu_list, ref string StatusCode, ref string Message);
        List<RoleEntity> GetRoleList(string client_ID, string project_ID, ref string status_Code, ref string message);
        void Create_Update_Role(string client_ID, string project_ID, string role_Name, string role_Desc, string inserted_by, ref  string status_Code, ref string message);

    }
}
