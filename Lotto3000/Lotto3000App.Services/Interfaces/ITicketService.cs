using Lotto3000App.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto3000App.Services.Interfaces
{

    public interface ITicketService
    {
        List<TicketModel> GetAllTickets();
        TicketModel GetTicketById(int id);
        void AddTicket(TicketModel ticketModel);
        void UpdateTicket(TicketModel ticketModel);
        void DeleteTicket(int id);
    }
}
