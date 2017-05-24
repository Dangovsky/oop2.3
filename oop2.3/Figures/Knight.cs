using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using oop2._3.Interfaces;

namespace oop2._3.Figures
{
    class Knight : IFigure
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
            return 3;
        }

        public Image GetImage()
        {
            return new Bitmap(@"Drawing\" + color + ToString() + ".png");
        }

        public Knight(string color, int identifer)
        {
            this.color = color;
            this.identifer = identifer;
        }

        override public string ToString()
        {
            return "Knight";
        }

        public List<int[]> PosibleMoves(int[] position, IFigure[,] board)
        {
            List<int[]> posibleMoves = new List<int[]>();
            int[] i = new int[] { -2, -2, -1, -1, +1, +1, +2, +2 };
            int[] j = new int[] { -1, +1, -2, +2, -2, +2, -1, +1 };
            for (int k = 0; k < i.Length - 1; k++)
            {
                if (position[0] + i[k] < 0 | position[1] + j[k] < 0 | position[0] + i[k] > 7 | position[1] + j[k] > 7)
                {
                    continue;
                }
                if (board[position[0] + i[k], position[1] + j[k]] == null)
                {
                    posibleMoves.Add(new int[3] { position[0] + i[k], position[1] + j[k], identifer});
                    continue;
                }
                if (board[position[0] + i[k], position[1] + j[k]].GetColor() != this.GetColor())
                {
                    posibleMoves.Add(new int[3] { position[0] + i[k], position[1] + j[k], identifer});                   
                }
            }
            return posibleMoves;
        }
    }
}