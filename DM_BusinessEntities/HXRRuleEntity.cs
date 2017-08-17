using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class HXRRuleEntity
    {
        public long Rule_ID { get; set; }
        public string Rule_Name { get; set; }
        public string Client_ID { get; set; }
        public long Active_Flag { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public string Default_value { get; set; }
        public System.DateTime Create_Date { get; set; }
        public Nullable<System.DateTime> Last_Modified_Date { get; set; }
        public string Last_Modified_By { get; set; }
        public string Rule_Description { get; set; }
        public string Project_ID { get; set; }        
        public string Conditional_Clause { get; set; }
    }
}
