using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public enum State {Empty = 0, First = 1, Second = 2, Draw = 3};
    public class Board
    {
        public class Tile
        {
            public int value { get; private set; }
            public State state { get; private set; }

            public Tile()
            {
                value = 0;
                state = State.Empty;
            }
            
            public bool Add(State s)
            {
                if (value < 4)
                {
                    value++;
                    if (value == 4)
                    {
                        state = s;
                        return true;
                    }
                }
                return false;
            }

            public void Refrash()
            {
                value = 0;
                state = State.Empty;
            }
        }
        
        public int Height { get; private set; }
        public int Width { get; private set; }
        public Tile[,] board { get; private set; }
        public State CurPlayer { get; private set; }
        public State Winner { get; private set; }

        public Board(int h, int w)
        {
            Height = h;
            Width = w;
            board = new Tile[Height, Width];

            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    board[i, j] = new Tile();

            CurPlayer = State.First;
            Winner = State.Empty;
        }

        public void Reset()
        {
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    board[i, j] = new Tile();
            CurPlayer = State.First;
            Winner = State.Empty;
        }


        public State GetFieldState(int x, int y)
        {
            return board[x, y].state;
        }

        public bool CheckFiled(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Height && y < Width;
        }

        public void ChangePlayer()
        {
            CurPlayer = CurPlayer == State.First ? State.Second : State.First;
        }

        public State TestVictory()
        {
            int firstCount = 0;
            int secondCount = 0;
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                {
                    if (board[i, j].state == State.Empty)
                        return State.Empty;
                    if (board[i, j].state == State.First)
                        firstCount++;
                    if (board[i, j].state == State.Second)
                        secondCount++;
                }
            if (firstCount > secondCount)
                return State.First;
            if (firstCount < secondCount)
                return State.Second;
            if (firstCount == secondCount && firstCount != 0 && secondCount != 0)
                return State.Draw;
            return State.Empty;
        }

        public bool MakeTurn(int x, int y, bool isHor)
        {
            bool change = false;
            bool gainPoint = false;
            if (isHor)
            {
                if (CheckFiled(x - 1, y))
                {
                    change = true;
                    if (board[x - 1, y].Add(CurPlayer))
                        gainPoint = true;
                }
                if (CheckFiled(x, y))
                {
                    change = true;
                    if (board[x, y].Add(CurPlayer))
                        gainPoint = true;
                }
            }
            else
            {
                if (CheckFiled(x, y - 1))
                {
                    change = true;
                    if (board[x, y - 1].Add(CurPlayer))
                        gainPoint = true;
                }
                if (CheckFiled(x, y))
                {
                    change = true;
                    if (board[x, y].Add(CurPlayer))
                        gainPoint = true;
                }
            }
            if (change && !gainPoint)
            {
                ChangePlayer();
            }
            Winner = TestVictory();
            return change && !gainPoint;
        }
    }
}
