// -----------------------------------------------------------------------
// <copyright file="FormGameSettings.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace A14_DN_MTA_Ex06
{
     using System;
     using System.Collections.Generic;
     using System.Text;
     using System.Windows.Forms;

     /// <summary>
     /// TODO: Update summary.
     /// </summary>
     public class FormGameSettings : Form
     {
          private Button buttonCancel;
          private Button buttonOK;
          private customControls.PlayerLablesAndNames playerLablesAndNames1;
          private customControls.BoardSizeNUDSetting boardSizeNUDSetting1;

          internal int NumOfCols
          {
               get { return boardSizeNUDSetting1.NumOfCols; }
          }

          internal int NumOfRows
          {
               get { return boardSizeNUDSetting1.NumOfRows; }
          }

          internal string Player1Name
          {
               get { return playerLablesAndNames1.Player1Name; }
          }

          internal string Player2Name
          {
               get { return playerLablesAndNames1.Player2Name; }
          }

          public FormGameSettings()
          {
               InitializeComponent();
               this.playerLablesAndNames1.PlayersTextChanged += new customControls.PlayersTextChangedEventHandler(playerLablesAndNames1_TextChanged);
          }

          private void InitializeComponent()
          {
               this.buttonCancel = new System.Windows.Forms.Button();
               this.buttonOK = new System.Windows.Forms.Button();
               this.playerLablesAndNames1 = new customControls.PlayerLablesAndNames();
               this.boardSizeNUDSetting1 = new customControls.BoardSizeNUDSetting();
               this.SuspendLayout();
               // 
               // buttonCancel
               // 
               this.buttonCancel.Location = new System.Drawing.Point(155, 214);
               this.buttonCancel.Name = "buttonCancel";
               this.buttonCancel.Size = new System.Drawing.Size(75, 23);
               this.buttonCancel.TabIndex = 10;
               this.buttonCancel.Text = "Cancel";
               this.buttonCancel.UseVisualStyleBackColor = true;
               this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
               // 
               // buttonOK
               // 
               this.buttonOK.Location = new System.Drawing.Point(49, 214);
               this.buttonOK.Name = "buttonOK";
               this.buttonOK.Size = new System.Drawing.Size(75, 23);
               this.buttonOK.TabIndex = 8;
               this.buttonOK.Text = "OK";
               this.buttonOK.UseVisualStyleBackColor = true;
               this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
               // 
               // playerLablesAndNames1
               // 
               this.playerLablesAndNames1.Location = new System.Drawing.Point(37, 12);
               this.playerLablesAndNames1.Name = "playerLablesAndNames1";
               this.playerLablesAndNames1.Size = new System.Drawing.Size(213, 76);
               this.playerLablesAndNames1.TabIndex = 2;
               // 
               // boardSizeNUDSetting1
               // 
               this.boardSizeNUDSetting1.Location = new System.Drawing.Point(40, 94);
               this.boardSizeNUDSetting1.Name = "boardSizeNUDSetting1";
               this.boardSizeNUDSetting1.Size = new System.Drawing.Size(190, 72);
               this.boardSizeNUDSetting1.TabIndex = 4;
               // 
               // FormGameSettings
               // 
               this.ClientSize = new System.Drawing.Size(262, 262);
               this.Controls.Add(this.boardSizeNUDSetting1);
               this.Controls.Add(this.playerLablesAndNames1);
               this.Controls.Add(this.buttonOK);
               this.Controls.Add(this.buttonCancel);
               this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
               this.MaximizeBox = false;
               this.MinimizeBox = false;
               this.Name = "FormGameSettings";
               this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
               this.Text = "Game Properties";
               this.ResumeLayout(false);
          }
       
          private void playerLablesAndNames1_TextChanged(object sender, EventArgs e)
          {
               customControls.PlayerLablesAndNames pLbls = sender as customControls.PlayerLablesAndNames;
               if (pLbls.Player1Name.Length == 0 || pLbls.Player2Name.Length == 0)
               {
                    buttonOK.Enabled = false;
               }
               else
               {
                    buttonOK.Enabled = true;
               }
          }
          
          private void buttonOK_Click(object sender, EventArgs e)
          {
               this.DialogResult = DialogResult.OK;               
               this.Close();
          }

          private void buttonCancel_Click(object sender, EventArgs e)
          {
               this.Close();
          }         
     }
}
