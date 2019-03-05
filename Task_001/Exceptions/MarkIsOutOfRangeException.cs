using System;
using System.Runtime.Serialization;

namespace Task_001
{
    public class MarkIsOutOfRangeException : Exception
    {
        #region Constructors

        public MarkIsOutOfRangeException() { }

        public MarkIsOutOfRangeException(string message) : base(message) { }

        public MarkIsOutOfRangeException(string message, Exception innerException) : base(message, innerException) { }

        public MarkIsOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion
    }
}
