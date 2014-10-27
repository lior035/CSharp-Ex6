namespace customControls
{
     using System;
     using System.Collections.Generic;
     using System.Text;
     using System.Windows.Forms;
     using System.Drawing;

     public class GameBoardCell : UserControl
     {
          private Button m_MatrixButton = new Button();

          public GameBoardCell()
          {
               InitializeCell();
          }

          private void InitializeCell()
          {
               m_MatrixButton.FlatStyle = FlatStyle.Flat;
               m_MatrixButton.BackgroundImage = Properties.Resources.FullCellRed;
               BitmapRegionTest.BitmapRegion.CreateControlRegion(m_MatrixButton, false);
               this.Region = m_MatrixButton.Region;
               this.Size = new Size(m_MatrixButton.BackgroundImage.Width, m_MatrixButton.Height);
               this.Controls.Add(m_MatrixButton);
          }
     
          public Region PictureRegion
          {
               get { return m_MatrixButton.Region; }
               set
               {
                    m_MatrixButton.Region = value;
                    this.Region = value;
               }
          }

          public Image PictureBackgroundImage
          {
               get { return m_MatrixButton.BackgroundImage; }
               set { m_MatrixButton.BackgroundImage = value; }
          }
     }
}