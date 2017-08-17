using System;
using System.Collections.Generic;

namespace DM_BusinessEntities
{
    public class DataReconEntity
    {
        public string Source_Column { get; set; }
        public string Target_Column { get; set; }
        public string source_field_name { get; set; }
        public string target_field_name { get; set; }
        public string source_trans_rule { get; set; }
        public string target_trans_rule { get; set; }
        public string Column_Name { get; set; }
        public string Data_Type { get; set; }
        public string Seq_No { get; set; }
        public string Is_key_column { get; set; }
        public string Expression { get; set; }

    }

    public class ComparisionData
    {
        public string ClientID { get; set; }
        public string ProjectID { get; set; }
        public string TemplateName { get; set; }
        public List<SourceKeyColumn> SourceKeyColumn { get; set; }
        public List<TargetKeyColumn> TargetKeyColumn { get; set; }
        public List<SourceDataEntity> SourceData { get; set; }
        public List<TargetDataEntity> TargetData { get; set; }
    }

    public class SourceDataEntity
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string SeqNo { get; set; }
        public string DataType { get; set; }
        public string KeyColumn { get; set; }
        public string Expression { get; set; }

    }

    public class TargetDataEntity
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string SeqNo { get; set; }
        public string DataType { get; set; }
        public string KeyColumn { get; set; }
        public string Expression { get; set; }
    }

    public class SourceKeyColumn
    {
        public string ColumnName { get; set; }
        public string SeqNo { get; set; }

    }

    public class TargetKeyColumn
    {
        public string ColumnName { get; set; }
        public string SeqNo { get; set; }
    }

    public class TemplateDataEntity
    {
        public string Source_Table_Name { get; set; }
        public string Source_Column_Name { get; set; }
        public string Source_Col_Seq_No { get; set; }
        public string Source_Expression { get; set; }
        public string Source_Column_Value { get; set; }
        public string Target_Table_Name { get; set; }
        public string Target_Column_Name { get; set; }
        public string Target_Col_Seq_No { get; set; }
        public string Target_Expression { get; set; }
        public string Target_Column_value { get; set; }
        public string Status { get; set; }
        public long Error_Count { get; set; }
        public long Run_ID { get; set; }
        public long Source_Record_Count { get; set; }
        public long Target_Record_Count { get; set; }
        public string TABLE_KEY_COLUMN1 { get; set; }
        public string TABLE_KEY_COLUMN2 { get; set; }
        public string TABLE_KEY_COLUMN3 { get; set; }
        public string TABLE_KEY_COLUMN4 { get; set; }
        public string TABLE_KEY_COLUMN5 { get; set; }

    }

    public class DataReconSourceTargetEntity
    {
        public Nullable<long> ID { get; set; }
        public string Client_ID { get; set; }
        public string Project_ID { get; set; }
        public string Template_Name { get; set; }
        public string Connection_ID { get; set; }
        public string Field_Seq_No { get; set; }
        public string Column_Name { get; set; }        
        public string Data_Type { get; set; }  
        public string Target_Connection_ID { get; set; } 
        public string Target_Column_Name { get; set; }
        public string Target_Data_Type { get; set; }
    }

}
