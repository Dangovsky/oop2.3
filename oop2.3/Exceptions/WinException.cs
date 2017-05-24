using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop2._3.Exceptions
{
    class WinException : System.Exception
    {
        public WinException(): base()
        {

        }

        public WinException(string message): base(message)
        {

        }
    }
}
