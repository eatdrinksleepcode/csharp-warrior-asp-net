namespace CSharpWarrior
{
    public interface IHandleBefore<in T> where T: Action
    {
        void HandleBefore(T action);
    }
}
