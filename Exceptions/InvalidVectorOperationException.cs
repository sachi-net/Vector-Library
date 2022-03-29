using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorLibrary.Exceptions
{
    public class InvalidVectorOperationException : Exception
    {
        public InvalidVectorOperationException()
        {

        }

        public InvalidVectorOperationException(string message) : base(message)
        {

        }

        public InvalidVectorOperationException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
