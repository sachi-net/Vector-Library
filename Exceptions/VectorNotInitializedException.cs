using System;

namespace VectorLibrary.Exceptions
{
    /// <summary>
    /// Exception when the vector is not initialized.
    /// </summary>
    public class VectorNotInitializedException : Exception
    {
        /// <summary>
        /// Initialize an instance of VectorNotInitializedException.
        /// </summary>
        public VectorNotInitializedException()
        {

        }

        /// <summary>
        /// Initialize an instance of VectorNotInitializedException with specified message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        public VectorNotInitializedException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initialize an instance of VectorNotInitializedException with specified message and exception that cause this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for this exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference.</param>
        public VectorNotInitializedException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
