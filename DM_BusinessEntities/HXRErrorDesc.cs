using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class HXRErrorDesc
    {
        public string Error_code { get; set; }
        public string Client_ID { get; set; }
        public string Error_Description { get; set; }
        public long Active_Flag { get; set; }
        public System.DateTime Create_Date { get; set; }
        public Nullable<System.DateTime> Last_Modified_Date { get; set; }
        public string last_Modified_By { get; set; }
    }
}
