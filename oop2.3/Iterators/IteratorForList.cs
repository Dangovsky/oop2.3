using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop2._3.Interfaces;
using oop2._3.Exceptions;

namespace oop2._3.Iterators
{
    class IteratorForList<T>: IIterator<T>
    {
        private List<T> figures;
        private int pointer;

        public IteratorForList(List<T> figures)
        {            
            if (figures.Count == 0)
            {
                throw new ChessException("List is empty");
            }
            this.figures = figures;
            pointer = 0;
        }

        public bool HasNext()
        {
            if (figures.Count != pointer+1)
            {
                return true;
            }
            return false;
        }

        public T Next()
        {
            return figures[pointer++];
        }
    }
}
