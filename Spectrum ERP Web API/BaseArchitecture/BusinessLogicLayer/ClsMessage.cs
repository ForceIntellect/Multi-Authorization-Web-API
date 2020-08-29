//*****************************************************************************
//    File           : 
//    Desc           : This class is used  create general messages used in project
//    Author         : Yusuf
//    Date           : Sept 06, 2008
//*******************************************************************************
//      Change History
//**********************************************************************************************************
//      Date:       Author:             Description:                                  | comment
//      --------    --------            ------------------------------------------------------------------
// 01) 24/09/08     karry               Code Review and changes for three tier artit.| Please comment code
// 02)
//******************************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using BaseArchitecture.DataAccessLayer;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace BaseArchitecture.BusinessLogicLayer
{
    public class ClsMessage : ClsDbProc
    {
        private System.Windows.Forms.GroupBox grpBoxIcon = new System.Windows.Forms.GroupBox();

        private System.Windows.Forms.RadioButton radioButton4 = new System.Windows.Forms.RadioButton();
        private System.Windows.Forms.RadioButton radioButton3 = new System.Windows.Forms.RadioButton();
        private System.Windows.Forms.RadioButton radioButton2 = new System.Windows.Forms.RadioButton();
        private System.Windows.Forms.RadioButton radioButton1 = new System.Windows.Forms.RadioButton();

        private System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Ok");
        private System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Cancel");
        private System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Yes");
        private System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("No");
        private System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Abort");
        private System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Retry");
        private System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Ignore");

        private System.Windows.Forms.ColumnHeader columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));

        private System.Windows.Forms.ListView listViewButtons = new System.Windows.Forms.ListView();

        public ClsMessage()
        {
            grpBoxIcon.Controls.Add(radioButton4);
            grpBoxIcon.Controls.Add(radioButton3);
            grpBoxIcon.Controls.Add(radioButton2);
            grpBoxIcon.Controls.Add(radioButton1);

            radioButton4.Text = "Question";
            radioButton3.Text = "Exclamation";
            radioButton2.Text = "Hand";
            radioButton1.Text = "Asterisk";

            listViewButtons.CheckBoxes = true;
            listViewButtons.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1});
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.StateImageIndex = 0;
            listViewItem5.StateImageIndex = 0;
            listViewItem6.StateImageIndex = 0;
            listViewItem7.StateImageIndex = 0;
            listViewButtons.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7});
        }

        /// <summary>
        /// Message before saving of data.
        /// </summary>
        /// <returns></returns>
        public bool SaveMessage()
        {
            if (globalVar.LoadTesting_Enable || globalVar.bIsAutoAuthorization)
            {
                //return msgboxconfirmationautoclose("Do you want to save ?", "Confirmation", MsgBoxStyle.YesNo) == MsgBoxResult.No;
                return false;
            }
            else
                return MessageBox.Show("Do you want to save ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No;
        }

        /// <summary>
        /// Message before updating of data.
        /// </summary>
        /// <returns></returns>
        public bool UpdateMessage()
        {
            return MessageBox.Show("Do you want to update ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No;
        }

        /// <summary>
        /// Message before deleting of data.
        /// </summary>
        /// <returns></returns>
        public bool DeleteMessage()
        {
            if (globalVar.bIsAutoAuthorization)
            {
                return false;
            }
            else
            {
                return MessageBox.Show("Do you want to delete ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No;
            }
        }

        /// <summary>
        /// Message before canceling of data.
        /// </summary>
        /// <returns></returns>
        public bool CancelMessage()
        {
            if (globalVar.bIsAutoAuthorization)
            {
                return false;
            }
            else
            {
                return MessageBox.Show("Do you want to cancel ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No;
            }
        }

        /// <summary>
        /// Message format for compulsary fields on form
        /// </summary>
        /// <param name="Message"></param>
        public void ValidationMessage(string Message)
        {
            if (globalVar.bIsAutoAuthorization)
            {
                UpdateAutoAuthorization(Message);
            }
            else if (globalVar.LoadTesting_Enable)
            {
                //msgboxautoclose(Message, "Compulsory Field", MsgBoxStyle.Information);
            }
            else
                MessageBox.Show(Message, "Compulsory Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    
        /// <summary>
        /// Message for error.
        /// </summary>
        /// <param name="Message"></param>
        public void ErrorMessage(string Message)
        {
            if (globalVar.bIsAutoAuthorization)
            {
                UpdateAutoAuthorization(Message);
            }
            else
            {
                MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        public void UpdateAutoAuthorization(string errMsg)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

                dictionary.Add("@LoginNo",  globalVar.LoginNo  );
                dictionary.Add("@InternalNo", globalVar.ActiveFormInternalID );

                dictionary.Add("@FormCode", globalVar.ActiveFormCode );
                dictionary.Add("@Error", errMsg);


                System.Data.DataSet ds = DictionaryExecuteProc("MobileApp.UpdateAuthorizeThroughApp", dictionary);

                System.Data.DataTableReader dr = ds.Tables[0].CreateDataReader();

                if (dr.Read())
                {
                    
                }
                dr.Close();
        }
        /// <summary>
        /// Message after saving of data.
        /// </summary>
        public void SavedSuccessMessage()
        {
            if (globalVar.LoadTesting_Enable || globalVar.bIsAutoAuthorization )
            {
                //msgboxautoclose("Data saved successfully.", "Success", MsgBoxStyle.Information);
            }
            else
                MessageBox.Show("Data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Message after updation of data.
        /// </summary>
        public void UpdateSuccessMessage()
        {
            MessageBox.Show("Data updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Message after deletion of data.
        /// </summary>
        public void DeleteSuccessMessage()
        {
            if (globalVar.bIsAutoAuthorization)
            {
                UpdateAutoAuthorization("Data deleted successfully.");
            }
            else
            {
                MessageBox.Show("Data deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// by akhil . Message for confirmation. as we want default button should be differnt than Yes
        /// </summary>
        /// <returns></returns>
        public bool ConfirmationMessage(string Message, MessageBoxDefaultButton Default)
        {
            if (globalVar.LoadTesting_Enable || globalVar.bIsAutoAuthorization)
            {
                //return msgboxconfirmationautoclose(Message, "Confirmation", MsgBoxStyle.YesNo) == MsgBoxResult.No;
                return false;
            }
            else
                return MessageBox.Show(Message, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information, Default) == DialogResult.No;
        }
        /// <summary>
        /// Message for confirmation when we are deleting rows from grid
        /// </summary>
        /// <returns></returns>
        public bool RowDeleteMessage(int NoOfRows)
        {
            return System.Windows.Forms.MessageBox.Show(
                "Deleting " + NoOfRows.ToString() + " row(s). Continue ?",
                "Delete rows?",
                System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.No;
        }

        /// <summary>
        /// Message format for success occured
        /// </summary>
        /// <param name="Message"></param>
        public void SuccessMessage(object Message)
        {
            if (globalVar.bIsAutoAuthorization)
            {
                UpdateAutoAuthorization( Convert.ToString( Message ));
            }
            else if (globalVar.LoadTesting_Enable)
            {
                msgboxautoclose(Convert.ToString(Message), "Success", MsgBoxStyle.Information);
            }
            else
                MessageBox.Show(Convert.ToString(Message), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Use this Procedure in FormValidation For Validation Of Forms' Like Duplicacy/ Unablt to delete
        /// </summary>
        /// <param name="Message"></param>
        public void FormValidation(object Message)
        {
            if (globalVar.bIsAutoAuthorization)
            {
                UpdateAutoAuthorization(Convert.ToString( Message));
            }
            else if (globalVar.LoadTesting_Enable)
            {
                //msgboxautoclose(Convert.ToString(Message), "Validation", MsgBoxStyle.Information);
            }
            else
                MessageBox.Show(Convert.ToString(Message), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Message before Processing of data.
        /// </summary>
        /// <returns></returns>
        public bool ConfirmationMessage(object Message)
        {
            
            if (globalVar.LoadTesting_Enable || globalVar.bIsAutoAuthorization )
            {
                //return msgboxconfirmationautoclose(Message.ToString(), "Confirmation", MsgBoxStyle.YesNo) == MsgBoxResult.No;
                return false;
            }
            else
                return MessageBox.Show(Message.ToString(), "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No;
        }
        /// <summary>
        /// Message before Processing of data.
        /// </summary>
        /// <returns></returns>
        public bool WarningMessage(object Message)
        {
            if (globalVar.LoadTesting_Enable || globalVar.bIsAutoAuthorization )
            {
                //return msgboxconfirmationautoclose(Message.ToString(), "Warning", MsgBoxStyle.YesNo) == MsgBoxResult.No;
                return false;
            }
            else
                return MessageBox.Show(Message.ToString(), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No;
        }
        /// <summary>
        /// Message before Processing of data.
        /// </summary>
        /// <returns></returns>
        public bool FormClosingMessage()
        {
            if (globalVar.bIsAutoAuthorization)
            {
                return false;
            }
            else
            {
                return MessageBox.Show("Do you want to close?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No;
            }
        }
        public bool FormClosingMessage(string strFormName)
        {
            if (globalVar.bIsAutoAuthorization)
            {
                return false;
            }
            else
            {
                return MessageBox.Show("Do you want to close " + strFormName + " ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No;
            }
        }
       
        private byte[] GetSnapShot()
        {

            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                bitmap.Save("Error.jpg", ImageFormat.Jpeg);
            }

            /* Code 2 start

            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            Graphics graphics = Graphics.FromImage(bitmap as Image);

            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

            bitmap.Save(@"c:\temp\screenshot.bmp", ImageFormat.Jpeg);

            Code 2 end */

            /* Code 3 start
            Bitmap BMP = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
                            System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height,
                            System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            System.Drawing.Graphics GFX = System.Drawing.Graphics.FromImage(BMP);
            GFX.CopyFromScreen(System.Windows.Forms.Screen.PrimaryScreen.Bounds.X,
                                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Y,
                                0, 0,
                                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size,
                                System.Drawing.CopyPixelOperation.SourceCopy);
             BMP.Save(@"c:\temp\screenshot.bmp", ImageFormat.Jpeg);
             Code 3 end */

            System.IO.FileStream fls = null;

            fls = new System.IO.FileStream("Error.jpg", System.IO.FileMode.Open, System.IO.FileAccess.Read);

            byte[] blob = new byte[fls.Length];

            fls.Read(blob, 0, System.Convert.ToInt32(fls.Length));

            fls.Close();

            return blob;

        }

        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "keybd_event", CharSet = System.Runtime.InteropServices.CharSet.Auto, ExactSpelling = true)]
        public static extern void keybd_event(byte vk, byte scan, int flags, int extrainfo);

        private const byte VK_RETURN = 0xd;
        private const byte KEYEVENTF_KEYDOWN = 0x0;
        private const byte KEYEVENTF_KEYUP = 0x2;

        public void msgboxautoclose(string Message, string title, MsgBoxStyle Style)
        {
            System.Threading.Thread t = new System.Threading.Thread(closeMsgbox);
            t.Start(.3);
            Interaction.MsgBox(Message, Style, title);
        }

        public MsgBoxResult msgboxconfirmationautoclose(string Message, string title, MsgBoxStyle Style)
        {
            System.Threading.Thread t = new System.Threading.Thread(closeMsgbox);
            t.Start(.3);
            return Interaction.MsgBox(Message, Style, title);
        }

        private void closeMsgbox(object delay)
        {
            System.Threading.Thread.Sleep(Convert.ToInt32(delay) * 1000);
            //Interaction.AppActivate("SpectrumERP");
            keybd_event(VK_RETURN, 0, KEYEVENTF_KEYDOWN, 0);
            keybd_event(VK_RETURN, 0, KEYEVENTF_KEYUP, 0);
        }
    }
}
