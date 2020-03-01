using System;

namespace Core.CustomExceptions
{
    /// <summary>
    /// Act as a base class to handle exceptions
    /// </summary>
    public class BaseException : Exception
    {
        /// <summary>
        /// throw exception with message only
        /// </summary>
        /// <param name="errorMessage"></param>
        public BaseException(string errorMessage) : base(message: errorMessage)
        { }

        /// <summary>
        /// throw exception with message and inner exception
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="innerException"></param>
        public BaseException(string errorMessage, Exception innerException) : base(errorMessage, innerException)
        { }
    }
}
