using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class DIMAPLUSCriteriaEntity
    {
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public Nullable<long> RoleId { get; set; }
        public Int32 Template_ID { get; set; }
        public Int32 Criteria_ID { get; set; }
        public string Template { get; set; }
        public int Tool_ID { get; set; }
        public string Objects { get; set; }
        public string Criteria { get; set; }
        public string SourceDelete { get; set; }
        public string ObjectType { get; set; }
        public string SlicingField { get; set; }
        public string SlicingValue { get; set; }
        public int Sx_Flag { get; set; }
        public int Tx_Flag { get; set; }
        public int Fx_Flag { get; set; }
        public string Is_Delete { get; set; }
        public Nullable<int> TotalRecords { get; set; }
        public string Condition { get; set; }
        public string ConfigId { get; set; }
        public Int32 Run_ID { get; set; }

    }
    public partial class DASM_FX_GET_CRITERIAMASTER_Res
    {
        public string Object_Type { get; set; }

        public string Object_Name { get; set; }

        public string Slicing_Field { get; set; }

        public string Slicing_Value { get; set; }

        public Nullable<int> Sx_Flag { get; set; }

        public Nullable<int> Tx_Flag { get; set; }

        public Nullable<int> Fx_Flag { get; set; }

        public Nullable<long> Source_Rec_Count { get; set; }

        public Nullable<long> Target_Rec_Count { get; set; }

        public System.DateTime Create_Date { get; set; }

        public Nullable<System.DateTime> ExtractionDate { get; set; }

        public Nullable<long> Template_ID { get; set; }

        public long Criteria_ID { get; set; }

        public string Client_ID { get; set; }

        public string Project_ID { get; set; }

        public string Expression_Code { get; set; }

        public string SQL_STMT { get; set; }

    }
}
