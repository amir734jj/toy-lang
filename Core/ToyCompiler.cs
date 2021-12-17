using System;
using Core.Interfaces;
using Models.Interfaces;

namespace Core
{
    public class ToyCompiler : IToyCompilerParser, IToyCompilerSemantic, IToyCompilerCodeGen, IToyCompilerBuild
    {
        private IToyParser _parser;
        private IToySemantics _semantics;
        private IToyCodeGen _codeGen;

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
                var result = _parser.Parse(s);
                Console.WriteLine(result);
            };
        }
    }
}