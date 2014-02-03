namespace CSharpWarrior
{
    public class SpeakAction : WarriorAction
    {
        private readonly string message;

        public SpeakAction(string message)
        {
            this.message = message;
        }

        public override void Act(Level level, ICrawlContext log)
        {
            log.WriteLineToCrawLog("Spartacus says '{0}'", message);
        }

        public string Message
        {
            get { return message; }
        }
    }
}