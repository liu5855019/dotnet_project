
namespace DM.Log.Common
{
    using System;

    public class DmException : Exception
    {
        public DmException() : base() { }
        public DmException(string message) : base(message) { }
        public DmException(string message, Exception innerException) : base(message, innerException) { }
    }
}
