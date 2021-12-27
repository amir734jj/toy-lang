using System.Collections.Generic;
using System.IO;

namespace Models
{
    public class CompilerPayload
    {
        public string Code { get; set; }
        
        public IToken Ast { get; set; }
        
        public List<string> Errors { get; set; }
        
        public string Result { get; set; }
        
        public Stream Stream { get; set; }
    }
}