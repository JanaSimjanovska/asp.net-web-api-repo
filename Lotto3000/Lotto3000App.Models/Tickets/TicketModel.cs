using Lotto3000App.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lotto3000App.Models.Tickets
{
    public class TicketModel
    {
        public int Id { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Number3 { get; set; }
        public int Number4 { get; set; }
        public int Number5 { get; set; }
        public int Number6 { get; set; }
        public int Number7 { get; set; }
        public bool IsWinning
        {
            get
            {
                return MatchingNumbersCounter > 2 ? true : false;
            }
            set
            {

            }
        }
        
        
        public Prize Prize { get; set; }
        public List<int> EnteredCombinationList
        {
            get
            {
                return new List<int> { Number1, Number2, Number3, Number4, Number5, Number6, Number7 };
            }
            set
            {

            }
        }
        public int MatchingNumbersCounter { get; set; }
        public int PlayerId { get; set; }
        public int SessionId { get; set; }

        public TicketModel()
        {
           
        }

        public int GetCounter(List<int> winningCombo)
        {
            return MatchingNumbersCounter = winningCombo.Where(x => EnteredCombinationList.Any(y => x == y)).Count();
        }

        public void GetPrize()
        {
            if (MatchingNumbersCounter > 2)
            {
                switch (MatchingNumbersCounter)
                {
                    case 3:
                        Prize = Prize.FiftyDollarsGiftCard;
                        break;
                    case 4:
                        Prize = Prize.HundredDollarsGiftCard;
                        break;
                    case 5:
                        Prize = Prize.TV;
                        break;
                    case 6:
                        Prize = Prize.Vacation;
                        break;
                    case 7:
                        Prize = Prize.Car_Jackpot;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
