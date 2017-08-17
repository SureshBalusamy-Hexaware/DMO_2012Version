using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public  class DashboardReportEntity
    {
        public string Client_ID { get; set; }
        public string Project_ID { get; set; }
        public long Tool_ID { get; set; }
        public long Report_ID { get; set; }
        public string Report_Name { get; set; }
        public string Report_Display_Name { get; set; }
        public string Report_Description { get; set; }
        public string Report_Server_Path { get; set; }
        public string Report_Category { get; set; }
    }
}
