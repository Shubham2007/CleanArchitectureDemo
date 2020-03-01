using System;

namespace Core.CustomExceptions
{
    /// <summary>
    /// Used to throw exception when diary note date is less than today date
    /// </summary>
    class MinimumDateException : BaseException
    {
        /// <summary>
        /// throw exception with message only
        /// </summary>
        /// <param name="errorMessage"></param>
        public MinimumDateException(string errorMessage) : base(errorMessage)
        { }

        /// <summary>
        /// throw exception with message and inner exception
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="innerException"></param>
        public MinimumDateException(string errorMessage, Exception innerException) : base(errorMessage, innerException)
        { }
    }
}
