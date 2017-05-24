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
    class Pawn : IFigure
    {
        private string color;
        private int identifer;
        private bool isMoved;

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
            return 1;
        }

        public Image GetImage()
        {
            return new Bitmap(@"Drawing\" + color + ToString() + ".png");
        }

        public Pawn(string color, int identifer)
        {
            this.color = color;
            this.identifer = identifer;
            isMoved = false;
        }

        override public string ToString()
        {
            return "Pawn";
        }

        public List<int[]> PosibleMoves(int[] position, IFigure[,] board)
        {
            List<int[]> posibleMoves = new List<int[]>();
            int[] j;
            if (this.GetColor() == "White")
            {
                j = new int[] { position[1] + 1, position[1] + 2 };
            }
            else
            {
                j = new int[] { position[1] - 1, position[1] - 2 };
            }
            if (FigureAdder(board, position[0], j[0], ref posibleMoves) && !isMoved)
            {
                FigureAdder(board, position[0], j[1], ref posibleMoves);
            }
            if (board[position[0] - 1, j[0]].GetColor() != this.GetColor())
            {
                posibleMoves.Add(new int[3] { position[0] - 1, j[0], identifer});
            }
            if (board[position[0] + 1, j[0]].GetColor() != this.GetColor())
            {
                posibleMoves.Add(new int[3] { position[0] + 1, j[0] , identifer});
            }
            return posibleMoves;
        }

        private bool FigureAdder(IFigure[,] board, int x, int y, ref List<int[]> posibleMoves)
        {
            if (board[x, y] == null)
            {
                posibleMoves.Add(new int[3] { x, y, identifer});
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
            throw new ChessException("Pawn.FigureAdder exception");
        }
    }
}
