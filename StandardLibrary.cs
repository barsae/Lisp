using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisp {
    public class StandardLibrary {
        /// <summary>
        /// This creates a new context and populates it with the 7 core callables, and the "defun" macro
        /// </summary>
        public static Context NewContext() {
            var context = new Context();

            context.Variables["quote"] = new Quote();
            context.Variables["atom"] = new Atom();
            context.Variables["eq"] = new Eq();
            context.Variables["car"] = new Car();
            context.Variables["cdr"] = new Cdr();
            context.Variables["cons"] = new Cons();
            context.Variables["cond"] = new Cond();
            context.Variables["defun"] = new Defun();

            return context;
        }
    }

    public class Quote : Macro {
        /// <summary>
        /// Return the argument unevaluated
        /// </summary>
        public override LispObject Call(Context context, List<LispObject> args) {
            if (args.Count != 1) {
                throw new LispException("quote expects exactly one argument");
            }

            return args[0];
        }
    }

    public class Eq : Function {
        /// <summary>
        /// Return whether or not both arguments are symbols and have the same value
        /// </summary>
        public override LispObject Call(Context context, List<LispObject> args) {
            if (args.Count != 2) {
                throw new LispException("eq expects exactly two arguments");
            }

            var one = args[0] as Symbol;
            var two = args[1] as Symbol;

            if (one == null && two == null) {
                return LispObject.T;
            }

            return LispObject.FromBool(one != null && two != null && one.Value == two.Value);
        }
    }

    public class Atom : Function {
        /// <summary>
        /// Return true if the argument is not a list
        /// </summary>
        public override LispObject Call(Context context, List<LispObject> args) {
            if (args.Count != 1) {
                throw new LispException("atom expects exactly one argument");
            }

            return LispObject.FromBool((args[0] as Cell) == null);
        }
    }

    public class Car : Function {
        /// <summary>
        /// Return return the "car" property of a cons
        /// </summary>
        public override LispObject Call(Context context, List<LispObject> args) {
            if (args.Count != 1) {
                throw new LispException("car expects exactly one argument");
            }

            return (args[0] as Cell).Car;
        }
    }

    public class Cdr : Function {
        /// <summary>
        /// Return return the "cdr" property of a cons
        /// </summary>
        public override LispObject Call(Context context, List<LispObject> args) {
            if (args.Count != 1) {
                throw new LispException("cdr expects exactly one argument");
            }

            return (args[0] as Cell).Cdr;
        }
    }

    public class Cons : Function {
        /// <summary>
        /// Create a cell with the car and cdr set from the arguments
        /// </summary>
        public override LispObject Call(Context context, List<LispObject> args) {
            if (args.Count != 2) {
                throw new LispException("cons expects exactly two arguments");
            }

            return new Cell(args[0], args[1]);
        }
    }

    public class Cond : Macro {
        /// <summary>
        /// Execute a sequence of predicates until one is true, and then execute the associated body
        /// </summary>
        public override LispObject Call(Context context, List<LispObject> args) {
            foreach (Cell arg in args) {
                var predicate = arg.Car;
                var result = arg.Cadr;

                if (LispObject.ToBool(context.Evaluate(predicate))) {
                    return context.Evaluate(result);
                }
            }

            return null;
        }
    }

    public class Defun : Macro {
        /// <summary>
        /// Create a lambda and associate it in the current context with the specified name
        /// </summary>
        public override LispObject Call(Context context, List<LispObject> args) {
            if (args.Count != 3) {
                throw new LispException("defun expects exactly three arguments");
            }

            var name = (args[0] as Symbol).Value;
            var arguments = ((Cell)args[1]).Select(arg => ((Symbol)arg).Value)
                                           .ToList();
            var body = args[2];

            var lambda = new Lambda(arguments, body);
            context.Variables[name] = lambda;
            return lambda;
        }
    }
}
