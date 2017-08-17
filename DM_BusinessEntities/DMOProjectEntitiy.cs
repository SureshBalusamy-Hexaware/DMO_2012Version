using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DMOProjectEntitiy
{
    public long ID { get; set; }
    public string Project_ID { get; set; }
    public string Client_ID { get; set; }
    public string Project_Description { get; set; }
    public long Active_Flag { get; set; }
    public System.DateTime Create_date { get; set; }
    public Nullable<System.DateTime> Modified_date { get; set; }
    public string Modified_By { get; set; }

}

