using System;
using System.Runtime.Serialization;

namespace CSharpWarrior
{
    [Serializable]
    public class CodeExecutionException : Exception
    {
        public CodeExecutionException(string message) : base(message)
        {

        }

        public CodeExecutionException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected CodeExecutionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            
        }
    }
}
