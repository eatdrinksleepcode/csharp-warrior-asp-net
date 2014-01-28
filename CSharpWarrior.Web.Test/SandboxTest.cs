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
            level = new Level {StartPosition = 0, ExitPosition = 1};
        }

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

        // TODO: implement by mocking LevelCrawler?
        [Test]
        public void ShouldFailToRunFaultyCode()
        {
            const string FaultyCode = @"
                public class Player : CSharpWarrior.IPlayer {
                    public CSharpWarrior.Action Play() {
                        throw new System.Exception(""Fail!"");
                    }
                }
                ";
            var ex = Assert.Throws<CodeExecutionException>(() => sandbox.ExecuteCode(FaultyCode, level));
            Assert.That(ex.Message, Is.EqualTo(Sandbox.FaultyCodeMessage));
            Assert.That(ex.InnerException, Is.Not.Null);
        }

        // TODO: implement by mocking LevelCrawler?
        [Test]
        public void ShouldFailToRunDangerousCode()
        {
            const string DangerousCode = @"
                public class Player : CSharpWarrior.IPlayer {
                    public CSharpWarrior.Action Play() {
                        System.IO.Directory.EnumerateFiles(@""C:\"");
                        return null;
                    }
                }
                ";
            var ex = Assert.Throws<CodeExecutionException>(() => sandbox.ExecuteCode(DangerousCode, level));
            Assert.That(ex.Message, Is.EqualTo(Sandbox.DangerousCodeMessage));
            Assert.That(ex.InnerException, Is.Not.Null);
        }

        [Test]
        public void ShouldPlayLevel()
        {
        }
    }
}
