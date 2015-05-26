using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisp {
    public class LispException : Exception {
        public LispException(string message, params object[] args) : base(string.Format(message, args)) {
        }
    }
}
