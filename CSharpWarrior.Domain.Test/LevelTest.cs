using System;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpWarrior
{
    public class LevelTest
    {
        private Location[] locations;
        private Level defaultLevel;

        [SetUp]
        public void Before()
        {
            locations = new[] {new Location(), new Location()};
            defaultLevel = new Level(locations);
        }

        [Test]
        public void MustHaveAtLeastTwoLocations()
        {
            Action invalidConstructor = () => new Level(new Location());

            invalidConstructor.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void WarriorShouldStartAtFirstLocation()
        {
            defaultLevel.WarriorPosition.Should().Be(locations[0]);
        }

        [Test]
        public void ShouldMoveWarrior()
        {
            defaultLevel.MoveWarrior();

            defaultLevel.WarriorPosition.Should().Be(locations[1]);
        }
    }
}
