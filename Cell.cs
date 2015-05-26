using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisp {
    public class Cell : LispObject, IEnumerable<LispObject> {
        public LispObject Car { get; set; }
        public LispObject Cdr { get; set; }

        public LispObject Cadr {
            get {
                return ((Cell)Cdr).Car;
            }
        }

        public Cell(LispObject car, LispObject cdr) {
            this.Car = car;
            this.Cdr = cdr;
        }

        /// <summary>
        /// Reverses a list without allocating any new conses.
        /// Note: you need to assign this back to the original variable to get the new head of list reference.
        /// </summary>
        public static Cell ReverseInPlace(Cell list) {
            Cell previous = null;

            while (list != null) {
                var cdr = list.Cdr as Cell;

                if (list.Cdr != null && cdr == null) {
                    throw new LispException("Can't reverse list that has non-cons element in cdr");
                }

                list.Cdr = previous;
                previous = list;
                list = cdr;
            }

            return previous;
        }

        public IEnumerator<LispObject> GetEnumerator() {
            var current = this;
            while (current != null) {
                yield return current.Car;

                if (current.Cdr != null && !(current.Cdr is Cell)) {
                    throw new LispException("Expected list during iteration");
                }
                current = current.Cdr as Cell;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public override string ToString() {
            // Handle quote specifically
            if (IsQuote(this)) {
                return "'" + LispObject.ToString(Cadr);
            }

            var sb = new StringBuilder("(");

            bool first = true;
            var next = this;
            while (next != null) {
                // Add whitespace if needed
                if (!first) {
                    sb.Append(" ");
                }

                // Add next element
                sb.Append(LispObject.ToString(next.Car));

                // Safely advance to next element
                var cdr = next.Cdr as Cell;
                if (next.Cdr != null && cdr == null) {
                    throw new LispException("Can't ToString list that has non-cons element in cdr");
                }
                next = cdr;

                // Mark that we've passed the first iteration
                first = false;
            }

            sb.Append(")");
            return sb.ToString();
        }

        public static bool IsQuote(LispObject obj) {
            var cell = obj as Cell;
            if (cell != null) {
                var symbol = cell.Car as Symbol;
                if (symbol != null) {
                    return symbol.Value == "quote";
                }
            }
            
            return false;
        }
    }
}
