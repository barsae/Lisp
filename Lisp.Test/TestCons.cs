using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lisp.Test {
    [TestClass]
    public class TestCons {
        [TestMethod]
        public void Reverse_Works() {
            var list = new Cell(new Symbol("a"),
                       new Cell(new Symbol("b"),
                       new Cell(new Symbol("c"),
                       null)));

            list = Cell.ReverseInPlace(list);

            Assert.AreEqual("(c b a)", list.ToString());
        }
    }
}
