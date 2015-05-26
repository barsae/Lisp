using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisp {
    public class Lambda : Function {
        public List<string> Arguments { get; set; }
        public LispObject Body { get; set; }

        public Lambda(List<string> arguments, LispObject body) {
            this.Arguments = arguments;
            this.Body = body;
        }

        public override LispObject Call(Context parent, List<LispObject> args) {
            if (args.Count != Arguments.Count()) {
                throw new ArgumentException(string.Format("Lambda expects exactly {0} arguments", Arguments.Count()));
            }
            
            var context = new Context(parent);
            for (int ii = 0; ii < args.Count; ii++) {
                context.Variables[Arguments[ii]] = args[ii];
            }

            return Evaluator.Evaluate(context, Body);
        }
    }
}
