using System;
using System.IO;

namespace CSharpWarrior
{
    public class CrawlContext : ICrawlContext, IDisposable
    {
        private readonly TextWriter writer = new StringWriter();

        public void WriteLineToCrawLog(string eventFormat, params object[] args)
        {
            writer.WriteLine(eventFormat, args);
        }

        public void Dispose()
        {
            if (null != writer)
            {
                writer.Dispose();
            }
        }

        public string ToCrawlLog()
        {
            return writer.ToString();
        }
    }
}
