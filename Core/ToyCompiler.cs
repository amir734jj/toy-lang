using System;
using System.IO;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
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
                var ast = _parser.Parse(s);
                File.WriteAllText($"/home/amir-pc/RiderProjects/toy-lang/Core/basic.{_parser.GetType().Name}", ast.ToString());

                _semantics.Semant(ast);
                _codeGen.CodeGen(ast);
            };
        }
    }
}