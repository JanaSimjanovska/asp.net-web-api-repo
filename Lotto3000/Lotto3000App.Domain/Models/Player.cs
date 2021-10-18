using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto3000App.Domain.Models
{
    public class Player : User
    {
        public List<Ticket> SubmittedTickets { get; set; } = new List<Ticket>();
        public override bool IsAdmin { get; } = false;

     
    }
}
