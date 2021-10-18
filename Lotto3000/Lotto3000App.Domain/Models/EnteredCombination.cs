using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto3000App.Domain.Models
{
    public class EnteredCombination : BaseEntity
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Number3 { get; set; }
        public int Number4 { get; set; }
        public int Number5 { get; set; }
        public int Number6 { get; set; }
        public int Number7 { get; set; }

        public List<int> EnteredNumbersList { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public EnteredCombination()
        {
            EnteredNumbersList = new List<int> { Number1, Number2, Number3, Number4, Number5, Number6, Number7 };
        }
    }
}
