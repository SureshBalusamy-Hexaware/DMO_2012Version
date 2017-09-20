using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM_BusinessEntities;

namespace DM_BusinessService
{
    public interface IHXRConfigurationMS
    {
        HXRGetClientEntity GetClientDetails(string UserName,ref string StatusCode, ref string Message);
        HXRConfigurationMSEntity GetConfigurationByType(string ClientID, string ProjectID, string SourceTarget, Nullable<long> tool_ID, Nullable<int> RoleId, ref string StatusCode, ref string Message);
        bool TestConnection(string DataSource, string UserID, string Password, string SchemaName);
        void SaveConfiguration(HXRConfigurationMSEntity ConfigMs, ref string StatusCode, ref string Message);
        void ImportMetaData(ImportMetaDataEntity ImportMetaData, ref string StatusCode, ref string Message, ref int GenerateMap);
        void ImportGenerateMap(ImportMetaDataEntity ImportMetaData, ref long tableCount, ref string StatusCode, ref string Message);
    }
}
