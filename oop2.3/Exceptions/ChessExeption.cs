using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop2._3.Exceptions
{
    class ChessException: System.Exception
    {
        public ChessException(): base()
        {

        }
        
        public ChessException(string message): base(message)
        {

        }
    }
}
