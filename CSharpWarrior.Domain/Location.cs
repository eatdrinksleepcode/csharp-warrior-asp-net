using System;
using System.Linq;

namespace CSharpWarrior
{
    [Serializable]
    public class Location
    {
        private readonly LocationElement[] locationElements;

        public Location(params LocationElement[] locationElements)
        {
            this.locationElements = locationElements;
        }

        public void TryHandleBefore(WarriorAction action, ICrawlContext context)
        {
            HandleBefore((dynamic) action, context);
        }

        private void HandleBefore<T>(T action, ICrawlContext context) where T : WarriorAction
        {
            foreach (var element in locationElements.OfType<IHandleBefore<T>>())
            {
                element.HandleBefore(action, context);
            }
        }

        public void TryHandleAfter(WarriorAction action, ICrawlContext context)
        {
            HandleAfter((dynamic) action, context);
        }

        private void HandleAfter<T>(T action, ICrawlContext context) where T : WarriorAction
        {
            foreach (var element in locationElements.OfType<IHandleAfter<T>>())
            {
                element.HandleAfter(action, context);
            }
        }
    }
}
