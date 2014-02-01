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

        public void TryHandleBefore(WarriorAction action)
        {
            foreach (var element in locationElements)
            {
                element.TryHandleBefore(action);
            }
        }

        public void TryHandleAfter(WarriorAction action)
        {
            foreach (var element in locationElements)
            {
                element.TryHandleAfter(action);
            }
        }
    }
}
