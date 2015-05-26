using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Lisp.Test {
    [TestClass]
    public class TestEq {
        [TestMethod]
        public void Eq_AreEqual_Works() {
            var context = StandardLibrary.NewContext();
            var lisp = Reader.ReadString("(eq 'a 'a)");

            Assert.AreEqual(context.Evaluate(lisp), LispObject.T);
        }

        [TestMethod]
        public void Eq_ArentEqual_Works() {
            var context = StandardLibrary.NewContext();
            var lisp = Reader.ReadString("(eq 'a 'b)");

            Assert.AreEqual(context.Evaluate(lisp), null);
        }
    }
}
