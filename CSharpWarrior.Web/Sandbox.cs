using System;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;

namespace CSharpWarrior
{
    public class Sandbox : IDisposable
    {
        public const string DangerousCodeMessage = "Provided code attempted to access protected resources";

        public const string IncorrectCodeMessage =
            "Code must have one and only one class that implements the 'IPlayer' interface";

        public const string BadCodeMessage = "Code does not compile: ";
        public const string FaultyCodeMessage = "Code execution failed";

        private AppDomain sandboxAppDomain;
        private readonly PlayerCompiler compiler = new PlayerCompiler();

        public object ExecuteAssembly<TAgent, TData>(string pathToAssembly, TData data) where TAgent: SandboxAgent
        {
            var sandboxAgent = CreateExecutor<TAgent>(pathToAssembly);
            return sandboxAgent.LoadAndExecute(pathToAssembly, data);
        }

        private TAgent CreateExecutor<TAgent>(string pathToAssembly) where TAgent: SandboxAgent
        {
            return (TAgent)Activator.CreateInstanceFrom(sandboxAppDomain ?? (sandboxAppDomain = CreateSandbox(pathToAssembly, typeof(TAgent))), typeof(TAgent).Assembly.ManifestModule.FullyQualifiedName,
                typeof(TAgent).FullName).Unwrap();
        }

        private AppDomain CreateSandbox(string pathToAssembly, Type agentType)
        {
            var perms = new PermissionSet(PermissionState.None);
            perms.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            perms.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, Path.GetDirectoryName(pathToAssembly)));
            perms.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, Path.GetDirectoryName(agentType.Assembly.Location)));

            return AppDomain.CreateDomain("Sandbox", null, AppDomain.CurrentDomain.SetupInformation, perms, agentType.Assembly.Evidence.GetHostEvidence<StrongName>());
        }

        public void Dispose()
        {
            if (null != sandboxAppDomain)
            {
                AppDomain.Unload(sandboxAppDomain);
                sandboxAppDomain = null;
            }
            compiler.Dispose();
        }
    }
}