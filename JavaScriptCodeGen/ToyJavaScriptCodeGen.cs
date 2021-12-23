using System;
using Models;
using Models.Interfaces;

namespace JavaScriptCodeGen
{
    public class ToyJavaScriptCodeGen : IAstDump
    {
        public void CodeGen(Classes classes)
        {
            var visitor = new JavaScriptCodeGenVisitor();

            Console.WriteLine(visitor.Visit(classes));
        }
    }
}