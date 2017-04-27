using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop2._3.Interfaces;
using oop2._3.Figures;

namespace oop2._3.Drawing
{
    static class DrawFactory
    {
        private static List<IFigure> flyweightFigures;
    }
}
