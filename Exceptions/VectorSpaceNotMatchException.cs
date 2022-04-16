using System;

namespace VectorLibrary.Exceptions
{
    /// <summary>
    /// Exception when the vector operates with non-matching dimensional vector.
    /// </summary>
    public class VectorSpaceNotMatchException : Exception
    {
        /// <summary>
        /// Initialize an instance of VectorSpaceNotMatchException.
        /// </summary>
        public VectorSpaceNotMatchException()
        {

        }

        /// <summary>
        /// Initialize an instance of VectorSpaceNotMatchException with specified message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        public VectorSpaceNotMatchException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initialize an instance of VectorSpaceNotMatchException with specified message and exception that cause this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference.</param>
        public VectorSpaceNotMatchException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
