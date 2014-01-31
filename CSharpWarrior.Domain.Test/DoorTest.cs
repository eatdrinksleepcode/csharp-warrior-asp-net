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
            door.HandleBefore(new WalkAction());
        }

        [Test]
        public void ShouldNotAllowPassageIfTheCorrectPasswordHasNotBeenSpoken()
        {
            const string password = "Open Sesame";
            var door = new Door(password);

            door.HandleAfter(new SpeakAction(password + password));
            Assert.Throws<LevelCrawlException>(() => door.HandleBefore(new WalkAction()));
        }
    }
}
