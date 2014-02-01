using System;

namespace CSharpWarrior
{
    [Serializable]
    public class LocationElement
    {
        public void TryHandleBefore(WarriorAction action)
        {
            TryHandle(action, typeof(IHandleBefore<>), "HandleBefore");
        }

        public void TryHandleAfter(WarriorAction action)
        {
            TryHandle(action, typeof(IHandleAfter<>), "HandleAfter");
        }

        private void TryHandle(WarriorAction action, Type handlerInterfaceType, string handlerMethodName)
        {
            var handlerInterface = handlerInterfaceType.MakeGenericType(action.GetType());
            if (handlerInterface.IsInstanceOfType(this))
            {
                handlerInterface.GetMethod(handlerMethodName).Invoke(this, new[] { action });
            }
        }

    }
}
