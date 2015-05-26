using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lisp {
    /// <summary>
    /// A macro is like a function, except that it's arguments are not evaluated before the call
    /// </summary>
    public abstract class Macro : LispObject {
        public abstract LispObject Call(Context context, List<LispObject> args);
    }
}
