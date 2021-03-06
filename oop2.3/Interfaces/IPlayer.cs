﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop2._3.Unit;

namespace oop2._3.Interfaces
{
    interface IPlayer : IObservable
    {
        GameCommand CalculateMove(IFigure[,] board);

        string ToString();
    }
}
