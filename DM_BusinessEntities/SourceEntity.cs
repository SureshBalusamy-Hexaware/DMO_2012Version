using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class SourceEntity
    { 

        public string Client_ID { get; set; }
        public string Project_ID { get; set; }
        public string Connection_ID { get; set; }
        public string Template_ID { get; set; }
        public string Template_Name { get; set; }
        public string Table_Name { get; set; }
        public string Field_Seq_No { get; set; }
        public string Field_Name { get; set; }
        public string Field_Description { get; set; }
        public string Field_Data_Type { get; set; }
        public string Field_Prec { get; set; }
        public string Field_Scale { get; set; }
        public string Field_Key { get; set; }
        public string Data_Key { get; set; }
        public string Field_Type { get; set; }
        public string Field_Data { get; set; }
        public string Business_name { get; set; }
        public string Created_By { get; set; }
        public DateTime Create_Date { get; set; }
        public string Modified_by { get; set; }
        public DateTime Modified_Date { get; set; }
        public string Row_ID { get; set; }
        public string Target_Row_ID { get; set; } 
        public string Target_Connection_ID { get; set; }
        public string Target_Table_Name { get; set; }
        public string Target_Field_Name { get; set; }
        public string Target_Data_Type { get; set; }
    }

    public class TargetEntity
    {
        public string Client_ID { get; set; }
        public string Project_ID { get; set; }
        public string Connection_ID { get; set; }
        public string Template_ID { get; set; }
        public string Template_Name { get; set; }
        public string Table_Name { get; set; }
        public string Field_Seq_No { get; set; }
        public string Field_Name { get; set; }
        public string Field_Description { get; set; }
        public string Field_Data_Type { get; set; }
        public string Field_Prec { get; set; }
        public string Field_Scale { get; set; }
        public string Field_Key { get; set; }
        public string Field_Type { get; set; }
        public string Field_Data { get; set; }
        public string Business_name { get; set; }
        public string Created_By { get; set; }
        public DateTime Create_Date { get; set; }
        public string Modified_by { get; set; }
        public DateTime Modified_Date { get; set; }
        public string Row_ID { get; set; }
    }

    public class TransformEntity
    {
        public string Client_ID { get; set; }
        public string Project_ID { get; set; }
        public string Field_Seq_No { get; set; }
        public string Trans_Name { get; set; }
        public string Field_Name { get; set; }
        public string Trans_Field { get; set; }
        public string Trans_Rule { get; set; }
        public string Field_Type { get; set; }
        public string Field_Length { get; set; }
        public string Field_Data_Type { get; set; }
        public string Field_Prec { get; set; }
        public string Field_Scale { get; set; }
        public string Trans_Type { get; set; }
        public string Trans_Order { get; set; }
        public string Source_Name { get; set; }
        public string Template_ID { get; set; }
        public string Template_Name { get; set; }
        public string Table_Name { get; set; }
        public DateTime Create_Date { get; set; }
        public string Modified_by { get; set; }
        public DateTime Modified_Date { get; set; }
        public string Trans_ID { get; set; }
    }
    public class TransitionAdd
    {
        public string Client_ID { get; set; }
        public string Project_Id { get; set; }
        public string Tool_Id { get; set; }
        public string TemplateName { get; set; }
        public string Template_ID { get; set; }
        public string Trans_ID { get; set; }        
        public string TransName { get; set; }
        public string TransType { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string TransRule { get; set; }
        public string TransOrder { get; set; }
        public string SourceConnectionID { get; set; }
        public string TargetConnectionID { get; set; }
    }

    public class TemplateList
    {
        public string Client_ID { get; set; }
        public string Project_Id { get; set; }
        public string Tool_Id { get; set; }
        public string Template_Name { get; set; }
        public string Create_Date { get; set; }  
        public Nullable<long> Template_ID { get; set; }
    }
    public class ParametersEntity
    {
        public long Parameter_ID { get; set; }
        public string Client_ID { get; set; }
        public string Project_ID { get; set; }
        public string Tool_ID { get; set; }
        public string Template_Name { get; set; }
        public string Template_ID { get; set; }
        public string Parameter_Name { get; set; }
        public string Parameter_Value { get; set; }
        public string Created_By { get; set; }
        public string Package_Save_Location { get; set; }
    }
}
