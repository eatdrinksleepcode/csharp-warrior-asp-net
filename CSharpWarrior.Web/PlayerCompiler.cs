using System;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace CSharpWarrior
{
    public class PlayerCompiler : IDisposable
    {
        private readonly CSharpCodeProvider compiler = new CSharpCodeProvider();
        private readonly CompilerParameters options = new CompilerParameters();

        public PlayerCompiler()
        {
            options.ReferencedAssemblies.Add(typeof(IPlayer).Assembly.Location);
        }

        public CompilerResults Compile(string code)
        {
            var results = compiler.CompileAssemblyFromSource(options, code);
            if (results.Errors.Count > 0)
            {
                throw new CodeCompilationException(results.Errors[0].ErrorText);
            }
            return results;
        }

        public void Dispose()
        {
            compiler.Dispose();
        }
    }
}