using System;
using System.Text;
using Antlr4.Runtime;
using Models;
using Models.Interfaces;

namespace AntlrParser
{
    public class ToyAntlrParser : IToyParser
    {
        public Classes Parse(string text)
        {
            var str = CharStreams.fromString(text);

            var lexer = new CoolLexer(str);
            var tokens = new CommonTokenStream(lexer);
            var parser = new CoolParser(tokens);
            
            var listenerLexer = new ErrorListener<int>();
            var listenerParser = new ErrorListener<IToken>();
            
            lexer.AddErrorListener(listenerLexer);
            parser.AddErrorListener(listenerParser);

            var tree = parser.classes();
            var visitor = new AstBuilderVisitor();
            
            return (Classes)visitor.Visit(tree);
        }
    }
}
