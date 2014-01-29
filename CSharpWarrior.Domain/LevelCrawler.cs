namespace CSharpWarrior
{
    public class LevelCrawler
    {
        private readonly Level level;
        private readonly IPlayer player;

        private int position;

        public LevelCrawler(Level level, IPlayer player)
        {
            this.level = level;
            this.player = player;
            this.MaximumTurns = 100;
        }

        public int MaximumTurns { get; set; }

        public void Crawl()
        {
            int turns = 0;
            position = level.StartPosition;
            while (position != level.ExitPosition)
            {
                if (turns >= MaximumTurns)
                {
                    throw new LevelCrawlException();
                }
                var action = player.Play();
                action.Act(this);
                turns++;
            }
        }

        public void Walk()
        {
            this.position++;
        }
    }
}
