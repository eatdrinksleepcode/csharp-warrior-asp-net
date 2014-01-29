using System;
using System.Runtime.Serialization;

namespace CSharpWarrior
{
    [Serializable]
    public class DangerousCodeExecutionException : CodeExecutionException
    {
        public DangerousCodeExecutionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DangerousCodeExecutionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
