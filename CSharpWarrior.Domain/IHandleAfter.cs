namespace CSharpWarrior
{
    public interface IHandleAfter<in T> where T: WarriorAction
    {
        void HandleAfter(T action, ICrawlContext context);
    }
}
