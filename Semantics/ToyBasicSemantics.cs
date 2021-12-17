using System;
using Models;
using Models.Interfaces;

namespace Semantics
{
    public class ToyBasicSemantics : IToySemantics
    {
        public void Semant(Classes classes)
        {
            var visitor = new SemanticsVisitor();
            visitor.Visit(classes);
        }
    }
}