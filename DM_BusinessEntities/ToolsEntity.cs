using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class ToolsEntity
    {      
        public int ToolID { get; set; } 
        public string ToolName { get; set; } 
        public string ActiveFlag { get; set; } 
        public string ModifiedBy { get; set; }
        public int Tool_TaskSequence { get; set; }
    }
}
