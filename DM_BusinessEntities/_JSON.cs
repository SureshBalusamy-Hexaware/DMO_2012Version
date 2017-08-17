using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class _JSON
    {
        public string total { get; set; }
        public string page { get; set; }
        public string records { get; set; }
        public List<rows> rows { get; set; }

    }
    public class rows
    {
        public string id { get; set; }
        public string[] cell { get; set; }
    }

}
