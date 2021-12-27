using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Models;
using Models.Interfaces;

namespace Semantics
{
    public class ToyBasicSemantics : IToySemantics
    {
        private readonly ILogger<ToyBasicSemantics> _logger;

        public ToyBasicSemantics(ILogger<ToyBasicSemantics> logger)
        {
            _logger = logger;
        }
        
        public void Semant(CompilerPayload compilerPayload)
        {
            var visitor = new SemanticsVisitor();
            visitor.Visit(compilerPayload.Ast);

            var errors = visitor.Semantics.Collect();
            
            if (errors.Any())
            {
                foreach (var (token, messages) in errors)
                {
                    foreach (var message in messages)
                    {
                        // ReSharper disable once AssignNullToNotNullAttribute
                        _logger.LogError("Concerning {Token}: {Message}", new string(token.ToString().Take(250).ToArray()), message);
                    }
                }

                throw new Exception(string.Join(Environment.NewLine, "Semantics failed."));
            }
        }
    }
}