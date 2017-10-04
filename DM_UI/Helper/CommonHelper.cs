using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;


public static class CommonHelper
{

    public static string BuildConnnectionString(string ServerIPAddress, string DataBaseName, string DataBasePassword, string SchemaName)
    {
        return string.Format("Data Source={0};User ID={1};pwd={2};Initial Catalog={3};Provider={4};Persist Security Info=True;", ServerIPAddress, DataBaseName, DataBasePassword, SchemaName, Convert.ToString(ConfigurationManager.AppSettings["SQLProvider"]));

    }
    public static string GetCurrentConnectionString()
    {
        var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
        SqlConnectionStringBuilder SQLbuilder = new SqlConnectionStringBuilder(builder.ProviderConnectionString);

        return string.Format("Data Source={0};User ID={1};pwd={2};Initial Catalog={3};Provider={4};Persist Security Info=True;", SQLbuilder.DataSource, SQLbuilder.UserID, SQLbuilder.Password, SQLbuilder.InitialCatalog, Convert.ToString(ConfigurationManager.AppSettings["SQLProvider"]));
    }

    public static string GetADOConnectionString()
    {
        var builder = new EntityConnectionStringBuilder(System.Configuration.ConfigurationManager.ConnectionStrings["DM_MetaDataEntities"].ConnectionString.ToString());
        SqlConnectionStringBuilder SQLbuilder = new SqlConnectionStringBuilder(builder.ProviderConnectionString);

        return string.Format("Data Source={0};User ID={1};pwd={2};Initial Catalog={3};Persist Security Info=True;", SQLbuilder.DataSource, SQLbuilder.UserID, SQLbuilder.Password, SQLbuilder.InitialCatalog);
    }


    public static DataTable ReadExcelData(string FilePath, string Extension, string isHDR)
    {


        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                         .ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                          .ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath, isHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;

        //Get the name of First Sheet
        connExcel.Open();
        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        connExcel.Close();

        //Read Data from First Sheet
        connExcel.Open();
        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        oda.SelectCommand = cmdExcel;
        oda.Fill(dt);
        connExcel.Close();

        return dt;

    }


    public static DataTable ReadExcelData(string FilePath, string Extension, string isHDR, string SheetName)
    {

        DataTable dt = new DataTable();
        OleDbConnection connExcel = new OleDbConnection();

        try
        {
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);
            connExcel.ConnectionString = conStr;
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();

            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
        }
        catch (Exception ex)
        {
            connExcel.Close();
            throw ex;
        }

        return dt;

    }

    public static DataTable ReadExcelSheetName(string FilePath, string Extension, string isHDR)
    {


        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                         .ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                          .ConnectionString;
                break;
        }
        conStr = String.Format(conStr, FilePath, isHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;

        //Get the name of First Sheet
        connExcel.Open();
        DataTable dtExcelSchema;
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        connExcel.Close();



        return dtExcelSchema;

    }


    public static void ToCSV(this DataTable dtDataTable, string strFilePath)
    {
        StreamWriter sw = new StreamWriter(strFilePath, false);

        //headers  
        for (int i = 0; i < dtDataTable.Columns.Count; i++)
        {
            sw.Write(dtDataTable.Columns[i]);
            if (i < dtDataTable.Columns.Count - 1)
            {
                sw.Write(",");
            }
        }
        sw.Write(sw.NewLine);
        foreach (DataRow dr in dtDataTable.Rows)
        {
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                if (!Convert.IsDBNull(dr[i]))
                {
                    string value = dr[i].ToString();
                    if (value.Contains(','))
                    {
                        value = String.Format("\"{0}\"", value);
                        sw.Write(value);
                    }
                    else
                    {
                        sw.Write(dr[i].ToString());
                    }
                }
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
        }
        sw.Close();
    }
    public static void DirectoryCheck()
    {
        string[] repositoryUrls = ConfigurationManager.AppSettings.AllKeys
                                 .Where(key => key.StartsWith("Path_"))
                                 .Select(key => ConfigurationManager.AppSettings[key])
                                 .ToArray();

        for (int i = 0; i < repositoryUrls.Length; i++)
        {
            try
            {
                string _path = repositoryUrls[i];

                if (_path == "AutomaticDBDeploy")
                    _path = AppDomain.CurrentDomain.BaseDirectory + _path + "\\";

                string dir = Path.GetDirectoryName(_path);

                if (!Directory.Exists(dir))                
                    Directory.CreateDirectory(dir);
                
            }
            catch (Exception __ex)
            {
                string filePath = AppDomain.CurrentDomain.BaseDirectory;
                string _filePath = Path.GetFullPath(@"C:\DMO\directoryCheck.txt");

                //if (!System.IO.File.Exists(_filePath))
                //    System.IO.File.Create(_filePath);

                using (StreamWriter writer = new StreamWriter(_filePath, true))
                {
                    writer.WriteLine("Date :" + DateTime.Now.ToString() +
                        " Message :" + __ex.Message + "<br/>" + Environment.NewLine + "" + Environment.NewLine);
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }
            }
        }
    }
}

