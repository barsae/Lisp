using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisp {
    public abstract class LispObject {
        public static Symbol T = new Symbol("t");

        public static string ToString(LispObject value) {
            if (value == null) {
                return "()";
            }

            return value.ToString();
        }

        public static LispObject FromBool(bool value) {
            return value ? LispObject.T : null;
        }

        public static bool ToBool(LispObject obj) {
            return obj != null;
        }
    }

    public class Symbol : LispObject {
        public string Value { get; set; }

        public Symbol(string symbol) {
            this.Value = symbol;
        }

        public override string ToString() {
            return Value;
        }
    }
}
