namespace A14_DN_MTA_Ex06
{
     using System;
     using System.Collections.Generic;
     using System.Text;
     using System.Windows.Forms;
     using System.Drawing;
     using System.Drawing.Drawing2D;
     using System.IO;

     public class Form4InARow : Form
     {
          private const int k_WidthOfCellImage = 67;
          private const int k_HeightOfCellImage = 67;
          private const int k_WidthSpacingForPanel = 10;
          private const int k_HeightSpacingForPanel = 10;
          private const int k_SpacingBetweenPanelAndStatus = 30;
          private const int k_CoinPictureHeight = 58;
          private const int k_CoinPictureWidth = 58;
          private const int k_ExtraSpacingForAnimation = 4;
          private const int k_CoinStartingCol = 30;
          private const int k_CoinStartingRow = 30;
          private const int k_AnimationMovementSpeed = 8;
          private int m_BlinkCounter = 0;
          private Button m_ButtonCoin = new Button();
          private Bitmap m_RedCoinBitMap = new Bitmap(Properties.Resources.CoinRed);
          private Bitmap m_YellowCoinBitMap = new Bitmap(Properties.Resources.CoinYellow);
          private int m_StopAnimationHeight;
          private customControls.GameBoardCell m_CellToStop = new customControls.GameBoardCell();
          private customControls.GameBoardCell[,] m_GameBoardMatrix;
          private Point m_ClickPanelLocation = new Point();
          private int m_CurrentColInsert;
          private FormGameSettings m_FormGameSettings = new FormGameSettings();
          private int m_NumOfBoardRows;
          private int m_NumOfBoardCols;
          private string m_Player1Name;
          private string m_Player2Name;
          private GameManager m_GameManager;
          private Panel m_PanelGameBoard = new Panel();
          private bool m_AnimationInProcess = false;
          private Region m_DefaultRegion = new Region();
          private bool m_IsFirstGame = true;
          private MenuStrip menuStripGame;
          private ToolStripMenuItem toolStripMenuItemGame;
          private ToolStripMenuItem toolStripMenuItemstartNewGame;
          private ToolStripMenuItem toolStripMenuItemStartNewTournament;
          private ToolStripMenuItem toolStripMenuItemproperties;
          private ToolStripMenuItem toolStripMenuItemAbout;
          private ToolStripSeparator toolStripMenuItem1;
          private ToolStripMenuItem toolStripMenuItemexit;
          private ToolStripMenuItem toolStripMenuItemHelp;
          private ToolStripMenuItem toolStripMenuItemHowToPlay;
          private ToolStripSeparator toolStripMenuItem2;
          private StatusStrip statusStripGameInfo;
          private ToolStripStatusLabel tslStatictextCurrentPlayer;
          private ToolStripStatusLabel tslCurrentPlayer;
          private ToolStripStatusLabel tslPlayer1Name;
          private ToolStripStatusLabel tslPlayer1Score;
          private ToolStripStatusLabel tslPlayer2Name;
          private ToolStripStatusLabel tslPlayer2Score;
          private Timer timerCoinAnimation;
          private Timer timerBlinkEffects;
          private System.ComponentModel.IContainer components;

          public Form4InARow()
          {
               InitializeComponent();
               m_ButtonCoin.BackgroundImage = m_RedCoinBitMap;
               m_DefaultRegion = m_ButtonCoin.Region;
               BitmapRegionTest.BitmapRegion.CreateControlRegion(m_ButtonCoin, true);
               this.Visible = true;
               m_FormGameSettings.ShowDialog();
               this.Visible = false;
               if (m_FormGameSettings.DialogResult == DialogResult.OK)
               {
                    setBoardSizeAndPlayerNames();
                    m_GameManager = new GameManager(m_NumOfBoardRows, m_NumOfBoardCols, GameManager.eMatchType.PlayerVsPlayer);
                    m_GameManager.GameOver += new GameOverEventHandler(m_GameManager_GameOver);
                    buildGameBoard();
                    m_IsFirstGame = false;
                    setStatusBar();
                    setCoinPictureProperties();
                    setPanelProperties();
                    setDynamicFormSize();
                    this.ShowDialog();
               }
               else
               {
                    this.Close();
               }
          }

          private void setStatusBar()
          {
               tslCurrentPlayer.Text = m_Player1Name;
               tslStatictextCurrentPlayer.Text = "Current Player:";
               tslPlayer1Name.Text = string.Format("  {0}:", m_Player1Name);
               tslPlayer2Name.Text = string.Format("{0}:", m_Player2Name);
               int player1Score, player2Score;
               m_GameManager.GetPlayerScoreByIndex(0, out player1Score);
               m_GameManager.GetPlayerScoreByIndex(1, out player2Score);
               tslPlayer1Score.Text = string.Format("{0},", player1Score);
               tslPlayer2Score.Text = string.Format("{0}", player2Score);
          }

          private void m_GameManager_GameOver(bool i_GameIsWon, Point[] i_FourInARowCellIndexes)
          {
               string tieOrWinText;
               m_ButtonCoin.Visible = false;

               if (i_GameIsWon)
               {
                    if (m_GameManager.GameStatus == GameManager.eGameStatus.Player1Turn)
                    {
                         tieOrWinText = m_Player1Name;
                    }
                    else
                    {
                         tieOrWinText = m_Player2Name;
                    }

                    timerBlinkEffects.Start();
               }
               else
               {
                    tieOrWinText = "Tie";
               }

               if (MessageBox.Show(string.Format("{0}! {1}Another round?", tieOrWinText, Environment.NewLine), "4 In A row!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
               {
                    startNewGame();
                    timerBlinkEffects.Stop();
               }
               else
               {
                    this.Close();
               }
          }

          private void resetGameBoard()
          {
               m_ButtonCoin.BackgroundImage = Properties.Resources.CoinRed;
               for (int row = 0; row < m_NumOfBoardRows; row++)
               {
                    for (int col = 0; col < m_NumOfBoardCols; col++)
                    {
                         m_GameBoardMatrix[row, col].PictureBackgroundImage = Properties.Resources.EmptyCell;
                    }
               }
          }

          private void setDynamicFormSize()
          {
               int formWidth = (k_WidthSpacingForPanel * 2) + m_PanelGameBoard.Width;
               int formHeight = m_PanelGameBoard.Height + menuStripGame.Height + k_HeightSpacingForPanel + statusStripGameInfo.Height + k_SpacingBetweenPanelAndStatus;
               this.ClientSize = new Size(formWidth, formHeight);
          }

          private void setCoinPictureProperties()
          {
               m_ButtonCoin.BackgroundImage = Properties.Resources.CoinRed;
               m_ButtonCoin.Height = k_CoinPictureHeight;
               m_ButtonCoin.Width = k_CoinPictureWidth;
               m_ButtonCoin.Location = new Point(k_CoinStartingCol, k_CoinStartingRow);
          }

          private void setPanelProperties()
          {
               m_PanelGameBoard.Location = new Point(k_WidthSpacingForPanel, menuStripGame.Height + k_HeightSpacingForPanel);
               m_PanelGameBoard.Height = (m_NumOfBoardRows + 1) * k_HeightOfCellImage;
               m_PanelGameBoard.Width = m_NumOfBoardCols * k_WidthOfCellImage;
               m_PanelGameBoard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
               m_PanelGameBoard.Controls.Add(m_ButtonCoin);
               m_ButtonCoin.BringToFront();
               m_PanelGameBoard.MouseMove += new MouseEventHandler(m_PanelGameBoard_MouseMove);
               m_PanelGameBoard.MouseClick += new MouseEventHandler(m_PanelGameBoard_MouseClick);
               this.Controls.Add(m_PanelGameBoard);
          }

          /// <summary>
          /// inserting a coin if location of mouse is the top of the panel and column not full
          /// </summary>
          /// <param name="sender"></param>
          /// <param name="e"></param>
          private void m_PanelGameBoard_MouseClick(object sender, MouseEventArgs e)
          {
               if (!m_AnimationInProcess)
               {
                    if (e.Y < k_CoinPictureHeight)
                    {
                         int colToInsert = e.Location.X / k_WidthOfCellImage;

                         if (m_GameManager.LogicGameBoard.FirstAvailablePoisitionInCol[colToInsert] < m_NumOfBoardRows)
                         {
                              m_CurrentColInsert = colToInsert;
                              m_AnimationInProcess = true;
                              m_ClickPanelLocation = e.Location;
                              performAnimation(colToInsert);
                         }
                    }
               }
          }

          /// <summary>
          /// animation of coin dropping
          /// </summary>
          /// <param name="i_ColToInsert"></param>
          private void performAnimation(int i_ColToInsert)
          {
               m_ButtonCoin.SendToBack();
               int rowIndex = m_NumOfBoardRows - m_GameManager.LogicGameBoard.FirstAvailablePoisitionInCol[i_ColToInsert] - 1;
               m_CellToStop = m_GameBoardMatrix[rowIndex, i_ColToInsert];
               m_ButtonCoin.Left = (i_ColToInsert * k_WidthOfCellImage) + k_ExtraSpacingForAnimation;
               m_ButtonCoin.Top = 0;
               m_StopAnimationHeight = (m_NumOfBoardRows - m_GameManager.LogicGameBoard.FirstAvailablePoisitionInCol[i_ColToInsert] + 1) * k_HeightOfCellImage;
               m_AnimationInProcess = true;
               timerCoinAnimation.Start();
          }

          private void buildGameBoard()
          {
               if (!m_IsFirstGame)
               {
                    deleteOldGameBoard();
                    setBoardSizeAndPlayerNames();
                    setPanelProperties();
                    setDynamicFormSize();
               }

               m_GameBoardMatrix = new customControls.GameBoardCell[m_NumOfBoardRows, m_NumOfBoardCols];
               for (int row = 0; row < m_NumOfBoardRows; row++)
               {
                    for (int col = 0; col < m_NumOfBoardCols; col++)
                    {
                         customControls.GameBoardCell newCell = new customControls.GameBoardCell();
                         newCell.Location = new Point(k_WidthOfCellImage * col, k_HeightOfCellImage * (row + 1));
                         newCell.MouseMove += new MouseEventHandler(m_PanelGameBoard_MouseMove);
                         m_PanelGameBoard.Controls.Add(newCell);
                         m_GameBoardMatrix[row, col] = newCell;
                    }
               }
          }

          private void deleteOldGameBoard()
          {
               for (int row = 0; row < m_NumOfBoardRows; row++)
               {
                    for (int col = 0; col < m_NumOfBoardCols; col++)
                    {
                         m_PanelGameBoard.Controls.Remove(m_GameBoardMatrix[row, col]);
                    }
               }
          }

          private void setBoardSizeAndPlayerNames()
          {
               m_NumOfBoardCols = m_FormGameSettings.NumOfCols;
               m_NumOfBoardRows = m_FormGameSettings.NumOfRows;
               m_Player1Name = m_FormGameSettings.Player1Name;
               m_Player2Name = m_FormGameSettings.Player2Name;
          }

          private void m_PanelGameBoard_MouseMove(object sender, MouseEventArgs e)
          {
               if (!m_AnimationInProcess)
               {
                    m_ButtonCoin.Location = new Point(e.Location.X + (sender as Control).Location.X + 30, e.Location.Y + (sender as Control).Location.Y);
               }
          }

          private void timerCoinAnimation_Tick(object sender, EventArgs e)
          {
               if (m_ButtonCoin.Bottom >= m_StopAnimationHeight)
               {
                    toDoWhenAnimationStops(e);
               }
               else
               {
                    m_ButtonCoin.Top += k_AnimationMovementSpeed;
               }
          }

          private void toDoWhenAnimationStops(EventArgs e)
          {
               timerCoinAnimation.Stop();
               m_AnimationInProcess = false;
               m_ButtonCoin.BringToFront();
               m_ButtonCoin.Location = m_ClickPanelLocation;
               if (m_GameManager.GameStatus == GameManager.eGameStatus.Player1Turn)
               {
                    m_ButtonCoin.BackgroundImage = Properties.Resources.CoinYellow;
                    m_CellToStop.PictureBackgroundImage = Properties.Resources.FullCellRed;
               }
               else
               {
                    m_ButtonCoin.BackgroundImage = Properties.Resources.CoinRed;
                    m_CellToStop.PictureBackgroundImage = Properties.Resources.FullCellYellow;
               }

               m_CellToStop.PictureRegion = m_DefaultRegion;
               m_GameManager.MakeTurn(m_CurrentColInsert);
               if (m_GameManager.GameStatus == GameManager.eGameStatus.Player1Turn)
               {
                    tslCurrentPlayer.Text = m_Player1Name;
               }
               else
               {
                    tslCurrentPlayer.Text = m_Player2Name;
               }
          }

          private void startNewGame()
          {
               m_ButtonCoin.Visible = true;
               buildGameBoard();
               m_ButtonCoin.BringToFront();
               setStatusBar();
               resetGameBoard();
               m_GameManager.resetGameManager(m_NumOfBoardRows, m_NumOfBoardCols);
          }

          private void toolStripMenuItemExit_Click(object sender, EventArgs e)
          {
               this.Close();
          }

          private void toolStripMenuItemStartNewGame_Click(object sender, EventArgs e)
          {
               startNewGame();
          }

          private void toolStripMenuItemStartNewTournament_Click(object sender, EventArgs e)
          {
               m_GameManager.resetScore();
               startNewGame();
          }

          private void toolStripMenuItemProperties_Click(object sender, EventArgs e)
          {
               if (m_FormGameSettings.ShowDialog() == DialogResult.OK)
               {
                    if (MessageBox.Show("Start a new game?", "4 In A row", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                         startNewGame();
                    }
                    else
                    {
                         MessageBox.Show("New board size will take effect on the next game.", "4 In A row");
                    }
               }
          }

          private void timerBlinkEffects_Tick(object sender, EventArgs e)
          {
               m_BlinkCounter++;
               foreach (Point winningCellPoint in m_GameManager.FourInARowWinningCells)
               {
                    customControls.GameBoardCell blinkingCell = m_GameBoardMatrix[m_NumOfBoardRows - winningCellPoint.X - 1, winningCellPoint.Y];
                    if (m_GameManager.GameStatus == GameManager.eGameStatus.Player1Turn)
                    {
                         blinkingCell.BackgroundImage = Properties.Resources.FullCellRed;
                    }
                    else
                    {
                         blinkingCell.BackgroundImage = Properties.Resources.FullCellYellow;
                    }

                    if (m_BlinkCounter % 2 == 0)
                    {
                         paintBackgroundOfPicture(Color.Magenta, blinkingCell);
                    }
                    else
                    {
                         paintBackgroundOfPicture(Color.Purple, blinkingCell);
                    }
               }
          }

          private void paintBackgroundOfPicture(Color i_ColorToPaint, Control i_Control)
          {
               customControls.GameBoardCell cellToPaint = i_Control as customControls.GameBoardCell;
               Color colorToBeReplaced = (cellToPaint.PictureBackgroundImage as Bitmap).GetPixel(0, 0);

               for (int row = 0; row < cellToPaint.PictureBackgroundImage.Height; row++)
               {
                    for (int col = 0; col < cellToPaint.PictureBackgroundImage.Width; col++)
                    {
                         if ((cellToPaint.PictureBackgroundImage as Bitmap).GetPixel(row, col) == colorToBeReplaced)
                         {
                              (cellToPaint.PictureBackgroundImage as Bitmap).SetPixel(row, col, i_ColorToPaint);
                         }
                    }
               }
          }

          private void toolStripMenuItemAbout_Click(object sender, EventArgs e)
          {
               FormAbout formAbout = new FormAbout();
               formAbout.ShowDialog();
          }

          private void InitializeComponent()
          {
               this.components = new System.ComponentModel.Container();
               this.menuStripGame = new System.Windows.Forms.MenuStrip();
               this.toolStripMenuItemGame = new System.Windows.Forms.ToolStripMenuItem();
               this.toolStripMenuItemstartNewGame = new System.Windows.Forms.ToolStripMenuItem();
               this.toolStripMenuItemStartNewTournament = new System.Windows.Forms.ToolStripMenuItem();
               this.toolStripMenuItemproperties = new System.Windows.Forms.ToolStripMenuItem();
               this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
               this.toolStripMenuItemexit = new System.Windows.Forms.ToolStripMenuItem();
               this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
               this.toolStripMenuItemHowToPlay = new System.Windows.Forms.ToolStripMenuItem();
               this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
               this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
               this.statusStripGameInfo = new System.Windows.Forms.StatusStrip();
               this.tslStatictextCurrentPlayer = new System.Windows.Forms.ToolStripStatusLabel();
               this.tslCurrentPlayer = new System.Windows.Forms.ToolStripStatusLabel();
               this.tslPlayer1Name = new System.Windows.Forms.ToolStripStatusLabel();
               this.tslPlayer1Score = new System.Windows.Forms.ToolStripStatusLabel();
               this.tslPlayer2Name = new System.Windows.Forms.ToolStripStatusLabel();
               this.tslPlayer2Score = new System.Windows.Forms.ToolStripStatusLabel();
               this.timerCoinAnimation = new System.Windows.Forms.Timer(this.components);
               this.timerBlinkEffects = new System.Windows.Forms.Timer(this.components);
               this.menuStripGame.SuspendLayout();
               this.statusStripGameInfo.SuspendLayout();
               this.SuspendLayout();
               // 
               // menuStripGame
               // 
               this.menuStripGame.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
               this.menuStripGame.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemGame,
            this.toolStripMenuItemHelp});
               this.menuStripGame.Location = new System.Drawing.Point(0, 0);
               this.menuStripGame.Name = "menuStripGame";
               this.menuStripGame.Size = new System.Drawing.Size(488, 24);
               this.menuStripGame.TabIndex = 0;
               this.menuStripGame.Text = "menuStrip1";
               // 
               // toolStripMenuItemGame
               // 
               this.toolStripMenuItemGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemstartNewGame,
            this.toolStripMenuItemStartNewTournament,
            this.toolStripMenuItemproperties,
            this.toolStripMenuItem1,
            this.toolStripMenuItemexit});
               this.toolStripMenuItemGame.Name = "toolStripMenuItemGame";
               this.toolStripMenuItemGame.Size = new System.Drawing.Size(50, 20);
               this.toolStripMenuItemGame.Text = "Game";
               // 
               // toolStripMenuItemstartNewGame
               // 
               this.toolStripMenuItemstartNewGame.Name = "toolStripMenuItemstartNewGame";
               this.toolStripMenuItemstartNewGame.Size = new System.Drawing.Size(194, 22);
               this.toolStripMenuItemstartNewGame.Text = "Start New Game";
               this.toolStripMenuItemstartNewGame.Click += new System.EventHandler(this.toolStripMenuItemStartNewGame_Click);
               // 
               // toolStripMenuItemStartNewTournament
               // 
               this.toolStripMenuItemStartNewTournament.Name = "toolStripMenuItemStartNewTournament";
               this.toolStripMenuItemStartNewTournament.Size = new System.Drawing.Size(194, 22);
               this.toolStripMenuItemStartNewTournament.Text = "Start New Tournament";
               this.toolStripMenuItemStartNewTournament.Click += new System.EventHandler(this.toolStripMenuItemStartNewTournament_Click);
               // 
               // toolStripMenuItemproperties
               // 
               this.toolStripMenuItemproperties.Name = "toolStripMenuItemproperties";
               this.toolStripMenuItemproperties.Size = new System.Drawing.Size(194, 22);
               this.toolStripMenuItemproperties.Text = "Properties...";
               this.toolStripMenuItemproperties.Click += new System.EventHandler(this.toolStripMenuItemProperties_Click);
               // 
               // toolStripMenuItem1
               // 
               this.toolStripMenuItem1.Name = "toolStripMenuItem1";
               this.toolStripMenuItem1.Size = new System.Drawing.Size(191, 6);
               // 
               // toolStripMenuItemexit
               // 
               this.toolStripMenuItemexit.Name = "toolStripMenuItemexit";
               this.toolStripMenuItemexit.Size = new System.Drawing.Size(194, 22);
               this.toolStripMenuItemexit.Text = "Exit";
               this.toolStripMenuItemexit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
               // 
               // toolStripMenuItemHelp
               // 
               this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemHowToPlay,
            this.toolStripMenuItem2,
            this.toolStripMenuItemAbout});
               this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
               this.toolStripMenuItemHelp.Size = new System.Drawing.Size(44, 20);
               this.toolStripMenuItemHelp.Text = "Help";
               // 
               // toolStripMenuItemHowToPlay
               // 
               this.toolStripMenuItemHowToPlay.Name = "toolStripMenuItemHowToPlay";
               this.toolStripMenuItemHowToPlay.Size = new System.Drawing.Size(146, 22);
               this.toolStripMenuItemHowToPlay.Text = "How To Play?";
               this.toolStripMenuItemHowToPlay.Click += new System.EventHandler(this.toolStripMenuItemHowToPlay_Click);
               // 
               // toolStripMenuItem2
               // 
               this.toolStripMenuItem2.Name = "toolStripMenuItem2";
               this.toolStripMenuItem2.Size = new System.Drawing.Size(143, 6);
               // 
               // toolStripMenuItemAbout
               // 
               this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
               this.toolStripMenuItemAbout.Size = new System.Drawing.Size(146, 22);
               this.toolStripMenuItemAbout.Text = "About";
               this.toolStripMenuItemAbout.Click += new System.EventHandler(this.toolStripMenuItemAbout_Click);
               // 
               // statusStripGameInfo
               // 
               this.statusStripGameInfo.AutoSize = false;
               this.statusStripGameInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatictextCurrentPlayer,
            this.tslCurrentPlayer,
            this.tslPlayer1Name,
            this.tslPlayer1Score,
            this.tslPlayer2Name,
            this.tslPlayer2Score});
               this.statusStripGameInfo.Location = new System.Drawing.Point(0, 382);
               this.statusStripGameInfo.Name = "statusStripGameInfo";
               this.statusStripGameInfo.Size = new System.Drawing.Size(488, 22);
               this.statusStripGameInfo.SizingGrip = false;
               this.statusStripGameInfo.TabIndex = 3;
               this.statusStripGameInfo.Text = "statusStrip1";
               // 
               // tslStatictextCurrentPlayer
               // 
               this.tslStatictextCurrentPlayer.Name = "tslStatictextCurrentPlayer";
               this.tslStatictextCurrentPlayer.Size = new System.Drawing.Size(0, 17);
               // 
               // tslCurrentPlayer
               // 
               this.tslCurrentPlayer.Name = "tslCurrentPlayer";
               this.tslCurrentPlayer.Size = new System.Drawing.Size(0, 17);
               // 
               // tslPlayer1Name
               // 
               this.tslPlayer1Name.Name = "tslPlayer1Name";
               this.tslPlayer1Name.Size = new System.Drawing.Size(0, 17);
               // 
               // tslPlayer1Score
               // 
               this.tslPlayer1Score.Name = "tslPlayer1Score";
               this.tslPlayer1Score.Size = new System.Drawing.Size(0, 17);
               // 
               // tslPlayer2Name
               // 
               this.tslPlayer2Name.Name = "tslPlayer2Name";
               this.tslPlayer2Name.Size = new System.Drawing.Size(0, 17);
               // 
               // tslPlayer2Score
               // 
               this.tslPlayer2Score.Name = "tslPlayer2Score";
               this.tslPlayer2Score.Size = new System.Drawing.Size(0, 17);
               // 
               // timerCoinAnimation
               // 
               this.timerCoinAnimation.Interval = 10;
               this.timerCoinAnimation.Tick += new System.EventHandler(this.timerCoinAnimation_Tick);
               // 
               // timerBlinkEffects
               // 
               this.timerBlinkEffects.Interval = 200;
               this.timerBlinkEffects.Tick += new System.EventHandler(this.timerBlinkEffects_Tick);
               // 
               // Form4InARow
               // 
               this.ClientSize = new System.Drawing.Size(488, 404);
               this.Controls.Add(this.statusStripGameInfo);
               this.Controls.Add(this.menuStripGame);
               this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
               this.MainMenuStrip = this.menuStripGame;
               this.MaximizeBox = false;
               this.Name = "Form4InARow";
               this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
               this.Text = "4 In A row!";
               this.menuStripGame.ResumeLayout(false);
               this.menuStripGame.PerformLayout();
               this.statusStripGameInfo.ResumeLayout(false);
               this.statusStripGameInfo.PerformLayout();
               this.ResumeLayout(false);
               this.PerformLayout();
          }

          private void toolStripMenuItemHowToPlay_Click(object sender, EventArgs e)
          {
               FormHelp formHelp = new FormHelp();
               formHelp.ShowDialog();
          }
     }
}