using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class UserToolEntity
    {
        //public long TX_ID { get; set; }

        //public long TOOL_ID { get; set; }

        //public string TOOL_NAME { get; set; }

        //public string TOOL_DESCRIPTION { get; set; }

        //public string ACTIVE_FLAG { get; set; }

        //public long UTX_ID { get; set; }

        //public long USER_ID { get; set; }

        //public string PRJ_ACTIVE_FLAG { get; set; }

        public long ID { get; set; }
        public long TOOL_ID { get; set; }
        public string Tool_Name { get; set; }
        public string Tool_Description { get; set; }
        public Nullable<int> ACTIVE_FLAG { get; set; }
    }
}
