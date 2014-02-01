namespace CSharpWarrior
{
    public class WalkAction : WarriorAction
    {
        public override void Act(Level level)
        {
            level.MoveWarrior();
        }
    }
}
