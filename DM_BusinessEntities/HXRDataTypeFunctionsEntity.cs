using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
   public  class HXRDataTypeFunctionsEntity
    {
        public long Function_ID { get; set; }

        public string Client_ID { get; set; }

        public string Project_ID { get; set; }

        public string Function_Syntax { get; set; }

        public string Data_type { get; set; }

        public string Function_Sample { get; set; }

        public System.DateTime Create_date { get; set; }

        public string Created_by { get; set; }
    }
   
}
