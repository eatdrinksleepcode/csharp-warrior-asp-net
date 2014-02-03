using System;

namespace CSharpWarrior
{
    [Serializable]
    public class LocationElement
    {
        public void TryHandleBefore(WarriorAction action, ICrawlContext context)
        {
            TryHandle(action, typeof(IHandleBefore<>), "HandleBefore", context);
        }

        public void TryHandleAfter(WarriorAction action, ICrawlContext context)
        {
            TryHandle(action, typeof(IHandleAfter<>), "HandleAfter", context);
        }

        private void TryHandle(WarriorAction action, Type handlerInterfaceType, string handlerMethodName, ICrawlContext context)
        {
            var handlerInterface = handlerInterfaceType.MakeGenericType(action.GetType());
            if (handlerInterface.IsInstanceOfType(this))
            {
                handlerInterface.GetMethod(handlerMethodName).Invoke(this, new object[] { action, context });
            }
        }

    }
}
