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
using System.Text.RegularExpressions;

namespace DM_BusinessService
{
    public class HXRRuleService : IRules
    {
        private readonly HXRRule _ruleMS;

        public HXRRuleService()
        {
            _ruleMS = new HXRRule();
        }
        public List<string> GetMetaDataTableNames(string client_ID, string project_ID, string config_ID, ref string status_Code, ref string message)
        {
            return _ruleMS.GetMetaDataTableNames(client_ID, project_ID, config_ID, ref status_Code, ref message);
        }

        public List<string> GetMetaDataColumnsByTableName(string client_ID, string project_ID, string database_IP, string source_Target, string database_Name, string Config_Id, string Table_Name, ref string status_Code, ref string message)
        {
            return _ruleMS.GetMetaDataColumnNamesByTableName(client_ID, project_ID, database_IP, source_Target, database_Name, Config_Id, Table_Name, ref status_Code, ref message);
        }

        public List<DM_BusinessEntities.RuleAttributeEntity> GetRuleAttributes(int page, int rows, string client_ID, string project_ID, string Table_Name, string Column_Name, long Rule_TypeID, ref string status_Code, ref string message, ref long TotalRecords)
        {
            var _RuleAttribute = _ruleMS.GetRuleAttributes(page, rows, client_ID, project_ID, Table_Name, Column_Name, Rule_TypeID, ref status_Code, ref message, ref  TotalRecords);

            if (_RuleAttribute != null)
            {
                Mapper.CreateMap<HXR_GET_RULE_ATTRIBUTE_FOR_DISPLAY_SP_Result, RuleAttributeEntity>();
                var _RuleAttributeModel = Mapper.Map<List<HXR_GET_RULE_ATTRIBUTE_FOR_DISPLAY_SP_Result>, List<RuleAttributeEntity>>(_RuleAttribute);
                return _RuleAttributeModel;
            }
            return null;

        }
        public void GenerateObjects(string client_ID, string project_ID, string table_name, long? config_ID, ref string status_Code, ref string message)
        {
            _ruleMS.GenerateObjects(client_ID, project_ID, table_name, config_ID, ref status_Code, ref  message);
        }
        public void UpdateVersionNumber(string client_ID, string project_ID, string ActiveAttributeRuleId, string InActiveAttributeRuleId, string table_name, string updated_by, ref string status_Code, ref string message)
        {
            _ruleMS.UpdateVersionNumber(client_ID, project_ID, ActiveAttributeRuleId, InActiveAttributeRuleId, table_name, updated_by, ref status_Code, ref message);
        }
        public void ActivateRuleAttribute(string client_ID, string project_ID, string Attribute_Rule_ID, string Status, string updated_by, ref string status_Code, ref string message)
        {
            _ruleMS.ActivateRuleAttribute(client_ID, project_ID, Attribute_Rule_ID, Status, updated_by, ref status_Code, ref message);
        }
        public List<HXRErrorDesc> GetErrorDesc(string client_ID, string project_ID, string error_Code, ref string status_Code, ref string message)
        {
            var _ErrorDesc = _ruleMS.GetErrorDesc(client_ID, project_ID, error_Code, ref status_Code, ref message);

            if (_ErrorDesc != null)
            {
                Mapper.CreateMap<HXR_GET_ERROR_DESC_SP_Result, HXRErrorDesc>();
                var _ErrorDescModel = Mapper.Map<List<HXR_GET_ERROR_DESC_SP_Result>, List<HXRErrorDesc>>(_ErrorDesc);
                return _ErrorDescModel;
            }
            return null;
        }

        public List<HXRRuleCategory> GetRuleCategory(Nullable<long> ruleCategory_ID, string ruleCategory_Name, ref string status_Code, ref string message)
        {
            var RuleCategories = _ruleMS.GetRuleCategory(ruleCategory_ID, ruleCategory_Name, ref status_Code, ref message);

            if (RuleCategories != null)
            {
                Mapper.CreateMap<HXR_GET_RULE_CATEGORY_SP_Result, HXRRuleCategory>();
                var _RuleCategoryModel = Mapper.Map<List<HXR_GET_RULE_CATEGORY_SP_Result>, List<HXRRuleCategory>>(RuleCategories);
                return _RuleCategoryModel;
            }
            return null;
        }

        public void UpdateRuleAttribute(string client_ID, string project_ID, long? attribute_Rule_ID, string rule_ID, long? ruleType_ID, string IS_Predefined, string error_Code,
            long? ruleCategory_ID, string column_Name, string default_value, string conditional_Clause, string Rule_desc, string priority, string last_Modified_By, string Data_Steward,
            ref string status_Code, ref string message)
        {
            _ruleMS.UpdateRuleAttribute(client_ID, project_ID, attribute_Rule_ID, rule_ID, ruleType_ID, IS_Predefined, error_Code, ruleCategory_ID, column_Name, default_value,
                    conditional_Clause, Rule_desc, priority, last_Modified_By, Data_Steward, ref status_Code, ref message);
        }

        public void UpdatePreRuleAttribute(string client_ID, string project_ID, long? attribute_Rule_ID, string rule_ID, long? ruleType_ID, string IS_Predefined, string error_Code,
            long? ruleCategory_ID, string column_Name, string default_value, string conditional_Clause, string priority, string last_Modified_By,
            ref string status_Code, ref string message)
        {
            _ruleMS.UpdateRuleAttribute(client_ID, project_ID, attribute_Rule_ID, rule_ID, ruleType_ID, IS_Predefined, error_Code, ruleCategory_ID, column_Name, default_value,
                    conditional_Clause, string.Empty, priority, last_Modified_By, string.Empty, ref status_Code, ref message);
        }


        public List<HXRValidateColumn> ValidateColumn(string client_ID, string project_ID, string IP_Address, string database_Name, string table_Name, string Source_Target, ref string status_Code, ref string message)
        {
            var ValidatedCols = _ruleMS.ValidateColumn(client_ID, project_ID, IP_Address, database_Name, table_Name, Source_Target, ref status_Code, ref message);
            if (ValidatedCols != null)
            {
                Mapper.CreateMap<HXR_VALIDATE_COLUMN_SP_Result, HXRValidateColumn>();
                var _ValidateColsModel = Mapper.Map<List<HXR_VALIDATE_COLUMN_SP_Result>, List<HXRValidateColumn>>(ValidatedCols);
                return _ValidateColsModel;
            }
            return null;
        }
        public List<HXRRuleTypeEntity> GetRuleType(string client_ID, string project_ID, long? ruleType_ID, string ruleType_Name, ref string status_Code, ref string message)
        {
            var RuleType = _ruleMS.GetRuleType(ruleType_ID, ruleType_Name, client_ID, project_ID, ref status_Code, ref message);
            if (RuleType != null)
            {
                Mapper.CreateMap<HXR_GET_RULE_TYPE_SP_Result1, HXRRuleTypeEntity>();
                var _RuleTypeModel = Mapper.Map<List<HXR_GET_RULE_TYPE_SP_Result1>, List<HXRRuleTypeEntity>>(RuleType);
                return _RuleTypeModel;
            }
            return null;
        }


        public List<HXRRuleEntity> GetRule(string client_ID, string project_ID, long? rule_Id, string rule_Name, ref string status_Code, ref string message)
        {
            var Rule = _ruleMS.GetRule(client_ID, project_ID, rule_Id, rule_Name, ref status_Code, ref message);
            if (Rule != null)
            {
                Mapper.CreateMap<HXR_GET_RULE_SP_Result, HXRRuleEntity>();
                var _RuleModel = Mapper.Map<List<HXR_GET_RULE_SP_Result>, List<HXRRuleEntity>>(Rule);
                return _RuleModel;
            }
            return null;
        }

        public void SavePreRuleAttribute(string client_ID, string project_ID, string rule_ID, long? ruleType_ID, string error_Code, long? ruleCategory_ID,
            string table_name, string column_Name, string default_value, string conditional_Clause, string rule_desc, string priority, string reference_Table,
            string reference_Column, string reference_Cond, string last_Modified_By, string data_steward, ref string status_Code, ref string message)
        {
            _ruleMS.SavePreRuleAttribute(client_ID, project_ID, rule_ID, ruleType_ID, error_Code, ruleCategory_ID, table_name, column_Name,
                default_value, conditional_Clause, rule_desc, priority, reference_Table, reference_Column, reference_Cond, last_Modified_By, data_steward, ref status_Code, ref message);
        }
        public void SaveUserRuleAttribute(string client_ID, string project_ID, string rule_ID, long? ruleType_ID, string error_Code, long? ruleCategory_ID,
            string table_name, string column_Name, string default_value, string conditional_Clause, string rule_desc, string priority, string reference_Table, string reference_Column,
            string reference_Cond, string last_Modified_By, string data_steward, ref string status_Code, ref string message)
        {

            string _rule = conditional_Clause.Replace("[", "b.[");

            _ruleMS.SavePreRuleAttribute(client_ID, project_ID, rule_ID, ruleType_ID, error_Code, ruleCategory_ID, table_name, column_Name, default_value, _rule,
                rule_desc, priority, reference_Table, reference_Column, reference_Cond, last_Modified_By, data_steward, ref status_Code, ref message);
        }
        public List<HXRRuleValidationEntity> GetUserDefineRuleValidation(string client_ID, string project_ID, string ToolID, long? RoleId, string TableName, string Query, ref  string status_Code, ref string message)
        {

            var RuleValidation = _ruleMS.GetUserDefineRuleValidation(client_ID, project_ID, ToolID, RoleId, TableName, Query, ref status_Code, ref message);

            if (RuleValidation != null)
            {
                Mapper.CreateMap<HXR_GET_RULE_VALIDATION_SP, HXRRuleValidationEntity>();
                var _RuleValidationModel = Mapper.Map<List<HXR_GET_RULE_VALIDATION_SP>, List<HXRRuleValidationEntity>>(RuleValidation);
                return _RuleValidationModel;
            }
            return null;
        }
        public List<HXRRuleValidationResultsEntity> GetRuleValidationResults(string client_ID, string project_ID, string table_name, string run_ID, string run_User, long? ConfigID, ref  string status_Code, ref string message)
        {
            var RuleValidationResults = _ruleMS.GetRuleValidationResults(client_ID, project_ID, table_name, run_ID, run_User, ConfigID, ref  status_Code, ref message);

            if (RuleValidationResults != null)
            {
                Mapper.CreateMap<HXR_GET_RULE_VALIDATION_RESULTS1, HXRRuleValidationResultsEntity>();
                var _RuleValidationReultsModel = Mapper.Map<List<HXR_GET_RULE_VALIDATION_RESULTS1>, List<HXRRuleValidationResultsEntity>>(RuleValidationResults);
                return _RuleValidationReultsModel;
            }
            return null;
        }


        public DataTable GetRuleValidationErrorData(string client_ID, string project_ID, long? config_ID, string table_name, int page_no, int recordsperpage, string run_ID,
            string Rule_cateogry_ID, string Rule_name, ref string status_Code, ref string message, ref long TotalCount)
        {
            DataTable _dtErrorData = _ruleMS.GetRuleValidationErrorData(client_ID, project_ID, config_ID, table_name, page_no, recordsperpage, run_ID, Rule_cateogry_ID, Rule_name, ref status_Code, ref message, ref TotalCount);
            return _dtErrorData;
        }
        public DataTable GetRuleValidationErrorData_Paging(string client_ID, string project_ID, string table_name, long? config_ID, long? pageNo, long? recordsPerPage, ref  string status_Code, ref string message)
        {
            DataTable _dtErrorData = _ruleMS.GetRuleValidationErrorData_Paging(client_ID, project_ID, table_name, config_ID, pageNo, recordsPerPage, ref   status_Code, ref  message);
            return _dtErrorData;
        }
        public void UpdateSourceTable(HXRSourceTable srcTbl, ref  string status_Code, ref string message)
        {
            _ruleMS.UpdateSourceTable(srcTbl.TableName, srcTbl.Rule_cateogry_ID, srcTbl.Rule_ID, srcTbl.PrimaryKeyCol, srcTbl.PrimaryKeyValue, srcTbl.UpdateCol, srcTbl.UpdateVal, srcTbl.update_all,
            srcTbl.run_id, srcTbl.ConfigID, ref  status_Code, ref message);
        }
        public DataTable GetRuleValidationErrorSampleData(long? config_ID, string table_name, string primary_key_column, string primary_key_value, ref  string status_Code,
            ref string message)
        {
            DataTable _dtErrorData = _ruleMS.GetRuleValidationErrorSampleData(config_ID, table_name, primary_key_column, primary_key_value, ref status_Code, ref message);
            return _dtErrorData;
        }
        public List<HXRDataTypeFunctionsEntity> GetFunctionByDataType(string client_ID, string project_ID, string DataType, ref  string status_Code, ref string message)
        {
            var DataTypeFunctions = _ruleMS.GetFunctionByDataType(client_ID, project_ID, DataType, ref status_Code, ref message);
            if (DataTypeFunctions != null)
            {
                Mapper.CreateMap<HXR_GET_RULE_FUNCTION_SP_Result, HXRDataTypeFunctionsEntity>();
                var _DataTypeFunctionsModel = Mapper.Map<List<HXR_GET_RULE_FUNCTION_SP_Result>, List<HXRDataTypeFunctionsEntity>>(DataTypeFunctions);

                return _DataTypeFunctionsModel;
            }
            return null;
        }
        #region RuleCategory
        public void SaveRuleCategory(string ruleCategory_Name, string ruleCategory_Desc, string inserted_by, ref  string status_Code, ref string message)
        {
            _ruleMS.SaveRuleCategory(ruleCategory_Name, ruleCategory_Desc, inserted_by, ref status_Code, ref message);
        }
        public void UpdateRuleCategory(Nullable<long> ruleCategory_ID, string ruleCategory_Name, string ruleCategory_Desc, Nullable<long> active_Flag, string last_Modified_By, ref  string status_Code, ref string message)
        {
            _ruleMS.UpdateRuleCategory(ruleCategory_ID, ruleCategory_Name, ruleCategory_Desc, active_Flag, last_Modified_By, ref status_Code, ref message);
        }
        #endregion
        public List<string> GetMetaDataTableList(string iPAddress, string databaseName, ref  string status_Code, ref string message)
        {
            var _dt = _ruleMS.GetMetaDataTableList(iPAddress, databaseName, ref status_Code, ref message);
            if (_dt == null) return null;
            var list = (from x in _dt.AsEnumerable()
                        select x.Field<string>(0)).ToList();

            return list;
        }
        public int InsertKeyColumns(string client_ID, string project_ID, long config_ID, string table_name, string key_column1, string key_column2, string key_column3, string key_column4, string key_column5, string created_by, ref string status_Code, ref string message)
        {
            var result = _ruleMS.InsertKeyColumns(client_ID, project_ID, config_ID, table_name, key_column1, key_column2, key_column3, key_column4, key_column5, created_by, ref status_Code, ref message);

            return result;
        }

        public List<HXRKeyColumnsEntity> GetKeyColumns(string client_ID, string project_ID, string table_name, Nullable<long> config_ID, ref string status_Code, ref string message)
        {
            var KeyColumns = _ruleMS.GetKeyColumns(client_ID, project_ID, table_name, config_ID, ref status_Code, ref message);
            if (KeyColumns != null)
            {
                Mapper.CreateMap<CMN_GET_KEY_COLUMNS_SP_Result, HXRKeyColumnsEntity>();
                var _KeyColumnsModel = Mapper.Map<List<CMN_GET_KEY_COLUMNS_SP_Result>, List<HXRKeyColumnsEntity>>(KeyColumns);
                return _KeyColumnsModel;
            }
            return null;
        }
        public void ValidateKeyColumns(string client_ID, string project_ID, string table_name, int Config_Id, string key_column1, string key_column2, string key_column3, string key_column4, string key_column5, ref string is_key_columns, ref string status_Code, ref string message)
        {
            _ruleMS.ValidateKeyColumns(client_ID, project_ID, table_name, Config_Id, key_column1, key_column2, key_column3, key_column4, key_column5, ref is_key_columns, ref status_Code, ref message);
        }

        #region RuleType
        public void SaveRuleType(string client_ID, string project_ID, string ruleType_Name, string ruleType_Desc, string inserted_by, ref  string status_Code, ref string message)
        {
            _ruleMS.SaveRuleType(client_ID, project_ID, ruleType_Name, ruleType_Desc, inserted_by, ref status_Code, ref message);
        }
        public void UpdateRuleType(Nullable<long> ruleType_ID, string ruleType_Name, string ruleType_Desc, Nullable<long> active_Flag, string last_Modified_By, ref  string status_Code, ref string message)
        {
            _ruleMS.UpdateRuleType(ruleType_ID, ruleType_Name, ruleType_Desc, active_Flag, last_Modified_By, ref status_Code, ref message);
        }
        #endregion
        #region RuleError
        public void SaveRuleError(string client_ID, string project_ID, string errorCode, string errorDescription, string inserted_by, ref  string status_Code, ref string message)
        {
            _ruleMS.SaveRuleError(client_ID, project_ID, errorCode, errorDescription, inserted_by, ref status_Code, ref message);
        }
        public void UpdateRuleError(string client_ID, string project_ID, string errorCode, string errorDescription, Nullable<long> active_Flag, string last_Modified_By, ref  string status_Code, ref string message)
        {
            _ruleMS.UpdateRuleError(client_ID, project_ID, errorCode, errorDescription, active_Flag, last_Modified_By, ref status_Code, ref message);
        }
        #endregion


        #region Rule
        public void SaveRule(string client_ID, string project_ID, string rule_Name, string rule_Description, DateTime? start_Date, DateTime? end_Date, string conditional_Clause, string default_value, string last_Modified_By, ref string status_Code, ref string message)
        {
            _ruleMS.SaveRule(client_ID, project_ID, rule_Name, rule_Description, start_Date, end_Date, conditional_Clause, default_value, last_Modified_By, ref status_Code, ref message);
        }

        public void UpdateRule(string client_ID, string project_ID, long? rule_ID, string rule_Name, long? active_Flag, string rule_Description, DateTime? start_Date, DateTime? end_Date, string conditional_Clause, string default_value, string last_Modified_By, ref string status_Code, ref string message)
        {
            _ruleMS.UpdateRule(client_ID, project_ID, rule_ID, rule_Name, active_Flag, rule_Description, start_Date, end_Date, conditional_Clause, default_value, last_Modified_By, ref status_Code, ref message);
        }
        #endregion



        public void Offline_Run(string client_ID, string project_ID, long? tool_ID, long? config_ID, string template_Name, string template_Path, string table_Name,
            string column_Name, string run_Status, string job_Description, string procedure_Name, string parameter_List, string created_by, ref string status_Code, ref string message)
        {
            _ruleMS.Offline_Run(client_ID, project_ID, tool_ID, config_ID, template_Name, template_Path, table_Name, column_Name, run_Status,
                    job_Description, procedure_Name, parameter_List, created_by, ref status_Code, ref message);
        }
        public DataTable GetTableData(string client_ID, string project_ID, string config_ID, string table_name, string ColumnList, byte IsDistinct, ref string status_Code, ref string message)
        {
            return _ruleMS.GetTableData(client_ID, project_ID, config_ID, table_name, ColumnList, IsDistinct, ref status_Code, ref message);
        }
        public List<HXROfflineStatusEntity> GetOfflineStatus(HXROfflineStatusEntity offline, ref string status_Code, ref string message)
        {
            var result = _ruleMS.GetOfflineStatus(offline, ref status_Code, ref message);

            if (result != null)
            {
                Mapper.CreateMap<CMN_GET_OFFLINE_JOB_STATS_SP_Result, HXROfflineStatusEntity>();
                var _resultModel = Mapper.Map<List<CMN_GET_OFFLINE_JOB_STATS_SP_Result>, List<HXROfflineStatusEntity>>(result);
                return _resultModel;
            }
            return null;
        }
        public Dictionary<Int64, string> RunNumberByTable(string client_ID, string project_ID, string Table_Name, ref string status_Code, ref string message)
        {
            return _ruleMS.RunNumberByTable(client_ID, project_ID, Table_Name, ref status_Code, ref message);
        }
        public void CheckKeyColumn(string client_ID, string project_ID, string table_Name, long? config_Id, ref string status_Code, ref string message)
        {
            _ruleMS.CheckKeyColumn(client_ID, project_ID, table_Name, config_Id, ref status_Code, ref message);
        }

        public Dictionary<string, string> RuleCriteriaByTable(string client_ID, string project_ID, string Table_Name, string Run_Id, ref string status_Code,
            ref string message)
        {
            return _ruleMS.RuleCriteriaByTable(client_ID, project_ID, Table_Name, Run_Id, ref status_Code, ref message);
        }

        public Dictionary<string, string> ErrorDataRuleByTable(string client_ID, string project_ID, string Table_Name, string Run_Id, string Rule_Category_Id,
            ref string status_Code, ref string message)
        {
            return _ruleMS.ErrorDataRuleByTable(client_ID, project_ID, Table_Name, Run_Id, Rule_Category_Id, ref status_Code, ref message);
        }
        public List<HXRRuleErrorSummary> GetRuleErrorSummary(string client_ID, string project_ID, string table_Name, string run_Number, long? rule_Category_Id, long? rule_ID, ref  string status_Code, ref string message)
        {
            var Rule = _ruleMS.GetRuleErrorSummary(client_ID, project_ID, table_Name, run_Number, rule_Category_Id, rule_ID, ref status_Code, ref message);
            if (Rule != null)
            {
                Mapper.CreateMap<HXR_GET_RULE_ERROR_SUMMARY_SP_Result, HXRRuleErrorSummary>();
                var _RuleModel = Mapper.Map<List<HXR_GET_RULE_ERROR_SUMMARY_SP_Result>, List<HXRRuleErrorSummary>>(Rule);
                return _RuleModel;
            }
            return null;
        }

    }
}
