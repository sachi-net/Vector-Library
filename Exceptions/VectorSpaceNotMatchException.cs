using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorLibrary.Exceptions
{
    public class VectorSpaceNotMatchException : Exception
    {
        public VectorSpaceNotMatchException()
        {

        }

        public VectorSpaceNotMatchException(string message) : base(message)
        {

        }

        public VectorSpaceNotMatchException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
