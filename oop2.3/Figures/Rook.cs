using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using oop2._3.Interfaces;
using oop2._3.Exceptions;

namespace oop2._3.Figures
{
    class Rook : IFigure
    {
        private string color;
        private int identifer;

        public string GetColor()
        {
            return color;
        }

        public int GetIdentifer()
        {
            return identifer;
        }

        public int GetWeight()
        {
            return 5;
        }

        public Image GetImage()
        {
            return new Bitmap(@"Drawing\" + color + ToString() + ".png");
        }

        public Rook(string color, int identifer)
        {
            this.color = color;
            this.identifer = identifer;
        }

        override public string ToString()
        {
            return "Rook";
        }

        public List<int[]> PosibleMoves(int[] position, IFigure[,] board)
        {
            List<int[]> posibleMoves = new List<int[]>();
            for (int i = position[0] + 1; i <= 7; i++)
            {
                if (!FigureAdder(board, i, position[1], ref posibleMoves))
                {
                    break;
                }
            }
            for (int i = position[0] - 1; i >= 0; i--)
            {
                if (!FigureAdder(board, i, position[1], ref posibleMoves))
                {
                    break;
                }
            }
            for (int i = position[1] + 1; i <= 7; i++)
            {
                if (!FigureAdder(board, position[0], i, ref posibleMoves))
                {
                    break;
                }
            }
            for (int i = position[1] - 1; i >= 0; i--)
            {
                if (!FigureAdder(board, position[0], i, ref posibleMoves))
                {
                    break;
                }
            }
            return posibleMoves;
        }

        private bool FigureAdder(IFigure[,] board, int x, int y, ref List<int[]> posibleMoves)
        {
            if (board[x, y] == null)
            {
                posibleMoves.Add(new int[3] { x, y, identifer });
                return true;
            }
            if (board[x, y].GetColor() != this.GetColor())
            {
                posibleMoves.Add(new int[3] { x, y, identifer });
                return false;
            }
            if (board[x, y].GetColor() == this.GetColor())
            {
                return false;
            }
            throw new ChessException("Rook.FigureAdder exception");
        }
    }
}
