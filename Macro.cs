using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lisp {
    public abstract class Macro : LispObject {
        public abstract LispObject Call(Context context, List<LispObject> args);
    }
}
