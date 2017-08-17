using DM_BusinessEntities;
using DM_BusinessService;
using DM_UI.App_Start;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace DM_UI.Controllers
{
    public class AutomatAPIController : ApiController
    {
        private readonly IAutomaton _autoMS;

        public AutomatAPIController()
        {
            _autoMS = new HXRAutomatonService();
        }

        [HttpGet]
        public List<string> GetMetaDataConnectionList(string Client_ID, string Project_ID, long Tool_ID, string Sourcetarget)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            return _autoMS.GetMetaDataConnectionList(Client_ID, Project_ID, Tool_ID, UIProperties.Sessions.Client.Role_ID, Sourcetarget, ref StatusCode, ref Message);
        }

        [HttpGet]
        public List<string> GetMetaDataSourceAndTargetConnectionList(string Client_ID, string Project_ID, int Tool_ID, long? RoleId)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            //int Tool_ID = (int)UIProperties.Tools.Automaton;
            var lst = new List<string>();
            lst.AddRange(_autoMS.GetMetaDataConnectionList(Client_ID, Project_ID, Tool_ID, RoleId, "Source", ref StatusCode, ref Message));
            lst.AddRange(_autoMS.GetMetaDataConnectionList(Client_ID, Project_ID, Tool_ID, RoleId, "Target", ref StatusCode, ref Message));
            return lst;
        }

        [HttpGet]
        public dynamic GetSourceTargetTableColumnDetail(string client_ID, string project_ID, string Table_name, string connectionid)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            var lst = _autoMS.GetMetaDataTableDetail(client_ID, project_ID, Table_name, connectionid, ref  StatusCode, ref Message) as IEnumerable<AutomatonSourceEntity>;

            var rows = new
            {
                rows = (
                    from auto in lst
                    select new
                    {
                        i = auto.Column_Name,
                        cell = new string[] {                               
                                auto.Column_Name,
                                auto.Data_Type ,                             
                                auto.Data_Precision,
                                auto.Data_Scale,
                                auto.Field_Data                            
                      }
                    }).ToArray()
            };

            return rows;


        }



        private DataTable GetMaskColumns()
        {
            DataTable dtColumns = new DataTable();
            dtColumns.Columns.Add("Column_Name", typeof(string));
            dtColumns.Columns.Add("Masking_Type", typeof(string));

            return dtColumns;
        }
        [HttpGet]
        public object GetDataMaskingColumn(string client_ID, string project_ID, string ConfigID, string table_name, string selected_columns)
        {
            try
            {
                string StatusCode = string.Empty, Message = string.Empty, profileStatus = string.Empty, profileDesc = string.Empty;
                string[] _selectedColumns = selected_columns.Split(',');
                DataTable dtColumns = GetMaskColumns();

                foreach (var item in _selectedColumns)
                {
                    string[] res = item.Split(':');
                    DataRow _r = dtColumns.NewRow();
                    _r["Column_Name"] = res[0];
                    _r["Masking_Type"] = res[1];
                    dtColumns.Rows.Add(_r);
                }
                DataTable dtRecord = _autoMS.Get_Mask_Table_Columns(client_ID, project_ID, table_name, Convert.ToInt64(ConfigID), dtColumns, ref StatusCode, ref Message);

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


                //return "Success:";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpGet]
        public List<MaskEntity> GetMaskTypes(string column_Type)
        {
            //string[] _arres;
            // string _strres;
            try
            {
                string StatusCode = string.Empty, Message = string.Empty;

                List<MaskEntity> _lstResult = _autoMS.GetMaskTypes(column_Type, ref StatusCode, ref Message);

                //_arres = _lstResult.Select(r => r.Mask_Type + ":" + r.Mask_Type).ToArray();
                //_strres = string.Join(";",_arres);
                //return _strres;
                //return "Success:";
                return _lstResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet] //api/DataProfilerAPI/DataProfiling
        public string SubmitDataMasking(string client_ID, string project_ID, string ConfigID, string table_name, string selected_columns)
        {
            try
            {
                string StatusCode = string.Empty, Message = string.Empty, profileStatus = string.Empty, profileDesc = string.Empty;

                return "";

                //return "Success:";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        [ActionName("GetMetaDataTableNames")]
        public List<string> Get(string client_ID, string project_ID, string config_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            return _autoMS.GetMetaDataTableNames(client_ID, project_ID, config_ID, ref StatusCode, ref Message);


        }
        [HttpGet]
        public List<string> GetTransformationType(string Client_ID, string Project_ID, long Tool_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            return _autoMS.GetTransformationDesc(Client_ID, Project_ID, Tool_ID, ref StatusCode, ref Message);
        }
        [HttpGet]
        public List<string> GetSourceTargetTable(string Client_ID, string Project_ID, long Tool_ID, string TemplateName, string type)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            return _autoMS.GetTransSuorceTargetTable(Client_ID, Project_ID, Tool_ID, TemplateName, type, ref StatusCode, ref Message);
        }
        [HttpGet]
        public List<string> GetMetaDataTableNameList(string Client_ID, string Project_ID, string Source_Target, long Tool_Id)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            return _autoMS.GetMetaDataTableNameList(Client_ID, Project_ID, Source_Target, Tool_Id, UIProperties.Sessions.Client.Role_ID, ref StatusCode, ref Message);
        }
        [HttpGet]
        public List<string> GetSourceTargetColumnsByTableName(string Client_ID, string Project_ID, long Tool_ID, string TemplateName, string TableName, string type)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            return _autoMS.GetSourceTargetColumnsByTableName(Client_ID, Project_ID, Tool_ID, TemplateName, TableName, type, ref StatusCode, ref Message);
        }
        [HttpGet]
        public List<string> GetTemplateList(string client_id, string Type, string Project_id)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            return _autoMS.GetTemplateList(client_id, Project_id, Type, ref StatusCode, ref Message);
        }
        [HttpGet]
        public dynamic GetTemplateNameList(string client_id, string Project_id, string Type)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            var mlist = _autoMS.GetTemplateNameList(client_id, Project_id, Type, ref StatusCode, ref Message) as IEnumerable<TemplateList>;

            var rows = new
            {
                rows = (
                    from auto in mlist
                    select new
                    {
                        i = auto.Template_ID,
                        cell = new string[] {
                             Convert.ToString(auto.Template_ID),
                            auto.Template_Name
                           

                      }
                    }).ToArray()
            };

            return rows;
        }
        [HttpGet]
        public dynamic GetTemplateNameListForScheduler(string client_id, string Project_id, string Type)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            var mlist = _autoMS.GetTemplateNameList(client_id, Project_id, Type, ref StatusCode, ref Message) as IEnumerable<TemplateList>;

            var rows = (from auto in mlist
                        select new
                        {
                            label = auto.Template_Name,
                            value = Convert.ToString(auto.Template_ID)
                        }).ToArray();


            return rows;
        }
        [HttpGet]
        public List<string> GetTemplateSourceTableList(string client_id, string Project_id, string Templatename)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            return _autoMS.GetTemplateSourceTableList(client_id, Project_id, Templatename, ref StatusCode, ref Message);
        }
        [HttpGet]
        public List<string> GetTemplateTargetTableList(string client_id, string Project_id, string Templatename, string type)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            return _autoMS.GetTemplateTargetTableList(client_id, Project_id, Templatename, type, ref StatusCode, ref Message);
        }
        [HttpGet]
        public object GetSrcList(string client_ID, string project_ID, string Templatename)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            List<string> SrcList = _autoMS.GetTemplateSourceTableList(client_ID, project_ID, Templatename, ref StatusCode, ref Message);
            return SrcList;
        }
        [HttpGet]
        public object GettargetList(string client_ID, string project_ID, string Templatename, string type)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            List<string> SrcList = _autoMS.GetTemplateTargetTableList(client_ID, project_ID, Templatename, type, ref StatusCode, ref Message);
            return SrcList;
        }
        [HttpGet]
        public object GettransList(string client_ID, string project_ID, string Templatename, string type)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            List<string> SrcList = _autoMS.GetTemplateTransTableList(client_ID, project_ID, Templatename, type, ref StatusCode, ref Message);
            return SrcList;
        }
        [HttpPost]
        public string SaveSourceGrid(List<SourceEntity> SourceEntity)
        {
            string StatusCode = string.Empty, Message = string.Empty, TemplateID = string.Empty;
            if (SourceEntity.Count <= 0)
            {
                return "0";
            }

            var Client_ID = SourceEntity[0].Client_ID;
            var Project_ID = SourceEntity[0].Project_ID;
            var TemplateName = SourceEntity[0].Template_Name;
            var Created_By = SourceEntity[0].Created_By;

            long Template_ID = _autoMS.InsertTemplate(Client_ID, Project_ID, TemplateName, Created_By, "DataType");

            foreach (var obj in SourceEntity)
            {
                obj.Template_ID = Convert.ToString(Template_ID);
            }
            _autoMS.SaveSourceGrid(SourceEntity, ref StatusCode, ref Message);

            return Template_ID.ToString();

        }
        [HttpPost]
        public void SaveTargetGrid(List<TargetEntity> TargetEntity)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            if (TargetEntity.Count <= 0)
            {
                return;
            }

            var Client_ID = TargetEntity[0].Client_ID;
            var Project_ID = TargetEntity[0].Project_ID;
            var TemplateName = TargetEntity[0].Template_Name;
            var Created_By = TargetEntity[0].Created_By;

            long Template_ID = _autoMS.InsertTemplate(Client_ID, Project_ID, TemplateName, Created_By, "DataType");

            foreach (var obj in TargetEntity)
            {
                obj.Template_ID = Convert.ToString(Template_ID);
            }

            _autoMS.SaveTargetGrid(TargetEntity, ref StatusCode, ref Message);

        }
        [HttpPost]
        public void SaveTransformGrid(List<TransformEntity> TransEntity)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            _autoMS.SaveTransGrid(TransEntity, ref StatusCode, ref Message);

        }
        [HttpPost]
        public void ModifySourceGrid(List<SourceEntity> SourceEntity)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            _autoMS.ModifySourceGrid(SourceEntity, ref StatusCode, ref Message);
        }
        [HttpPost]
        public void ModifyTargetGrid(List<TargetEntity> TargetEntity)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            _autoMS.ModifyTargetGrid(TargetEntity, ref StatusCode, ref Message);
        }
        [HttpPost]
        public void ModifyTransGrid(List<TransformEntity> TransEntity)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            _autoMS.ModifyTransGrid(TransEntity, ref StatusCode, ref Message);
        }

        [HttpGet]
        public dynamic GetTemplateTargetRecords(string client_ID, string project_ID, string Template_Id, string Template_Name, string sourceTargettype)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            var SourceTemplate = _autoMS.GetTargetTemplateList(client_ID, project_ID, Template_Id, Template_Name, sourceTargettype, ref  StatusCode, ref Message) as IEnumerable<TemplateSourceTargetEntity>;
            var rows = new
            {
                rows = (
                    from auto in SourceTemplate
                    select new
                    {
                        i = auto.Field_Seq_No,
                        cell = new string[] { 
                            auto.Field_Seq_No,
                            auto.Field_Type,
                            auto.Field_Name,
                            auto.Field_Data_Type,
                            auto.Field_Prec,
                            auto.Field_Scale,
                            auto.Field_Data,
                            auto.Field_Key,
                            auto.Table_Name ,
                            auto.Row_ID
                            
                      }
                    }).ToArray()


            };
            return rows;
        }
        [HttpGet]
        public dynamic GetTemplateSourceRecords(string client_ID, string project_ID, string Template_Id, string Template_Name, string sourceTargettype)
        {

            string StatusCode = string.Empty, Message = string.Empty;
            var SourceTemplate = _autoMS.GetTemplateSourceTargetTableList(client_ID, project_ID, Template_Id, ref  StatusCode, ref Message) as IEnumerable<TemplateSourceTargetEntity>;
            var rows = new
            {

                rows = (
                   from auto in SourceTemplate
                   select new
                   {
                       i = auto.Field_Seq_No,
                       cell = new string[] {
                            auto.Field_Seq_No,                                                                                
                            auto.Table_Name,
                            auto.Field_Name,
                            auto.Field_Data_Type,
                            auto.Target_Table_Name,
                            auto.Target_Field_Name,
                            auto.Target_Data_Type,
                             auto.Field_Type,
                            auto.Row_ID,
                           auto.Target_Row_ID 
                            
                      }
                   }).ToArray()
            };
            return rows;
        }



        [HttpGet]
        public dynamic GetTransTemplateList(string client_ID, string project_ID, string TemplateName)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            var AutomatonAttributes = _autoMS.GetTransTemplateList(client_ID, project_ID, TemplateName, ref  StatusCode, ref Message) as IEnumerable<TransformEntity>;
            var rows = new
            {
                rows = (
                    from auto in AutomatonAttributes
                    select new
                    {
                        i = auto.Trans_Order,
                        cell = new string[] {
                            auto.Trans_Order,
                            auto.Trans_Type,
                            auto.Trans_Name,
                            auto.Trans_Rule,
                            auto.Table_Name,
                            auto.Trans_Field,
                            auto.Field_Type,
                            auto.Field_Length,                            
                            auto.Trans_ID 
                      }
                    }).ToArray()
            };

            return rows;
        }

        [HttpGet]
        public dynamic GetTableDataDetail(string client_ID, string project_ID, string Database_IP, string Table_name, string connectionid, string Field_Type, long Recordcount, string tgtConnectionid)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            var lst = _autoMS.GetMetaDataTableDetail(client_ID, project_ID, Table_name, connectionid, ref  StatusCode, ref Message) as IEnumerable<AutomatonSourceEntity>;
            var targetRecordCount = Recordcount;
            var totalTransactions = Recordcount == 0 ? 1 : Recordcount + 1;
            var lst_target = _autoMS.GetMetaDataTableBusinessName(client_ID, project_ID, Table_name, tgtConnectionid, ref  StatusCode, ref Message) as IEnumerable<AutomatonSourceEntity>;


            totalTransactions = targetRecordCount == 0 ? 1 : targetRecordCount + 1;

            var mtargetrows = (from auto in lst_target
                               select new AutomatonSourceEntity()
                               {
                                   Field_Seq_No = Convert.ToString(totalTransactions++),
                                   Field_Type = Field_Type,
                                   Column_Name = auto.Column_Name,
                                   Data_Type = auto.Data_Type,
                                   Data_Precision = auto.Data_Precision,
                                   Data_Scale = auto.Data_Scale,
                                   Field_Data = auto.Field_Data,
                                   Key_column = auto.Key_column,
                                   Table_Name = auto.Table_Name,
                                   Row_ID = "0"

                               }).ToList();

            totalTransactions = Recordcount == 0 ? 1 : Recordcount + 1;

            var mrows = (from auto in lst
                         select new AutomatonSourceEntity()
                         {
                             Field_Seq_No = Convert.ToString(totalTransactions++),
                             Field_Type = Field_Type,
                             Column_Name = auto.Column_Name,
                             Data_Type = auto.Data_Type,
                             Data_Precision = auto.Data_Precision,
                             Data_Scale = auto.Data_Scale,
                             Field_Data = auto.Field_Data,
                             Key_column = auto.Key_column,
                             Table_Name = auto.Table_Name,
                             Row_ID = "0"
                         }).ToList();


            var result = (from auto in mrows
                          select new
                          {
                              Field_Seq_No = auto.Field_Seq_No,
                              Field_Type = Field_Type,
                              Column_Name = auto.Column_Name,
                              Data_Type = auto.Data_Type,
                              Data_Precision = auto.Data_Precision,
                              Data_Scale = auto.Data_Scale,
                              Field_Data = auto.Field_Data,
                              Key_column = auto.Key_column,
                              Table_Name = auto.Table_Name,
                              Row_ID = "0",
                              Target_Seq_No = (from auto_busniness in mtargetrows
                                               where auto_busniness.Field_Seq_No == auto.Field_Seq_No
                                               select auto_busniness.Field_Seq_No).FirstOrDefault<string>(),
                              Target_Column_Name = (from auto_busniness in mtargetrows
                                                    where auto_busniness.Field_Seq_No == auto.Field_Seq_No
                                                    select auto_busniness.Column_Name).FirstOrDefault<string>(),
                              Target_Table_Name = (from auto_busniness in mtargetrows
                                                   where auto_busniness.Field_Seq_No == auto.Field_Seq_No
                                                   select auto_busniness.Table_Name).FirstOrDefault<string>(),
                              Target_Data_Type = auto.Data_Type,
                              Target_Precision = auto.Data_Precision
                          }).ToList();

            return result;
        }







        [HttpGet]
        public dynamic GetTableColumnDetail(string client_ID, string project_ID, string Table_name, string connectionid)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            var lst = _autoMS.GetMetaDataTableDetail(client_ID, project_ID, Table_name, connectionid, ref  StatusCode, ref Message) as IEnumerable<AutomatonSourceEntity>;

            var mrows = (from auto in lst
                         select new AutomatonSourceEntity()
                         {
                             Column_Name = auto.Column_Name,
                             Data_Type = auto.Data_Type

                         }).ToList();
            return mrows;


        }

        [HttpGet]
        public dynamic GetTableColumnDetailForAutoComplete(string client_ID, string project_ID, string Table_name, string connectionid)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            var lst = _autoMS.GetMetaDataTableDetail(client_ID, project_ID, Table_name, connectionid, ref  StatusCode, ref Message) as IEnumerable<AutomatonSourceEntity>;

            var mrows = (from auto in lst
                         select new
                         {
                             label = auto.Column_Name,
                             value = auto.Data_Type

                         }).ToArray();
            return mrows;

        }

        [HttpPost]
        public dynamic GetTransformationTableData(TransitionAdd transEntity)
        {
            string StatusCode = string.Empty, Message = string.Empty;


            var trans = new TransformEntity();

            trans.Table_Name = transEntity.TableName;
            trans.Field_Name = transEntity.ColumnName;
            trans.Trans_Name = transEntity.TransName;
            trans.Trans_Order = transEntity.TransOrder;
            trans.Trans_Rule = transEntity.TransRule;
            trans.Trans_Type = transEntity.TransType;

            //var lst = _autoMS.GetColumnDetail(transEntity.Client_ID, transEntity.Project_Id, transEntity.TemplateName, transEntity.TableName, transEntity.ColumnName, ref  StatusCode, ref Message) as List<TransformEntity>;

            //if (lst != null && lst.Count > 0)
            //{
            //    trans.Field_Data_Type = lst[0].Field_Data_Type;
            //    trans.Field_Length = lst[0].Field_Prec;
            //}

            var lstTable = new List<AutomatonSourceEntity>();
            lstTable = _autoMS.GetMetaDataTableDetail(transEntity.Client_ID, transEntity.Project_Id, transEntity.TableName, transEntity.SourceConnectionID, ref  StatusCode, ref Message) as List<AutomatonSourceEntity>;
            if (lstTable.Count > 0)
            {
                var ls = lstTable.Where(DB => DB.Column_Name == transEntity.ColumnName).FirstOrDefault();

                if (ls != null)
                {
                    trans.Field_Data_Type = ls.Data_Type;
                    trans.Field_Length = ls.Data_Precision;
                }
            }
            else
            {
                lstTable = _autoMS.GetMetaDataTableDetail(transEntity.Client_ID, transEntity.Project_Id, transEntity.TableName, transEntity.TargetConnectionID, ref  StatusCode, ref Message) as List<AutomatonSourceEntity>;
                var ls1 = lstTable.Where(DB => DB.Column_Name == transEntity.ColumnName).FirstOrDefault();

                if (ls1 != null)
                {
                    trans.Field_Data_Type = ls1.Data_Type;
                    trans.Field_Length = ls1.Data_Precision;
                }
            }


            trans.Trans_ID = "0";

            return trans;
        }
        [HttpGet]
        public dynamic GetSourceTemplateList(string client_ID, string project_ID, string Template_Id, string Template_Name, string sourceTargettype)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            var SourceTemplate = _autoMS.GetSourceTemplateList(client_ID, project_ID, Template_Id, Template_Name, sourceTargettype, ref  StatusCode, ref Message) as IEnumerable<TemplateSourceTargetEntity>;
            var rows = new
            {
                rows = (
                    from auto in SourceTemplate
                    select new
                    {
                        i = auto.Field_Seq_No,
                        cell = new string[] {
                            auto.Table_Name,
                            auto.Field_Name,
                            auto.Field_Seq_No,
                            auto.Field_Type,
                            auto.Field_Data_Type,
                            auto.Field_Prec,
                            auto.Field_Scale,
                            auto.Field_Data,
                            auto.Field_Key, 
                            auto.Row_ID
                            
                      }
                    }).ToArray()
            };
            return rows;

        }
        [HttpGet]
        public dynamic GetTargetTemplateList(string client_ID, string project_ID, string Template_Id, string Template_Name, string sourceTargettype)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            var SourceTemplate = _autoMS.GetTargetTemplateList(client_ID, project_ID, Template_Id, Template_Name, sourceTargettype, ref  StatusCode, ref Message) as IEnumerable<TemplateSourceTargetEntity>;
            var rows = new
            {
                rows = (
                    from auto in SourceTemplate
                    select new
                    {
                        i = auto.Field_Seq_No,
                        cell = new string[] { 
                            auto.Table_Name, 
                            auto.Field_Name,
                            auto.Field_Seq_No,
                            auto.Field_Type,
                            auto.Field_Data_Type,
                            auto.Field_Prec,
                            auto.Field_Scale,
                            auto.Field_Data,
                            auto.Field_Key, 
                            auto.Row_ID
                            
                      }
                    }).ToArray()


            };
            return rows;
        }

        public object GetBatchData(string client_ID, string project_ID, long? config_ID, string table_name, string BatchID, string SheetName, string FileName)
        {
            try
            {
                string StatusCode = string.Empty, Message = string.Empty;

                long TotalCount = 0;
                // DataTable _dtData = _autoMS.GetBatchData(client_ID, project_ID, config_ID, table_name, BatchID, 1, 5, ref StatusCode, ref Message, ref TotalCount);
                DataTable _dtData = new DataTable();

                //if (!string.IsNullOrWhiteSpace(FileName))
                //{
                //    string ExcelFileLocation = ConfigurationManager.AppSettings["ExcelFileLocation"];
                //    string FilePath = ExcelFileLocation + FileName;
                //    List<string> SheetNames = SheetName.Split('~').Distinct<string>().ToList();
                //    var TargetAutomatonAttributes = new List<AutomatonSourceEntity>();
                //    foreach (string mSheetName in SheetNames)
                //    {
                //        var dt = CommonHelper.ReadExcelData(FilePath, System.IO.Path.GetExtension(FilePath), "Yes", mSheetName);
                //        _dtData.Merge(dt);
                //    }

                //}
                //else
                //{
                _dtData = _autoMS.GetBatchData(client_ID, project_ID, config_ID, table_name, BatchID, 1, 5, ref StatusCode, ref Message, ref TotalCount);
                //}

                string[] Columns = _dtData.Columns.Cast<DataColumn>().Select(r => r.ColumnName).ToArray();
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
        public object GetBatchData_Paging(int page, int rows, string client_ID, string project_ID, int config_ID, string Table_Name, string BatchID, string SheetName, string FileName)
        {
            try
            {
                string StatusCode = string.Empty, Message = string.Empty;

                long TotalCount = 0;
                //DataTable _dtData = _autoMS.GetBatchData(client_ID, project_ID, config_ID, Table_Name, BatchID, page, rows, ref StatusCode, ref Message, ref TotalCount);


                DataTable _dtData = new DataTable();

                //if (!string.IsNullOrWhiteSpace(FileName))
                //{
                //    string ExcelFileLocation = ConfigurationManager.AppSettings["ExcelFileLocation"];
                //    string FilePath = ExcelFileLocation + FileName;
                //    List<string> SheetNames = SheetName.Split('~').Distinct<string>().ToList();
                //    var TargetAutomatonAttributes = new List<AutomatonSourceEntity>();
                //    foreach (string mSheetName in SheetNames)
                //    {
                //        var dt = CommonHelper.ReadExcelData(FilePath, System.IO.Path.GetExtension(FilePath), "Yes", mSheetName);
                //        _dtData.Merge(dt);
                //    }

                //}
                //else
                //{
                _dtData = _autoMS.GetBatchData(client_ID, project_ID, config_ID, Table_Name, BatchID, page, rows, ref StatusCode, ref Message, ref TotalCount);
                //}



                TotalCount = _dtData.Rows.Count;// remove once paging Concept is implemented in stored Procedure

                int _ColumnCount = _dtData.Columns.Count;

                var jstr = new _JSON();
                jstr.total = Math.Ceiling(Convert.ToDouble(TotalCount) / rows).ToString();
                jstr.page = page.ToString();
                jstr.records = TotalCount.ToString();
                jstr.rows = new List<DM_BusinessEntities.rows>();

                int _rowIndex = 1;
                _dtData.Rows.Cast<DataRow>().ToList().ForEach(datarow =>
                {
                    string[] _r = new string[_ColumnCount];
                    int _colIndex = 0;
                    rows r = new DM_BusinessEntities.rows();

                    _dtData.Columns.Cast<DataColumn>().ToList().ForEach(column =>
                    {
                        _r[_colIndex] = datarow[column].ToString();
                        _colIndex++;
                    });
                    r.id = _rowIndex.ToString();

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

        [HttpPost]
        public string SaveSourceTargetGrid(List<SourceEntity> SourceEntityList)
        {
            string StatusCode = string.Empty, Message = string.Empty, TemplateID = string.Empty;


            if (SourceEntityList.Count <= 0)
            {
                return "0";
            }

            var Client_ID = SourceEntityList[0].Client_ID;
            var Project_ID = SourceEntityList[0].Project_ID;
            var TemplateName = SourceEntityList[0].Template_Name;
            var Created_By = SourceEntityList[0].Created_By;

            long Template_ID = _autoMS.InsertTemplate(Client_ID, Project_ID, TemplateName, Created_By, "Transformation");

            foreach (var obj in SourceEntityList)
            {
                obj.Template_ID = Convert.ToString(Template_ID);
            }


            var lists = SourceEntityList.Where(a => a.Field_Name != null && a.Field_Name != "").ToList();

            //Save Source Records to Table 
            _autoMS.SaveSourceGrid(lists, ref StatusCode, ref Message);

            //Save Target Records To Table 
            var TargetEntity = new List<TargetEntity>();

            foreach (var mobject in SourceEntityList)
            {
                var mlocalobj = new TargetEntity();
                mlocalobj.Client_ID = mobject.Client_ID;
                mlocalobj.Project_ID = mobject.Project_ID;
                mlocalobj.Connection_ID = mobject.Target_Connection_ID;

                if (string.IsNullOrWhiteSpace(mobject.Target_Table_Name))
                {
                    mlocalobj.Field_Name = "Ignore";
                }
                else
                {
                    mlocalobj.Field_Name = mobject.Target_Field_Name;
                }


                if (string.IsNullOrWhiteSpace(mobject.Template_ID))
                {
                    mlocalobj.Template_ID = Template_ID.ToString();
                }
                else
                {
                    mlocalobj.Template_ID = mobject.Template_ID;
                }
                mlocalobj.Table_Name = mobject.Target_Table_Name;
                mlocalobj.Template_Name = mobject.Template_Name;
                mlocalobj.Field_Type = "Target";
                mlocalobj.Field_Seq_No = mobject.Field_Seq_No;

                mlocalobj.Field_Data_Type = mobject.Target_Data_Type;
                mlocalobj.Row_ID = mobject.Target_Row_ID == "" ? "0" : mobject.Target_Row_ID;
                mlocalobj.Create_Date = mobject.Create_Date;
                mlocalobj.Modified_Date = mobject.Modified_Date;
                mlocalobj.Created_By = mobject.Created_By;

                TargetEntity.Add(mlocalobj);
            }
            _autoMS.SaveTargetGrid(TargetEntity, ref StatusCode, ref Message);

            return Template_ID.ToString();

        }

        [HttpGet]
        public dynamic GetTemplateSourceTargetRecords(string client_ID, string project_ID, string Template_Id)
        {

            string StatusCode = string.Empty, Message = string.Empty;
            var SourceTemplate = _autoMS.GetTemplateSourceTargetTableList(client_ID, project_ID, Template_Id, ref  StatusCode, ref Message) as IEnumerable<TemplateSourceTargetEntity>;
            var rows = new
            {
                rows = (
                    from auto in SourceTemplate
                    select new
                    {
                        i = auto.Field_Seq_No,
                        cell = new string[] {
                            auto.Field_Seq_No,                                                    
                            auto.Target_Table_Name,
                            auto.Target_Field_Name,
                            auto.Target_Data_Type,
                            auto.Table_Name,
                            auto.Field_Name,
                            auto.Field_Data_Type,
                             auto.Field_Type,
                            auto.Row_ID,
                           auto.Target_Row_ID 
                            
                      }
                    }).ToArray()
            };
            return rows;

        }

        [HttpGet]
        public dynamic GetEntityList(string client_ID, string project_ID, long Config_ID)
        {

            string StatusCode = string.Empty, Message = string.Empty;
            var _Entities = _autoMS.GetEntityList(client_ID, project_ID, Config_ID, ref StatusCode, ref Message);

            var rows = new
            {
                total = 0,
                page = 0,
                records = 0,
                rows = (
                    from auto in _Entities
                    select new
                    {
                        // i = auto.Entity_ID,
                        cell = new string[] { 
                            auto.Entity_ID.ToString(),
                            auto.Entity_Name,
                            auto.Entity_Business_Name,
                            auto.Entity_Description,
                            auto.Legacy_Appl_Name,
                            auto.Ordinal_Position.ToString(),
                            auto.Input_Type,
                            auto.LOB,                         
                            auto.Parent_Entity_ID.ToString()                
                            
                      }
                    }).ToArray()


            };
            return rows;


        }

        [HttpGet]
        public dynamic GetEntityColList(string client_ID, string project_ID, long Entity_Id, long Config_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            var _Entities = _autoMS.GetEntityColList(client_ID, project_ID, Entity_Id, Config_ID, ref StatusCode, ref Message);

            var rows = new
            {
                rows = (
                    from auto in _Entities
                    select new
                    {
                        i = auto.Column_Name,
                        cell = new string[] {
                            auto.ID.ToString(),
                            auto.Entity_ID.ToString(),
                            auto.Column_Name,
                            auto.Attribute_Business_Name,
                            auto.Attribute_Business_Data_Type,
                            auto.Attribute_Business_Data_precision,
                            auto.Attribute_Desscription                            
                      }
                    }).ToArray()


            };
            return rows;


        }

        [HttpGet]
        public dynamic GetEntityNameList(string client_ID, string project_ID, long Config_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            var _Entities = _autoMS.GetEntityList(client_ID, project_ID, Config_ID, ref StatusCode, ref Message);
            return _Entities;
        }


        [HttpPost]
        public string SaveEntityList(List<BusinessNameEntity> mobject)
        {
            //Duplicate validation
            var businessname = mobject.GroupBy(g => g.Entity_Business_Name).Where(h => h.Count() > 1);
            //var _obj = mobject.Where(j => j.Ordinal_Position > 0).ToList();
            //var ordinalPositon = _obj.GroupBy(g => g.Ordinal_Position).Where(h => h.Count() > 1);

            if (businessname.Count() > 0)
                return "Saving failed. Business Name must be unique. ";

            //if (ordinalPositon.Count() > 0)
            //    return "Saving failed. Ordinal Position must be unique. ";



            return _autoMS.SaveEntityName(mobject);
        }
        private DataTable GetEntityColTable()
        {
            DataTable dtColumns = new DataTable();
            dtColumns.Columns.Add("Entity_ID", typeof(Int32));
            dtColumns.Columns.Add("Client_ID", typeof(string));
            dtColumns.Columns.Add("Project_ID", typeof(string));
            dtColumns.Columns.Add("Attribute_Business_Name", typeof(string));
            dtColumns.Columns.Add("Attribute_Business_Data_Type", typeof(string));
            dtColumns.Columns.Add("Attribute_Business_Data_precision", typeof(string));
            dtColumns.Columns.Add("Attribute_Desscription", typeof(string));
            dtColumns.Columns.Add("Column_Name", typeof(string));
            dtColumns.Columns.Add("Modified_By", typeof(string));
            return dtColumns;
        }
        [HttpPost]
        public string SaveEntityCol(List<BusinessColumnEntity> colObj)
        {
            try
            {
                string StatusCode = string.Empty, Message = string.Empty;

                DataTable dtColumns = GetEntityColTable();

                foreach (var item in colObj)
                {
                    DataRow _r = dtColumns.NewRow();
                    _r["Entity_ID"] = item.Entity_ID;
                    _r["Client_ID"] = item.Client_ID;
                    _r["Project_ID"] = item.Project_ID;
                    _r["Attribute_Business_Name"] = item.Attribute_Business_Name;
                    _r["Attribute_Business_Data_Type"] = item.Attribute_Business_Data_Type;
                    _r["Attribute_Business_Data_precision"] = item.Attribute_Business_Data_precision;
                    _r["Attribute_Desscription"] = item.Attribute_Desscription;
                    _r["Column_Name"] = item.Column_Name;
                    _r["Modified_By"] = item.Modified_by;

                    dtColumns.Rows.Add(_r);
                }
                _autoMS.SaveEntityColData(dtColumns, ref StatusCode, ref Message);
                return Message;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpGet]
        public dynamic GetTemplateSourceTargetTableName(string client_ID, string project_ID, string Template_Id, string Type)
        {

            string StatusCode = string.Empty, Message = string.Empty;
            var SourceTemplate = _autoMS.GetTemplateSourceTargetTableList(client_ID, project_ID, Template_Id, ref  StatusCode, ref Message) as IEnumerable<TemplateSourceTargetEntity>;

            if (Type.ToLower() == "target")
            {
                var TargetTablerows = (
                from auto in SourceTemplate
                select new string[] 
                {
                    auto.Target_Table_Name
                }
                ).Distinct().ToList().ToArray();

                return TargetTablerows;
            }
            else
            {
                var SourceRows = (
                   from auto in SourceTemplate
                   select new string[] 
                   {
                       auto.Table_Name
                   }
                   ).Distinct().ToList().ToArray();

                return SourceRows;
            }

        }

        [HttpGet]
        public dynamic GetTableToDownload(string client_ID, string project_ID, long config_ID)
        {

            string StatusCode = string.Empty, Message = string.Empty;

            if (UIProperties.Sessions.TargetConfigEntity == null || UIProperties.Sessions.TargetConfigEntity.Config_ID == null) return null;

            AutomatonExportStatus req = new AutomatonExportStatus();
            req.Client_ID = UIProperties.Sessions.Client.Client_ID;
            req.Project_ID = UIProperties.Sessions.Client.project_ID;

            req.Config_ID = config_ID;//Convert.ToInt64(UIProperties.Sessions.TargetConfigEntity.Config_ID);
            req.Run_User = UIProperties.Sessions.UserName;
            var _tables = _autoMS.GetExportStatus(req, ref StatusCode, ref Message);
            var rows = new
            {
                rows = (
                from t in _tables
                select new
                {
                    i = t.ID,
                    cell = new string[] {
                        t.Table_Name,
                        t.Download_XLS,
                        t.Download_XLSX,
                        t.Download_CSV,
                        t.Download_XML,
                        t.ID.ToString(),
                        t.Folder_Path,
                        t.File_Name

                    }
                })
            };
            return rows;


        }
        [HttpGet]
        public string SubmitDownloadRequest(string TableName, string OutputFormat, long Config_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            AutomatonExportStatus req = new AutomatonExportStatus();

            req.Config_ID = Config_ID;//UIProperties.Sessions.TargetConfigEntity.Config_ID;
            req.Client_ID = UIProperties.Sessions.Client.Client_ID;
            req.Project_ID = UIProperties.Sessions.Client.project_ID;
            req.Table_Name = TableName;
            req.Folder_Path = UIProperties.DownloadPath;
            req.Output_Format = OutputFormat;
            req.Run_User = UIProperties.Sessions.UserName;
            _autoMS.SubmitDownloadRequest(req, ref StatusCode, ref Message);
            return Message;
        }

        [HttpGet]
        public dynamic GetTemplateParameterData(string client_ID, string project_ID, string TemplateName)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            var mAttributes = _autoMS.GetTemplateParameterList(client_ID, project_ID, TemplateName, ref  StatusCode, ref Message) as IEnumerable<ParametersEntity>;

            var rows = new
            {
                total = 0,
                page = 0,
                records = 0,
                rows = (
                    from auto in mAttributes
                    select new
                    {
                        i = auto.Parameter_ID,
                        cell = new string[] {
                            auto.Parameter_ID.ToString() ,
                            auto.Parameter_Name,
                            auto.Parameter_Value

                      }
                    }).ToArray()
            };

            return rows;
        }

        [HttpPost]
        public void SaveParameter(ParametersEntity Entity)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            _autoMS.SaveParameter(Entity, ref StatusCode, ref Message);
        }

        [HttpGet]
        public dynamic GetAutomatonBatchStatus(string client_ID, string project_ID, string Tool_ID)
        {
            DataTable dt = _autoMS.GetAutomatonBatchStatus(client_ID, project_ID, Convert.ToInt64(Tool_ID));

            string json = JsonConvert.SerializeObject(dt, new DataTableConverter());

            var rows = new
            {
                rows = (
                from DataRow dr in dt.Rows
                select new
                {
                    cell = new string[] {
                        Convert.ToString(dr["Template_Name"]),
                        Convert.ToString(dr["Run_Status"]),
                        Convert.ToString(dr["Start_Time"]),
                        Convert.ToString(dr["End_Time"])
                   }

                }).ToArray()
            };

            return rows;
        }

        [HttpGet]
        public dynamic GetMetaDataTableBusinessName(string client_ID, string project_ID, string Database_IP, string Table_name, string connectionid, string Field_Type, long Recordcount)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            var lst = _autoMS.GetMetaDataTableBusinessName(client_ID, project_ID, Table_name, connectionid, ref  StatusCode, ref Message) as IEnumerable<AutomatonSourceEntity>;
            var totalTransactions = Recordcount == 0 ? 1 : Recordcount + 1;

            var mrows = (from auto in lst
                         select new AutomatonSourceEntity()
                         {
                             Field_Seq_No = Convert.ToString(totalTransactions++),
                             Field_Type = Field_Type,
                             Column_Name = auto.Column_Name,
                             Data_Type = auto.Data_Type,
                             Data_Precision = auto.Data_Precision,
                             Data_Scale = auto.Data_Scale,
                             Field_Data = auto.Field_Data,
                             Key_column = auto.Key_column,
                             Table_Name = auto.Table_Name,
                             Row_ID = "0"

                         }).ToList();
            return mrows;


        }

        [HttpPost]
        public dynamic SaveTransTable(TransitionAdd transEntity)
        {
            string _Result;
            var trans = new TransformEntity();
            trans.Client_ID = transEntity.Client_ID;
            trans.Project_ID = transEntity.Project_Id;
            trans.Table_Name = transEntity.TableName;
            trans.Field_Name = transEntity.ColumnName;
            trans.Trans_Name = transEntity.TransName;
            trans.Trans_Order = transEntity.TransOrder;
            trans.Trans_Rule = transEntity.TransRule;
            trans.Trans_Type = transEntity.TransType;
            trans.Template_ID = transEntity.Template_ID;
            trans.Create_Date = DateTime.Now;
            trans.Modified_Date = DateTime.Now;
            trans.Modified_by = UIProperties.Sessions.UserName;
            trans.Trans_ID = transEntity.Trans_ID == null || transEntity.Trans_ID == "" ? "0" : Convert.ToString(transEntity.Trans_ID);

            //Start code for finding DataType for Transformation Columns
            string StatusCode = string.Empty, Message = string.Empty;
            var lstTable = new List<AutomatonSourceEntity>();
            lstTable = _autoMS.GetMetaDataTableDetail(transEntity.Client_ID, transEntity.Project_Id, transEntity.TableName, transEntity.SourceConnectionID, ref  StatusCode, ref Message) as List<AutomatonSourceEntity>;
            if (lstTable.Count > 0)
            {
                var ls = lstTable.Where(DB => DB.Column_Name == transEntity.ColumnName).FirstOrDefault();
                if (ls != null)
                {
                    trans.Field_Data_Type = ls.Data_Type;
                    trans.Field_Length = ls.Data_Precision;
                }
            }
            else
            {
                lstTable = _autoMS.GetMetaDataTableDetail(transEntity.Client_ID, transEntity.Project_Id, transEntity.TableName, transEntity.TargetConnectionID, ref  StatusCode, ref Message) as List<AutomatonSourceEntity>;
                var ls1 = lstTable.Where(DB => DB.Column_Name == transEntity.ColumnName).FirstOrDefault();

                if (ls1 != null)
                {
                    trans.Field_Data_Type = ls1.Data_Type;
                    trans.Field_Length = ls1.Data_Precision;
                }
            }

            //End code for finding DataType for Transformation Columns 

            var transformEntity = new List<TransformEntity>();
            transformEntity.Add(trans);

            _autoMS.SaveTransGrid(transformEntity, ref StatusCode, ref Message);

            if (Message == "Success") _Result = "Saved successfully."; else _Result = "failed to save record.";
            return _Result;
        }

        [HttpGet]
        public dynamic GetTemplateSrcTgtTableNames(string client_ID, string project_ID, string Template_Id, string Template_Name, string SourceConnectionID, string TargetConnectionID)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            var SourceTemplate = _autoMS.GetTargetTemplateList(client_ID, project_ID, Template_Id, Template_Name, "SOURCE", ref  StatusCode, ref Message) as IEnumerable<TemplateSourceTargetEntity>;
            var TargetTemplate = _autoMS.GetSourceTemplateList(client_ID, project_ID, Template_Id, Template_Name, "TARGET", ref  StatusCode, ref Message) as IEnumerable<TemplateSourceTargetEntity>;


            List<TransitionAdd> rows = (
                                   from values in SourceTemplate.Select(db => db.Table_Name).Distinct()
                                   select new TransitionAdd
                                   {
                                       TableName = values.ToString()
                                   }).ToList();

            rows.ForEach(db => db.SourceConnectionID = SourceConnectionID);
            var row1 = (from values in TargetTemplate.Select(db => db.Table_Name).Distinct()
                        select new TransitionAdd
                        {
                            TableName = values.ToString()
                        }).ToList();
            row1.ForEach(db => db.SourceConnectionID = TargetConnectionID);
            rows.AddRange(row1);
            return rows;
        }

        [HttpGet]
        public dynamic GetEntitySuggestedAttr(string client_ID, string project_ID, string table_name, string column_name)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            var suggestedAttr = _autoMS.GetEntitySuggestedAttr(client_ID, project_ID, table_name, column_name, ref StatusCode, ref Message);

            return suggestedAttr;
        }

        //Get Target DB Data Type
        [HttpGet]
        public dynamic GetTargetDBDataTypes(string client_id, string project_id, Nullable<long> config_ID)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            var dataTypes = _autoMS.GetTargetDBDataTypes(client_id, project_id, config_ID, ref StatusCode, ref Message);


            return dataTypes;
            //return new {"1":"nvarvarh"}
        }

        //Date Colum check
        [HttpGet]
        public dynamic DateColumnCheck(string client_id, string project_id, string table_Name, string column_name, Nullable<long> config_ID, string columnType)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            var dataTypes = _autoMS.DateColumnCheck(client_id, project_id, table_Name, column_name, config_ID, columnType, ref StatusCode, ref Message);

            var data = new { StatusCode = StatusCode, Message = Message };
            return data;
            //return new {"1":"nvarvarh"}
        }


        [HttpGet]
        public dynamic tableColumnCount(string client_ID, string project_ID, Nullable<long> config_ID, string table_Name)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            long? clmnCnt = 0;

            var dataTypes = _autoMS.GetTargetDBtblClmnCount(client_ID, project_ID, UIProperties.Sessions.TargetConfigEntity.Config_ID,
                table_Name, ref clmnCnt, ref StatusCode, ref Message);

            var data = new { StatusCode = StatusCode, Message = Message, columnCount = clmnCnt };

            return data;

        }

        [HttpGet]
        public dynamic GetScheduledTransformation(string client_ID, string project_ID, string Tool_ID, string trans_type)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            var list = _autoMS.GetScheduledTransformation(client_ID, project_ID, Convert.ToInt64(Tool_ID), trans_type, ref StatusCode, ref Message) as IEnumerable<SchedulerEntity>;

            var rows = new
            {
                total = 0,
                page = 0,
                records = 0,
                rows = (
                    from auto in list
                    select new
                    {
                        cell = new string[] {
                            auto.Offline_Job_ID.ToString() ,
                            auto.Template_Name,
                            auto.Run_Status,
                            auto.Schedule_Date.ToString("MM-dd-yyyy HH:mm"),
                            Convert.ToString(auto.Start_Time),
                            Convert.ToString(auto.End_Time)
                      }
                    }).ToArray()
            };

            return rows;
        }

        [HttpGet]
        public string InsertScheduleInformation(string TemplateName, string ScheduledDate)
        {

            string StatusCode = string.Empty, Message = string.Empty;

            string Client_ID = UIProperties.Sessions.Client.Client_ID;
            string project_ID = UIProperties.Sessions.Client.project_ID;
            string UserName = UIProperties.Sessions.UserName;
            string TemplatePath = System.IO.Path.Combine(ConfigurationManager.AppSettings["Generatexml_Save_PkgLocation"], TemplateName + ".dtsx");
            string ToolID = UIProperties.Sessions.ToolID;
            string RunStatus = "S";

            _autoMS.InsertOfflineBatchJobs(Client_ID, project_ID, Convert.ToInt64(ToolID), TemplateName, TemplatePath, UserName, RunStatus, "", Convert.ToDateTime(ScheduledDate), ref StatusCode, ref Message);

            Message = StatusCode == "0" ? "Transformation scheduled successfully" : Message;
            return Message;

        }

        [HttpPost]
        public object SaveFileUploadTemplate(AutomatonFileUploadTemplateEntity Entity)
        {
            string Template_ID = string.Empty, StatusCode = string.Empty, Message = string.Empty;

            //if (!System.IO.File.Exists(Entity.File_Location))
            //{
            //    return "File not found.";

            //}

            _autoMS.SaveFileUploadTemplate(Entity, ref Template_ID, ref StatusCode, ref Message);

            var data = new { Template_ID = Template_ID, Message = Message };

            return data;
        }


        #region Tranformation Logic

        public dynamic GetTableDataDetailAutomatic(string client_ID, string project_ID, string Database_IP, string trtTable_Name, string trtConnectionid,
            string Template_ID, string Template_Name,
              string sourceTable_Name, string srcconnectionid, string Field_Type, long Recordcount, string tgtConnectionid)
        {
            string StatusCode = string.Empty, Message = string.Empty;

            List<TemplateSourceTargetEntity> Tempsrctrt = new List<TemplateSourceTargetEntity>();
            if (Template_ID != "Select")
            {
                Tempsrctrt = _autoMS.GetSourceTemplateList(client_ID, project_ID, Template_ID, Template_Name, null, ref  StatusCode, ref Message);
            }


            string[] targettables = sourceTable_Name.Split(',').Select(sValue => sValue.Trim()).ToArray();
            List<AutomatonSourceEntity> joinTargetTableslist = new List<AutomatonSourceEntity>();
            var sourceRecordCount = Recordcount;
            var sorceTransactions = Recordcount == 0 ? 1 : Recordcount + 1;
            if (Template_ID == "Select")
            {

            }

            foreach (var item in targettables)
            {
                // foreach (var target in _autoMS.GetMetaDataTableDetail(client_ID, project_ID, item, trtConnectionid, ref  StatusCode, ref Message))
                foreach (var target in _autoMS.GetMetaDataTableDetailAutomatic(client_ID, project_ID, Template_ID == "Select" ? 0 : (long)Convert.ToDouble(Template_ID), UIProperties.Sessions.Client.Role_ID,
                   item, "SOURCE", trtConnectionid, ref  StatusCode, ref Message))
                {
                    AutomatonSourceEntity ase = new AutomatonSourceEntity();
                    ase.Field_Seq_No = Convert.ToString(sorceTransactions++);
                    ase.Table_Name = target.Table_Name;
                    ase.Column_Name = target.Column_Name;
                    ase.Field_Type = Field_Type;
                    ase.Data_Type = target.Data_Type;
                    ase.Data_Scale = target.Data_Scale;
                    ase.Data_Precision = target.Data_Precision;
                    ase.Row_ID = target.Row_ID;
                    joinTargetTableslist.Add(ase);
                }
            }


            var targetRecordCount = Recordcount;
            var totalTransactions = Recordcount == 0 ? 1 : Recordcount + 1;

            string[] sourcetables = trtTable_Name.Split(',').Select(sValue => sValue.Trim()).ToArray();
            List<AutomatonSourceEntity> joinSourceTableslist = new List<AutomatonSourceEntity>();
            foreach (var item in sourcetables)
            {
                foreach (var target in _autoMS.GetMetaDataTableDetailAutomatic(client_ID, project_ID, Template_ID == "Select" ? 0 : (long)Convert.ToDouble(Template_ID), UIProperties.Sessions.Client.Role_ID,
                    item, "SOURCE", trtConnectionid, ref  StatusCode, ref Message))
                //foreach (var target in _autoMS.GetMetaDataTableDetail(client_ID, project_ID, item, trtConnectionid, ref  StatusCode, ref Message))
                {
                    AutomatonSourceEntity ase = new AutomatonSourceEntity();
                    ase.Field_Seq_No = Convert.ToString(totalTransactions++);
                    ase.Table_Name = target.Table_Name;
                    ase.Column_Name = target.Column_Name;
                    ase.Field_Type = Field_Type;
                    ase.Data_Type = target.Data_Type;
                    ase.Data_Scale = target.Data_Scale;
                    ase.Data_Precision = target.Data_Precision;
                    ase.Row_ID = target.Row_ID;
                    joinSourceTableslist.Add(ase);
                }
            }

            totalTransactions = targetRecordCount == 0 ? 1 : targetRecordCount + 1;
            totalTransactions = Recordcount == 0 ? 1 : Recordcount + 1;

            List<TemplateSourceTargetEntity> ListASE = new List<TemplateSourceTargetEntity>();
            foreach (var auto in joinTargetTableslist)
            {
                string flag = String.Empty;
                TemplateSourceTargetEntity ASE = new TemplateSourceTargetEntity();
                ASE.Field_Seq_No = auto.Field_Seq_No;
                ASE.Table_Name = auto.Table_Name;
                ASE.Column_Name = auto.Column_Name;
                ASE.Data_Type = auto.Data_Type;
                ASE.Row_ID = "0";
                foreach (var item in Tempsrctrt)
                {

                    if (item.Field_Seq_No == auto.Field_Seq_No)
                    {

                        ASE.Target_Table_Name = item.Table_Name;
                        ASE.Target_Column_Name = item.Field_Name;
                        ASE.Target_Data_Type = item.Field_Data_Type;
                        ASE.Field_Type = item.Field_Type;
                        flag = "Exists";
                    }
                }
                if (flag != "Exists")
                {
                    ASE.Target_Field_Name = (from auto_busniness in joinSourceTableslist
                                             where auto_busniness.Column_Name.ToLower() == auto.Column_Name.ToLower()
                                             select auto_busniness.Column_Name).FirstOrDefault<string>();
                    ASE.Target_Table_Name = (from auto_busniness in joinSourceTableslist
                                             where auto_busniness.Column_Name.ToLower() == auto.Column_Name.ToLower()
                                             select auto_busniness.Table_Name).FirstOrDefault<string>();
                    ASE.Target_Column_Name = (from auto_busniness in joinSourceTableslist
                                              where auto_busniness.Column_Name.ToLower() == auto.Column_Name.ToLower()
                                              select auto_busniness.Column_Name).FirstOrDefault<string>();
                    ASE.Target_Data_Type = (from auto_busniness in joinSourceTableslist
                                            where auto_busniness.Column_Name.ToLower() == auto.Column_Name.ToLower()
                                            select auto_busniness.Data_Type).FirstOrDefault<string>();
                    ASE.Field_Type = (from auto_busniness in joinSourceTableslist
                                      where auto_busniness.Column_Name.ToLower() == auto.Column_Name.ToLower()
                                      select auto_busniness.Field_Type).FirstOrDefault<string>();
                }
                ListASE.Add(ASE);
            }
            var result = ListASE;
            return result;



        }


        public dynamic GetTableDataDetailTrans(string client_ID, string project_ID, string Database_IP, string Table_name, string connectionid, string Field_Type, long Recordcount, string tgtConnectionid)
        {
            string StatusCode = string.Empty, Message = string.Empty;
            var lst = _autoMS.GetMetaDataTableDetail(client_ID, project_ID, Table_name, connectionid, ref  StatusCode, ref Message) as IEnumerable<AutomatonSourceEntity>;
            var targetRecordCount = Recordcount;
            var totalTransactions = Recordcount == 0 ? 1 : Recordcount + 1;

            totalTransactions = Recordcount == 0 ? 1 : Recordcount + 1;

            var mrows = (from auto in lst
                         select new AutomatonSourceEntity()
                         {
                             Field_Seq_No = Convert.ToString(totalTransactions++),
                             Field_Type = Field_Type,
                             Column_Name = auto.Column_Name,
                             Data_Type = auto.Data_Type,
                             Data_Precision = auto.Data_Precision,
                             Data_Scale = auto.Data_Scale,
                             Field_Data = auto.Field_Data,
                             Key_column = auto.Key_column,
                             Table_Name = auto.Table_Name,
                             Row_ID = "0"
                         }).ToList();
            return mrows;
        }

        #endregion

        [HttpPost]
        public dynamic GenerateReconcile(string Template_ID)
        {
            string Client_ID = UIProperties.Sessions.Client.Client_ID;
            string project_ID = UIProperties.Sessions.Client.project_ID;
            string StatusCode = string.Empty, Message = string.Empty;

            _autoMS.GenerateReconcile(Client_ID, project_ID, Template_ID, UIProperties.Sessions.Client.Role_ID, UIProperties.Sessions.Client.User_ID.ToString(), ref StatusCode, ref Message);

            //return Json(new { StatusCode = StatusCode, Message = Message }, JsonRequestBehavior.AllowGet);
            return new { StatusCode = StatusCode, Message = Message };

        }

    }
}
