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
        public void ShouldRunProvidedCode()
        {
            const string WorkingCode = @"
                public class Player : CSharpWarrior.IPlayer {
                    public string Play() {
                        return ""Success"";
                    }
                }
                ";
            Assert.That(sandbox.Execute(WorkingCode), 
            Is.EqualTo("Success"));
        }

        [Test]
        public void ShouldFailToRunNonCompilingCode()
        {
            const string NonCompilingCode = @"
                public class Player {
                ";
            var ex = Assert.Throws<CodeExecutionException>(() => sandbox.Execute(NonCompilingCode));
            StringAssert.StartsWith(Sandbox.BadCodeMessage, ex.Message);
        }

        [Test]
        public void ShouldFailToRunIncorrectCode()
        {
            const string IncorrectCode = @"
                public class Player {
                    public string Play() {
                        return ""Failure"";
                    }
                }
                ";
            var ex = Assert.Throws<CodeExecutionException>(() => sandbox.Execute(IncorrectCode));
            Assert.That(ex.Message, Is.EqualTo(Sandbox.IncorrectCodeMessage));
        }

        [Test]
        public void ShouldFailToRunFaultyCode()
        {
            const string FaultyCode = @"
                public class Player : CSharpWarrior.IPlayer {
                    public string Play() {
                        throw new System.Exception(""Fail!"");
                    }
                }
                ";
            var ex = Assert.Throws<CodeExecutionException>(() => sandbox.Execute(FaultyCode));
            Assert.That(ex.Message, Is.EqualTo(Sandbox.FaultyCodeMessage));
            Assert.That(ex.InnerException, Is.Not.Null);
        }

        [Test]
        public void ShouldFailToRunDangerousCode()
        {
            const string DangerousCode = @"
                public class Player : CSharpWarrior.IPlayer {
                    public string Play() {
                        System.IO.Directory.EnumerateFiles(@""C:\"");
                        return ""Haha!"";
                    }
                }
                ";
            var ex = Assert.Throws<CodeExecutionException>(() => sandbox.Execute(DangerousCode));
            Assert.That(ex.Message, Is.EqualTo(Sandbox.DangerousCodeMessage));
            Assert.That(ex.InnerException, Is.Not.Null);
        }
    }
}
