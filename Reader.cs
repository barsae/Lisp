using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisp {
    public static class Reader {
        public static List<LispObject> ReadFile(string path) {
            var expressions = new List<LispObject>();

            using (var stream = new StreamReader(path)) {
                ReadWhiteSpace(stream);

                while (!stream.EndOfStream) {
                    expressions.Add(Read(stream));
                    ReadWhiteSpace(stream);
                }
            }

            return expressions;
        }

        public static LispObject ReadString(string lisp) {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            writer.Write(lisp);
            writer.Flush();
            stream.Position = 0;

            return Reader.Read(new StreamReader(stream));
        }

        public static LispObject Read(StreamReader stream) {
            ReadWhiteSpace(stream);

            var next = (char)stream.Peek();
            switch (next) {
                case '(': return ReadList(stream);
                case '\'': return ReadQuote(stream);
                default: return ReadSymbol(stream);
            }
        }

        private static LispObject ReadQuote(StreamReader stream) {
            // Eat the "'" character
            stream.Read();

            return new Cell(new Symbol("quote"),
                   new Cell(Read(stream),
                   null));
        }

        private static LispObject ReadSymbol(StreamReader stream) {
            var symbol = "";

            while (!stream.EndOfStream && IsSymbolChar((char)stream.Peek())) {
                symbol += (char)stream.Read();
            }

            return new Symbol(symbol);
        }

        private static bool IsSymbolChar(char c) {
            return !char.IsWhiteSpace(c) && !"()'".Contains(c);
        }

        private static LispObject ReadList(StreamReader stream) {
            Cell list = null;

            // Eat the initial ( character
            stream.Read();

            ReadWhiteSpace(stream);
            while (!stream.EndOfStream && stream.Peek() != ')') {
                list = new Cell(Read(stream), list);
                ReadWhiteSpace(stream);
            }

            if (stream.EndOfStream) {
                throw new LispException("Failed to find terminating ')' character");
            }

            // Eat the trailing ) character
            stream.Read();

            return Cell.ReverseInPlace(list);
        }

        private static void ReadWhiteSpace(StreamReader stream) {
            while (!stream.EndOfStream && (char.IsWhiteSpace((char)stream.Peek()) || stream.Peek() == ';')) {
                if (stream.Read() == ';') {
                    while (!stream.EndOfStream && stream.Read() != '\n') {
                    }
                }
            }
        }
    }
}
