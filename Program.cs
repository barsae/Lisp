using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisp {
    public class Program {
        /// <summary>
        /// This program interprets and executes the eval.lisp code, and then drops to a kind of REPL
        /// </summary>
        public static void Main(string[] args) {
            var context = StandardLibrary.NewContext();

            try {
                foreach (var expression in Reader.ReadFile("eval.lisp")) {
                    Console.WriteLine(expression);
                    context.Evaluate(expression);
                }
            } catch (LispException ex) {
                Console.WriteLine("Error while executing eval.lisp", ex);
            }

            var line = Console.ReadLine();
            while (line != "exit") {
                try {
                    Console.WriteLine(context.Evaluate(line));
                } catch (LispException ex) {
                    Console.WriteLine(ex);
                }
                line = Console.ReadLine();
            }

            Console.WriteLine("Done");
            Console.ReadKey(true);
        }
    }
}
