using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using Models;
using Models.Interfaces;
using Newtonsoft.Json;

namespace Core
{
    public class ToyCompiler : IToyCompilerParser, IToyCompilerSemantic, IToyCompilerCodeGen, IToyCompilerBuild
    {
        private readonly ILogger<ToyCompiler> _logger;
        private IToyParser _parser;
        private IToySemantics _semantics;
        private IAstDump _astDump;
        public static readonly byte[] BasicFileText = File.ReadAllBytes(Path.Join(Directory.GetParent(Assembly.GetAssembly(typeof(ToyCompiler)).Location).FullName, "basic.toy"));

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

        public Action<CompilerPayload> Build()
        {
            return code =>
            {
                code.Stream = new MemoryStream(BasicFileText.Concat(Encoding.Default.GetBytes(code.Code ?? "")).ToArray());

                _parser.Parse(code);
                _semantics.Semant(code);
                _astDump.CodeGen(code);
            };
        }
    }
}