using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note
{
    enum ValidFlags
    {
        Required =  0b0001,
        Leters =     0b0010,
        Digits =     0b0100,
        Symbols =   0b1000
    }
}
