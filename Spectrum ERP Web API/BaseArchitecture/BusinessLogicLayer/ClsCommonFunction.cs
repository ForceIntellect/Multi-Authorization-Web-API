//*****************************************************************************
//    File           : 
//    Desc           : This class is used for capturing data from database 
//
//    Author         : Yusuf
//    Date           : Sept 24, 2008
//*******************************************************************************
//      Change History
//*******************************************************************************
//      Date:       Author:             Description:
//      --------    --------            -----------------------------------------
// 01) 24/09/08     karry               Code Review and changes for three tier artit.
// 02)   
//******************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Globalization;

namespace BaseArchitecture.BusinessLogicLayer
{
    public class ClsCommonFunction : ClsMessage
    {

        private const uint CB_ERR = 0;
        private const int CB_SELECTSTRING = 0x14D;
        private const int CB_SHOWDROPDOWN = 0x14F;

        //If you want to prevent an (unreferenced) managed object from possibly being garbage collected
        //during a call to SendMessage, you can wrap the handle in a HandleRef structure.
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = false)]
        private static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, string lParam);

        delegate void SetTextCallbackCB(DataTable dt, ComboBox cmb, string[] columnNames);
        delegate void ControlValueFromProcedureCallBack(object ctrl, object value);
        //delegate void ControlValueFromProcedureCallBackUltraTextEditor(UltraTextEditor ctrl, object value);
        //delegate void ControlValueFromProcedureCallBackUltraOptionSet(UltraOptionSet ctrl, object value);
        //delegate void ControlValueFromProcedureCallBackUltraComboEditor(UltraComboEditor ctrl, object value);
        //delegate void ControlValueFromProcedureCallBackUltraUltraCombo(UltraCombo ctrl, object value);
        //delegate void ControlValueFromProcedureCallBackUltraCheckEditor(TextBox ctrl, object value);
        //delegate void ControlValueFromProcedureCallBackTextBox(TextBox ctrl, object value);
        //delegate void ControlValueFromProcedureCallBackRadioButton(RadioButton ctrl, object value);
        //delegate void ControlValueFromProcedureCallBackGroupBox(GroupBox ctrl, object value);
        //delegate void ControlValueFromProcedureCallBackCheckBox(CheckBox ctrl, object value);
        //delegate void ControlValueFromProcedureCallBackComboBox(ComboBox ctrl, object value);
        //delegate void ControlValueFromProcedureCallBackDateTimePicker(DateTimePicker ctrl, object value);

     
        public enum Rounding
        {
            flsNoRounding,
            flsUpperSide,
            flsLowerSide,
            flsToCustom,
            flsAwayFromZero,
            flsToEven
        }

        public enum FunctionType
        {
            flsSave,
            flsUpdate,
            flsDelete,
            flsPrint,
            flsPrintPreview,
            flsOpen,
            flsAuthorize,
            flsRelease,
            flsExport,
            flsEmail,
            flsNotify,
        }

        /// <summary>
        /// Declare format for numeric,float,string,alphanumeric
        /// </summary>
        public enum EnterType
        {
            NumericType,
            FloatType
        }
        /// <summary>
        /// Declare format for Date/Time
        /// </summary>
        public enum FormatType
        {
            DateFormat,
            TimeFormat
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool Beep(int freq, int duration);

        //--------------------Ajay : new code on 18/10/08 -----------------------------Open
        public enum ToolBarOption : int
        {
            New = 4,
            Open = 5,
            Save = 6,
            Delete = 7,
            Close = 8,
            CloseAll = 9,
            Search = 11,
            Print = 13,
            PrintPreview = 14,
            Find = 16,
            Navigation = 20,
            Export = 30,
            Email = 40,
            Notify = 50
        }

        private int[] options;

        public int[] Options
        {
            set { options = value; }
        }

        public struct FilterValue
        {
            public string columnName;
            public object data;

        }



        /// <summary>
        /// It will return dataset after executing procedure  using Aray for store parameter values
        /// </summary>
        /// <param name="procName">Procedure Name</param>
        /// <param name="myarr"> paramenter of Procedure </param>
        /// <returns></returns>
        public DataSet ExecuteProc(string procName, object[,] array)
        {
            DataSet ds = new DataSet();
            ds = ArrayExecuteProc(procName, array);
            return (ds);
        }

        /// <summary>
        /// It will return dataset after executing procedure
        /// </summary>
        /// <param name="procName">Procedure Name</param>
        /// <param name="myarr"> paramenter of Procedure </param>
        /// <returns></returns>
        public DataSet ExecuteProc(string procName, object[,] myarr, SqlConnection con, SqlTransaction trans)
        {
            DataSet ds = new DataSet();
            ds = ArrayExecuteProc(procName, myarr, con, trans);
            //SqlConnection.ClearPool(con);
            return (ds);
        }

        /// <summary>
        /// It will return dataset after executing procedure  using Dictionary for store parameter values
        /// </summary>
        /// <param name="procName">Procedure Name</param>
        /// <param name="myarr"> paramenter of Procedure </param>
        /// <returns></returns>
        public DataSet ExecuteProc(string procName, Dictionary<string, object> dictionary)
        {
            DataSet ds = new DataSet();
            if (conn != null)
                ds = DictionaryExecuteProc(procName, dictionary, conn, tran);
            else
                ds = DictionaryExecuteProc(procName, dictionary);
            return (ds);
        }

        public DataSet ExecuteProcReader(string procName, Dictionary<string, object> dictionary, short NoOftable)
        {
            DataSet ds;
            if (conn != null)
                ds = DictionaryExecuteProcReader(procName, dictionary, conn, tran, NoOftable);
            else
                ds = DictionaryExecuteProcReader(procName, dictionary, NoOftable);

            return (ds);
        }

        /// <summary>
        /// method to excute query of sql
        /// </summary>
        /// <param name="sql">sql query</param>
        public DataSet ExecuteQuery(string sqlQuery)
        {
            if (conn != null)
                return StrExecuteQuery(sqlQuery, conn, tran);
            else
                return StrExecuteQuery(sqlQuery);
        }

        /// <summary>
        /// method to excute query of sql
        /// </summary>
        /// <param name="sql">sql query</param>
        public object ExecuteScalar(string sqlQuery)
        {
            if (conn != null)
                return StrExecuteScalar(sqlQuery, conn, tran);
            else
                return StrExecuteScalar(sqlQuery);
        }

        /// <summary>
        /// It will return dataset after executing procedure using Dictionary for store parameter values with pool of connection
        /// </summary>
        /// <param name="procName">Procedure Name</param>
        /// <param name="myarr"> paramenter of Procedure </param>
        /// <returns></returns>
        public DataSet ExecuteProc(string procName, Dictionary<string, object> dictionary, SqlConnection con, SqlTransaction trans)
        {
            DataSet ds = new DataSet();
            ds = DictionaryExecuteProc(procName, dictionary, con, trans);
            //SqlConnection.ClearPool(con);
            return (ds);
        }
              
        public DateTime FirstDateOfdtPicker(Control ctrl)
        {
            try
            {
                if (ctrl is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)ctrl;
                    DateTime firstDay = new DateTime(dtp.Value.Year, dtp.Value.Month, 1);
                    return firstDay.Date;

                }

                else
                    throw new Exception(ctrl.ToString() + "\n" + " This control is not validated in BaseArchitecture.BusinessLogicLayer.StrForProcedure.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Used for converting DateTimePicker value LastDate in object format
        /// 
        /// </summary>
        /// <param name="ctrl">Control Name</param>
        /// <returns>Return Value in object Format</returns>
        public DateTime LastDateOfdtPicker(Control ctrl)
        {
            try
            {
                if (ctrl is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)ctrl;
                    DateTime firstDay = new DateTime(dtp.Value.Year, dtp.Value.Month, 1);
                    int DaysinMonth = DateTime.DaysInMonth(dtp.Value.Year, dtp.Value.Month);
                    DateTime lastDay = firstDay.AddDays(DaysinMonth - 1);
                    return lastDay.Date;
                }

                else
                    throw new Exception(ctrl.ToString() + "\n" + " This control is not validated in BaseArchitecture.BusinessLogicLayer.StrForProcedure.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

                                                             
        /// <summary>
        /// it replace single quote with double as we required in Procedure or in datatable filteration
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        //public string AllowSingleQuote(string str)
        //{
        //    str = str.Replace("'", "''");
        //    return str;
        //}

        /// <summary>
        /// this function will check for login user right for particular action in passed form in logined company
        /// </summary>
        /// <param name="enmFunction"></param>
        /// <param name="Frm"></param>
        /// <param name="giLoginType"></param>
        /// <param name="giLoginNo"></param>
        /// <param name="gCompanyNo"></param>
        /// <returns></returns>
        public bool CheckRights(FunctionType enmFunction, Form Frm, int giLoginType, int giLoginNo, int gCompanyNo)
        {
            bool functionReturnValue = false;

            try
            {
                if (giLoginType == 1)
                {
                    functionReturnValue = true;
                    return functionReturnValue;
                }

                Dictionary<string, object> dictionary = new Dictionary<string, object>();

                dictionary.Add("@Code", Frm.Tag);
                dictionary.Add("@FunctionName", Convert.ToString(enmFunction).Remove(0, 3));

                dictionary.Add("@LoginNo", giLoginNo);
                dictionary.Add("@CompanyNo", gCompanyNo);

                DataTableReader dr = ExecuteProc("[Security].[CheckFormRights]", dictionary).CreateDataReader();

                if (dr.Read())
                {
                    if (dr[0].ToString() == "0")
                    {

                        string Funct = null;
                        if (Convert.ToString(Frm.Tag) == "")
                        {
                            Funct = "Check for Form Tag, contact Service Provider";
                        }
                        else
                        {
                            //if ((string.IsNullOrEmpty(myarr[1, 0].ToString())))
                            //{
                            //    Funct = "Form -" + dr[1].ToString();
                            //}
                            //else
                            //{
                            //    Funct = "Form -" + dr[1].ToString() + " :: Function -" + myarr[1, 0];
                            //}
                        }

                        //functionReturnValue = false;
                        //Infragistics.Win.Misc.UltraDesktopAlertShowWindowInfo showInfo = new Infragistics.Win.Misc.UltraDesktopAlertShowWindowInfo();
                        //Infragistics.Win.Misc.UltraDesktopAlert objAlert = new Infragistics.Win.Misc.UltraDesktopAlert();
                        //showInfo.Caption = Funct;
                        //showInfo.Text = "Insufficient Access Rights !";
                        //objAlert.Style = Infragistics.Win.Misc.DesktopAlertStyle.Office2007;
                        //objAlert.AutoClose = Infragistics.Win.DefaultableBoolean.True;
                        //objAlert.AutoCloseDelay = 1000;

                        ////this.desktopAlert.FixedSize = new Size(0, 200);
                        //// objAlert.TextAppearance.ForeColor = objAlert.TextAppearance.ForeColor.A;
                        //showInfo.ScreenPosition = Infragistics.Win.Misc.ScreenPosition.Center;

                        //try
                        //{

                        //    System.Media.SoundPlayer sound = new System.Media.SoundPlayer("Notify.wav");
                        //    sound.Play();
                        //}
                        //catch
                        //{
                        //    Microsoft.VisualBasic.Interaction.Beep();
                        //    // Beep(32760, 40000);

                        //}

                        //// showInfo.Sound = objSound.GetSound(SoundName.Notify);
                        //Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance();
                        //Appearance1.BackColor = System.Drawing.Color.Red;
                        //objAlert.Appearance.BackColor = System.Drawing.Color.Red;
                        //objAlert.AnimationSpeed = Infragistics.Win.Misc.AnimationSpeed.Default;
                        //objAlert.MultipleWindowDisplayStyle = Infragistics.Win.Misc.MultipleWindowDisplayStyle.None;
                        //// objAlert.AlertButtonClicked += new Infragistics.Win.Misc.AlertButtonClickedHandler(objAlert_AlertButtonClicked);
                        //objAlert.Show(showInfo);

                        return functionReturnValue;
                    }

                }
                dr.Close();

                functionReturnValue = true;
            }

            catch (Exception ex)
            {
                ErrorMessage(ex.Message);
            }

            return functionReturnValue;
        }
             

        /// <summary>
        /// Method to write binary data to physical file
        /// </summary>
        /// <param name="filePath">file Path on local system </param>
        /// <returns>true(success)/false(failure)</returns>
        public void FileWriter(string filePath, byte[] docData, string fileName)
        {
            try
            {
                //create file  with the file path
                FileStream fileStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
                //Binary writer on file 
                BinaryWriter binaryWriter = new BinaryWriter(fileStream);
                binaryWriter.Write(docData);
                binaryWriter.Close();
                fileStream.Close();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
                                

  

        public SqlDependency Recursion(string sql)
        {
            string connstring = Connection();
            using (SqlConnection conn = new SqlConnection(connstring))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                try
                {
                    SqlDependency depend = new SqlDependency(cmd);
                    SqlDependency.Start(connstring);

                    //depend.OnChange += new OnChangeEventHandler(MyOnChanged);

                    conn.Open();

                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    rdr.Close();

                    conn.Close();

                    return depend;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #region For Load testing

        Random rndNum = new Random();


        public string LoadTesting_TextBoxText(byte tag, int maxlength, int dcPt)
        {
            if (tag == 0)
            {
                if (maxlength <= 0 || strStory.Length - maxlength < 0)
                {
                    return "MaxLengthNotSet";
                }
                else
                {
                    int startindex = rndNum.Next(1, strStory.Length - maxlength);
                    if (globalVar.LoadTesting_BoundryValues_Enable)
                    {
                        return strStory.Substring(startindex, maxlength);
                    }
                    else
                    {
                        return strStory.Substring(startindex, rndNum.Next(1, maxlength));
                    }
                }
            }
            else if (tag == 1)
            {
                int val = 0;
                if (maxlength - dcPt > 6)
                {
                    if (globalVar.LoadTesting_BoundryValues_Enable)
                    {
                        val = 6;
                    }
                    else
                    {
                        val = rndNum.Next(1, 6);
                    }
                }
                else
                {
                    if (globalVar.LoadTesting_BoundryValues_Enable)
                    {
                        val = maxlength - dcPt - 1;
                    }
                    else
                    {
                        val = rndNum.Next(1, maxlength - dcPt - 1);
                    }
                }
                return rndNum.Next(1, Convert.ToInt32(new string('9', val))) + "." + rndNum.Next(1, Convert.ToInt32(new string('9', dcPt)));
            }
            else if (tag == 2)
            {
                int val = 0;
                if (maxlength > 6)
                {
                    if (globalVar.LoadTesting_BoundryValues_Enable)
                    {
                        val = 6;
                    }
                    else
                    {
                        val = rndNum.Next(1, 6);
                    }
                }
                else
                {
                    if (globalVar.LoadTesting_BoundryValues_Enable)
                    {
                        val = maxlength;
                    }
                    else
                    {
                        val = rndNum.Next(1, maxlength);
                    }
                }
                return Convert.ToString(rndNum.Next(1, Convert.ToInt32(new string('9', val))));
            }
            else
            {
                return "1";
            }
        }

        string strStory = "Birbal Stories are very famous and popular in India among all ages of people. They are also called by another name Akbar-Birbal Stories."
                        + "There was a Mogul Emperor in India, Akbar The Great (1542-1605). His full name was Jalaludden Mohammed Akbar Padshah Ghazi and he ruled India from 1560 to 1605. He himself was illiterate, but he invited several learned people in his court. Among these people, nine were very famous and were called Nav Ratna (nine jewels of the Mogul Crown) of his court. Among these nine jewels, five people were more famous - Tansen, Todarmal, Abul Fazal, Maan Singh and Birbal."
                        + "1. Tansen ... A Great Singer"
                        + "2. Dasvant ... A Great Painter"
                        + "3. King Todarmal ... A Financial Wizard"
                        + "4. Abdu us-Samad ... A Brilliant Calligrapher and Designer of Imperil Coins"
                        + "5. Abul Fazal ... A Great Historian ( whose brother was Faizi )"
                        + "6. Faizi ... A Great Poet"
                        + "7. Mir Fareh-ullah Shirazi ... Financier,Philosopher,Physician & Astronomer"
                        + "8. King Maan Singh ... A Great Man known for His Chivalry"
                        + "9. Birbal ... A Great Man known for His Valuable Advice"
                        + "Akbar's son Prince Sultan Salim, later known as Jehangir wrote that nobody could make out that Akbar was an illiterate. Akbar was a very hard-working King. It is also said about him that he slept only three hours a night."
                        + "Birbal (1528-1583) is surely one of the most popular figures in Indian history equally regarded by adults and children. Birbal's duties in Akbar's court were mostly administrative and military but he was a very close friend of Akbar too, because Akbar loved his wisdom, wit and subtle humor. He was a minister in the administration of Mogul Emperor Akbar and one of the members of inner council of nine advisors. He was a poet and an author too."
                        + "It is believed that he was a son of poor Braahman of Trivikrampur on the banks of River Yamuna. According to a popular legend, he died on an expedition to Afghanistan at the head of a large military force due to treachery. It is also said that when Birbal died, Akbar mourned him for several months."
                        + "The exchanges between Akbar and Birbal have been recorded in many volumes. Many of these have become folk stories in Indian tradition. Birbal's collection of poetry published under the pen name Brahm are preserved in Bharatpur Museum, Rajasthan, India."
                        + "Many courtiers were jealous with Birbal and often plotted for his downfall. There are many stories found on this issue too. There are a couple of other stories too which are of the same time and type and are as interesting as Birbal's ones."
                        + "There are many books published about Birbal Stories.";

        #endregion

        public decimal CustomRound(decimal value, Rounding enmround)
        {

            value = Convert.ToDecimal(value.ToString("F02"));

            if (enmround == Rounding.flsAwayFromZero)
            {
                value = Math.Round(value, 2, MidpointRounding.AwayFromZero);
            }
            else if (enmround == Rounding.flsLowerSide)
            {
                value = Math.Floor(value);
            }
            else if (enmround == Rounding.flsToCustom)
            {
                value = CustomRound(value);
            }
            else if (enmround == Rounding.flsToEven)
            {
                value = Math.Round(value, 2, MidpointRounding.ToEven);
            }
            else if (enmround == Rounding.flsUpperSide)
            {
                value = Math.Ceiling(value);
            }

            return value;

        }

        private decimal CustomRound(decimal value)
        {

            value = Convert.ToDecimal(value.ToString("F02"));

            if (value - Math.Floor(value) >= Convert.ToDecimal(0.5))
            {
                value = Math.Ceiling(value);
            }
            else
            {
                value = Math.Floor(value);
            }

            return value;

        }

        //public void MailViaOutlook(string sourceDocumentFormCode, Int32 sourceDocumentNo, DateTime sourceDocumentDate,
        //                            string sourceDocumentNoYearly, Int16 From, string ToAddress, string Subject, string Body,
        //                            Dictionary<string, string> filePathDic)
        //{
        //    Microsoft.Office.Interop.Outlook.Application objOutlk = new Microsoft.Office.Interop.Outlook.Application();
        //    Microsoft.Office.Interop.Outlook.NameSpace oNS = objOutlk.GetNamespace("mapi");
        //    //Outlook
        //    //const int olMailItem = 0;            
        //    Microsoft.Office.Interop.Outlook.MailItem objMail = (Microsoft.Office.Interop.Outlook.MailItem)objOutlk.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

        //    objMail = (Microsoft.Office.Interop.Outlook.MailItem)objOutlk.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
        //    //Email item

        //    //Insert your "To" address...it can by dynamically populated
        //    objMail.To = ToAddress;

        //    //Insert your "CC" address...it can by dynamically populated
        //    //objMail.cc = "ooo@yyy.com" 'Enter an address here To include a carbon copy; bcc is For blind carbon copy's

        //    //Set up Subject Line
        //    objMail.Subject = Subject;

        //    if (filePathDic != null)
        //    {
        //        foreach (KeyValuePair<string, string> kvp in filePathDic)
        //        {
        //            String sSource = kvp.Value.ToString();
        //            String sDisplayName = kvp.Value.ToString();
        //            int iPosition = 1;
        //            int iAttachType = (int)Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue;
        //            Microsoft.Office.Interop.Outlook.Attachment oAttach = objMail.Attachments.Add(sSource, iAttachType, iPosition, sDisplayName);

        //            //objMail.Attachments.Add(kvp.Value);
        //        }
        //    }

        //    //Set up your message body
        //    objMail.Body = Body;

        //    //Use this To display before sending, otherwise call (use) objMail.Send to send without reviewing
        //    objMail.Display(true);

        //    //Clean up
        //    objMail = null;
        //    objOutlk = null;
        //}

        public string SendMail(string to, string smtpServer, int portNo, string userId, string password, string displayName, bool ssl
                                    , string subject, string body, string cc, string bcc)
        {
            try
            {
                using (MailMessage mailmsg = new MailMessage())
                {
                    mailmsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.Delay;

                    mailmsg.From = new MailAddress(userId, displayName, System.Text.Encoding.UTF8);

                    mailmsg.Subject = subject;
                    mailmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                    mailmsg.Body = body;
                    mailmsg.BodyEncoding = System.Text.Encoding.UTF8;
                    mailmsg.IsBodyHtml = true;

                    SmtpClient smtpclient = new SmtpClient(smtpServer, portNo);
                    smtpclient.EnableSsl = ssl;
                    smtpclient.Credentials = new System.Net.NetworkCredential(userId, password);

                    if ((cc == "") && (bcc == ""))
                    {
                        string Tree = to + ",";
                        while (!string.IsNullOrEmpty(Tree))
                        {
                            mailmsg.To.Clear();
                            if (!string.IsNullOrEmpty(Tree.Substring(0, Tree.IndexOf(",", 0))))
                            {
                                mailmsg.To.Add(Tree.Substring(0, Tree.IndexOf(",", 0)));
                                smtpclient.Send(mailmsg);
                            }
                            Tree = Tree.Substring(Tree.IndexOf(",", 0) + 1, Tree.Length - Tree.IndexOf(",", 0) - 1);
                        }
                    }
                    else
                    {
                        mailmsg.To.Clear();
                        string Tree = to + ",";

                        Boolean IsIdExist = false;

                        foreach (string str in to.Split(','))
                        {
                            if (str != "")
                            {
                                mailmsg.To.Add(str);
                                IsIdExist = true;
                            }
                        }

                        foreach (string str in cc.Split(','))
                        {
                            if (str != "")
                            {
                                mailmsg.CC.Add(str);
                                IsIdExist = true;
                            }

                        }

                        foreach (string str in bcc.Split(','))
                        {
                            if (str != "")
                            {
                                mailmsg.Bcc.Add(str);
                                IsIdExist = true;
                            }
                        }

                        if (IsIdExist == true)
                        {
                            smtpclient.Send(mailmsg);
                        }
                    }
                }
            }
            catch (SmtpFailedRecipientsException)
            {
                return "Either arguments of mail id passed are wrong or empty.";
            }
            catch (SmtpFailedRecipientException)
            {
                return "Either arguments of mail id passed are wrong or empty.";
            }
            catch (ArgumentNullException)
            {
                return "Either arguments of mail id passed are wrong or empty.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "";
        }
        
        public bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"); 
        }

    }
}