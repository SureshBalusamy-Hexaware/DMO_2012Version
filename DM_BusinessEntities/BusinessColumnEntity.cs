using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class BusinessColumnEntity
    {
        public long ID { get; set; }
        public Nullable<long> Entity_ID { get; set; }

        public string Column_Name { get; set; }
        public string Attribute_Business_Name { get; set; }
        public string Attribute_Business_Data_Type { get; set; }
        public string Attribute_Business_Data_precision { get; set; }        
        public string Attribute_Desscription { get; set; }
        public string Client_ID { get; set; }
        public string Project_ID { get; set; }
        public string Modified_by { get; set; }

    }
}
