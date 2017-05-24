using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop2._3.Interfaces;
using oop2._3.Figures;
using oop2._3.Iterators;
using oop2._3.Exceptions;

namespace oop2._3.Drawing
{
    class DrawFactory
    {
        private static List<IFigure> flyweightFigures;
        
        internal void DrawFigure(int[] coordinates, string figureName, string color)
        {
            IteratorForList<IFigure> iterator = new IteratorForList<IFigure>(flyweightFigures);
            IFigure tmp;
            while (iterator.HasNext())
            {
                tmp = iterator.Next();
                if ((tmp.ToString() == figureName) && (tmp.GetColor() == color))                    
                {
                    //tmp.Draw(coordinates);
                    return;
                }
            }
            tmp = Create(figureName, color);
            ///tmp.Draw(coordinates);
            flyweightFigures.Add(tmp);
        }

        private IFigure Create(string figureName, string color)
        {
            switch (figureName)
            {
                case "Bishop":
                    return new Bishop(color, -1);
                case "King":
                    return new King(color, -1);
                case "Knight":
                    return new Knight(color, -1);
                case "Pawn":
                    return new Pawn(color, -1);
                case "Queen":
                    return new Queen(color, -1);
                case "Rook":
                    return new Rook(color, -1);
                default:
                    throw new ChessException("DrawFactory.Create. Unknown figure name");
            }
        }
    }
}
