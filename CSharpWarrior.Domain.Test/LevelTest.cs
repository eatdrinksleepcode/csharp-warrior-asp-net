using System;
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
            Assert.Throws<ArgumentException>(() => new Level(new Location()));
        }

        [Test]
        public void WarriorShouldStartAtFirstLocation()
        {
            Assert.That(defaultLevel.WarriorPosition, Is.EqualTo(locations[0]));
        }

        [Test]
        public void ShouldMoveWarrior()
        {
            defaultLevel.MoveWarrior();

            Assert.That(defaultLevel.WarriorPosition, Is.EqualTo(locations[1]));
        }
    }
}
