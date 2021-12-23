using System;
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
        
        public Classes Parse(Stream stream)
        {
            var result = Parser.Classes().Parse(new CharStream<Unit>(stream, Encoding.Default));

            if (result.IsOk())
            {
                return result.Result;
            }

            _logger.LogError("Parser failed: {%s}", result.Error);
            
            throw new ArgumentException();
        }
    }
}