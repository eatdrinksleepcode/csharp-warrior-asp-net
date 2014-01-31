using System;

namespace CSharpWarrior
{
    public class LevelCrawlException : Exception
    {
        public LevelCrawlException(string message)
            : base(message)
        {
        }
    }
}
