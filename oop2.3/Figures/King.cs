using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using oop2._3.Interfaces;

namespace oop2._3.Figures
{
    class King : IFigure
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
            return 100;
        }

        public Image GetImage()
        {
            return new Bitmap(@"Drawing\" + color + ToString() + ".png");
        }

        public King(string color, int identifer)
        {
            this.color = color;
            this.identifer = identifer;
        }

        override public string ToString()
        {
            return "King";
        }

        public List<int[]> PosibleMoves(int[] position, IFigure[,] board)
        {
            List<int[]> posibleMoves = new List<int[]>();
            for (int i = position[0] - 1; i < position[1] + 1; i++)
            {
                for (int j = position[1] - 1; j < position[1] + 1; j++)
                {
                    if (board[i, j] == null)
                    {
                        posibleMoves.Add(new int[3] { i, j, identifer});
                        continue;
                    }
                    if (board[i,j].GetColor() != this.GetColor())
                    {
                        posibleMoves.Add(new int[3] { i, j, identifer });
                    }
                }
            }
            return posibleMoves;
        }
    }
}