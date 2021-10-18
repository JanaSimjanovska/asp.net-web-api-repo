using Lotto3000App.DataAccess.Interfaces;
using Lotto3000App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lotto3000App.DataAccess.Implementations
{
    public class PlayerRepository : IUserRepository<Player>
    {
        private Lotto3000AppDbContext _lotto3000AppDbContext;

        public PlayerRepository(Lotto3000AppDbContext lotto3000AppDbContext)
        {
            _lotto3000AppDbContext = lotto3000AppDbContext;
        }

        public void Add(Player entity)
        {
            _lotto3000AppDbContext.Players.Add(entity);
            _lotto3000AppDbContext.SaveChanges();

        }

        public void Delete(Player entity)
        {
            _lotto3000AppDbContext.Players.Remove(entity);
            _lotto3000AppDbContext.SaveChanges();
        }

        public List<Player> GetAll()
        {
            return _lotto3000AppDbContext.Players.ToList();
        }

        public Player GetById(int id)
        {
            return _lotto3000AppDbContext
                .Players
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Player entity)
        {
            _lotto3000AppDbContext.Players.Update(entity);
            _lotto3000AppDbContext.SaveChanges();

        }

        public User GetUserByUsername(string username)
        {
            return _lotto3000AppDbContext
                .Players
                .FirstOrDefault(x => x.Username == username);
        }

        public User LoginUser(string username, string password)
        {
            return _lotto3000AppDbContext
                .Players
                .FirstOrDefault(x => x.Username == username && x.Password == password);
        }
    }
}
