using System;
using System.Runtime.Serialization;

namespace Constructorio_NET
{
    [Serializable]
    public class ConstructorException : Exception
    {
        public ConstructorException(string msg) : base(msg)
        {
        }

        public ConstructorException(Exception e) : base(e.Message)
        {
        }

        protected ConstructorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
