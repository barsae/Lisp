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

            throw new ArgumentException(string.Format("Unable to find value for: {0}", symbol));
        }
    }
}
