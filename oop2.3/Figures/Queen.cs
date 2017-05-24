using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using oop2._3.Interfaces;

namespace oop2._3.Figures
{
    class Queen : IFigure
    {
        private string color;
        private int identifer;
        private Bishop bishop;
        private Rook rook;

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
            return 10;
        }

        public Image GetImage()
        {
            return new Bitmap(@"Drawing\" + color + ToString() + ".png");
        }

        public Queen(string color, int identifer)
        {
            this.color = color;
            this.identifer = identifer;
            bishop = new Bishop(color, identifer);
            rook = new Rook(color, identifer);
        }

        public override string ToString()
        {
            return "Queen";
        }

        public List<int[]> PosibleMoves(int[] position, IFigure[,] board)
        {
            List<int[]> posibleMoves = new List<int[]>();
            posibleMoves.AddRange(rook.PosibleMoves(position, board));
            posibleMoves.AddRange(bishop.PosibleMoves(position, board));
            return posibleMoves;
        }
    }
}
