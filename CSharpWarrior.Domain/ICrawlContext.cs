namespace CSharpWarrior
{
    public interface ICrawlContext
    {
        void WriteLineToCrawLog(string eventFormat, params object[] args);
    }
}
