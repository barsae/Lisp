using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lisp.Test {
    [TestClass]
    public class TestCons {
        [TestMethod]
        public void Reverse_Works() {
            var list = new Cons(new Symbol("a"),
                       new Cons(new Symbol("b"),
                       new Cons(new Symbol("c"),
                       null)));

            list = Cons.ReverseInPlace(list);

            Assert.AreEqual("(c b a)", list.ToString());
        }
    }
}
