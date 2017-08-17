using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public  class HXRRuleTypeEntity
    {
        public long RuleType_ID { get; set; }
        public string RuleType_Name { get; set; }
        public string RuleType_Desc { get; set; }
        public long Active_Flag { get; set; }
    }
}
