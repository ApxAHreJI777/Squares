using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class Game
    {
        public Board gameBoard;
        public Player player1, player2;
        public State Winner { get; private set; }
        public Game(Player p1, Player p2, Board b)
        {
            this.player1 = p1;
            this.player2 = p2;
            this.gameBoard = b;
            Winner = State.Empty;
        }

        public bool NextTurn()
        {
            Field move;
            if (gameBoard.CurPlayer == State.First)
                move = this.player1.Play(); 
            else
                move = this.player2.Play();
            if (move.X != -1 && move.Y != -1)
                gameBoard.MakeTurn(move.X, move.Y, move.H);
            this.Winner = gameBoard.TestVictory();
            if (move.X == -100)
                this.Winner = State.Draw;
            return this.Winner != State.Empty;
        }

        public void Reset()
        {
            Winner = State.Empty;
            gameBoard.Reset();
        }

        public void changePlayer(Player p, int n)
        {
            if (n == 1)
                player1 = p;
            else
                player2 = p;
        }
    }
}
