namespace A14_DN_MTA_Ex06
{
     using System;
     using System.Collections.Generic;
     using System.Text;
     using System.Windows.Forms;

     public class FormAbout : Form
     {
          private Label label1;

          public FormAbout()
          {
               InitializeComponent();
          }

          private void InitializeComponent()
          {
               this.label1 = new System.Windows.Forms.Label();
               this.SuspendLayout();
               // 
               // label1
               // 
               this.label1.AutoSize = true;
               this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
               this.label1.Location = new System.Drawing.Point(28, 112);
               this.label1.Name = "label1";
               this.label1.Size = new System.Drawing.Size(234, 39);
               this.label1.TabIndex = 0;
               this.label1.Text = "4 In A Row!! :)";
               this.label1.Click += new System.EventHandler(this.label1_Click);
               // 
               // FormAbout
               // 
               this.BackColor = System.Drawing.Color.White;
               this.ClientSize = new System.Drawing.Size(284, 262);
               this.Controls.Add(this.label1);
               this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
               this.Name = "FormAbout";
               this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
               this.Click += new System.EventHandler(this.FormAbout_Click);
               this.ResumeLayout(false);
               this.PerformLayout();
          }

          private void FormAbout_Click(object sender, EventArgs e)
          {
               this.Close();
          }

          private void label1_Click(object sender, EventArgs e)
          {
               this.Close();
          }
     }
}
