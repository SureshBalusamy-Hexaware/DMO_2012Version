using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class DIMAPLUSReportEntity
    {
        public string Object_Type { get; set; }
        public long Source_Object_Count { get; set; }
        public long Target_Object_Count { get; set; }
        public long Run_ID { get; set; }
        public long Template_ID { get; set; }

        public string Type { get; set; }
        public string Object_Name { get; set; }
        public string Slicing_Field { get; set; }
        public string Slicing_Value { get; set; }
        public long Source_Records { get; set; }
        public long Target_Records { get; set; }
        public string Criteria_Sucess { get; set; }
        public string Is_Delete { get; set; }


    }
}
