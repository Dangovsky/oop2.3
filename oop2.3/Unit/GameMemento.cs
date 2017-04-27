using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop2._3.Unit
{
    class GameMemento
    {
        public readonly Game state;

        public GameMemento(Game gameToSave)
        {
            state = gameToSave;
        }        
    }
}
