using Lotto3000App.DataAccess.Interfaces;
using Lotto3000App.Domain.Models;
using Lotto3000App.Mappers;
using Lotto3000App.Models.Tickets;
using Lotto3000App.Services.Interfaces;
using Lotto3000App.Shared.CustomEntities;
using Lotto3000App.Shared.Exceptions;
using Lotto3000App.Validators;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto3000App.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private IRepository<Ticket> _ticketRepository;
        private IUserRepository<Player> _playerRepository;

        


        public TicketService(IRepository<Ticket> ticketRepository, IUserRepository<Player> playerRepository)
        {
            _ticketRepository = ticketRepository;
            _playerRepository = playerRepository;

        }
        public void AddTicket(TicketModel ticketModel)
        {
            Player playerDb = _playerRepository.GetById(ticketModel.PlayerId);
            if(playerDb == null)
                throw new NotFoundException($"No player with id {ticketModel.PlayerId} was found!");

            TicketValidation.ValidateLottoNum(ticketModel);
            Ticket ticketDb = ticketModel.ToTicket();
            _ticketRepository.Add(ticketDb);
        }

        public void DeleteTicket(int id)
        {
            Ticket ticketDb = _ticketRepository.GetById(id);
            if (ticketDb == null)
                throw new NotFoundException($"Ticket with Id {id} was not found.");

            _ticketRepository.Delete(ticketDb);
        }

        public List<TicketModel> GetAllTickets()
        {
            List<Ticket> ticketsDb = _ticketRepository.GetAll();
            List<TicketModel> ticketModels = new List<TicketModel>();
            foreach (Ticket ticket in ticketsDb)
            {
                ticketModels.Add(ticket.ToTicketModel());
            }
            return ticketModels;
        }

        public TicketModel GetTicketById(int id)
        {
            Ticket ticketDb = _ticketRepository.GetById(id);
            if (ticketDb == null)
                throw new NotFoundException($"Ticket with id {id} was not found.");

            return ticketDb.ToTicketModel();
        }

        public void UpdateTicket(TicketModel ticketModel)
        {
            Ticket ticketDb = _ticketRepository.GetById(ticketModel.Id);
            if (ticketDb == null)
                throw new NotFoundException($"Ticket with id {ticketModel.Id} was not found.");
            Player playerDb = _playerRepository.GetById(ticketModel.PlayerId);
            if (playerDb == null)
                throw new NotFoundException($"No player with id {ticketModel.PlayerId} was found!");

            TicketValidation.ValidateLottoNum(ticketModel);

            ticketDb.EnteredCombination.Number1 = ticketModel.Number1;
            ticketDb.EnteredCombination.Number2 = ticketModel.Number2;
            ticketDb.EnteredCombination.Number3 = ticketModel.Number3;
            ticketDb.EnteredCombination.Number4 = ticketModel.Number4;
            ticketDb.EnteredCombination.Number5 = ticketModel.Number5;
            ticketDb.EnteredCombination.Number6 = ticketModel.Number6;
            ticketDb.EnteredCombination.Number7 = ticketModel.Number7;

            ticketDb.PlayerId = playerDb.Id;
            ticketDb.Player = playerDb;
            _ticketRepository.Update(ticketDb);
        }
    }
}
