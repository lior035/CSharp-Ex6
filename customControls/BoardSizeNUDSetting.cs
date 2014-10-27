namespace customControls
{
     using System;
     using System.Collections.Generic;
     using System.Text;
     using System.Windows.Forms;

     public class BoardSizeNUDSetting : UserControl
     {
          private NumericUpDown nUDRows;
          private NumericUpDown nUDCols;
          private Label label3;
          private Label label1;
     
          public BoardSizeNUDSetting()
          {
               InitializeComponent();
          }

          public int NumOfCols
          {
               get { return (int)nUDCols.Value; }
          }

          public int NumOfRows
          {
               get { return (int)nUDRows.Value; }
          }

          private void InitializeComponent()
          {
               this.nUDRows = new System.Windows.Forms.NumericUpDown();
               this.nUDCols = new System.Windows.Forms.NumericUpDown();
               this.label3 = new System.Windows.Forms.Label();
               this.label1 = new System.Windows.Forms.Label();
               ((System.ComponentModel.ISupportInitialize)(this.nUDRows)).BeginInit();
               ((System.ComponentModel.ISupportInitialize)(this.nUDCols)).BeginInit();
               this.SuspendLayout();
               // 
               // nUDRows
               // 
               this.nUDRows.Location = new System.Drawing.Point(132, 8);
               this.nUDRows.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
               this.nUDRows.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
               this.nUDRows.Name = "nUDRows";
               this.nUDRows.Size = new System.Drawing.Size(54, 20);
               this.nUDRows.TabIndex = 9;
               this.nUDRows.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
               // 
               // nUDCols
               // 
               this.nUDCols.Location = new System.Drawing.Point(132, 48);
               this.nUDCols.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
               this.nUDCols.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
               this.nUDCols.Name = "nUDCols";
               this.nUDCols.Size = new System.Drawing.Size(54, 20);
               this.nUDCols.TabIndex = 10;
               this.nUDCols.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
               // 
               // label3
               // 
               this.label3.AutoSize = true;
               this.label3.Location = new System.Drawing.Point(3, 10);
               this.label3.Name = "label3";
               this.label3.Size = new System.Drawing.Size(113, 13);
               this.label3.TabIndex = 8;
               this.label3.Text = "Board Height (in cells):";
               // 
               // label1
               // 
               this.label1.AutoSize = true;
               this.label1.Location = new System.Drawing.Point(3, 50);
               this.label1.Name = "label1";
               this.label1.Size = new System.Drawing.Size(110, 13);
               this.label1.TabIndex = 7;
               this.label1.Text = "Board Width (in cells):";
               // 
               // BoardSizeNUDSetting
               // 
               this.Controls.Add(this.nUDRows);
               this.Controls.Add(this.nUDCols);
               this.Controls.Add(this.label3);
               this.Controls.Add(this.label1);
               this.Name = "BoardSizeNUDSetting";
               this.Size = new System.Drawing.Size(190, 72);
               ((System.ComponentModel.ISupportInitialize)(this.nUDRows)).EndInit();
               ((System.ComponentModel.ISupportInitialize)(this.nUDCols)).EndInit();
               this.ResumeLayout(false);
               this.PerformLayout();
          }
     }
}
