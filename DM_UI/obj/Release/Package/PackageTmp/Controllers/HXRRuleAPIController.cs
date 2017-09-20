using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DM_BusinessEntities;
using DM_BusinessService;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;
using DM_UI.App_Start;


namespace DM_UI.Controllers
{

    public class HXRRuleAPIController : ApiController
    {
        private readonly IRules _ruleMS;
        private readonly IHXRConfigurationMS _configMS;

        #region Public Constructor
        public HXRRuleAPIController()
        {
            _ruleMS = new HXRRuleService();
            _configMS = new HXRConfigurationMSService();

        }
        #endregion
        public List<string> GetMetaDataTableList(string dataSource, string databaseName)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            var lst = _ruleMS.GetMetaDataTableList(dataSource, databaseName, ref StatusCode, ref  Message);
            return lst;

        }


        [HttpGet]
        public HXRConfigurationMSEntity GetConfigBySourceTarget(string ClientID, string ProjectID, string SourceTarget, long? ToolID, int? RoleId)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            //  long ToolID = 7;
            long toolId = Convert.ToInt64(UIProperties.Tools.HexaRule);
            //int? RoleId = UIProperties.Sessions.Client.Role_ID;
            HXRConfigurationMSEntity _configEntity = _configMS.GetConfigurationByType(ClientID, ProjectID, SourceTarget, ToolID, RoleId, ref StatusCode, ref Message);
            return _configEntity;
        }

        //[AcceptVerbs("GET", "POST")]
        // [HttpGet]        // GET api/hxrruleapi
        [ActionName("MetaDataTable")]
        public List<string> Get(string client_ID, string project_ID, string config_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            return _ruleMS.GetMetaDataTableNames(client_ID, project_ID, config_ID, ref StatusCode, ref Message);
        }
        [HttpGet]
        [ActionName("GetRunNumbers")]
        public IEnumerable<KeyValuePair<Int64, string>> RunNumberByTable(string client_ID, string project_ID, string Table_Name)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            Dictionary<Int64, string> RunNumberByTable = _ruleMS.RunNumberByTable(client_ID, project_ID, Table_Name, ref StatusCode, ref Message);
            IEnumerable<KeyValuePair<Int64, string>> r = RunNumberByTable.OrderByDescending(i => i.Key);

            return r;
        }
        [HttpGet]
        [ActionName("GetRuleCriteria")]
        public Dictionary<string, string> RuleCriteriaByTable(string client_ID, string project_ID, string Table_Name, string Run_Id)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            return _ruleMS.RuleCriteriaByTable(client_ID, project_ID, Table_Name, Run_Id, ref StatusCode, ref Message);
        }
        [HttpGet]
        [ActionName("GetErrorDataRule")]
        public Dictionary<string, string> ErrorDataRuleByTable(string client_ID, string project_ID, string Table_Name, string Run_Id, string Rule_Category_Id)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            return _ruleMS.ErrorDataRuleByTable(client_ID, project_ID, Table_Name, Run_Id, Rule_Category_Id, ref StatusCode, ref Message);
        }


        //
        [HttpGet] // GET api/hxrruleapi
        public List<string> GetMetaDataColumnByTableName(string client_ID, string project_ID, string database_IP, string source_Target, string database_Name,
            string Config_Id, string Table_Name)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            return _ruleMS.GetMetaDataColumnsByTableName(client_ID, project_ID, database_IP, source_Target, database_Name, Config_Id, Table_Name, ref StatusCode, ref Message);
        }

        [HttpGet] // GET 
        public List<HXRRuleTypeEntity> GetRuleType()
        {
            string StatusCode = string.Empty, Message = string.Empty;
            // var result = _ruleMS.GetRuleType(null, null, ref StatusCode, ref Message);
            string _ClientId = UIProperties.Sessions.Client.Client_ID;
            string _ProjectId = UIProperties.Sessions.Client.project_ID;

            return _ruleMS.GetRuleType(_ClientId, _ProjectId, null, null, ref StatusCode, ref Message);
        }
        [HttpGet] // GET 
        public List<HXRRuleEntity> GetRule(string client_ID, string project_ID, string rule_Id, string rule_Name)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            long? RuleID = 0;
            RuleID = Convert.ToInt64(rule_Id);
            //long.TryParse(rule_Id, out RuleID);         
            if (RuleID <= 0)
                RuleID = null;
            return _ruleMS.GetRule(client_ID, project_ID, RuleID, rule_Name, ref StatusCode, ref Message);
        }

        [HttpGet] // GET api/hxrruleapi
        public string BackspaceButtonClick(string textval)
        {

            string StatusCode = string.Empty, Message = string.Empty;

            //    String textarea_val=request.getParameter("textval");
            string textarea_val = textval;
            if (textarea_val != null)
            {
                //String string_arr[]=textarea_val.split(" ");
                string[] string_arr = textarea_val.Split(' ');
                //String back_stringarr[]=new String[string_arr.length-1];
                string[] back_stringarr = new string[string_arr.Length - 1];
                //for(int i=0;i<string_arr.length-1;i++)
                for (int i = 0; i < string_arr.Length - 1; i++)
                    //    back_stringarr[i]=string_arr[i];
                    back_stringarr[i] = string_arr[i];
                ///*
                //for(int i=0;i<back_stringarr.length;i++)
                //{
                //    //System.out.println("back apace array--> "+back_stringarr[i]);
                //}
                //*/
                //String final_string="";
                string final_string = "";
                //for(int i=0;i<back_stringarr.length;i++)
                for (int i = 0; i < back_stringarr.Length; i++)
                //{
                {
                    //    final_string=final_string+" "+back_stringarr[i];
                    final_string = final_string + " " + back_stringarr[i];
                    //}
                }
                //////System.out.println("final--> "+final_string);
                //final_string=final_string.Trim();
                final_string = final_string.Trim();
                //pw.write(final_string);
                return final_string;
            }
            return null;
        }



        #region Validate Complete
        [HttpGet]
        public void ValidateComplete(string client_ID, string project_ID, string IP_Address, string database_Name, string table_Name, string Source_Target, string textval)
        {


        }
        public String getVarType(String amount)
        {
            bool onlyDigits = true;
            int dotCount = 0;
            if (amount == null)
            {
                return "noval";
            }
            String trimmed = amount.Trim();
            if (trimmed.Length == 0)
            {
                return "noval";
            }
            int a = 0;
            //if (trimmed.charAt(0) == '-')
            if ((trimmed.ToCharArray())[0] == '-')
            {
                a++;
            }
            for (int max = trimmed.Length; a < max; a++)
            {
                //if (trimmed.charAt(a) == '.')
                if ((trimmed.ToCharArray())[a] == '.')
                {
                    dotCount++;
                    if (dotCount > 1) break;
                }
                //else if (!Character.isDigit(trimmed.charAt(a)))
                else if (!Char.IsDigit((trimmed.ToCharArray())[a]))
                {
                    onlyDigits = false;
                    break;
                }
            }

            if (onlyDigits)
            {
                if (dotCount == 0)
                {
                    return "integer";
                }
                else if (dotCount == 1)
                {
                    return "float";
                }
                else
                {
                    return "string";
                }
            }
            return "string";
        }
        #endregion

        [HttpGet]
        public object GetErrorCode(string client_ID, string project_ID)
        //public List<string> GetErrorCode(string client_ID, string project_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            List<HXRErrorDesc> ErrorEntity = _ruleMS.GetErrorDesc(client_ID, project_ID, null, ref StatusCode, ref Message);
            List<string> ErrorCodes = ErrorEntity.Select(r => r.Error_code + ":" + r.Error_Description).ToList<string>();
            //List<string> ErrorCodes = ErrorEntity.Select(r => r.Error_code).ToList<string>();
            var errorcode = ErrorEntity.Select(r => new { ErrorCode = r.Error_code, ErrorDesc = r.Error_Description });
            // var errorcode1 = substr()

            //return ErrorCodes;
            return errorcode;


        }
        [HttpGet]
        public List<string> GetErrorDescByErrorCode(string client_ID, string project_ID, string ErrorCode)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            List<HXRErrorDesc> ErrorEntity = _ruleMS.GetErrorDesc(client_ID, project_ID, ErrorCode, ref StatusCode, ref Message);
            List<string> ErrorCodes = ErrorEntity.Select(r => r.Error_Description).ToList<string>();


            return ErrorCodes;
        }
        [HttpGet]
        public object GetRuleCategories()
        {
            string StatusCode = string.Empty, Message = string.Empty;
            List<HXRRuleCategory> _RuleCategory = _ruleMS.GetRuleCategory(null, null, ref StatusCode, ref Message);
            var RuleCategories = from cat in _RuleCategory
                                 select new
                                 {
                                     RuleCategory_Name = cat.RuleCategory_Name,
                                     RuleCategory_ID = cat.RuleCategory_ID
                                 };
            return RuleCategories;
        }
        public dynamic GetAllRules(string client_ID, string project_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            List<HXRRuleEntity> _Rule = _ruleMS.GetRule(client_ID, project_ID, null, null, ref StatusCode, ref Message);

            var rows = new
            {
                total = 0,
                page = 0,
                records = 0,
                rows = (
                    from rule in _Rule
                    select new
                    {
                        i = rule.Rule_ID,
                        cell = new string[] {
                         rule.Rule_ID.ToString(),
                         rule.Client_ID ,
                         rule.Project_ID,
                         rule.Rule_Name,                         
                         rule.Conditional_Clause,
                         rule.Default_value
                      }
                    }).ToArray()
            };
            return rows;
        }
        public dynamic GetAllRuleCategories()
        {
            string StatusCode = string.Empty, Message = string.Empty;
            List<HXRRuleCategory> _RuleCategory = _ruleMS.GetRuleCategory(null, null, ref StatusCode, ref Message);

            var rows = new
            {
                total = 0,
                page = 0,
                records = 0,
                rows = (
                    from rule in _RuleCategory
                    select new
                    {
                        i = rule.RuleCategory_ID,
                        cell = new string[] {
                            rule.RuleCategory_ID.ToString(),
                         rule.RuleCategory_Name,
                         rule.RuleCategory_Desc,
                         rule.Active_Flag.ToString()                         
                      }
                    }).ToArray()
            };
            return rows;
        }
        public dynamic GetAllRuleTypes()
        {

            string StatusCode = string.Empty, Message = string.Empty;
            string _ClientId = UIProperties.Sessions.Client.Client_ID;
            string _ProjectId = UIProperties.Sessions.Client.project_ID;
            List<HXRRuleTypeEntity> _RuleType = _ruleMS.GetRuleType(_ClientId, _ProjectId, null, null, ref StatusCode, ref Message);

            var rows = new
            {
                total = 0,
                page = 0,
                records = 0,
                rows = (
                    from rule in _RuleType
                    select new
                    {
                        i = rule.RuleType_ID,
                        cell = new string[] {
                            rule.RuleType_ID.ToString(),
                         rule.RuleType_Name,
                         rule.RuleType_Desc,
                         rule.Active_Flag.ToString()                         
                      }
                    }).ToArray()
            };
            return rows;
        }
        [HttpGet]
        public dynamic GetRuleAttributes(int page, int rows, string client_ID, string project_ID, string Table_Name, string Column_Name, long Rule_TypeID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            long TotalRecords = 0;
            var ruleAttributes = _ruleMS.GetRuleAttributes(page, rows, client_ID, project_ID, Table_Name, Column_Name, Rule_TypeID, ref  StatusCode, ref Message, ref TotalRecords) as IEnumerable<RuleAttributeEntity>;


            var _rows = new
            {
                total = Math.Ceiling(Convert.ToDouble(TotalRecords) / rows).ToString(),
                page = page.ToString(),
                records = TotalRecords,
                rows = (
                    from rule in ruleAttributes
                    select new
                    {
                        i = rule.Rule_ID,
                        cell = new string[] {
                         rule.Column_Name,
                         rule.RuleCategory_Name,
                         rule.RuleType_Name,
                         rule.Rule_Description,
                         rule.Rule_Desc,
                         rule.Conditional_Clause,                         
                         rule.Error_Description,
                         rule.status.ToString()
                      }
                    }).ToArray()
            };
            return _rows;

            //return _ruleMS.GetRuleAttributes(client_ID, project_ID, Table_Name, Column_Name, Rule_TypeID, ref  StatusCode, ref Message);

        }

        [HttpGet]
        public dynamic GetPreRuleAttributes(int page, int rows, string client_ID, string project_ID, string Table_Name, string Column_Name, long Rule_TypeID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            long TotalRecords = 0;
            var ruleAttributes = _ruleMS.GetRuleAttributes(1, 10, client_ID, project_ID, Table_Name, Column_Name, Rule_TypeID, ref  StatusCode, ref Message, ref TotalRecords) as IEnumerable<RuleAttributeEntity>;

            //'E': 'Error'
            ruleAttributes.Where(r => r.Priority == "E").ToList().ForEach(j => j.Priority = "Error");

            //'W': 'Warning'
            ruleAttributes.Where(r => r.Priority == "W").ToList().ForEach(j => j.Priority = "Warning");

            //'R': 'Report'
            ruleAttributes.Where(r => r.Priority == "R").ToList().ForEach(j => j.Priority = "Report");

            var _rows = new
            {
                total = Math.Ceiling(Convert.ToDouble(TotalRecords) / rows).ToString(),
                page = page.ToString(),
                records = TotalRecords.ToString(),
                rows = (
                    from rule in ruleAttributes
                    select new
                    {
                        i = rule.Rule_ID,
                        cell = new string[] {
                         rule.Attribute_Rule_ID.ToString(),  
                         rule.RuleType_Name,
                         rule.RuleCategory_Name,
                         rule.Priority,
                         rule.Rule_Name,
                         rule.Default_Value,
                         //rule.Error_Code,                         
                         rule.Error_Description
                         //,rule.status.ToString()
                      }
                    }).ToArray()
            };
            return _rows;

            //return _ruleMS.GetRuleAttributes(client_ID, project_ID, Table_Name, Column_Name, Rule_TypeID, ref  StatusCode, ref Message);

        }

        [HttpGet]
        public dynamic GetUserRuleAttributes(int page, int rows, string client_ID, string project_ID, string Table_Name, string Column_Name, long Rule_TypeID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            long TotalRecords = 0;
            var ruleAttributes = _ruleMS.GetRuleAttributes(1, 10, client_ID, project_ID, Table_Name, Column_Name, Rule_TypeID, ref  StatusCode, ref Message, ref TotalRecords) as IEnumerable<RuleAttributeEntity>;


            var _rows = new
            {
                total = Math.Ceiling(Convert.ToDouble(TotalRecords) / rows).ToString(),
                page = page.ToString(),
                records = TotalRecords.ToString(),
                rows = (
                    from rule in ruleAttributes
                    select new
                    {
                        i = rule.Rule_ID,
                        cell = new string[] {
                            rule.Attribute_Rule_ID.ToString(),
                            rule.Table_Name,
                            rule.Column_Name,
                            rule.RuleType_Name,
                            rule.RuleCategory_Name,
                            rule.Priority == "E" ? "Error" : rule.Priority == "W" ? "Warning":rule.Priority =="R"?"Report": rule.Priority,
                            rule.Rule_Description,
                            rule.Rule_Desc,
                            rule.Conditional_Clause,
                            //rule.Error_Code,                         
                            rule.Error_Description
                         //,rule.status.ToString()
                         ,rule.Data_Steward
                      }
                    }).ToArray()
            };
            return _rows;

            //return _ruleMS.GetRuleAttributes(client_ID, project_ID, Table_Name, Column_Name, Rule_TypeID, ref  StatusCode, ref Message);

        }



        [HttpGet]
        public dynamic GetAllocationRuleAttributes(int page, int rows, string client_ID, string project_ID, string Table_Name, string Column_Name, long Rule_TypeID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            long TotalRecords = 0;
            var ruleAttributes = _ruleMS.GetRuleAttributes(page, rows, client_ID, project_ID, Table_Name, Column_Name, Rule_TypeID, ref  StatusCode, ref Message, ref TotalRecords) as IEnumerable<RuleAttributeEntity>;

            //var pageIndex = Convert.ToInt32(1) - 1;
            //  var pageSize = 10;
            //var totalRecords = ruleAttributes.Count();
            //var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            //'Column Name', 'Rule Category', 'Rule Type', 'Rule Desc', 'Rule Condition', 'Error Code', 'Status'

            var _rows = new
            {
                total = Math.Ceiling(Convert.ToDouble(TotalRecords) / rows).ToString(),
                page = page.ToString(),
                records = TotalRecords.ToString(),
                rows = (
                    from rule in ruleAttributes
                    select new
                    {
                        i = rule.Rule_ID,
                        cell = new string[] {
                         rule.Attribute_Rule_ID.ToString(),
                         rule.Column_Name,
                         rule.RuleCategory_Name,
                         rule.RuleType_Name,
                         rule.Rule_Description,
                         rule.Rule_Desc,
                         rule.Conditional_Clause,                         
                         rule.Error_Description,
                         rule.status.ToString(),
                         rule.Version_No.ToString(),
                         rule.Start_Date.ToString(),
                         rule.End_Date.ToString()
                      }
                    }).ToArray()
            };
            return _rows;

            //return _ruleMS.GetRuleAttributes(client_ID, project_ID, Table_Name, Column_Name, Rule_TypeID, ref  StatusCode, ref Message);

        }

        [HttpGet]
        public string GenerateObjects(string client_ID, string project_ID, string table_name, string ConfigId)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            long ConfigID;
            long.TryParse(ConfigId, out ConfigID);
            _ruleMS.GenerateObjects(client_ID, project_ID, table_name, ConfigID, ref StatusCode, ref Message);
            return Message;
        }
        [HttpGet]
        public void UpdateVersionNumber(string client_ID, string project_ID, string ActiveAttributeRuleId, string InActiveAttributeRuleId, string table_name)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            string updated_by = string.Empty; //Get it from Session
            _ruleMS.UpdateVersionNumber(client_ID, project_ID, ActiveAttributeRuleId, InActiveAttributeRuleId, table_name, updated_by, ref StatusCode, ref Message);
        }
        [HttpGet]
        public void UpdateRuleAttribute(string client_ID, string project_ID, string attribute_Rule_ID, string rule_ID, long? ruleType_ID, string error_Code,
            long? ruleCategory_ID, string column_Name, string default_value, string conditional_Clause, string ruleDesc, string priority, string last_Modified_By, string Data_Steward)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            conditional_Clause = conditional_Clause.Replace("[", "b.[");
            _ruleMS.UpdateRuleAttribute(client_ID, project_ID, Convert.ToInt64(attribute_Rule_ID), rule_ID, ruleType_ID, "N", error_Code, ruleCategory_ID, column_Name, default_value,
                    conditional_Clause, ruleDesc, priority, last_Modified_By, Data_Steward, ref StatusCode, ref Message);
        }

        [HttpGet]
        public void UpdatePreRuleAttribute(string client_ID, string project_ID, long? attribute_Rule_ID, string rule_ID, long? ruleType_ID, string error_Code,
            long? ruleCategory_ID, string column_Name, string default_value, string conditional_Clause, string priority, string last_Modified_By)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            List<HXRRuleEntity> lstRule = _ruleMS.GetRule(client_ID, project_ID, Convert.ToInt32(rule_ID), null, ref StatusCode, ref Message);
            string ConditionalClause = string.Empty;
            if (lstRule.Count > 0)
            {
                ConditionalClause = lstRule[0].Conditional_Clause
                   .Replace("@col", "b." + column_Name)
                   .Replace("@default", default_value);
            }

            _ruleMS.UpdatePreRuleAttribute(client_ID, project_ID, attribute_Rule_ID, rule_ID, ruleType_ID, "Y", error_Code, ruleCategory_ID, column_Name, default_value,
                    ConditionalClause, priority, last_Modified_By, ref StatusCode, ref Message);
        }
        public List<HXRDataTypeFunctionsEntity> GetFunctionByDataType(string client_ID, string project_ID, string DataType)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            string _Type = UIProperties.GetDataType(DataType);

            List<HXRDataTypeFunctionsEntity> _lstFunctions = _ruleMS.GetFunctionByDataType(client_ID, project_ID, _Type, ref StatusCode, ref Message);

            return _lstFunctions;
        }

        [HttpPost]
        public string SaveHygieneRule([FromBody]RuleAttributeEntity[] ruleattributes)
        {
            try
            {
                foreach (RuleAttributeEntity item in ruleattributes)
                {
                    SavePreRuleAttribute(item.Client_ID, item.Project_ID, item.Rule_ID.ToString(), item.RuleType_ID, item.Error_Code, item.RuleCategory_ID,
                item.Table_Name, item.Column_Name, item.Default_Value, item.Conditional_Clause, item.Priority, item.Reference_Table, item.Reference_Column,
                item.Reference_Cond, UIProperties.Sessions.UserName, item.Data_Steward);
                }
                return "Saved successfully.";

            }
            catch (Exception _ex)
            {
                return null;
            }
        }
        [HttpGet]
        public string SavePreRuleAttribute(string client_ID, string project_ID, string rule_ID, long? ruleType_ID, string error_Code, long? ruleCategory_ID,
            string table_name, string column_Name, string default_value, string conditional_Clause, string priority, string reference_Table, string reference_Column,
            string reference_Cond, string last_Modified_By, string data_steward)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            last_Modified_By = UIProperties.Sessions.UserName;
            List<HXRRuleEntity> lstRule = _ruleMS.GetRule(client_ID, project_ID, Convert.ToInt32(rule_ID), null, ref StatusCode, ref Message);
            string ConditionalClause = string.Empty;
            if (lstRule.Count > 0)
            {
                ConditionalClause = lstRule[0].Conditional_Clause
                    .Replace("@col", "b." + column_Name)
                    .Replace("@default", default_value);
                _ruleMS.SavePreRuleAttribute
                    (client_ID, project_ID, rule_ID, ruleType_ID, error_Code, ruleCategory_ID, table_name, column_Name, default_value,
                    ConditionalClause, string.Empty, priority, reference_Table, reference_Column, reference_Cond, last_Modified_By, data_steward, ref StatusCode, ref Message);
            }
            return Message;

        }

        [HttpGet]
        public string SaveUserRuleAttribute(string client_ID, string project_ID, string rule_ID, long? ruleType_ID, string error_Code, long? ruleCategory_ID,
            string table_name, string column_Name, string default_value, string conditional_Clause, string rule_desc, string priority, string reference_Table,
            string reference_Column, string reference_Cond, string last_Modified_By, string data_steward)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            //  List<HXRRuleEntity> lstRule = _ruleMS.GetRule(client_ID, project_ID, null, "USER", ref StatusCode, ref Message);
            // List<HXRRuleTypeEntity> lstRuleType = _ruleMS.GetRuleType(ruleType_ID, "USER", ref StatusCode, ref Message);

            //if (lstRule != null)//&& lstRuleType != null)
            // {
            string ruleId = null;//lstRule[0].Rule_ID.ToString();
            // var ruleTypeId = ruleType_ID;//lstRuleType[0].RuleType_ID;


            _ruleMS.SaveUserRuleAttribute
                (client_ID, project_ID, ruleId, ruleType_ID, error_Code, ruleCategory_ID, table_name, column_Name, default_value, conditional_Clause, rule_desc,
                priority, reference_Table, reference_Column, reference_Cond, last_Modified_By, data_steward, ref StatusCode, ref Message);
            return Message;
            // }
            //return "Failed";

        }
        [HttpGet]
        public string GetUserDefineRuleValidation(string client_ID, string project_ID, string TableName, string Query)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            string MsgSuccess = "Validation Successful";
            // string MsgEnterRule = "Enter the rule..!!";
            string MsgInvalidRule = "Invalid Rule.";
            string MsgValidation = string.Empty;
            List<HXRRuleValidationEntity> ValidationMsg = _ruleMS.GetUserDefineRuleValidation(client_ID, project_ID, "7", UIProperties.Sessions.Client.Role_ID, TableName,
                Query, ref StatusCode, ref  Message);

            if (ValidationMsg != null && ValidationMsg.Count > 0)
            {
                MsgValidation = ValidationMsg[0].VAL;
                if (MsgValidation == "ERROR")
                {
                    return MsgInvalidRule;
                }
                else
                    return MsgSuccess;
            }
            return MsgInvalidRule;

        }
        [HttpGet]
        public void ActivateRuleAttribute(string client_ID, string project_ID, string Attribute_Rule_ID, string Status)//, string updated_by)
        {
            //string client_ID, string project_ID, string Attribute_Rule_ID, string Status, string updated_by, ref string status_Code, ref string message
            string StatusCode = string.Empty, Message = string.Empty;
            string updated_by = string.Empty; //Get it from Session
            int _attr_rule_id = 0;
            int.TryParse(Attribute_Rule_ID, out _attr_rule_id);
            if (_attr_rule_id > 0)
                _ruleMS.ActivateRuleAttribute(client_ID, project_ID, Attribute_Rule_ID, Status, updated_by, ref StatusCode, ref Message);
        }


        public HXRRuleValidationResultsEntity GetRuleValidationResults(string client_ID, string project_ID, string table_name, string run_ID,
            string run_User, long? ConfigID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            // run_User = "Hexaware";
            //run_User = UIProperties.Sessions.UserName;

            List<HXRRuleValidationResultsEntity> _RuleValidationResults = _ruleMS.GetRuleValidationResults(client_ID, project_ID, table_name, run_ID,
                run_User, ConfigID, ref StatusCode, ref Message);
            return _RuleValidationResults != null && _RuleValidationResults.Count > 0 ? _RuleValidationResults[0] : null;

        }


        #region Correction & Reprocess

        [HttpGet]
        public dynamic GetRuleErrorSummary(string client_ID, string project_ID, string config_ID, string table_name, string run_ID, long? Rule_cateogry_ID, long? Rule_name)
        {

            string StatusCode = string.Empty, Message = string.Empty;
            List<HXRRuleErrorSummary> _Rule = _ruleMS.GetRuleErrorSummary(client_ID, project_ID, table_name, run_ID, Rule_cateogry_ID, Rule_name, ref  StatusCode, ref  Message);

            var totalRecords = 0;
            var rows = new
            {
                total = 0,
                page = 0,
                records = 0,
                rows = (
                    from rule in _Rule
                    select new
                    {
                        i = totalRecords++,
                        cell = new string[] {
                         rule.RuleCategory_Name,
                         rule.Rule_Name,
                         rule.Re.ToString(),
                         rule.Rec_Pass_Percent.ToString(),
                         rule.Rec_Fail_Percent.ToString() ,
                         rule.Record_Fail_Count.ToString(),
                         rule.Rule_ID.ToString(),
                         rule.RuleCategory_ID.ToString()
                      }
                    }).ToArray()
            };
            return rows;
        }
        public object GetRuleValidationErrorData(string client_ID, string project_ID, long? config_ID, string table_name, string run_ID)
        {
            try
            {
                string StatusCode = string.Empty, Message = string.Empty;

                //HXRRuleValidationResultsEntity _RuleValidationResults = GetRuleValidationResults(client_ID, project_ID, table_name, run_ID,
                //    UIProperties.Sessions.UserName, config_ID);
                List<HXRRuleValidationResultsEntity> _lstRuleValidationResults = _ruleMS.GetRuleValidationResults(client_ID, project_ID, table_name, run_ID,
                UIProperties.Sessions.UserName, config_ID, ref StatusCode, ref Message);


                if (StatusCode == "-1" || _lstRuleValidationResults == null || _lstRuleValidationResults.Count <= 0)
                {
                    return Message;
                }
                HXRRuleValidationResultsEntity _RuleValidationResults = _lstRuleValidationResults[0];
                run_ID = _RuleValidationResults.Run_Id.ToString();

                long TotalCount = 0;
                DataTable _dtErrorData = _ruleMS.GetRuleValidationErrorData(client_ID, project_ID, config_ID, table_name, 1, 5, run_ID, null, null,
                    ref StatusCode, ref Message, ref TotalCount);


                string[] Columns = _dtErrorData.Columns.Cast<DataColumn>().Select(r => r.ColumnName).ToArray();
                string _Columns = string.Join(",", Columns);


                var data = new
                {
                    HXRRuleValidationResultsEntity = _RuleValidationResults,
                    ColNames = _Columns

                };
                return data;

            }
            catch (Exception _ex)
            {
                return _ex.Message;
            }
        }
        public object GetRuleValidationErrorData_Paging(int page, int rows, string client_ID, string project_ID, string Table_Name, int config_ID, string run_ID,
            string Rule_cateogry_ID, string Rule_name)
        {
            try
            {
                string StatusCode = string.Empty, Message = string.Empty;

                long TotalCount = 0;
                DataTable _dtErrorData = _ruleMS.GetRuleValidationErrorData(client_ID, project_ID, config_ID, Table_Name, page, rows, run_ID, Rule_cateogry_ID,
                    Rule_name, ref StatusCode, ref Message, ref TotalCount);

                int _ColumnCount = _dtErrorData.Columns.Count;

                var jstr = new _JSON();
                jstr.total = Math.Ceiling(Convert.ToDouble(TotalCount) / rows).ToString();
                jstr.page = page.ToString();
                jstr.records = TotalCount.ToString();
                jstr.rows = new List<DM_BusinessEntities.rows>();

                int _rowIndex = 1;
                _dtErrorData.Rows.Cast<DataRow>().ToList().ForEach(datarow =>
                {
                    string[] _r = new string[_ColumnCount];
                    int _colIndex = 0;
                    rows r = new DM_BusinessEntities.rows();

                    _dtErrorData.Columns.Cast<DataColumn>().ToList().ForEach(column =>
                    {
                        _r[_colIndex] = datarow[column].ToString();
                        _colIndex++;
                    });
                    r.id = _rowIndex.ToString();
                    //r.cell = new List<string[]>();                   

                    r.cell = _r;

                    jstr.rows.Add(r);
                    _rowIndex++;
                });
                return jstr;
            }
            catch (Exception _ex)
            {
                return _ex.Message;
            }
        }

        #endregion 

        public object GetRuleValidationErrorDataColumns(string client_ID, string project_ID, long? config_ID, string table_name, string run_ID, string Rule_cateogry_ID,
            string Rule_name)
        {
            try
            {
                string StatusCode = string.Empty, Message = string.Empty;

                long TotalCount = 0;
                DataTable _dtErrorData = _ruleMS.GetRuleValidationErrorData(client_ID, project_ID, config_ID, table_name, 1, 5, run_ID, null, null,
                    ref StatusCode, ref Message, ref TotalCount);

                string[] Columns = _dtErrorData.Columns.Cast<DataColumn>().Select(r => r.ColumnName).ToArray();
                string _Columns = string.Join(",", Columns);


                var data = new
                {
                    ColNames = _Columns
                };
                return data;

            }
            catch (Exception _ex)
            {
                return _ex.Message;
            }
        }
        [HttpGet]
        public string SubmitOfflineRun(string client_ID, string project_ID, long? config_ID, string table_name)//, string run_ID)
        {
            try
            {
                string StatusCode = string.Empty, Message = string.Empty;
                string procedure_name = string.Empty, parameter_list = string.Empty;

                _ruleMS.Offline_Run(client_ID, project_ID, (int)UIProperties.Tools.HexaRule, config_ID, null, null, table_name, null, null, null, procedure_name,
                    parameter_list, UIProperties.Sessions.UserName, ref StatusCode, ref Message);

                return Message;
            }
            catch (Exception _ex)
            {
                return _ex.Message;
            }
        }
        [HttpGet]
        public dynamic GetOfflineStatus(int page, int rows, string client_ID, string project_ID, string Table_Name)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            long TotalRecords = 0;

            HXROfflineStatusEntity _offline = new HXROfflineStatusEntity();
            _offline.Client_ID = client_ID;
            _offline.Project_ID = project_ID;
            _offline.Tool_ID = (int)UIProperties.Tools.HexaRule;
            _offline.Created_by = UIProperties.Sessions.UserName;

            var _results = _ruleMS.GetOfflineStatus(_offline, ref StatusCode, ref Message);

            var _rows = new
            {
                total = Math.Ceiling(Convert.ToDouble(TotalRecords) / rows).ToString(),
                page = page.ToString(),
                records = TotalRecords.ToString(),
                rows = (
                    from rule in _results
                    select new
                    {
                        i = rule.Offline_Job_ID,
                        cell = new string[] {
                            rule.Offline_Job_ID.ToString(),
                            rule.Table_Name,                                                        
                            rule.Run_Status,
                            rule.Run_Status_Message,
                            rule.Start_Time.ToString(),
                            rule.End_Time.ToString(),
                            rule.Created_by                            
                      }
                    }).ToArray()
            };
            return _rows;

            //return _ruleMS.GetRuleAttributes(client_ID, project_ID, Table_Name, Column_Name, Rule_TypeID, ref  StatusCode, ref Message);

        }
        [HttpGet]
        public object GetRuleValidationErrorData_Columns(string client_ID, string project_ID, long? config_ID, string table_name)
        {
            try
            {
                string StatusCode = string.Empty, Message = string.Empty;

                DataTable _dtErrorData = _ruleMS.GetRuleValidationErrorData_Paging(client_ID, project_ID, table_name, config_ID, 1, 5, ref StatusCode, ref Message);

                string[] Columns = _dtErrorData.Columns.Cast<DataColumn>().Select(r => r.ColumnName).ToArray();
                string _Columns = string.Join(",", Columns);

                // string json = JsonConvert.SerializeObject(_dtErrorData, new DataTableConverter());
                //  JsonSerializer DS = new JsonSerializer();
                //   DS.Serialize(rows);
                //"page":"1",
                //  "total":2,
                //  "records":"13", 
                //  "rows":
                var data = new
                {
                    ColNames = _Columns,
                    //  rows = new
                    //{
                    //page = "1",
                    //total = 10,
                    //records = 50,
                    //rows = json
                    //rows = rows
                    //}
                };
                return data;

            }
            catch (Exception _ex)
            {
                return _ex.Message;
            }
        }


              
        

        [HttpPost]
        public string UpdateSourceTable(HXRSourceTable srcTbl)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            //_ruleMS.UpdateSourceTable(TableName, PrimaryKeyCol, PrimaryKeyValue, UpdateCol, UpdateVal, update_all, run_id, ConfigID, ref StatusCode, ref Message);
            _ruleMS.UpdateSourceTable(srcTbl, ref StatusCode, ref Message);
            return Message;
        }

        //// GET api/hxrruleapi/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/hxrruleapi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/hxrruleapi/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/hxrruleapi/5        
        public void Delete(int id)
        {
        }
        [HttpGet] //api/HXRRuleAPI/GetRuleValidationErrorSampleData
        public object GetRuleValidationErrorSampleData(long config_ID, string table_name, string key_column, string key_value)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            DataTable dtRecord = _ruleMS.GetRuleValidationErrorSampleData(config_ID, table_name, key_column, key_value, ref StatusCode, ref Message);

            string[] Columns = dtRecord.Columns.Cast<DataColumn>().Select(r => r.ColumnName).ToArray();
            string _Columns = string.Join(",", Columns);

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dtRecord.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dtRecord.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            string json = JsonConvert.SerializeObject(dtRecord, new DataTableConverter());

            var data = new
            {
                ColNames = _Columns,
                rows = json
            };
            return data;
        }
        [HttpGet]
        public string InsertKeyColumns(string client_ID, string project_ID, string table_name, string key_column1, string key_column2,
            string key_column3, string key_column4, string key_column5, string last_Modified_By)
        {

            string StatusCode = string.Empty, Message = string.Empty;
            _ruleMS.InsertKeyColumns(client_ID, project_ID, UIProperties.Sessions.ConfigEntity.Config_ID, table_name, key_column1, key_column2, key_column3, key_column4,
                key_column5, last_Modified_By, ref StatusCode, ref Message);
            return Message;
        }
        [HttpGet]
        public string[] ValidateKeyColumns(string client_ID, string project_ID, string table_name, int Config_Id, string key_column1, string key_column2,
            string key_column3, string key_column4, string key_column5)
        {
            string StatusCode = string.Empty, Message = string.Empty, Is_Key_Columns = string.Empty;
            _ruleMS.ValidateKeyColumns(client_ID, project_ID, table_name, Config_Id, key_column1, key_column2, key_column3, key_column4, key_column5, ref Is_Key_Columns,
                ref StatusCode, ref Message);
            string[] msg = new string[2];
            msg[0] = Message;
            msg[1] = Is_Key_Columns;

            return msg;
        }

        [HttpGet]
        public object CheckKeyColumn(string client_ID, string project_ID, string table_Name, long? config_Id)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            _ruleMS.CheckKeyColumn(client_ID, project_ID, table_Name, config_Id, ref StatusCode, ref Message);

            var result = new { statuscode = StatusCode, message = Message };

            return result;
        }

        [HttpGet] // GET 
        // public List<HXRKeyColumnsEntity> GetKeyColumns(string client_ID, string project_ID, string table_name)
        public dynamic GetKeyColumns(string client_ID, string project_ID, string table_name, string config_Id)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            Nullable<long> config_ID = Convert.ToInt64(config_Id);
            //return _ruleMS.GetKeyColumns(client_ID, project_ID, table_name, ref StatusCode, ref Message);

            List<HXRKeyColumnsEntity> _RuleKeyColumn = _ruleMS.GetKeyColumns(client_ID, project_ID, table_name, config_ID, ref StatusCode, ref Message);
            var rows = new
            {
                total = 0,
                page = 0,
                records = 0,
                rows = (
                    from rule in _RuleKeyColumn
                    select new
                    {
                        i = rule.Table_Name,
                        cell = new string[] {
                            rule.Table_Name,
                            rule.Key_Column1,
                            rule.Key_Column2,
                            rule.Key_Column3,
                            rule.Key_Column4,
                            rule.Key_Column5
                      }
                    }).ToArray()
            };
            return rows;

        }
        [HttpGet]
        public dynamic GetAllRuleErrors(string client_ID, string project_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            List<HXRErrorDesc> _RuleError = _ruleMS.GetErrorDesc(client_ID, project_ID, null, ref StatusCode, ref Message);

            var rows = new
            {
                total = 0,
                page = 0,
                records = 0,
                rows = (
                    from rule in _RuleError
                    select new
                    {
                        i = rule.Error_code,
                        cell = new string[] {
                         rule.Error_code.ToString(),
                         rule.Error_Description, 
                         rule.Active_Flag.ToString()                         
                      }
                    }).ToArray()
            };
            return rows;
        }

        [HttpGet]
        public object GetTableData(string table_name, string ColumnList)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            string _ConfigId = UIProperties.Sessions.ConfigEntity.Config_ID.ToString();
            string _ClientId = UIProperties.Sessions.Client.Client_ID;
            string _ProjectId = UIProperties.Sessions.Client.project_ID;
            //string _ColumnList = "ALL";
            //string _TableName = table_name;

            ColumnList = "Top 100 " + ColumnList;

            DataTable dtRecord = _ruleMS.GetTableData(_ClientId, _ProjectId, _ConfigId, table_name, ColumnList, 0, ref StatusCode, ref Message);

            string[] Columns = dtRecord.Columns.Cast<DataColumn>().Select(r => r.ColumnName).ToArray();
            string _Columns = string.Join(",", Columns);

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dtRecord.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dtRecord.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            string json = JsonConvert.SerializeObject(dtRecord, new DataTableConverter());

            var data = new
            {
                ColNames = _Columns,
                rows = json
            };
            return data;
        }

       

    }
}
