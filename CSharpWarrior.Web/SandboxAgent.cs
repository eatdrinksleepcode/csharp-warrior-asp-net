using System;
using System.Reflection;
using System.Security;

namespace CSharpWarrior
{
    public abstract class SandboxAgent : MarshalByRefObject
    {
        private class AssemblyWrapper : IAssembly
        {
            private readonly Assembly source;

            public AssemblyWrapper(Assembly source)
            {
                this.source = source;
            }

            public Type[] GetTypes()
            {
                return source.GetTypes();
            }
        }

        public void LoadAndExecute(string pathToAssembly, object data)
        {
            var loadedAssembly = Assembly.LoadFrom(pathToAssembly);
            try
            {
                Execute(new AssemblyWrapper(loadedAssembly), data);
            }
            catch (SecurityException ex)
            {
                throw new DangerousCodeExecutionException(Sandbox.DangerousCodeMessage, ex);
            }
            catch (Exception ex)
            {
                throw new CodeExecutionException(Sandbox.FaultyCodeMessage, ex);
            }
        }

        public abstract void Execute(IAssembly loadedAssembly, object data);
    }
}