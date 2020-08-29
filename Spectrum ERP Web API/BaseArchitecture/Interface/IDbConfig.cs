//*****************************************************************************
//    File           : 
//    Desc           : This Interface is used databse configuration  class
//    Author         : karry
//    Date           : 24/09/08  
//*******************************************************************************
//      Change History
//*******************************************************************************
//      Date:       Author:             Description:
//      --------    --------            -----------------------------------------
// 01)    
// 02)
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseArchitecture.Interface
{
    interface IDbConfig
    {
        /// <summary>
        /// Interface to Get Registry Setting
        /// </summary>
        void GetRegistrySetting();
        /// <summary>
        /// Interface to Get Setting excuted
        /// </summary>
        /// <param name="Section">section </param>
        /// <param name="Key">registry key</param>
        /// <param name="Default">default value</param>
        /// <returns></returns>
        string GetSetting(string Section, string Key, string Default);
        /// <summary>
        /// Interface to Save Registry Setting
        /// </summary>
        void SaveRegistrySetting();
        /// <summary>
        /// Interface to save settings
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Setting"></param>
        void SaveSetting(string Section, string Key, string Setting);
        /// <summary>
        /// Interface to involve connection string 
        /// </summary>
        /// <returns></returns>
        string Connection();
    }
}
