namespace CSharpWarrior
{
    public class LevelCrawler
    {
        private readonly Level level;
        private readonly IPlayer player;

        public LevelCrawler(Level level, IPlayer player)
        {
            this.level = level;
            this.player = player;
            this.MaximumTurns = 100;
        }

        public int MaximumTurns { get; set; }

        public void CrawlLevel()
        {
            int turns = 0;
            while (level.WarriorPosition != level.ExitPosition)
            {
                if (turns >= MaximumTurns)
                {
                    throw new LevelCrawlException(string.Format("Maximum number of turns ({0}) exceeded", MaximumTurns));
                }
                var action = player.Play();
                level.ActOut(action);
                turns++;
            }
        }
    }
}
