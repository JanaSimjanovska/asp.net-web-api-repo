using Lotto3000App.DataAccess.Implementations;
using Lotto3000App.DataAccess.Interfaces;
using Lotto3000App.Domain.Models;
using Lotto3000App.Models.Sessions;
using Lotto3000App.Services.Interfaces;
using Lotto3000App.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Lotto3000App.Mappers;
using Lotto3000App.Models.Tickets;
using System.Linq;

namespace Lotto3000App.Services.Implementations
{
    public class SessionService : ISessionService
    {
        private IRepository<Session> _sessionRepository;
        private IRepository<Ticket> _ticketRepository;
        private ITicketService _ticketService;
        public SessionService(IRepository<Session> sessionRepository, IRepository<Ticket> ticketRepository, ITicketService ticketService)
        {
            _sessionRepository = sessionRepository;
            _ticketRepository = ticketRepository;
            _ticketService = ticketService;
        }

        public void AddSession(SessionModel sessionModel)
        {
            _sessionRepository.Add(sessionModel.ToSession());
        }

        public List<SessionModel> GetAllSessions()
        {
            List<Session> sessionsDb = _sessionRepository.GetAll();
            List<SessionModel> sessionModels = new List<SessionModel>();
            foreach (Session session in sessionsDb)
            {
                sessionModels.Add(session.ToSessionModel());
            }
            return sessionModels;
        }

        public SessionModel GetSessionById(int id)
        {
            Session sessionDb = _sessionRepository.GetById(id);
            if (sessionDb == null)
                throw new NotFoundException($"Session with id {id} was not found.");
            return sessionDb.ToSessionModel();
        }

        public void DeleteSession(int id)
        {
            Session sessionDb = _sessionRepository.GetById(id);
            if (sessionDb == null)
                throw new NotFoundException($"Session with id {id} was not found.");
            _sessionRepository.Delete(sessionDb);
        }
        public void UpdateSession(SessionModel sessionModel)
        {
            Session sessionDb = _sessionRepository.GetById(sessionModel.Id);
            if (sessionDb == null)
                throw new NotFoundException($"Session with id {sessionModel.Id} was not found.");
           
            sessionDb.EndOfSession = sessionModel.EndOfSession;
            sessionDb.Duration = sessionModel.Duration;
            _sessionRepository.Update(sessionDb);
        }

        public void InitiateDraw()
        {
            SessionModel sessionModel = GetAllSessions().LastOrDefault();

            if (sessionModel == null)
                throw new NotFoundException($"No active session has been found.");

            sessionModel.EndSession();
            sessionModel.SubmittedTickets = _ticketService.GetAllTickets().Where(x => x.SessionId == sessionModel.Id).ToList();

            UpdateSession(sessionModel);

            InitiateSession();
        }

     

        public void InitiateSession()
        {
            SessionModel sessionModel = new SessionModel();
            AddSession(sessionModel);
        }

        

        
    }
}
