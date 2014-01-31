namespace CSharpWarrior
{
    public interface IHandleAfter<in T> where T: Action
    {
        void HandleAfter(T action);
    }
}
