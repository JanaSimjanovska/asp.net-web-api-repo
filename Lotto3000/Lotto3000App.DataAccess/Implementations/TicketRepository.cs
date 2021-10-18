using Lotto3000App.DataAccess.Interfaces;
using Lotto3000App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lotto3000App.DataAccess.Implementations
{
    public class TicketRepository : IRepository<Ticket>
    {
        private Lotto3000AppDbContext _lotto3000AppDbContext;
        public TicketRepository(Lotto3000AppDbContext lotto3000AppDbContext)
        {
            _lotto3000AppDbContext = lotto3000AppDbContext;
        }
        public void Add(Ticket entity)
        {
            _lotto3000AppDbContext.Tickets.Add(entity);
            _lotto3000AppDbContext.SaveChanges();
        }

        public void Delete(Ticket entity)
        {
            _lotto3000AppDbContext.Tickets.Remove(entity);
            _lotto3000AppDbContext.SaveChanges();
        }

        public List<Ticket> GetAll()
        {
            return _lotto3000AppDbContext
                .Tickets
                .Include(x => x.Player)
                .Include(x => x.EnteredCombination)
                .ToList();
        }

        public Ticket GetById(int id)
        {
            return _lotto3000AppDbContext
                .Tickets
                .Include(x => x.Player)
                .Include(x => x.EnteredCombination)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Ticket entity)
        {
            _lotto3000AppDbContext.Tickets.Update(entity);
            _lotto3000AppDbContext.SaveChanges();
        }
    }
}
