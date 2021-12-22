using System;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using Models.Interfaces;

namespace Core
{
    public class ToyCompiler : IToyCompilerParser, IToyCompilerSemantic, IToyCompilerCodeGen, IToyCompilerBuild
    {
        private readonly ILogger<ToyCompiler> _logger;
        private IToyParser _parser;
        private IToySemantics _semantics;
        private IToyCodeGen _codeGen;

        public ToyCompiler(ILogger<ToyCompiler> logger)
        {
            _logger = logger;
        }
        
        public IToyCompilerSemantic WithParser(IToyParser parser)
        {
            _parser = parser;
            
            return this;
        }

        public IToyCompilerCodeGen WithSemantics(IToySemantics semantics)
        {
            _semantics = semantics;
            return this;
        }

        public IToyCompilerBuild WithCodeGen(IToyCodeGen codeGen)
        {
            _codeGen = codeGen;
            return this;
        }

        public Action<string> Build()
        {
            return s =>
            {
                var ast = _parser.Parse(s);
                //_logger.LogInformation("{%s}", ast);

                _semantics.Semant(ast);
                _codeGen.CodeGen(ast);
            };
        }
    }
}