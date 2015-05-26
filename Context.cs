using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisp {
    public class Context {
        public Context Parent { get; set; }
        public Dictionary<string, LispObject> Variables { get; set; }

        public Context(Context parent = null) {
            Variables = new Dictionary<string, LispObject>();
            this.Parent = parent;
        }

        public LispObject Lookup(Symbol symbol) {
            if (Variables.ContainsKey(symbol.Value)) {
                return Variables[symbol.Value];
            }

            if (Parent != null) {
                return Parent.Lookup(symbol);
            }

            throw new LispException("Unable to find value for: {0}", symbol);
        }

        public LispObject Evaluate(string lisp) {
            return Evaluate(Reader.ReadString(lisp));
        }

        public LispObject Evaluate(LispObject obj) {
            var symbol = obj as Symbol;
            var cell = obj as Cell;

            if (symbol != null) {
                // If we are evaluating a symbol, look it up in the current context
                return Lookup(symbol);
            } else if (cell != null) {
                // If we are evaluating a list, look up the callable in the current context, and call it
                symbol = cell.Car as Symbol;
                var value = Lookup(symbol);

                var func = value as Function;
                var macro = value as Macro;

                if (func != null) {
                    // If it's a function, evaluate it's arguments first
                    var args = ((Cell)cell.Cdr).Select(arg => Evaluate(arg))
                                               .ToList();
                    return func.Call(this, args);
                } else if (macro != null) {
                    // If it's a macro, don't evaluate the arguments
                    var args = ((Cell)cell.Cdr).ToList();
                    return macro.Call(this, args);
                }
            }

            throw new LispException("Do not know how to evaluate: {0}", obj);
        }
    }
}
