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
                throw new LispException("Lambda expects exactly {0} argument(s)", Arguments.Count());
            }
            
            // Create a child context to hold the bound arguments
            var context = new Context(parent);
            for (int ii = 0; ii < args.Count; ii++) {
                context.Variables[Arguments[ii]] = args[ii];
            }

            return context.Evaluate(Body);
        }
    }
}
