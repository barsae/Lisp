using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisp {
    public static class Evaluator {
        public static LispObject Evaluate(Context context, LispObject obj) {
            var symbol = obj as Symbol;
            var cons = obj as Cons;

            if (symbol != null) {
                return context.Lookup(symbol);
            } else if (cons != null) {
                symbol = cons.Car as Symbol;
                var value = context.Lookup(symbol);

                var func = value as Function;
                var macro = value as Macro;

                if (func != null) {
                    var args = ((Cons)cons.Cdr).Select(arg => Evaluate(context, arg))
                                               .ToList();
                    return func.Call(context, args);
                } else if (macro != null) {
                    var args = ((Cons)cons.Cdr).ToList();
                    return macro.Call(context, args);
                }
            }

            throw new NotImplementedException(string.Format("Do not know how to evaluate: {0}", obj));
        }
    }
}
