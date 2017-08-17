using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM_BusinessEntities;

namespace DM_BusinessService
{
    public interface IProfiler
    {
        //List<ProfilerEntity> GetProfilerTableNames(string client_ID, string project_ID, string source_Target, string database_Name, ref string status_Code, ref string message);
        List<ProfilerEntity> GetProfilerTableList(string client_ID, string project_ID, long config_ID, ref string status_Code, ref string message);
        List<ProfilerEntity> GetProfilerTemplateList(string client_ID, string project_ID, long? config_ID, ref string status_Code, ref string message);
        List<ProfilerEntity> GetProfilerTableDetails(string client_ID, string project_ID, string table_name, string config_ID, ref string status_Code, ref string message);
        List<ProfilerEntity> GetProfilerParameterRecordsValue(long tool_ID, ref string status_Code, ref string message);
        List<ProfilerTemplateEntity> GetProfilerTemplateDetails(string client_ID, string project_ID, string template_name,long config_ID, ref string status_Code, ref string message);
        List<string> GetTableCodeColumnList(string client_ID, string project_ID, long config_ID, ref string status_Code, ref string message);
        DataTable GetProfilerTableSampleRecords(long? config_ID, string table_name, string column_list, string row_count, ref  string status_Code, ref string message, ref long total_count, int pageno = 1, int rows = 1);
        DataTable GetColumnCodeValues(string client_id, string project_id, long? config_ID, string table_name, string column_name, byte? isDistinct, ref  string status_Code, ref string message);
        DataTable GetBatchProfileStatus(int page, int rows, string client_id, string project_id);
        int InsertProfileTemplate(string client_ID, string project_ID, string template_name, string table_name, string column_name, string data_type, bool null__ratio_profile, bool statistics_Profile, bool value_Distribution_Profile, bool length_Distribution_Profile, bool pattern_Profile, bool candidate_Key_Profile, bool profiling_Status, long? config_ID, string created_by, ref string status_Code, ref string message);
        int InsertProfileData(ProfileData profile_data, string profiled_by, ref string status_Code, ref string message);
        int CreateCodeRule(string client_ID, string project_ID, long? config_ID, string table_name, string column_name, string code_value, string created_by, ref string status_Code, ref string message);
        long UpdateProfileStatus(string client_ID, string project_ID, long? config_ID, string template_name, string table_name, string profile_type, string profile_desc, string profile_status, long rec_count, string profiled_by, long? profileID, ref string status_Code, ref string message);
        string UpdateProfileXML(string tableName, string columnName, string[] removeComputeOptions, string templateName, string database_name, string server_ip, string srcUserName, string srcPwd, string client_id, string project_id,
            long tool_id, int row_count, string[] allColumns, long profileID, long? RoleId);
        string OfflineProfiling(string client_id, string project_id, long tool_id, string templateName, string profileOutputXMLFile, long profileID);
        List<ProfileCodeRuleEntity> GetCodeRule(string client_ID, string project_ID, string table_name, string column_name, ref string StatusCode, ref string Message);
    }
}
