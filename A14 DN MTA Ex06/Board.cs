namespace A14_DN_MTA_Ex06
{
     using System;
     using System.Text;

     public class Board
     {
          private eBoardCellTypes[,] m_GameBoard;
          private int[] m_FirstAvailablePoisitionInCol;
          private int m_NumOfOccupiedCells;          
          private int m_ColSize;
          private int m_RowSize;

          public static char GetCharOfCell(eBoardCellTypes i_CellType)
          {
               char result;

               if (i_CellType == eBoardCellTypes.Circle)
               {
                    result = 'O';
               }
               else if (i_CellType == eBoardCellTypes.X)
               {
                    result = 'X';
               }
               else
               {
                    result = ' ';
               }

               return result;
          }

          public Board(int i_RowSize, int i_ColSize)
          {
               m_RowSize = i_RowSize;
               m_ColSize = i_ColSize;
               m_GameBoard = new eBoardCellTypes[i_RowSize, i_ColSize];
               m_FirstAvailablePoisitionInCol = new int[i_ColSize];               
               InitializeBoard(i_RowSize, i_ColSize);
               ResetBoard(i_RowSize, i_ColSize);
          }

          /// <summary>
          /// resetting board for default values
          /// </summary>
          /// <param name="i_RowSize"></param>
          /// <param name="i_ColSize"></param>
          public void ResetBoard(int i_RowSize, int i_ColSize)
          {
               m_NumOfOccupiedCells = 0;
               for (int i = 0; i < i_ColSize; i++)
               {
                    m_FirstAvailablePoisitionInCol[i] = 0;
               }

               for (int row = 0; row < i_RowSize; row++)
               {
                    for (int col = 0; col < i_ColSize; col++)
                    {
                         m_GameBoard[row, col] = eBoardCellTypes.Empty;
                    }
               }
          }

          public void InitializeBoard(int i_RowSize, int i_ColSize)
          {
               for (int row = 0; row < i_RowSize; row++)
               {
                    for (int col = 0; col < i_ColSize; col++)
                    {
                         m_GameBoard[row, col] = eBoardCellTypes.Empty;
                    }
               }
          }

          public enum eBoardCellTypes
          {
               Circle,
               Empty,
               X,
          }

          public enum eBoardDirections
          {
               Horizontal,
               Vertical,
               RightCross,
               LeftCross,
          }

          public eBoardCellTypes GetCellEnum(int i_Row, int i_Col)
          {
               return this.m_GameBoard[i_Row, i_Col];
          }

          public eBoardCellTypes[,] GameBoard
          {
               get { return m_GameBoard; }
          }

          public int RowSize
          {
               get { return m_RowSize; }
          }

          public int ColSize
          {
               get { return m_ColSize; }
          }

          public int[] FirstAvailablePoisitionInCol
          {
               get { return m_FirstAvailablePoisitionInCol; }
          }

          public void IncreaseOccupiedCells()
          {
               this.m_NumOfOccupiedCells++;
          }

          public int NumOfOccupiedCells
          {
               get { return m_NumOfOccupiedCells; }
          }
      
          public bool IsBoardFull()
          {
               return this.m_NumOfOccupiedCells == m_ColSize * m_RowSize;
          }

          public bool CheckIsInsideBoardBoundaries(int i_Row, int i_Col)
          {
               return !((i_Row < 0) || (i_Row > m_RowSize - 1) || (i_Col < 0) || (i_Col > m_ColSize - 1));
          }
     }
}
