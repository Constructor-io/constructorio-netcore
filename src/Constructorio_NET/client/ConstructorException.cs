using System;

namespace Constructorio_NET
{
    public class ConstructorException : Exception
    {
        public ConstructorException(string msg) : base(msg)
        {
        }

        public ConstructorException(Exception e)
        {
        }
    }
}
