using System;
using System.Text;
using Antlr4.Runtime;
using Microsoft.Extensions.Logging;
using Models;
using Models.Interfaces;

namespace AntlrParser
{
    public class ToyAntlrParser : IToyParser
    {
        private readonly ILogger<ToyAntlrParser> _logger;

        public ToyAntlrParser(ILogger<ToyAntlrParser> logger)
        {
            _logger = logger;
        }
        
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
            
            foreach (var token in lexer.GetAllTokens())
            {
                _logger.LogInformation("{} {}", lexer.Vocabulary.GetSymbolicName(token.Type), token);
            }

            var tree = parser.classes();
            var visitor = new AstBuilderVisitor();
            
            return (Classes)visitor.Visit(tree);
        }
    }
}
