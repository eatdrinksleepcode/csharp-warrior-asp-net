using NUnit.Framework;

namespace CSharpWarrior
{
    public class PlayerCompilerTest
    {
        private PlayerCompiler compiler;

        [SetUp]
        public void Before()
        {
            compiler = new PlayerCompiler();
        }

        [Test]
        public void ShouldCompileValidCode()
        {
            const string ValidCode = @"
                using CSharpWarrior;
                public class Player : IPlayer {
                    public WarriorAction Play() {
                        return new WalkAction();
                    }
                }
                ";
            CollectionAssert.IsEmpty(compiler.Compile(ValidCode).Errors);
        }

        [Test]
        public void ShouldNotCompileInvalidCode()
        {
            const string InvalidCode = @"
                foobar
                ";
            Assert.Throws<CodeCompilationException>(() => compiler.Compile(InvalidCode));
        }
    }
}
