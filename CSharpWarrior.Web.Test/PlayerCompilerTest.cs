using FluentAssertions;
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

            var compilation = compiler.Compile(ValidCode);

            compilation.Errors.Should().BeEmpty();
        }

        [Test]
        public void ShouldNotCompileInvalidCode()
        {
            const string InvalidCode = @"
                foobar
                ";


            compiler.Invoking(c => c.Compile(InvalidCode))
                .ShouldThrow<CodeCompilationException>();
        }
    }
}
