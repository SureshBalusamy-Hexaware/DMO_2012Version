using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class HXRRuleErrorSummary
    {
        public string RuleCategory_Name { get; set; }
        public string Column_Name { get; set; }
        public Nullable<long> Re { get; set; }
        public Nullable<decimal> Rec_Pass_Percent { get; set; }
        public Nullable<long> Record_Fail_Count { get; set; }
        public Nullable<decimal> Rec_Fail_Percent { get; set; }
        public Nullable<long> Warnings { get; set; }
        public string Rule_Name { get; set; }
        public Nullable<long> Rule_ID { get; set; }
        public Nullable<long> RuleCategory_ID { get; set; }

    }

}
