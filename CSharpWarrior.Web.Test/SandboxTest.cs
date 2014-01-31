using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace CSharpWarrior
{
    public class SandboxTest
    {
        private Sandbox sandbox;
        private Level level;

        [SetUp]
        public void Before()
        {
            sandbox = new Sandbox();
            level = new Level(new[] {new Location(), new Location(),});
        }

        [Test]
        public void ShouldFailToRunDangerousCode()
        {
            Assert.Throws<DangerousCodeExecutionException>(() =>
                sandbox.ExecuteAssembly<DangerousAgent, object>(Assembly.GetExecutingAssembly().Location, null)
                );
        }

        [Test]
        public void ShouldFailToRunFaultyCode()
        {
            var ex = Assert.Throws<CodeExecutionException>(() =>
                sandbox.ExecuteAssembly<FaultyAgent, object>(Assembly.GetExecutingAssembly().Location, null)
                );
            Assert.That(ex.InnerException, Is.TypeOf<Exception>());
        }

        /*
        [Test]
        public void ShouldRunProvidedCode()
        {
            const string WorkingCode = @"
                public class Player : CSharpWarrior.IPlayer {
                    public CSharpWarrior.Action Play() {
                        return new CSharpWarrior.WalkAction();
                    }
                }
                ";
            Assert.That(sandbox.ExecuteCode(WorkingCode, level), 
            Is.EqualTo("Level complete"));
        }

        [Test]
        public void ShouldFailToRunIncorrectCode()
        {
            const string IncorrectCode = @"
                public class Player { // Does not implement IPlayer
                }
                ";
            var ex = Assert.Throws<CodeExecutionException>(() => sandbox.ExecuteCode(IncorrectCode, level));
            Assert.That(ex.Message, Is.EqualTo(Sandbox.IncorrectCodeMessage));
        }

        [Test]
        public void ShouldPlayLevel()
        {
        }
 */
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
