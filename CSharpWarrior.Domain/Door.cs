using System;

namespace CSharpWarrior
{
    [Serializable]
    public class Door : LocationElement, IHandleBefore<WalkAction>, IHandleAfter<SpeakAction>
    {
        private readonly string password;
        private bool isOpen;

        public Door(string password)
        {
            this.password = password;
        }

        public void HandleBefore(WalkAction action)
        {
            if (!isOpen)
            {
                throw new LevelCrawlException("Cannot walk through a closed door!");
            }
        }

        public void HandleAfter(SpeakAction action)
        {
            if (action.Message == password)
            {
                isOpen = true;
            }
        }
    }
}
