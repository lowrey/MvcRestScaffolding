using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcRestScaffoldingLib.Calls
{
    class CallException : System.Exception
    {
        public CallException()
            : base() { }
        public CallException(string message)
            : base(message) { }
        public CallException(string message, Exception innerException)
            : base(message, innerException) { }

    }
}
