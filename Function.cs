using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lisp {
    /// <summary>
    /// This is a "normal" function, where arguments are evaluated, and then passed to the function
    /// </summary>
    public abstract class Function : LispObject {
        public abstract LispObject Call(Context context, List<LispObject> args);
    }
}
