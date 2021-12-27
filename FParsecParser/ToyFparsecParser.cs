using System;
using System.Linq;
using System.IO;
using System.Text;
using FParsec;
using FParsec.CSharp;
using Microsoft.Extensions.Logging;
using Microsoft.FSharp.Core;
using Models;
using Models.Interfaces;

namespace FParsecParser
{
    public class ToyFparsecParser : IToyParser
    {
        private readonly ILogger<ToyFparsecParser> _logger;

        public ToyFparsecParser(ILogger<ToyFparsecParser> logger)
        {
            _logger = logger;
        }
        
        public void Parse(CompilerPayload compilerPayload)
        {
            var result = Parser.Classes().Parse(new CharStream<Unit>(compilerPayload.Stream, Encoding.Default));

            if (result.IsOk())
            {
                compilerPayload.Ast = result.Result;
                return;
            }
            
            foreach (var errorMessage in result.Error.AsEnumerable())
            {
                compilerPayload.Errors.Add(errorMessage.ToString());
            }

            _logger.LogError("Parser failed: {%s}", result.Error);
            
            throw new ArgumentException();
        }
    }
}