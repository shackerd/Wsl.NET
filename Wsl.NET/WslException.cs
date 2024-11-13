using System;

namespace Wsl.NET
{
    [Serializable]
    public class WslException : Exception
    {
        public WslException() { }
        public WslException(string message) : base(message) { }
        public WslException(string message, Exception inner) : base(message, inner) { }
        protected WslException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}