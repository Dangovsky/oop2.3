using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop2._3.Exeptions
{
    class ChessException: Exception
    {
        public ChessException(): base()
        {

        }
        
        public ChessException(string message): base(message)
        {

        }
    }
}
