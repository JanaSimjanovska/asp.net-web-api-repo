using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto3000App.Shared.Exceptions
{
    public class TicketException : Exception
    {
        public TicketException(string message) : base(message)
        {

        }
    }
}
