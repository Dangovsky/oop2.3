using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop2._3.Interfaces;

namespace oop2._3.Players.Stategies
{
    class Defensive : IStraregy
    {
        private string color;

        public Defensive(string color)
        {
            this.color = color;
        }

        public int[] Turn(IFigure[,] board, int numberOfRecursions, out IFigure killedFigure)
        {

        }
    }
}
