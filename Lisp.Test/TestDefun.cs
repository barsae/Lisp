using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Lisp.Test {
    [TestClass]
    public class TestDefun {
        [TestMethod]
        public void Defun_OneArgument_Works() {
            var context = StandardLibrary.NewContext();
            var lisp = Reader.ReadString("(defun test (a) a)");
            var call = Reader.ReadString("(test 'b)");

            var lambda = Evaluator.Evaluate(context, lisp);
            Assert.IsNotNull(lambda as Lambda);

            var result = Evaluator.Evaluate(context, call);
            Assert.AreEqual("b", (result as Symbol).Value);
        }

        [TestMethod]
        public void Defun_Null_TrueWorks() {
            var context = StandardLibrary.NewContext();
            var lisp = Reader.ReadString("(defun null (x) (eq x '()))");
            var call = Reader.ReadString("(null 'a)");

            var lambda = Evaluator.Evaluate(context, lisp);
            Assert.IsNotNull(lambda as Lambda);

            var result = Evaluator.Evaluate(context, call);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Defun_Null_FalseWorks() {
            var context = StandardLibrary.NewContext();
            var lisp = Reader.ReadString("(defun null (x) (eq x '()))");
            var call = Reader.ReadString("(null '())");

            var lambda = Evaluator.Evaluate(context, lisp);
            Assert.IsNotNull(lambda as Lambda);

            var result = Evaluator.Evaluate(context, call);
            Assert.AreEqual("t", (result as Symbol).Value);
        }
    }
}
