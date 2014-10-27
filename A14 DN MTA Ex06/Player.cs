// -----------------------------------------------------------------------
// <copyright file="Player.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace A14_DN_MTA_Ex06
{
     using System;
     using System.Collections;
     using System.Collections.Generic;

     public class Player
     {
          private int m_Score;

          /// <summary>
          /// the computer move algorithm:
          /// check for a winning move
          /// if exist - perform
          /// else - check for non losing moves
          /// if exist - choose a random non-losing move.  else - choose a random move
          /// </summary>
          /// <param name="i_GameManager"></param>
          public static void MakeComputerMove(GameManager i_GameManager)
          {
               int winningMoveIndex;
               int randMove;
               bool isGameOver;
               Random rand = new Random();
               List<int> legalMovesArray;
               List<int> legalNonLosingMovesArray = new List<int>();

               if (GameLogic.WinningMoveExists(i_GameManager, out winningMoveIndex))
               {
                    i_GameManager.PerformMoveChanges(winningMoveIndex, out isGameOver);
               }
               else
               {
                    legalNonLosingMovesArray = generateNonLosingMoveArray(i_GameManager, out legalMovesArray);
                    if (legalNonLosingMovesArray.Count > 0)
                    {
                         randMove = legalNonLosingMovesArray[rand.Next(legalNonLosingMovesArray.Count)];
                         i_GameManager.PerformMoveChanges(randMove, out isGameOver);
                    }
                    else
                    {
                         randMove = legalMovesArray[rand.Next(legalNonLosingMovesArray.Count)];
                         i_GameManager.PerformMoveChanges(randMove, out isGameOver);
                    }
               }
          }

          /// <summary>
          /// makes a human move, and in case of PlayerVsComputer makes the computer move
          /// right after in case game is not over which will be indicated by the variable isGameOver
          /// </summary>
          /// <param name="i_GameManager"></param>
          /// <param name="i_ColNumber"></param>          
          public static void MakeHumanMove(GameManager i_GameManager, int i_ColNumber)
          {
               bool isGameOver;
               i_GameManager.PerformMoveChanges(i_ColNumber, out isGameOver);
               if (!isGameOver && i_GameManager.MatchType == GameManager.eMatchType.PlayerVsComputer)
               {
                    MakeComputerMove(i_GameManager);
               }
          }

          public Player()
          {
               m_Score = 0;
          }

          public int Score
          {
               get { return m_Score; }
               set { m_Score = value; }
          }

          public void IncreaseScore()
          {
               this.m_Score++;
          }

          public enum ePlayerType
          {
               HumanPlayer,
               CoumputerPlayer,
          }

          /// <summary>
          /// generating legal moves array and a non-losing moves array by the following algorith:
          /// if a move is legal - enter it to legal moves array and check if there is a winning move for the opponent.
          /// if there is no winning response add it to the non-losing moves array
          /// </summary>
          /// <param name="i_GameManager"></param>
          /// <param name="o_LegalMovesArray"></param>
          /// <returns></returns>
          private static List<int> generateNonLosingMoveArray(GameManager i_GameManager, out List<int> o_LegalMovesArray)
          {
               o_LegalMovesArray = new List<int>();
               List<int> legalNonLosingMovesArray = new List<int>();
               int winningMoveIndex;

               for (int col = 0; col < i_GameManager.LogicGameBoard.ColSize; col++)
               {
                    if (GameLogic.IsLegalMove(col, i_GameManager.LogicGameBoard))
                    {
                         o_LegalMovesArray.Add(col);
                         i_GameManager.InsertMoveToGameBoard(col);
                         i_GameManager.ChangePlayerTurn();
                         if (!GameLogic.WinningMoveExists(i_GameManager, out winningMoveIndex))
                         {
                              legalNonLosingMovesArray.Add(col);
                         }

                         i_GameManager.ChangePlayerTurn();
                         i_GameManager.EraseMoveFromGameBoard(col);
                    }
               }

               return legalNonLosingMovesArray;
          }
     }
}