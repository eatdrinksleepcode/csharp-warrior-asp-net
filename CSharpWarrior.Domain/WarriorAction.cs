namespace CSharpWarrior
{
    public abstract class WarriorAction
    {
        public abstract void Act(Level level, ICrawlContext context);
    }
}
