using Lotto3000App.Domain.Models;
using Lotto3000App.Models.Sessions;
using Lotto3000App.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto3000App.Mappers
{
    public static class SessionMapper
    {
        public static SessionModel ToSessionModel(this Session session)
        {
            SessionModel sessionModel = new SessionModel();

            sessionModel.Id = session.Id;
            sessionModel.StartOfSession = session.StartOfSession;
            sessionModel.EndOfSession = session.EndOfSession;
            sessionModel.Duration = session.Duration;

            foreach (Ticket ticket in session.SubmittedTickets)
            {
                sessionModel.SubmittedTickets.Add(ticket.ToTicketModel());
            }
            foreach (Ticket ticket in session.WinningTickets)
            {
                sessionModel.WinningTickets.Add(ticket.ToTicketModel());
            }
            sessionModel.WinningComboList = new List<int>()
            {   session.WinningCombination.Number1,
                session.WinningCombination.Number2,
                session.WinningCombination.Number3,
                session.WinningCombination.Number4,
                session.WinningCombination.Number5,
                session.WinningCombination.Number6,
                session.WinningCombination.Number7,
                session.WinningCombination.Number8
            };

            return sessionModel;
        }

        public static Session ToSession(this SessionModel sessionModel)
        {
            Session sessionDb = new Session();

            sessionDb.EndOfSession = sessionModel.EndOfSession;
            sessionDb.Duration = sessionModel.Duration;
            sessionDb.WinningCombination.GetWinningCombination(sessionDb.WinningCombination.WinningComboList);
            sessionDb.WinningCombination.SetNumbers();

            //foreach (TicketModel ticketModel in sessionModel.SubmittedTickets)
            //{
            //    sessionDb.SubmittedTickets.Add(ticketModel.ToTicket());
            //}
            //foreach (TicketModel ticketModel in sessionModel.WinningTickets)
            //{
            //    sessionDb.WinningTickets.Add(ticketModel.ToTicket());
            //}


            return sessionDb;
        }

    }
}
