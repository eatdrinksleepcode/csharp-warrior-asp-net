using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using Microsoft.CSharp;

namespace CSharpWarrior
{
    public class Sandbox : IDisposable
    {
        public const string DangerousCodeMessage = "Provided code attempted to access protected resources";

        public const string IncorrectCodeMessage =
            "Code must have a public 'Player' class with a public static 'Play' method which takes no arguments and returns a String";

        public const string BadCodeMessage = "Code does not compile: ";
        public const string FaultyCodeMessage = "Code execution failed";

        private static readonly Type ExecutorType = typeof(RemoteExecutor);

        private AppDomain sandboxAppDomain;
        private RemoteExecutor executor;
        private CSharpCodeProvider compiler = new CSharpCodeProvider();
        private CompilerParameters options = new CompilerParameters();

        public string Execute(string codeToCompile)
        {
            var compiledCode = compiler.CompileAssemblyFromSource(options, codeToCompile);
            if (compiledCode.Errors.Count > 0)
            {
                throw new CodeExecutionException(BadCodeMessage +
                                                 compiledCode.Errors.Cast<CompilerError>().First().ErrorText);
            }

            return (executor ?? (executor = CreateExecutor(compiledCode.PathToAssembly))).Execute(compiledCode.PathToAssembly);
        }

        private RemoteExecutor CreateExecutor(string pathToAssembly)
        {
            return (RemoteExecutor)Activator.CreateInstanceFrom(sandboxAppDomain ?? (sandboxAppDomain = CreateSandbox(pathToAssembly)), ExecutorType.Assembly.ManifestModule.FullyQualifiedName,
                ExecutorType.FullName).Unwrap();
        }

        private AppDomain CreateSandbox(string pathToAssembly)
        {
            var perms = new PermissionSet(PermissionState.None);
            perms.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            perms.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, Path.GetDirectoryName(pathToAssembly)));

            return AppDomain.CreateDomain("Sandbox", null, AppDomain.CurrentDomain.SetupInformation, perms, ExecutorType.Assembly.Evidence.GetHostEvidence<StrongName>());
        }

        public class RemoteExecutor : MarshalByRefObject
        {
            public string Execute(string pathToAssembly)
            {
                var loadedAssembly = Assembly.LoadFrom(pathToAssembly);
                var playMethod = (from type in loadedAssembly.GetTypes()
                                  where type.Name == "Player" && type.IsPublic
                                  from method in type.GetMethods(BindingFlags.Static | BindingFlags.Public)
                                  where method.Name == "Play" && method.GetParameters().Length == 0 && method.ReturnType == typeof(string)
                                  select method).FirstOrDefault();
                if (null == playMethod)
                {
                    throw new CodeExecutionException(IncorrectCodeMessage);
                }
                try
                {
                    return (string)playMethod.Invoke(null, null);
                }
                catch (TargetInvocationException ex)
                {
                    if (ex.InnerException is SecurityException)
                    {
                        throw new CodeExecutionException(DangerousCodeMessage, ex.InnerException);
                    }
                    else
                    {
                        throw new CodeExecutionException(FaultyCodeMessage, ex.InnerException);
                    }
                }
            }
        }

        public void Dispose()
        {
            if (null != sandboxAppDomain)
            {
                AppDomain.Unload(sandboxAppDomain);
                sandboxAppDomain = null;
            }
            if (null != compiler)
            {
                compiler.Dispose();
                compiler = null;
            }
        }
    }
}