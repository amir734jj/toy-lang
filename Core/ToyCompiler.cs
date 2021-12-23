using System;
using System.IO;
using System.Linq;
using System.Text;
using Core.Extensions;
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
        private IAstDump _astDump;
        public static readonly MemoryStream BasicFileText = new(File.ReadAllBytes("basic.toy"));

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

        public IToyCompilerBuild WithAstDump(IAstDump astDump)
        {
            _astDump = astDump;
            return this;
        }

        public Action<string> Build()
        {
            return code =>
            {
                var ast = _parser.Parse(new ConcatenatedStream(
                    BasicFileText,
                    new MemoryStream(Encoding.Default.GetBytes(code))));

                _semantics.Semant(ast);
                _astDump.CodeGen(ast);
            };
        }
    }
}