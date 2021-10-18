using Lotto3000App.Domain.Models;
using Lotto3000App.Models.Sessions;
using Lotto3000App.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto3000App.Services.Interfaces
{
    public interface ISessionService 
    {
        List<SessionModel> GetAllSessions();
        SessionModel GetSessionById(int id);
        void InitiateSession();
        void AddSession(SessionModel sessionModel);
        void InitiateDraw();
        void DeleteSession(int id);
        void UpdateSession(SessionModel session);
        //List<int> GetWinningCombination();
    }
}
