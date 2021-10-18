using Lotto3000App.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lotto3000App.Domain.Models
{
    public class Ticket : BaseEntity
    {
        public Session Session { get; set; }
        public int SessionId { get; set; }
        public EnteredCombination EnteredCombination { get; set; } = new EnteredCombination();
        public Player Player { get; set; }
        public int PlayerId { get; set; }
        public int MatchingNumbersCounter { get; set; } 
        public Prize Prize { get; set; }
       
        public bool IsWinning { get; set; }




    }
}