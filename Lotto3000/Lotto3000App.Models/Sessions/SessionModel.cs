using Lotto3000App.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto3000App.Models.Sessions
{
    public class SessionModel
    {
        public int Id { get; set; }
        public DateTime StartOfSession { get; set; } 
        public DateTime EndOfSession { get; set; }
        public TimeSpan Duration { get; set; }
        public List<TicketModel> SubmittedTickets { get; set; } = new List<TicketModel>();
        public List<TicketModel> WinningTickets { get; set; } = new List<TicketModel>();

        public List<int> WinningComboList { get; set; } = new List<int>();

       
        public void EndSession()
        {
            EndOfSession = DateTime.UtcNow;
            Duration = EndOfSession - StartOfSession;
        }

        
    }
}
