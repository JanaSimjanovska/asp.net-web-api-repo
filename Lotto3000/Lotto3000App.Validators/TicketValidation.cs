using Lotto3000App.Models.Tickets;
using Lotto3000App.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lotto3000App.Validators
{
    public static class TicketValidation
    {
        public static void ValidateLottoNum(TicketModel ticketModel)
        {
            List<int> ticketCombination = new List<int>();
            foreach (PropertyInfo property in ticketModel.GetType().GetProperties())
            {
                if (property.Name == "PlayerId" || property.Name == "Id" || property.Name == "SessionId" || property.Name == "Prize" || property.Name == "EnteredCombinationList" || property.Name == "IsWinning" || property.Name == "MatchingNumbersCounter")
                    continue;
                int propValue = (int)property.GetValue(ticketModel, null);

                if (propValue < 1 || propValue > 37)
                    throw new TicketException("Entered number must be 1 - 37!");

                ticketCombination.Add(propValue);
            }

            if (ticketCombination.Distinct().Count() != ticketCombination.Count) 
                throw new TicketException("Numbers must not repeat. Please enter 7 different numbers");

        }
    }
}
