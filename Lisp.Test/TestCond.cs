using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lisp.Test {
    [TestClass]
    public class TestCond {
        [TestMethod]
        public void Cond_Basic_TrueWorks() {
            var context = StandardLibrary.NewContext();
            var lisp = Reader.ReadString("(cond ('t 'a) ('() 'b))");

            var result = context.Evaluate(lisp);
            Assert.AreEqual("a", (result as Symbol).Value);
        }
    }
}
