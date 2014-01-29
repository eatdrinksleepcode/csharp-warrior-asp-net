using Moq;
using NUnit.Framework;

namespace CSharpWarrior
{
    public class LevelCrawlerAgentTest
    {
        public class ValidPlayer : IPlayer
        {
            public Action Play()
            {
                return new WalkAction();
            }
        }

        private LevelCrawlerAgent agent;

        [SetUp]
        public void Before()
        {
            agent = new LevelCrawlerAgent();
        }

        [Test]
        public void ShouldFailToRunWithoutValidPlayer()
        {
            var assembly = new Mock<IAssembly>();
            assembly.Setup(a => a.GetTypes()).Returns(new[] {typeof (string)});
            var ex = Assert.Throws<CodeExecutionException>(() => agent.Execute(assembly.Object, new Level()));
            Assert.That(ex.Message, Is.EqualTo(Sandbox.IncorrectCodeMessage));
        }

        [Test]
        public void ShouldFailToRunWithMoreThanOneValidPlayer()
        {
            var assembly = new Mock<IAssembly>();
            assembly.Setup(a => a.GetTypes()).Returns(new[] { typeof(ValidPlayer), typeof(ValidPlayer) });
            var ex = Assert.Throws<CodeExecutionException>(() => agent.Execute(assembly.Object, new Level()));
            Assert.That(ex.Message, Is.EqualTo(Sandbox.IncorrectCodeMessage));
        }

        [Test]
        public void ShouldRunWithValidPlayer()
        {
            var assembly = new Mock<IAssembly>();
            assembly.Setup(a => a.GetTypes()).Returns(new[] {typeof (ValidPlayer)});
            agent.Execute(assembly.Object, new Level());
        }
    }
}
