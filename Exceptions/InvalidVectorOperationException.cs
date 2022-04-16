using System;

namespace VectorLibrary.Exceptions
{
    /// <summary>
    /// Exception when the vector is unable to perform the iven operation.
    /// </summary>
    public class InvalidVectorOperationException : Exception
    {
        /// <summary>
        /// Initialize an instance of InvalidVectorOperationException.
        /// </summary>
        public InvalidVectorOperationException()
        {

        }

        /// <summary>
        /// Initialize an instance of InvalidVectorOperationException with specified message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        public InvalidVectorOperationException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initialize an instance of InvalidVectorOperationException with specified message and exception that cause this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference.</param>
        public InvalidVectorOperationException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
