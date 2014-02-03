using System;

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
            foreach (var element in locationElements)
            {
                element.TryHandleBefore(action, context);
            }
        }

        public void TryHandleAfter(WarriorAction action, ICrawlContext context)
        {
            foreach (var element in locationElements)
            {
                element.TryHandleAfter(action, context);
            }
        }
    }
}
