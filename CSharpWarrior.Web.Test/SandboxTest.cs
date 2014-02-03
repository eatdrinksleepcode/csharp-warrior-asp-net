using System;
using System.IO;
using System.Reflection;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CSharpWarrior
{
    public class SandboxTest
    {
        private Sandbox sandbox;

        [SetUp]
        public void Before()
        {
            sandbox = new Sandbox();
        }

        [Test]
        public void ShouldFailToRunDangerousCode()
        {
            sandbox.Invoking(
                s => s.ExecuteAssembly<DangerousAgent, object>(Assembly.GetExecutingAssembly().Location, null))
                .ShouldThrow<DangerousCodeExecutionException>();
        }

        [Test]
        public void ShouldFailToRunFaultyCode()
        {
            sandbox.Invoking(
                s => s.ExecuteAssembly<FaultyAgent, object>(Assembly.GetExecutingAssembly().Location, null))
                .ShouldThrow<CodeExecutionException>()
                .WithInnerException<Exception>();
        }

        [Test]
        public void ShouldRunAgent()
        {
            var result = sandbox.ExecuteAssembly<SimpleAgent, object>(Assembly.GetExecutingAssembly().Location, null);

            result.Should().Be(SimpleAgent.ReturnValue);
        }
    }

    public class SimpleAgent : SandboxAgent
    {
        public const string ReturnValue = "Data from the other side";

        public override object Execute(IAssembly loadedAssembly, object data)
        {
            return ReturnValue;
        }
    }

    public class DangerousAgent : SandboxAgent
    {
        public override object Execute(IAssembly loadedAssembly, object data)
        {
            return Directory.EnumerateFiles(@"C:");
        }
    }

    public class FaultyAgent : SandboxAgent
    {
        public override object Execute(IAssembly loadedAssembly, object data)
        {
            throw new Exception("Fail!");
        }
    }
}
