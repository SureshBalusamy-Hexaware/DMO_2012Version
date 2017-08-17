using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM_BusinessEntities;
using System.Data;
namespace DM_BusinessService
{
    public interface IRules
    {
        List<string> GetMetaDataTableNames(string client_ID, string project_ID, string config_ID, ref string status_Code, ref string message);
        Dictionary<Int64, string> RunNumberByTable(string client_ID, string project_ID, string Table_Name, ref string status_Code, ref string message);
        Dictionary<string, string> RuleCriteriaByTable(string client_ID, string project_ID, string Table_Name, string Run_Id, ref string status_Code, ref string message);
        Dictionary<string, string> ErrorDataRuleByTable(string client_ID, string project_ID, string Table_Name, string Run_Id, string Rule_Category_Id, ref string status_Code, ref string message);

        List<string> GetMetaDataColumnsByTableName(string client_ID, string project_ID, string database_IP, string source_Target, string database_Name,
            string Config_Id, string Table_Name, ref string status_Code, ref string message);
        List<RuleAttributeEntity> GetRuleAttributes(int page, int rows, string client_ID, string project_ID, string Table_Name, string Column_Name, Int64 Rule_TypeID, ref string status_Code, ref string message, ref long TotalRecords);
        void GenerateObjects(string client_ID, string project_ID, string table_name, long? config_ID, ref  string status_Code, ref string message);
        void UpdateVersionNumber(string client_ID, string project_ID, string ActiveAttributeRuleId, string InActiveAttributeRuleId, string table_name, string updated_by, ref  string status_Code, ref string message);
        void ActivateRuleAttribute(string client_ID, string project_ID, string Attribute_Rule_ID, string Status, string updated_by, ref  string status_Code, ref string message);
        List<HXRErrorDesc> GetErrorDesc(string client_ID, string project_ID, string error_Code, ref string status_Code, ref string message);
        List<HXRRuleCategory> GetRuleCategory(Nullable<long> ruleCategory_ID, string ruleCategory_Name, ref string status_Code, ref string message);
         
        void UpdateRuleAttribute(string client_ID, string project_ID, Nullable<long> attribute_Rule_ID, string rule_ID, Nullable<long> ruleType_ID, string IS_Predefined, string error_Code, Nullable<long> ruleCategory_ID, string column_Name, string default_value,
            string conditional_Clause, string Rule_desc, string priority, string last_Modified_By, string Data_Steward, ref  string status_Code, ref string message);

        void UpdatePreRuleAttribute(string client_ID, string project_ID, Nullable<long> attribute_Rule_ID, string rule_ID, Nullable<long> ruleType_ID, string IS_Predefined, string error_Code, Nullable<long> ruleCategory_ID, string column_Name, string default_value, string conditional_Clause, string priority, string last_Modified_By, ref  string status_Code, ref string message);

        void SavePreRuleAttribute(string client_ID, string project_ID, string rule_ID, Nullable<long> ruleType_ID, string error_Code, Nullable<long> ruleCategory_ID,
            string table_name, string column_Name, string default_value, string conditional_Clause, string rule_desc, string priority, string reference_Table, string
            reference_Column, string reference_Cond, string last_Modified_By, string data_steward, ref  string status_Code, ref string message);

        void SaveUserRuleAttribute(string client_ID, string project_ID, string rule_ID, Nullable<long> ruleType_ID, string error_Code, Nullable<long> ruleCategory_ID,
            string table_name, string column_Name, string default_value, string conditional_Clause, string rule_desc, string priority, string reference_Table, string reference_Column, string reference_Cond, string last_Modified_By,
            string data_steward, ref  string status_Code, ref string message);
        List<HXRValidateColumn> ValidateColumn(string client_ID, string project_ID, string IP_Address, string database_Name, string table_Name, string Source_Target, ref  string status_Code, ref string message);
        List<HXRRuleTypeEntity> GetRuleType(string client_ID, string project_ID, Nullable<long> ruleType_ID, string ruleType_Name, ref string status_Code, ref string message);
        List<HXRRuleEntity> GetRule(string client_ID, string project_ID, Nullable<long> rule_Id, string rule_Name, ref string status_Code, ref string message);
        List<HXRRuleValidationEntity> GetUserDefineRuleValidation(string client_ID, string project_ID, string ToolID,long? RoleId, string TableName, string Query, ref  string status_Code, ref string message);

        List<HXRRuleValidationResultsEntity> GetRuleValidationResults(string client_ID, string project_ID, string table_name, string run_ID, string run_User, long? ConfigID, ref  string status_Code, ref string message);
        DataTable GetRuleValidationErrorData(string client_ID, string project_ID, long? config_ID, string table_name, int page_no, int recordsperpage, string run_ID,  string Rule_cateogry_ID, string Rule_name,ref  string status_Code, ref string message, ref long TotalCount);
        DataTable GetRuleValidationErrorData_Paging(string client_ID, string project_ID, string table_name, long? config_ID, long? pageNo, long? recordsPerPage, ref  string status_Code, ref string message);

        void UpdateSourceTable(HXRSourceTable srcTbl, ref  string status_Code, ref string message);

        DataTable GetRuleValidationErrorSampleData(long? config_ID, string table_name, string primary_key_column, string primary_key_value, ref  string status_Code, ref string message);
        List<HXRDataTypeFunctionsEntity> GetFunctionByDataType(string client_ID, string project_ID, string DataType, ref  string status_Code, ref string message);
        void SaveRuleCategory(string ruleCategory_Name, string ruleCategory_Desc, string inserted_by, ref  string status_Code, ref string message);
        void UpdateRuleCategory(Nullable<long> ruleCategory_ID, string ruleCategory_Name, string ruleCategory_Desc, Nullable<long> active_Flag, string last_Modified_By, ref  string status_Code, ref string message);
        List<string> GetMetaDataTableList(string iPAddress, string databaseName, ref  string status_Code, ref string message);
        int InsertKeyColumns(string client_ID, string project_ID, long config_ID, string table_name, string key_column1, string key_column2, string key_column3, string key_column4, string key_column5, string created_by, ref string status_Code, ref string message);
        List<HXRKeyColumnsEntity> GetKeyColumns(string client_ID, string project_ID, string table_name, Nullable<long> config_ID, ref string status_Code, ref string message);

        void SaveRuleError(string client_ID, string project_ID, string errorCode, string errorDescription, string inserted_by, ref  string status_Code, ref string message);
        void UpdateRuleError(string client_ID, string project_ID, string errorCode, string errorDescription, Nullable<long> active_Flag, string last_Modified_By, ref  string status_Code, ref string message);

        void SaveRule(string client_ID, string project_ID, string rule_Name, string rule_Description, Nullable<System.DateTime> start_Date, Nullable<System.DateTime> end_Date, string conditional_Clause, string default_value, string last_Modified_By,
            ref  string status_Code, ref string message);
        void UpdateRule(string client_ID, string project_ID, Nullable<long> rule_ID, string rule_Name, Nullable<long> active_Flag, string rule_Description,
            Nullable<System.DateTime> start_Date, Nullable<System.DateTime> end_Date, string conditional_Clause, string default_value, string last_Modified_By,
            ref  string status_Code, ref string message);

        void SaveRuleType(string client_ID, string project_ID, string ruleType_Name, string ruleType_Desc, string inserted_by, ref  string status_Code, ref string message);
        void UpdateRuleType(Nullable<long> ruleType_ID, string ruleType_Name, string ruleType_Desc, Nullable<long> active_Flag, string last_Modified_By, ref  string status_Code, ref string message);
        void ValidateKeyColumns(string client_ID, string project_ID, string table_name, int Config_Id, string key_column1, string key_column2, string key_column3, string key_column4, string key_column5, ref string is_key_columns, ref string status_Code, ref string message);
        void Offline_Run(string client_ID, string project_ID, Nullable<long> tool_ID, Nullable<long> config_ID, string template_Name, string template_Path,
            string table_Name, string column_Name, string run_Status, string job_Description, string procedure_Name, string parameter_List, string created_by,
            ref  string status_Code, ref string message);
        List<HXROfflineStatusEntity> GetOfflineStatus(HXROfflineStatusEntity offline, ref string status_Code, ref string message);
        DataTable GetTableData(string client_ID, string project_ID, string config_ID, string table_name, string ColumnList, byte IsDistinct, ref string status_Code, ref string message);
        void CheckKeyColumn(string client_ID, string project_ID, string table_Name, long? config_Id, ref  string status_Code, ref string message);
        List<HXRRuleErrorSummary> GetRuleErrorSummary(string client_ID, string project_ID, string table_Name, string run_Number, long? rule_Category_Id, long? rule_ID, ref string status_Code, ref string message);
    }
}
