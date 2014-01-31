namespace CSharpWarrior
{
    public class SpeakAction : Action
    {
        private readonly string message;

        public SpeakAction(string message)
        {
            this.message = message;
        }

        public override void Act(Level level)
        {
        }

        public string Message
        {
            get { return message; }
        }
    }
}