using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace oop2._3.Interfaces
{
    interface IFigure
    {
        string GetColor();

        int GetIdentifer();

        int GetWeight();

        Image GetImage();

        List<int[]> PosibleMoves(int[] position, IFigure[,] board); // [x, y, identifer]
        
        string ToString();
    }
}
