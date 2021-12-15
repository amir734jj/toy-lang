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

            var newS = new StringBuilder();

            while (true)
            {
                var roToken = lexer.NextToken();
                var token = (CommonToken)roToken;

                if (token.Channel == TokenConstants.DefaultChannel)
                {
                    if (token.Type == TokenConstants.EOF)
                    {
                        break;
                    }

                    newS.AppendLine(lexer.Vocabulary.GetSymbolicName(token.Type) + ": " + token.Text);
                }
            }

            Console.Error.WriteLine(newS.ToString());

            lexer.Reset();

            var tokens = new CommonTokenStream(lexer);
            var parser = new CoolParser(tokens);
            var listenerLexer = new ErrorListener<int>();
            var listenerParser = new ErrorListener<IToken>();
            lexer.AddErrorListener(listenerLexer);
            parser.AddErrorListener(listenerParser);

            var tree = parser.classes();
            Console.WriteLine(tree.ToStringTree(parser));
        }
    }
}