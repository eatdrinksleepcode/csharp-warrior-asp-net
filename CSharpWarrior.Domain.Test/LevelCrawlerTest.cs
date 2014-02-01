using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CSharpWarrior
{
    public class LevelCrawlerTest
    {
        private static Location[] MakeLocations(int count)
        {
            return Enumerable.Range(0, count).Select(x => new Location()).ToArray();
        }

        [Test]
        public void ShouldCrawlLevel()
        {
            var level = new Level(MakeLocations(2));
            var player = new Mock<IPlayer>();
            player.Setup(p => p.Play()).Returns(new WalkAction());
            var crawler = new LevelCrawler(level, player.Object) {MaximumTurns = 1};

            crawler.Invoking(c => c.CrawlLevel())
                .ShouldNotThrow();
            // TODO: add better assertion here
        }

        [Test]
        public void ShouldThrowIfCrawlDoesNotCompleteWithinMaxTurns()
        {
            var level = new Level(MakeLocations(3));
            var player = new Mock<IPlayer>();
            player.Setup(p => p.Play()).Returns(new WalkAction());
            var crawler = new LevelCrawler(level, player.Object) { MaximumTurns = 1 };

            crawler.Invoking(c => c.CrawlLevel())
                .ShouldThrow<LevelCrawlException>();
        }
    }
}
