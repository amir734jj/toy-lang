using System;
using System.Linq;
using System.Reflection;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
            {
                return 
                    assembly.GetTypes()
                        .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                        .ToArray();
            }

            string better(string amir)
            {
                return char.ToLower(amir[0]) + amir.Substring(1);
            }

            var types = GetTypesInNamespace(Assembly.Load("Models"), "Models");
            
            
            Console.WriteLine(string.Join("\n", types.Select(x => x.Name).Select(x => $"case {x} {better(x)}:\n return Visit({better(x)});")));
        }
    }
}