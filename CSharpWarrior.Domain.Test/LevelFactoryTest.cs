using System;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpWarrior
{
    class LevelFactoryTest
    {
        [Test]
        public void GenerateUniqueLevels()
        {
            var factory = new LevelFactory();

            var level = factory.MakeLevel(1);

            level.Should().NotBeSameAs(factory.MakeLevel(1));
        }

        [Test]
        public void InvalidLevel()
        {
            var factory = new LevelFactory();

            factory.Invoking(f => f.MakeLevel(1000))
                .ShouldThrow<ArgumentOutOfRangeException>();
        }
    }
}
