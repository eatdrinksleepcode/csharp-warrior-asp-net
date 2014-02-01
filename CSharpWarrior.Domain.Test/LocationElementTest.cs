using NUnit.Framework;

namespace CSharpWarrior
{
    public class LocationElementTest
    {
        private class TestAction : WarriorAction
        {
            public override void Act(Level level)
            {
            }
        }

        private class ActiveLocationElement : LocationElement, IHandleBefore<TestAction>
        {
            public bool HandledAction { get; private set; }

            public void HandleBefore(TestAction action)
            {
                HandledAction = true;
            }
        }
        private class LazyLocationElement : LocationElement
        {
            public bool HandledAction { get; private set; }

            public void Handle(TestAction action)
            {
                HandledAction = true;
            }
        }

        [Test]
        public void ShouldHandleDeclaredActions()
        {
            var element = new ActiveLocationElement();
            var action = new TestAction();
            element.TryHandleBefore(action);

            Assert.That(element.HandledAction, Is.True);
        }

        [Test]
        public void ShouldNotHandleUndeclaredActions()
        {
            var element = new LazyLocationElement();
            var action = new TestAction();
            element.TryHandleBefore(action);

            Assert.That(element.HandledAction, Is.False);
        }
    }
}
