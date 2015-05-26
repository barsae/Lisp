using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Lisp.Test {
    [TestClass]
    public class TestReader {
        [TestMethod]
        public void ReadSymbol_Works() {
            TestRead("symbol");
        }

        [TestMethod]
        public void ReadQuote_Symbol_Works() {
            TestRead("'symbol");
        }

        [TestMethod]
        public void ReadQuote_List_Works() {
            TestRead("'(a b c)");
        }

        [TestMethod]
        public void ReadList_Works() {
            TestRead("(a b c)");
        }

        /// <summary>
        /// Tests that Reading and ToStringing a lisp fragment produces the original value
        /// </summary>
        public void TestRead(string lisp) {
            Assert.AreEqual(lisp, LispObject.ToString(Reader.ReadString(lisp)));
        }
    }
}
