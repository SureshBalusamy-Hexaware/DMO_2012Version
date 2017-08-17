using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_BusinessEntities
{
    public class ProfilerEntity
    {
        //public string Client_ID { get; set; }
        //public string Project_ID { get; set; }
        //public string TableName { get; set; }
        //public string TableType { get; set; }
        //public string DBName { get; set; }

        public string Table_Name { get; set; }
        public string Database_name { get; set; }
        public string Column_Name { get; set; }
        public string data_type { get; set; }
        public string Template_Name { get; set; }
        public string Key_column { get; set; }

        public string Parameter_ID { get; set; }
        public string Prameter_value { get; set; }

        public ProfilingOption Profile { get; set; }
    }

    public class ProfilingOption
    {
        public bool NullRatio { get; set; }
        public bool Statistics { get; set; }
        public bool ValueDistribution { get; set; }
        public bool LengthDistribution { get; set; }
        public bool Pattern { get; set; }
        public bool CandidateKey { get; set; }
        public bool FunctionalDependency { get; set; }
    }


    public class ProfileData
    {
        public long Profile_ID { get; set; }
        public string Client_ID { get; set; }
        public string Project_ID { get; set; }
        public byte Null_Ratio_Profile { get; set; }
        public byte Statistics_Profile { get; set; }
        public byte Value_Distribution_Profile { get; set; }
        public byte Length_Distribution_Profile { get; set; }
        public byte Pattern_Profile { get; set; }
        public byte Candidate_Key_Profile { get; set; }
        public byte Profiling_Status { get; set; }
        public List<ColumnList> ColumnList { get; set; }
    }

    public class ColumnList
    {
        public string Column_Name { get; set; }
        public string Data_Type { get; set; }
    }

    public class ProfileCodeRuleEntity
    {
        public string Table_Name { get; set; }
        public string Column_Name { get; set; }
        public string Conditional_Clause { get; set; }
        public string Create_Date { get; set; }
    }

}