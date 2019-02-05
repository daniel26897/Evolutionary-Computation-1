﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Game
    {
        private Board board;
        private const int DRAW = -1;
        private const int START_WIN = 1;
        private const int SECOND_WIN = 2;
        private const int INCOMPLETE_TREE = 0;

        public Game(Board board)
        {
            this.board = board;
        }

        public void setBoard(Board board)
        {
            this.board = board;
        }

        public Board getBoard()
        {
            return board;
        }
        public void playTwoSetMatch(Individual player1, Individual player2)
        {
            /*
             *  play a two game match, each time another player get to start
             */
            int result=0;
            // set both players the same board instance
            player1.setBoard(board);
            player2.setBoard(board);

            // make two iteration, each time let another player start (change the order of parameters in playGame method)
            board.resetBoard();
            result += playGame(player1, player2);
            board.resetBoard();
            result += playGame(player2, player1);

        }
        public int playGame(Individual startingPlayer, Individual secondPlayer)
        {
            int result = -1;

            // set the starting player value to 1 ('X')
            startingPlayer.setValue(1);
            // set the second player value to 2 ('O')
            secondPlayer.setValue(2);
            while (true)
            {

                // starting player code block
                //////////////////////////////////////
                // can make either a random move or a move using the strategy tree
                if (!startingPlayer.makeStrategyMove())
                {
                    // an attempt to make a move on an occupied space was made
                    // abort the game
                    Console.WriteLine("Move " + startingPlayer.getValue() + " Failed");
                    Console.WriteLine("SHUTTING GAME DOWN: OBSOLETE GARBAGE");
                    break;
                }
                // check if the starting player move resulted in a win
                result = board.checkWin(startingPlayer);
                if (result != 0)
                    break;
                //////////////////////////////////////
                // starting player code block end

                // second player code block
                //////////////////////////////////////
                if (!secondPlayer.makeStrategyMove())
                {
                    Console.WriteLine("Move " + secondPlayer.getValue() + " Failed");
                    Console.WriteLine("SHUTTING GAME DOWN: OBSOLETE GARBAGE");
                    break;
                }

                result = board.checkWin(secondPlayer);
                if (result != 0)
                    break;
                //////////////////////////////////////
            }
            // add the result accordingly
            switch (result)
            {
                case DRAW:
                    startingPlayer.addDraw(true);
                    secondPlayer.addDraw(false);
                    break;
                case START_WIN:
                    startingPlayer.addWin(true);
                    secondPlayer.addLoss(false);
                    break;
                case SECOND_WIN:
                    secondPlayer.addWin(false);
                    startingPlayer.addLoss(true);
                    break;
                case INCOMPLETE_TREE:
                    Console.WriteLine("Incomplete Tree Error");
                    break;
                default:
                    Console.WriteLine("CATASTROPHIC FAILURE!!!");
                    break;
            }
            return result;
        }

        public void printBoard()
        {
            board.printBoard();
        }

        public void resetBoard()
        {
            board.resetBoard();
        }
    }
}
