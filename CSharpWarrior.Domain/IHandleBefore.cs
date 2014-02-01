namespace CSharpWarrior
{
    public interface IHandleBefore<in T> where T: WarriorAction
    {
        void HandleBefore(T action);
    }
}
