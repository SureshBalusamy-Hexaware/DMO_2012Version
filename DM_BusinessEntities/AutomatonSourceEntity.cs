using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class AutomatonSourceEntity
    {
        public Nullable<long> ID { get; set; }
        public string Client_ID { get; set; }
        public string Project_ID { get; set; }
        public string Field_Type { get; set; }
        public Nullable<long> Config_ID { get; set; }
        public string Database_IP { get; set; }
        public string Source_Target { get; set; }
        public string Table_Catalog { get; set; }
        public string Table_Schema { get; set; } 
        public string Table_Name { get; set; }
        public string Column_Name { get; set; }
        public int Ordinal_Position { get; set; }
        public string Key_column { get; set; }
        public string Field_Data { get; set; }
        public string Constraint_Type { get; set; }
        public string Column_Default { get; set; }
        public string Is_Nullable { get; set; }
        public string Data_Type { get; set; }
        public string Character_Set_Catalog { get; set; }
        public string Character_Set_Schema { get; set; }
        public string Character_Set_Name { get; set; }
        public string Domain_Catalog { get; set; }
        public string Domain_Schema { get; set; }
        public string Domain_Name { get; set; }
        public string Data_Precision { get; set; }
        public string Data_Scale { get; set; }
        public DateTime Create_Date { get; set; }
        public DateTime Modified_On { get; set; }
        public string Modified_By { get; set; }
        public string Field_Seq_No { get; set; }
        public string Row_ID { get; set; }
    }

    public class TemplateSourceTargetEntity
    {
        public Nullable<long> ID { get; set; }
        public string Client_ID { get; set; }
        public string Project_ID { get; set; }
        public string Connection_ID { get; set; }
        public string Template_ID { get; set; }
        public string Table_Name { get; set; }
        public string Column_Name { get; set; }
        public string Data_Type { get; set; }
        public string Template_Name { get; set; }
        public string Field_Seq_No { get; set; }
        public string Field_Name { get; set; }
        public string Field_Description { get; set; }
        public string Field_Data_Type { get; set; }
        public string Field_Prec { get; set; }
        public string Field_Key { get; set; }
        public string Field_Scale { get; set; }
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
        public string Target_Column_Name { get; set; }
        public string Target_Field_Name { get; set; }
        public string Target_Data_Type { get; set; }
    }



    public class SchedulerEntity
    {
        public long Offline_Job_ID { get; set; }

        public string Template_Name { get; set; }

        public string Run_Status { get; set; }

        public string Task_Description { get; set; }

        public System.DateTime Schedule_Date { get; set; }

        public Nullable<System.DateTime> Start_Time { get; set; }

        public Nullable<System.DateTime> End_Time { get; set; }

    }

}
