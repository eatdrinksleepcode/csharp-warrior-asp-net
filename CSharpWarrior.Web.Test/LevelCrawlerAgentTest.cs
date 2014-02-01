using Moq;
using NUnit.Framework;

namespace CSharpWarrior
{
    public class LevelCrawlerAgentTest
    {
        public class ValidPlayer : IPlayer
        {
            public WarriorAction Play()
            {
                return new WalkAction();
            }
        }

        private LevelCrawlerAgent agent;
        private Level defaultLevel;

        [SetUp]
        public void Before()
        {
            agent = new LevelCrawlerAgent();
            defaultLevel = new Level(new []{new Location(), new Location(), });
        }

        [Test]
        public void ShouldFailToRunWithoutValidPlayer()
        {
            var assembly = new Mock<IAssembly>();
            assembly.Setup(a => a.GetTypes()).Returns(new[] {typeof (string)});
            var ex = Assert.Throws<CodeExecutionException>(() => agent.Execute(assembly.Object, defaultLevel));
            Assert.That(ex.Message, Is.EqualTo(Sandbox.IncorrectCodeMessage));
        }

        [Test]
        public void ShouldFailToRunWithMoreThanOneValidPlayer()
        {
            var assembly = new Mock<IAssembly>();
            assembly.Setup(a => a.GetTypes()).Returns(new[] { typeof(ValidPlayer), typeof(ValidPlayer) });
            var ex = Assert.Throws<CodeExecutionException>(() => agent.Execute(assembly.Object, defaultLevel));
            Assert.That(ex.Message, Is.EqualTo(Sandbox.IncorrectCodeMessage));
        }

        [Test]
        public void ShouldRunWithValidPlayer()
        {
            var assembly = new Mock<IAssembly>();
            assembly.Setup(a => a.GetTypes()).Returns(new[] {typeof (ValidPlayer)});
            agent.Execute(assembly.Object, defaultLevel);
        }
    }
}
