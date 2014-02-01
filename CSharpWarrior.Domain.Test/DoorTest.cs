using System;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpWarrior
{
    public class DoorTest
    {
        [Test]
        public void ShouldAllowPassageIfTheCorrectPasswordHasBeenSpoken()
        {
            const string password = "Open Sesame";
            var door = new Door(password);

            door.HandleAfter(new SpeakAction(password));

            door.Invoking(d => d.HandleBefore(new WalkAction()))
                .ShouldNotThrow();
        }

        [Test]
        public void ShouldNotAllowPassageIfTheWrongPasswordHasBeenSpoken()
        {
            const string password = "Open Sesame";
            var door = new Door(password);

            door.HandleAfter(new SpeakAction(password + password));

            door.Invoking(d => d.HandleBefore(new WalkAction()))
                .ShouldThrow<LevelCrawlException>();
        }

        [Test]
        public void ShouldNotAllowPassageIfNoPasswordHasBeenSpoken()
        {
            var door = new Door("Open Sesame");

            door.Invoking(d => d.HandleBefore(new WalkAction()))
                .ShouldThrow<LevelCrawlException>();
        }
    }
}
