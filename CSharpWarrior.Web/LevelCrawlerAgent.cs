using System;
using System.Linq;

namespace CSharpWarrior
{
    public class LevelCrawlerAgent : SandboxAgent
    {
        public override void Execute(IAssembly loadedAssembly, object data)
        {
            var level = (Level)data;
            var player = (from type in loadedAssembly.GetTypes()
                          where typeof(IPlayer).IsAssignableFrom(type)
                          select (IPlayer)Activator.CreateInstance(type)
                ).ToArray();
            if (player.Length != 1)
            {
                throw new CodeExecutionException(Sandbox.IncorrectCodeMessage);
            }
            var crawler = new LevelCrawler(level, player[0]);
            crawler.Crawl();
        }
    }
}