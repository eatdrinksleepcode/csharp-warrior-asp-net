namespace CSharpWarrior
{
    public class WalkAction : Action
    {
        public override void Act(Level level)
        {
            level.MoveWarrior();
        }
    }
}
