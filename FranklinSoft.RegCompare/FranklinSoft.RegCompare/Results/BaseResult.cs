using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FranklinSoft.RegCompare.Results
{
    public abstract class BaseResult
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
        public Exception Exception { get; set; }
    }
}
