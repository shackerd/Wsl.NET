namespace Wsl.NET
{
    [System.Serializable]
    public class LxssServiceException : System.Exception
    {
        public LxssServiceException() { }
        public LxssServiceException(string message) : base(message) { }
        public LxssServiceException(string message, System.Exception inner) : base(message, inner) { }
        protected LxssServiceException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
