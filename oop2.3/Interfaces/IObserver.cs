﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop2._3.Interfaces
{
    interface IObserver
    {
        void HandleEvent(ICommand gameCommand);
    }
}
