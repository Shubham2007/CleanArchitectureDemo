using System;

namespace Core.CustomExceptions
{
    /// <summary>
    /// Used to throw exception when user id is not valid
    /// </summary>
    public class InvalidUserIdException : BaseException
    {
        /// <summary>
        /// throw exception with message only
        /// </summary>
        /// <param name="errorMessage"></param>
        public InvalidUserIdException(string errorMessage) : base(errorMessage)
        { }

        /// <summary>
        /// throw exception with message and inner exception
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="innerException"></param>
        public InvalidUserIdException(string errorMessage, Exception innerException) : base(errorMessage, innerException)
        { }
    }
}
