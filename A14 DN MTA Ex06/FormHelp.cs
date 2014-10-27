namespace A14_DN_MTA_Ex06
{
     using System;
     using System.Collections.Generic;
     using System.Text;
     using System.Windows.Forms;
     using System.IO;

     public class FormHelp : Form
     {
          private RichTextBox richTextBoxHelp;
          private Button buttonOK;

          public FormHelp()
          {              
               InitializeComponent();           
               try
               {
                    richTextBoxHelp.LoadFile(@"C:\FourInARowHelp.txt", RichTextBoxStreamType.PlainText);
               }
               catch (Exception e)
               {
                    richTextBoxHelp.Text = "Help file not found";
               }

               this.richTextBoxHelp.ReadOnly = true;
          }

          private void InitializeComponent()
          {
               this.richTextBoxHelp = new System.Windows.Forms.RichTextBox();
               this.buttonOK = new System.Windows.Forms.Button();
               this.SuspendLayout();
               // 
               // richTextBoxHelp
               // 
               this.richTextBoxHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                           | System.Windows.Forms.AnchorStyles.Right)));
               this.richTextBoxHelp.BackColor = System.Drawing.Color.White;
               this.richTextBoxHelp.Location = new System.Drawing.Point(0, 1);
               this.richTextBoxHelp.Name = "richTextBoxHelp";
               this.richTextBoxHelp.Size = new System.Drawing.Size(288, 216);
               this.richTextBoxHelp.TabIndex = 0;
               this.richTextBoxHelp.Text = "";
               // 
               // buttonOK
               // 
               this.buttonOK.Location = new System.Drawing.Point(202, 223);
               this.buttonOK.Name = "buttonOK";
               this.buttonOK.Size = new System.Drawing.Size(60, 27);
               this.buttonOK.TabIndex = 1;
               this.buttonOK.Text = "OK";
               this.buttonOK.UseVisualStyleBackColor = true;
               this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
               // 
               // FormHelp
               // 
               this.BackColor = System.Drawing.Color.LightGray;
               this.ClientSize = new System.Drawing.Size(284, 262);
               this.Controls.Add(this.buttonOK);
               this.Controls.Add(this.richTextBoxHelp);
               this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
               this.MaximizeBox = false;
               this.MinimizeBox = false;
               this.Name = "FormHelp";
               this.Text = "How to play?";
               this.ResumeLayout(false);
          }

          private void buttonOK_Click(object sender, EventArgs e)
          {
               this.Close();
          }
     }
}
