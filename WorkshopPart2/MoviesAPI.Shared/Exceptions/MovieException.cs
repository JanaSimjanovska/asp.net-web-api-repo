using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesAPI.Shared.Exceptions
{
    public class MovieException : Exception
    {
        public MovieException(string message) : base(message)
        {

        }
    }
}
