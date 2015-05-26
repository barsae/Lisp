using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisp {
    public class StandardLibrary {
        public static Context NewContext() {
            var context = new Context();

            context.Variables["quote"] = new Quote();
            context.Variables["atom"] = new Atom();
            context.Variables["eq"] = new Eq();
            context.Variables["car"] = new Car();
            context.Variables["cdr"] = new Cdr();
            context.Variables["cons"] = new ConsFunc();
            context.Variables["cond"] = new Cond();
            context.Variables["defun"] = new Defun();

            return context;
        }
    }

    public class Quote : Macro {
        public override LispObject Call(Context context, List<LispObject> args) {
            if (args.Count != 1) {
                throw new Exception("quote expects exactly one argument");
            }

            return args[0];
        }
    }

    public class Eq : Function {
        public override LispObject Call(Context context, List<LispObject> args) {
            if (args.Count != 2) {
                throw new Exception("eq expects exactly two arguments");
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
        public override LispObject Call(Context context, List<LispObject> args) {
            if (args.Count != 1) {
                throw new Exception("atom expects exactly one argument");
            }

            return LispObject.FromBool((args[0] as Cons) == null);
        }
    }

    public class Car : Function {
        public override LispObject Call(Context context, List<LispObject> args) {
            if (args.Count != 1) {
                throw new Exception("car expects exactly one argument");
            }

            return (args[0] as Cons).Car;
        }
    }

    public class Cdr : Function {
        public override LispObject Call(Context context, List<LispObject> args) {
            if (args.Count != 1) {
                throw new Exception("cdr expects exactly one argument");
            }

            return (args[0] as Cons).Cdr;
        }
    }

    public class ConsFunc : Function {
        public override LispObject Call(Context context, List<LispObject> args) {
            if (args.Count != 2) {
                throw new Exception("cons expects exactly two arguments");
            }

            return new Cons(args[0], args[1]);
        }
    }

    public class Cond : Macro {
        public override LispObject Call(Context context, List<LispObject> args) {
            foreach (Cons arg in args) {
                var predicate = arg.Car;
                var result = arg.Cadr;

                if (LispObject.ToBool(Evaluator.Evaluate(context, predicate))) {
                    return Evaluator.Evaluate(context, result);
                }
            }

            return null;
        }
    }

    public class Defun : Macro {
        public override LispObject Call(Context context, List<LispObject> args) {
            if (args.Count != 3) {
                throw new Exception("defun expects exactly three arguments");
            }

            var name = (args[0] as Symbol).Value;
            var arguments = ((Cons)args[1]).Select(arg => ((Symbol)arg).Value)
                                           .ToList();
            var body = args[2];

            var lambda = new Lambda(arguments, body);
            context.Variables[name] = lambda;
            return lambda;
        }
    }
}
