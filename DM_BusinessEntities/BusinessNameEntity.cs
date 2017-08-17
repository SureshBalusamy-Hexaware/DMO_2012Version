using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class BusinessNameEntity
    {
        public long Entity_ID { get; set; }
        public string Entity_Name { get; set; }
        public string Client_ID { get; set; }
        public string Project_ID { get; set; }
        public string Entity_Business_Name { get; set; }
        public string Entity_Description { get; set; }
        public Nullable<int> Ordinal_Position { get; set; }
        public string LOB { get; set; }
        public string Legacy_Appl_Name { get; set; }
        public string Input_Type { get; set; }
        public Nullable<long> Parent_Entity_ID { get; set; }
        public Nullable<int> Active_Flag { get; set; }
        public string Modified_by { get; set; }
    }

    public class BusinessEntitySuggestedAttributes
    {
        public long Col_Min_length { get; set; }
        public long Col_Max_length { get; set;}
        public string Data_Type_List { get; set; }
    }
    public class BusinessEntityTargetDBDataType
    {
        public string DataType { get; set; }

        public int isEdit { get; set; }

        public string Type_Description { get; set; }
    } 
}
