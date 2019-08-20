using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FranklinSoft.RegCompare
{
    public enum ErrorCodes
    {
        EMPTY = 0,
        TIMEOUT = 1300,
        SECURITY_EXCEPTION = 1400,
        UNAUTHORIZED_ACCESS_EXCEPTION = 1500,
        ARGUMENT_NULL_EXCEPTION = 1600,
        ARGUMENT_EXCEPTION = 1700,
        IO_EXCEPTION = 1800,
        GENERIC_EXCEPTION = 9000

    }
}
