using System;
using System.Runtime.Serialization;

namespace CSharpWarrior
{
    [Serializable]
    public class LevelCrawlException : Exception
    {
        public LevelCrawlException(string message)
            : base(message)
        {
        }

        protected LevelCrawlException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
