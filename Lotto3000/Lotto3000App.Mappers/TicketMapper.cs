using Lotto3000App.Domain.Models;
using Lotto3000App.Models.Tickets;
using Lotto3000App.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto3000App.Mappers
{
    public static class TicketMapper
    {
        public static Ticket ToTicket(this TicketModel ticketModel)
        {
            Ticket ticketDb = new Ticket();
            ticketDb.PlayerId = ticketModel.PlayerId;

            ticketDb.EnteredCombination.Number1 = ticketModel.Number1;
            ticketDb.EnteredCombination.Number2 = ticketModel.Number2;
            ticketDb.EnteredCombination.Number3 = ticketModel.Number3;
            ticketDb.EnteredCombination.Number4 = ticketModel.Number4;
            ticketDb.EnteredCombination.Number5 = ticketModel.Number5;
            ticketDb.EnteredCombination.Number6 = ticketModel.Number6;
            ticketDb.EnteredCombination.Number7 = ticketModel.Number7;
            ticketDb.EnteredCombination.EnteredNumbersList = ticketModel.EnteredCombinationList;
            ticketDb.IsWinning = ticketModel.IsWinning;
            ticketDb.Prize = ticketModel.Prize;
            ticketDb.MatchingNumbersCounter = ticketModel.MatchingNumbersCounter;
            ticketDb.SessionId = ticketModel.SessionId;

            

            return ticketDb;

        }

        public static TicketModel ToTicketModel(this Ticket ticket)
        {

            return new TicketModel()
            {
                PlayerId = ticket.PlayerId,
                Number1 = ticket.EnteredCombination.Number1,
                Number2 = ticket.EnteredCombination.Number2,
                Number3 = ticket.EnteredCombination.Number3,
                Number4 = ticket.EnteredCombination.Number4,
                Number5 = ticket.EnteredCombination.Number5,
                Number6 = ticket.EnteredCombination.Number6,
                Number7 = ticket.EnteredCombination.Number7,
                MatchingNumbersCounter = ticket.MatchingNumbersCounter,
                EnteredCombinationList = new List<int>()
                {
                    ticket.EnteredCombination.Number1,
                    ticket.EnteredCombination.Number2,
                    ticket.EnteredCombination.Number3,
                    ticket.EnteredCombination.Number4,
                    ticket.EnteredCombination.Number5,
                    ticket.EnteredCombination.Number6,
                    ticket.EnteredCombination.Number7,
                },
                IsWinning = ticket.IsWinning,
                Prize = ticket.Prize,
                SessionId = ticket.SessionId
                

            };

        }
    }
}
