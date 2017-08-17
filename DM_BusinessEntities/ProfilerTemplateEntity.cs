using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class ProfilerTemplateEntity
    {        
        public string Template_Name { get; set; }
        public string Table_Name { get; set; }
        public string Column_Name { get; set; }
        public string Data_Type { get; set; }
        public string Null_Ratio_Profile { get; set; }
        public string Statistics_Profile { get; set; }
        public string Value_Distribution_Profile { get; set; }
        public string Length_Distribution_Profile { get; set; }
        public string Pattern_Profile { get; set; }
        public string Candidate_Key_Profile { get; set; }
        public string Profiling_Status { get; set; }
        public string Profile_Type { get; set; }
        public long Rec_Count { get; set; } 
    }
}
