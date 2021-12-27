using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Microsoft.Extensions.Logging;
using Models;
using Models.Interfaces;
using IToken = Antlr4.Runtime.IToken;

namespace AntlrParser
{
    public class ToyAntlrParser : IToyParser
    {
        private readonly ILogger<ToyAntlrParser> _logger;

        public ToyAntlrParser(ILogger<ToyAntlrParser> logger)
        {
            _logger = logger;
        }
        
        public void Parse(CompilerPayload compilerPayload)
        {
            var str = CharStreams.fromStream(compilerPayload.Stream);

            var lexer = new CoolLexer(str);
            var tokens = new CommonTokenStream(lexer);
            var parser = new CoolParser(tokens);
            
            var listenerLexer = new ErrorListener<int>();
            var listenerParser = new ErrorListener<IToken>();
            
            lexer.AddErrorListener(listenerLexer);
            parser.AddErrorListener(listenerParser);
            
            foreach (var token in lexer.GetAllTokens())
            {
                if (token.Channel == Lexer.DefaultTokenChannel)
                {
                    _logger.LogTrace("{%s}: {%s}", lexer.Vocabulary.GetSymbolicName(token.Type), token.Text);
                }
            }
            
            lexer.Reset();

            var tree = parser.classes();
            var visitor = new AstBuilderVisitor();

            compilerPayload.Ast = (Classes)visitor.Visit(tree);
        }
    }
}
