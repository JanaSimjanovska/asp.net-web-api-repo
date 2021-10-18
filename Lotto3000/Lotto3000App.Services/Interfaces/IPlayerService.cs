using Lotto3000App.Domain.Models;
using Lotto3000App.Mappers;
using Lotto3000App.Models.Tickets;
using Lotto3000App.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto3000App.Services.Interfaces
{
    public interface IPlayerService : IUserService<Player>
    {
        void SubmitTicket(TicketModel ticketModel);
        List<WinnerModel> GetWinners();

    }
}
