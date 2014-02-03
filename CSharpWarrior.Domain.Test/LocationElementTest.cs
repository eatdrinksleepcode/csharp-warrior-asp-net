using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace CSharpWarrior
{
    public class LocationElementTest
    {
        private Mock<ICrawlContext> context;

        [SetUp]
        public void Before()
        {
            context = new Mock<ICrawlContext>();
        }

        private class TestAction : WarriorAction
        {
            public override void Act(Level level, ICrawlContext context)
            {
            }
        }

        private class ActiveLocationElement : LocationElement, IHandleBefore<TestAction>
        {
            public bool HandledAction { get; private set; }

            public void HandleBefore(TestAction action, ICrawlContext context)
            {
                HandledAction = true;
            }
        }
        private class LazyLocationElement : LocationElement
        {
            public bool HandledAction { get; private set; }

            public void HandleBefore(TestAction action, ICrawlContext context)
            {
                HandledAction = true;
            }
        }

        [Test]
        public void ShouldHandleDeclaredActions()
        {
            var element = new ActiveLocationElement();
            var action = new TestAction();
            element.TryHandleBefore(action, context.Object);

            element.HandledAction.Should().BeTrue();
        }

        [Test]
        public void ShouldNotHandleUndeclaredActions()
        {
            var element = new LazyLocationElement();
            var action = new TestAction();
            element.TryHandleBefore(action, context.Object);

            element.HandledAction.Should().BeFalse();
        }
    }
}
