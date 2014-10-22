using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksList.Model
{
    using System;

    public class InvalidBookDataException : Exception
    {
        public InvalidBookDataException()
        {
        }

        public InvalidBookDataException(string message)
            : base(message)
        {
        }

        public InvalidBookDataException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
