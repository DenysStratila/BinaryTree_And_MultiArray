using System;
using System.Runtime.Serialization;

namespace Task_001
{
    public class ComparatorHasNotBeenFoundException: Exception
    {
        #region Constructors

        public ComparatorHasNotBeenFoundException() { }

        public ComparatorHasNotBeenFoundException(string message) : base(message) { }

        public ComparatorHasNotBeenFoundException(string message, Exception innerException) : base(message, innerException) { }

        public ComparatorHasNotBeenFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion
    }
}
