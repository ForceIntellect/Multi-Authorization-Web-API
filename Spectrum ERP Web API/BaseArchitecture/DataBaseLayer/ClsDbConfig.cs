//*****************************************************************************
//    File           : 
//    Desc           : This class is used for database configuration 
//    Author         : Yusuf
//    Date           : Sept 06, 2008
//*******************************************************************************
//      Change History
//*******************************************************************************
//      Date:       Author:             Description:
//      --------    --------            -----------------------------------------
// 01) 24/09/08     karry               Code Review and changes for three tier artit.
// 02)
//******************************************************************************

using System.Windows.Forms;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;
using System;

namespace BaseArchitecture.DataBaseLayer
{
    /// <summary>
    /// class for database connectivity configuration  
    /// </summary>
    /// 

    class loginDetail
    {
        public string UserID { get; set; }
        public string Pwd { get; set; }
        public string DB { get; set; }
        public string Location { get; set; }

    }

    public class ClsDbConfig
    {
        public static BaseArchitecture.BusinessLogicLayer.ClsGlobalVariables globalVar;
        public string SettingFile;

        public enum DatabaseName
        {
            Attachment = 1,
            DMS = 2
        }

        public ClsDbConfig()
        {
            globalVar.WorkingDate = System.DateTime.Now.Date;
            globalVar.CompulsoryFieldColor = System.Drawing.Color.Cornsilk;
            globalVar.RowEditTemplateColor = System.Drawing.Color.CornflowerBlue;

            globalVar.CrDr = new System.Collections.Generic.Dictionary<byte, string>();
            globalVar.CrDr.Add(1, "Dr");
            globalVar.CrDr.Add(2, "Cr");

         //   Application.StartupPath;

         
            SettingFile = AppDomain.CurrentDomain.BaseDirectory + "setting.txt";
            GetRegistrySetting();
        }

        /// <summary>
        /// fill the database variables
        /// </summary>
        private void GetRegistrySetting()
        {
            //globalVar.DatabaseServer = GetSetting("MYSETTING", "DatabaseServer", "");
            //globalVar.DatabaseName = GetSetting("MYSETTING", "DatabaseName", "");
            //globalVar.UserName = GetSetting("MYSETTING", "UserName", "");
            //globalVar.Password = GetSetting("MYSETTING", "Password", "");


            if (File.Exists(SettingFile))
            {

                string text = File.ReadAllText(SettingFile);

                var h = JsonConvert.DeserializeObject<loginDetail>(text);

                globalVar.UserName = h.UserID;
                globalVar.Password = h.Pwd;
                globalVar.DatabaseName = h.DB;
                globalVar.DatabaseServer = h.Location;

            }
            else
            {
                throw new Exception("Setting File not found in  : "+ SettingFile);
            }
            

            //globalVar.DatabaseServer = "192.168.0.107";
            //globalVar.DatabaseName = "ERPV2";
            //globalVar.UserName = "edp";
            //globalVar.Password = "edp";
        }
        /// <summary>
        /// get the registry settings of the database fields
        /// </summary>
        /// <param name="Section">Key name in database</param>
        /// <param name="Key">String Name in the key</param>
        /// <param name="Default">Default value</param>
        /// <returns>Value in the registry of specified key</returns>
        private string GetSetting(string Section, string Key, string Default)
        {
            if (Default == null)
            {
                Default = "";
            }
            Microsoft.Win32.RegistryKey key1 = Application.UserAppDataRegistry.OpenSubKey(Section);
            if (key1 != null)
            {
                object obj1 = key1.GetValue(Key, Default);
                key1.Close();
                if (obj1 != null)
                {
                    if (!(obj1 is string))
                    {
                        return null;
                    }
                    return (string)obj1;
                }
                return null;
            }
            return Default;
        }

        /// <summary>
        /// use database variables to set registry
        /// </summary>
        public void SaveRegistrySetting()
        {
            SaveSetting("MYSETTING", "DatabaseServer", globalVar.DatabaseServer);
            SaveSetting("MYSETTING", "DatabaseName", globalVar.DatabaseName);
            SaveSetting("MYSETTING", "UserName", globalVar.UserName);
            SaveSetting("MYSETTING", "Password", globalVar.Password);
        }

        /// <summary>
        /// Use to create key at the specified location and set its string value
        /// </summary>
        /// <param name="Section">Key name in database</param>
        /// <param name="Key">String Name in the key</param>
        /// <param name="Setting">Default value</param>
        private void SaveSetting(string Section, string Key, string Setting)
        {
            Microsoft.Win32.RegistryKey key1 = Application.UserAppDataRegistry.CreateSubKey(Section);
            key1.SetValue(Key, Setting);
            key1.Close();
        }

        /// <summary>
        /// method to get connection string
        /// </summary>
        /// <returns></returns>
        protected string Connection()
        {
            //GetRegistrySetting();
            return "user id=" + globalVar.UserName + ";password=" + globalVar.Password + ";data source=" + globalVar.DatabaseServer + 
                    ";database=" + globalVar.DatabaseName +
                    ";Persist Security Info=false;Pooling=True;Max Pool Size= 2000;Connect Timeout=120;Encrypt =False;Enlist=false";//;Packet Size= 32500";
            //      @"Data Source=yusuf;" +
            //        @"Initial Catalog=erp;Persist Security Info" +
            //        @"=True;User ID=yusuf;Password=A;" +
            //        @"Pooling=True;Min Pool Size=5;Max Pool Size=100;" +
            //        @"Asynchronous Processing=True;" +
            //        @"MultipleActiveResultSets=True;Connect Timeout=15";
        }
        /// <summary>
        /// method to get connection string
        /// </summary>
        /// <returns></returns>
        protected string Connection(DatabaseName dbName)
        {
            //GetRegistrySetting();
            return "user id=" + globalVar.UserName + ";password=" + globalVar.Password + ";data source=" + globalVar.DatabaseServer + 
                    ";database=" + globalVar.DatabaseName + dbName +
                    ";Persist Security Info=false;Pooling=True;Max Pool Size= 10;Connect Timeout=120; " +
                    "Encrypt =False;Enlist=false";//;Packet Size= 32500";
            //      @"Data Source=yusuf;" +
            //        @"Initial Catalog=erp;Persist Security Info" +
            //        @"=True;User ID=yusuf;Password=A;" +
            //        @"Pooling=True;Min Pool Size=5;Max Pool Size=100;" +
            //        @"Asynchronous Processing=True;" +
            //        @"MultipleActiveResultSets=True;Connect Timeout=15";
        }
    }
}
