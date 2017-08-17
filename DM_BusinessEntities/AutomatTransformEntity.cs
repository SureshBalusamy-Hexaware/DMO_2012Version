using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class AutomatTransformEntity
    {
        public string Trans_Table { get; set; }
        public string Trans_Column { get; set; }
        public string Data_Type { get; set; }
        public string Data_Length { get; set; }
        public string Trans_Type { get; set; }
        public string Trans_Name { get; set; }
        public int Trans_Order { get; set; }
        public string Trans_Rule { get; set; }
    }
}
