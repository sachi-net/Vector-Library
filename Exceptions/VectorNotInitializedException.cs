using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorLibrary.Exceptions
{
    public class VectorNotInitializedException : Exception
    {
        public VectorNotInitializedException()
        {

        }

        public VectorNotInitializedException(string message) : base(message)
        {

        }

        public VectorNotInitializedException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
