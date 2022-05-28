using System;

namespace VectorLibrary.Exceptions
{
    /// <summary>
    /// Exception when attempt to divide vector by 1:1 external division.
    /// </summary>
    public class ZeroExternalDivisionException : Exception
    {
        /// <summary>
        /// Initialize an instance of ZeroExternalDivisionException.
        /// </summary>
        public ZeroExternalDivisionException()
        {

        }

        /// <summary>
        /// Initialize an instance of ZeroExternalDivisionException with specified message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        public ZeroExternalDivisionException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initialize an instance of ZeroExternalDivisionException with specified message and exception that cause this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference.</param>
        public ZeroExternalDivisionException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
