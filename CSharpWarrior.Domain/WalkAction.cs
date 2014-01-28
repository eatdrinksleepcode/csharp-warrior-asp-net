namespace CSharpWarrior
{
    public class WalkAction : Action
    {
        public override void Act(LevelCrawler levelCrawler)
        {
            levelCrawler.Walk();
        }
    }
}
