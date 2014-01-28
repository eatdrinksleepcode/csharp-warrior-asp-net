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
            return compiler.CompileAssemblyFromSource(options, code);
        }

        public void Dispose()
        {
            compiler.Dispose();
        }
    }
}