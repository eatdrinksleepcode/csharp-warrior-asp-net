using System;
using System.IO;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpWarrior
{
    public class SandboxTest
    {
        private Sandbox sandbox;
        private Level anyLevel;

        [SetUp]
        public void Before()
        {
            sandbox = new Sandbox();
            anyLevel = null;
        }

        [Test]
        public void ShouldFailToRunDangerousCode()
        {
            sandbox.Invoking(
                s => s.ExecuteAssembly<DangerousAgent, object>(Assembly.GetExecutingAssembly().Location, anyLevel))
                .ShouldThrow<DangerousCodeExecutionException>();
        }

        [Test]
        public void ShouldFailToRunFaultyCode()
        {
            sandbox.Invoking(
                s => s.ExecuteAssembly<FaultyAgent, object>(Assembly.GetExecutingAssembly().Location, anyLevel))
                .ShouldThrow<CodeExecutionException>()
                .WithInnerException<Exception>();
        }
    }

    public class DangerousAgent : SandboxAgent
    {
        public override void Execute(IAssembly loadedAssembly, object data)
        {
            Directory.EnumerateFiles(@"C:");
        }
    }

    public class FaultyAgent : SandboxAgent
    {
        public override void Execute(IAssembly loadedAssembly, object data)
        {
            throw new Exception("Fail!");
        }
    }
}
