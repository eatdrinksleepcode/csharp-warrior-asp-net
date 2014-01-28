using Moq;
using NUnit.Framework;

namespace CSharpWarrior
{
    public class LevelCrawlerTest
    {
        private class NoOpAction : Action
        {
            public override void Act(LevelCrawler levelCrawler)
            {
            }
        }

        [Test]
        public void ShouldCrawlLevel()
        {
            var level = new Level {StartPosition = 0, ExitPosition = 1};
            var player = new Mock<IPlayer>();
            player.Setup(p => p.Play()).Returns(new WalkAction());
            var crawler = new LevelCrawler(level, player.Object);
            crawler.Crawl();
        }

        [Test]
        public void ShouldThrowIfCrawlDoesNotCompleteWithinMaxTurns()
        {
            var level = new Level {StartPosition = 0, ExitPosition = 1};
            var player = new Mock<IPlayer>();
            player.Setup(p => p.Play()).Returns(new NoOpAction());
            var runner = new LevelCrawler(level, player.Object) { MaximumTurns = 1 };
            Assert.Throws<LevelCrawlException>(() => runner.Crawl());
        }
    }
}
