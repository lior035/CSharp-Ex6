namespace A14_DN_MTA_Ex06
{
     using System.Collections;
     using System.Collections.Generic;
     using System.Drawing;

     public class GameLogic
     {
          private static readonly int sr_WinningStreakSize = 4;
          private static readonly int sr_LineRadiusToCheck = 3;
          private static BoardEnumAndIndex[] m_FourInARowIndexes = new BoardEnumAndIndex[4];

          public static Point WinningCellByIndex(int i_Index)
          {
               Point result;
               if (i_Index < 0 || i_Index > 4)
               {
                    result = Point.Empty;
               }
               else
               {
                    result = m_FourInARowIndexes[i_Index].Index;
               }

               return result;
          }

          private class BoardEnumAndIndex
          {
               private Board.eBoardCellTypes m_CellType = new Board.eBoardCellTypes();
               private Point m_CellIndex = new Point();

               public Board.eBoardCellTypes CellType
               {
                    get { return m_CellType; }
                    set { m_CellType = value; }
               }

               public Point Index
               {
                    get { return m_CellIndex; }
                    set { m_CellIndex = value; }
               }                              
          }

          /// <summary>
          /// this function checks whether there is a winning four a line
          /// </summary>
          /// <param name="i_LineOfEnums">generated for 4 directions to check 4 in a line streak</param>
          /// <param name="i_CurrCellEnum">type of cell according to player</param>
          /// <returns></returns>
          private static bool isFourInLine(BoardEnumAndIndex[] i_LineOfEnums, Board.eBoardCellTypes i_CurrCellEnum)
          {
               bool fourInLineFound = false;
               int streakOfSameElement = 0;
               bool streakInProgress = false;
               int streakBeginning = 0;

               for (int i = 0; i < (sr_LineRadiusToCheck * 2) + 1; i++)
               {
                    if (!streakInProgress)
                    {
                         streakBeginning = i;
                    }

                    if (i_CurrCellEnum.Equals(i_LineOfEnums[i].CellType))
                    {
                         if (!streakInProgress)
                         {
                              streakInProgress = true;                              
                         }

                         streakOfSameElement++;
                         if (streakOfSameElement == sr_WinningStreakSize)
                         {
                              fourInLineFound = true;
                              for (int j = 0; j < 4; j++)
                              {
                                   m_FourInARowIndexes[j] = i_LineOfEnums[j + streakBeginning];
                              }

                              break;
                         }
                    }
                    else
                    {
                         streakInProgress = false;
                         streakOfSameElement = 0;
                    }
               }
               
               return fourInLineFound;
          }

          /// <summary>
          /// this function checks whether the move performed is a winning move
          /// it checks 4 in a row for 4 directions horizontal/vertical/right-cross/left-cross
          /// </summary>
          /// <param name="i_CurrentInsertCellCol">col of move</param>
          /// <param name="i_GameBoard"></param>
          /// <returns></returns>
          private static bool isWinningMove(int i_CurrentInsertCellCol, Board i_GameBoard)
          {
               int currentInsertCellRow = i_GameBoard.FirstAvailablePoisitionInCol[i_CurrentInsertCellCol] - 1;
               Board.eBoardCellTypes currentInsertCellEnum = i_GameBoard.GetCellEnum(i_GameBoard.RowSize - 1 - currentInsertCellRow, i_CurrentInsertCellCol);
               bool result = isFourInLine(generateLineOfCellEnumsFromBoardByDirection(i_GameBoard, Board.eBoardDirections.Horizontal, i_CurrentInsertCellCol), currentInsertCellEnum)
                          || isFourInLine(generateLineOfCellEnumsFromBoardByDirection(i_GameBoard, Board.eBoardDirections.Vertical, i_CurrentInsertCellCol), currentInsertCellEnum)
                          || isFourInLine(generateLineOfCellEnumsFromBoardByDirection(i_GameBoard, Board.eBoardDirections.RightCross, i_CurrentInsertCellCol), currentInsertCellEnum)
                          || isFourInLine(generateLineOfCellEnumsFromBoardByDirection(i_GameBoard, Board.eBoardDirections.LeftCross, i_CurrentInsertCellCol), currentInsertCellEnum);

               return result;
          }

          /// <summary>
          /// aux function for 4 in a row detection
          /// generating a row of board enums according to direction input
          /// </summary>
          /// <param name="i_GameBoard"></param>
          /// <param name="i_Direction"></param>
          /// <param name="i_CurrentInsertCellCol"></param>
          /// <returns></returns>
          private static BoardEnumAndIndex[] generateLineOfCellEnumsFromBoardByDirection(Board i_GameBoard, Board.eBoardDirections i_Direction, int i_CurrentInsertCellCol)
          {
               BoardEnumAndIndex[] result = new BoardEnumAndIndex[(sr_LineRadiusToCheck * 2) + 1];
               int rowSize = i_GameBoard.RowSize;
               int currentInsertCellRow = i_GameBoard.FirstAvailablePoisitionInCol[i_CurrentInsertCellCol] - 1;
               int startOfLineRow, startOfLineCol, rowDirection, colDirection;
               
               for (int j = 0; j < 7; j++)
               {
                    result[j] = new BoardEnumAndIndex();
               }

               for (int i = 0; i < result.Length; i++)
               {
                    result[i].CellType = Board.eBoardCellTypes.Empty;                   
               }

               switch (i_Direction)
               {
                    case Board.eBoardDirections.Horizontal:
                         startOfLineCol = i_CurrentInsertCellCol - 3;
                         startOfLineRow = currentInsertCellRow;
                         rowDirection = 0;
                         colDirection = 1;
                         break;
                    case Board.eBoardDirections.Vertical:
                         startOfLineCol = i_CurrentInsertCellCol;
                         startOfLineRow = currentInsertCellRow - 3;
                         rowDirection = 1;
                         colDirection = 0;
                         break;
                    case Board.eBoardDirections.RightCross:
                         startOfLineCol = i_CurrentInsertCellCol - 3;
                         startOfLineRow = currentInsertCellRow - 3;
                         rowDirection = 1;
                         colDirection = 1;
                         break;
                    case Board.eBoardDirections.LeftCross:
                         startOfLineCol = i_CurrentInsertCellCol - 3;
                         startOfLineRow = currentInsertCellRow + 3;
                         rowDirection = -1;
                         colDirection = 1;
                         break;
                    default:
                         startOfLineRow = startOfLineCol = rowDirection = colDirection = 0;
                         break;
               }

               for (int i = 0; i < (sr_LineRadiusToCheck * 2) + 1; i++)
               {
                    int currentCellToCheckRow = startOfLineRow + (i * rowDirection);
                    int currentCellToCheckCol = startOfLineCol + (i * colDirection);
                    if (i_GameBoard.CheckIsInsideBoardBoundaries(currentCellToCheckRow, currentCellToCheckCol))
                    {
                         result[i].CellType = i_GameBoard.GetCellEnum(rowSize - 1 - currentCellToCheckRow, currentCellToCheckCol);
                         result[i].Index = new Point(currentCellToCheckRow, currentCellToCheckCol);
                    }
               }

               return result;
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="i_ColInput"></param>
          /// <param name="i_GameBoard"></param>
          /// <returns>returns true if move is performed within column boundaries and in a non-full column</returns>
          public static bool IsLegalMove(int i_ColInput, Board i_GameBoard)
          {
               bool result;

               if (i_ColInput < 0 || i_ColInput > i_GameBoard.ColSize - 1)
               {
                    result = false;
               }
               else
               {
                    int indexOfFirstAvailableRowInInputCol = i_GameBoard.FirstAvailablePoisitionInCol[i_ColInput];
                    result = indexOfFirstAvailableRowInInputCol < i_GameBoard.RowSize;
               }

               return result;
          }

          /// <summary>
          /// 
          /// </summary>
          /// <param name="i_ColInput"></param>
          /// <param name="i_GameBoard"></param>
          /// <param name="o_GameIsWon"></param>
          /// <returns>returns true is win or tie occur</returns>
          public static bool IsGameOver(int i_ColInput, Board i_GameBoard, out bool o_GameIsWon)
          {
               bool result;

               o_GameIsWon = isWinningMove(i_ColInput, i_GameBoard);
               result = o_GameIsWon || i_GameBoard.IsBoardFull();

               return result;
          }

          /// <summary>
          /// part of the computer move algorithm
          /// it checks the response of the opponent, checking if there is a possible win after move performed
          /// </summary>
          /// <param name="i_GameManager"></param>
          /// <param name="o_WinningMoveIndex"></param>
          /// <returns></returns>
          public static bool WinningMoveExists(GameManager i_GameManager, out int o_WinningMoveIndex)
          {
               bool result = false;               
               o_WinningMoveIndex = 0;

               for (int col = 0; col < i_GameManager.LogicGameBoard.ColSize; col++)
               {
                    if (GameLogic.IsLegalMove(col, i_GameManager.LogicGameBoard))
                    {
                         i_GameManager.InsertMoveToGameBoard(col);
                         if (GameLogic.isWinningMove(col, i_GameManager.LogicGameBoard))
                         {
                              result = true;
                              o_WinningMoveIndex = col;
                         }

                         i_GameManager.EraseMoveFromGameBoard(col);
                         if (result)
                         {
                              break;
                         }
                    }
               }

               return result;
          }
     }
}