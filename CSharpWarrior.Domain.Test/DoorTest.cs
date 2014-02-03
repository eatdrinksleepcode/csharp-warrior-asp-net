using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CSharpWarrior
{
    public class DoorTest
    {
        private ICrawlContext context;

        [SetUp]
        public void Before()
        {
            context = Mock.Of<ICrawlContext>();
        }

        [Test]
        public void ShouldAllowPassageIfTheCorrectPasswordHasBeenSpoken()
        {
            const string password = "Open Sesame";
            var door = new Door(password);

            door.HandleAfter(new SpeakAction(password), context);

            door.Invoking(d => d.HandleBefore(new WalkAction(), context))
                .ShouldNotThrow();
        }

        [Test]
        public void ShouldNotAllowPassageIfTheWrongPasswordHasBeenSpoken()
        {
            const string password = "Open Sesame";
            var door = new Door(password);

            door.HandleAfter(new SpeakAction(password + password), context);

            door.Invoking(d => d.HandleBefore(new WalkAction(), context))
                .ShouldThrow<LevelCrawlException>();
        }

        [Test]
        public void ShouldNotAllowPassageIfNoPasswordHasBeenSpoken()
        {
            var door = new Door("Open Sesame");

            door.Invoking(d => d.HandleBefore(new WalkAction(), context))
                .ShouldThrow<LevelCrawlException>();
        }
    }
}
