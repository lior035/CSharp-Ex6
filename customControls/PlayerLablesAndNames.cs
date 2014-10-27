namespace customControls
{
     using System;
     using System.Collections.Generic;
     using System.Text;
     using System.Windows.Forms;

     public delegate void PlayersTextChangedEventHandler(object sender, EventArgs e);

     /// <summary>
     /// user control for games with 2 player textbox inputs
     /// </summary>
     public class PlayerLablesAndNames : UserControl
     {
          private TextBox TextboxPlayer2;
          private Label label4;
          private Label label2;
          private TextBox TextboxPlayer1;
          
          public PlayerLablesAndNames()
          {
               InitializeComponent();
               this.TextboxPlayer1.TextChanged += new System.EventHandler(this.TextboxPlayer1_TextChanged);
               this.TextboxPlayer2.TextChanged += new System.EventHandler(this.TextboxPlayer2_TextChanged);
          }

          public event PlayersTextChangedEventHandler PlayersTextChanged;
          
          public string Player1Name
          {
               get { return TextboxPlayer1.Text; }
          }

          public string Player2Name
          {
               get { return TextboxPlayer2.Text; }
          }

          private void TextboxPlayer2_TextChanged(object sender, EventArgs e)
          {
               onTextChanged();
          }
         
          private void TextboxPlayer1_TextChanged(object sender, EventArgs e)
          {
               onTextChanged();
          }

          private void onTextChanged()
          {
               if (PlayersTextChanged != null)
               {
                    PlayersTextChanged.Invoke(this, null);
               }
          }

          private void InitializeComponent()
          {
               this.TextboxPlayer2 = new System.Windows.Forms.TextBox();
               this.label4 = new System.Windows.Forms.Label();
               this.label2 = new System.Windows.Forms.Label();
               this.TextboxPlayer1 = new System.Windows.Forms.TextBox();
               this.SuspendLayout();
               // 
               // TextboxPlayer2
               // 
               
               this.TextboxPlayer2.Location = new System.Drawing.Point(102, 39);
               this.TextboxPlayer2.MaxLength = 8;
               this.TextboxPlayer2.Name = "TextboxPlayer2";
               this.TextboxPlayer2.Size = new System.Drawing.Size(100, 20);
               this.TextboxPlayer2.TabIndex = 2;
               this.TextboxPlayer2.Text = "Player2";
               // 
               // label4
               // 
               this.label4.AutoSize = true;
               this.label4.Location = new System.Drawing.Point(6, 6);
               this.label4.Name = "label4";
               this.label4.Size = new System.Drawing.Size(48, 13);
               this.label4.TabIndex = 6;
               this.label4.Text = "Player 1:";
               // 
               // label2
               // 
               this.label2.AutoSize = true;
               this.label2.Location = new System.Drawing.Point(6, 46);
               this.label2.Name = "label2";
               this.label2.Size = new System.Drawing.Size(48, 13);
               this.label2.TabIndex = 4;
               this.label2.Text = "Player 2:";
               // 
               // TextboxPlayer1
               // 
               this.TextboxPlayer1.Location = new System.Drawing.Point(102, 3);
               this.TextboxPlayer1.MaxLength = 8;
               this.TextboxPlayer1.Name = "TextboxPlayer1";
               this.TextboxPlayer1.Size = new System.Drawing.Size(100, 20);
               this.TextboxPlayer1.TabIndex = 0;
               this.TextboxPlayer1.Text = "Player1";
               // 
               // PlayerLablesAndNames
               // 
               this.Controls.Add(this.TextboxPlayer1);
               this.Controls.Add(this.TextboxPlayer2);
               this.Controls.Add(this.label4);
               this.Controls.Add(this.label2);
               this.Name = "PlayerLablesAndNames";
               this.Size = new System.Drawing.Size(204, 66);
               this.ResumeLayout(false);
               this.PerformLayout();
          }         
     }
}
