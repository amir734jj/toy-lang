using Microsoft.Extensions.Logging;
using Models;
using Models.Interfaces;
using Newtonsoft.Json;

namespace AstJsonDump
{
    public class ToyAstJsonDump : IAstDump
    {
        private readonly ILogger<ToyAstJsonDump> _logger;

        public ToyAstJsonDump(ILogger<ToyAstJsonDump> logger)
        {
            _logger = logger;
        }
        
        public void CodeGen(CompilerPayload compilerPayload)
        {
            var result = JsonConvert.SerializeObject(compilerPayload.Ast, Formatting.Indented);
            _logger.LogTrace("{}", result);
        }
    }
}