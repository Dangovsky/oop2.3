using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop2._3.Interfaces
{
    interface ICommand
    {
        IFigure[,] Execute(ref bool WhiteTurn, out IFigure killedFigure);
    }
}
