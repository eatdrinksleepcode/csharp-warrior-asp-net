namespace CSharpWarrior
{
    public class WalkAction : WarriorAction
    {
        public override void Act(Level level, ICrawlContext context)
        {
            level.MoveWarrior();
            context.WriteLineToCrawLog("Spartacus moves forward");
        }
    }
}
