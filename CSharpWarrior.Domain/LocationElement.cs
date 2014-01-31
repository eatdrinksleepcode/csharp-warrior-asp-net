using System;

namespace CSharpWarrior
{
    [Serializable]
    public class LocationElement
    {
        public void TryHandleBefore(Action action)
        {
            TryHandle(action, typeof(IHandleBefore<>), "HandleBefore");
        }

        public void TryHandleAfter(Action action)
        {
            TryHandle(action, typeof(IHandleAfter<>), "HandleAfter");
        }

        private void TryHandle(Action action, Type handlerInterfaceType, string handlerMethodName)
        {
            var handlerInterface = handlerInterfaceType.MakeGenericType(action.GetType());
            if (handlerInterface.IsInstanceOfType(this))
            {
                handlerInterface.GetMethod(handlerMethodName).Invoke(this, new[] { action });
            }
        }

    }
}
