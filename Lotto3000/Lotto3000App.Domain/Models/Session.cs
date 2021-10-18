using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto3000App.Domain.Models
{

    public class Session : BaseEntity
    {
        public DateTime StartOfSession { get; set; } = DateTime.UtcNow;
        public DateTime EndOfSession { get; set; }
        public TimeSpan Duration { get; set; } 
        public List<Ticket> SubmittedTickets { get; set; } = new List<Ticket>();
        public List<Ticket> WinningTickets { get; set; } = new List<Ticket>();

        public WinningCombination WinningCombination { get; set; } = new WinningCombination();

        //public List<int> WinningCombo = new List<int>();

        
    }
}
