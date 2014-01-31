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

        public void TryHandleBefore(Action action)
        {
            foreach (var element in locationElements)
            {
                element.TryHandleBefore(action);
            }
        }

        public void TryHandleAfter(Action action)
        {
            foreach (var element in locationElements)
            {
                element.TryHandleAfter(action);
            }
        }
    }
}
