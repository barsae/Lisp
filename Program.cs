using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisp {
    public class Program {
        public static void Main(string[] args) {
            var context = StandardLibrary.NewContext();

            foreach (var expression in Reader.ReadFile("eval.lisp")) {
                Console.WriteLine(expression);
                Evaluator.Evaluate(context, expression);
            }

            var lisp = Reader.ReadString("(eval. '(cdr '(some list)) '())");
            Console.WriteLine(lisp);
            Console.WriteLine(Evaluator.Evaluate(context, lisp));

            Console.WriteLine("Done");
            Console.ReadKey(true);
        }
    }
}
