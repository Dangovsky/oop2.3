using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop2._3.Exceptions;

namespace oop2._3.Unit
{
    class CounterSingleton
    {
        private static int counter;

        public CounterSingleton()
        {
            counter = 0;
        }

        public int GetIdentifer()
        {
            if(counter == 36)
            {
                throw new ChessException("Singleton > 36");
            }
            return counter++;
        }
    }
}
