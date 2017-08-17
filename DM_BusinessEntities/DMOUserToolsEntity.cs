using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class DMOUserToolsEntity
    {

        //public int UTX_ID { get; set; }
        //public int TOOL_ID { get; set; }
        //public string Tool_Name { get; set; }
        //public string Tool_Description { get; set; }
        //public string ACTIVE_FLAG { get; set; }
        public long USER_TOOL_ID { get; set; }
        public string Tool_Name { get; set; }
        public string Tool_Description { get; set; }
        public Nullable<int> ACTIVE_FLAG { get; set; }
    }
}
