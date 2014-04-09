using System;
using System.Reflection;
using System.Security;

namespace CSharpWarrior
{
    public abstract class SandboxAgent : MarshalByRefObject
    {
        private const string DangerousCodeMessage = "Provided code attempted to access protected resources";
        private const string FaultyCodeMessage = "Code execution failed";

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

        public object LoadAndExecute(string pathToAssembly, object data)
        {
            var loadedAssembly = Assembly.LoadFrom(pathToAssembly);
            try
            {
                return Execute(new AssemblyWrapper(loadedAssembly), data);
            }
            catch (SecurityException ex)
            {
                throw new DangerousCodeExecutionException(DangerousCodeMessage, ex);
            }
            catch (Exception ex)
            {
                throw new CodeExecutionException(FaultyCodeMessage, ex);
            }
        }

        public abstract object Execute(IAssembly loadedAssembly, object data);
    }
}