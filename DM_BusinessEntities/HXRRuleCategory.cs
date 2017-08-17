using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class HXRRuleCategory
    {
        public long RuleCategory_ID { get; set; }
        public string RuleCategory_Name { get; set; }
        public string RuleCategory_Desc { get; set; }
        public long Active_Flag { get; set; }
    }
}
