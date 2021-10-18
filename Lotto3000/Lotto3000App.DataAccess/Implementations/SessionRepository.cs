using Lotto3000App.DataAccess.Interfaces;
using Lotto3000App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lotto3000App.DataAccess.Implementations
{
    public class SessionRepository : IRepository<Session>
    {
        private Lotto3000AppDbContext _lotto3000AppDbContext;
        public SessionRepository(Lotto3000AppDbContext lotto3000AppDbContext)
        {
            _lotto3000AppDbContext = lotto3000AppDbContext;
        }
        public void Add(Session entity)
        {
            
            _lotto3000AppDbContext.Sessions.Add(entity);
            _lotto3000AppDbContext.SaveChanges();
        }

        public void Delete(Session entity)
        {
            _lotto3000AppDbContext.Sessions.Remove(entity);
            _lotto3000AppDbContext.SaveChanges();
        }
    

        public List<Session> GetAll()
        {
            return _lotto3000AppDbContext
                .Sessions
                .Include(x => x.WinningCombination)
                .ToList();
        }

        public Session GetById(int id)
        {
            return _lotto3000AppDbContext
                .Sessions
                .Include(x => x.WinningCombination)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Session entity)
        {
            _lotto3000AppDbContext.Sessions.Update(entity);
            _lotto3000AppDbContext.SaveChanges();
        }
    }
}
