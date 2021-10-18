using Lotto3000App.DataAccess.Interfaces;
using Lotto3000App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lotto3000App.DataAccess.Implementations
{
    public class AdminRepository : IUserRepository<Admin>
    {
        private Lotto3000AppDbContext _lotto3000AppDbContext;

        public AdminRepository(Lotto3000AppDbContext lotto3000AppDbContext)
        {
            _lotto3000AppDbContext = lotto3000AppDbContext;
        }

        public void Add(Admin entity)
        {
            _lotto3000AppDbContext.Admins.Add(entity);
            _lotto3000AppDbContext.SaveChanges();
        }

        public void Delete(Admin entity)
        {
            _lotto3000AppDbContext.Admins.Remove(entity);
            _lotto3000AppDbContext.SaveChanges();
        }

        public List<Admin> GetAll()
        {
            return _lotto3000AppDbContext.Admins.ToList();
        }

        public Admin GetById(int id)
        {
            return _lotto3000AppDbContext
                .Admins
                .FirstOrDefault(x => x.Id == id);
        }
       
        public void Update(Admin entity)
        {
            _lotto3000AppDbContext.Admins.Update(entity);
            _lotto3000AppDbContext.SaveChanges();
        }

        public User GetUserByUsername(string username)
        {
            return _lotto3000AppDbContext
                .Admins
                .FirstOrDefault(x => x.Username == username);
        }

        public User LoginUser(string username, string password)
        {
            return _lotto3000AppDbContext
                .Admins
                .FirstOrDefault(x => x.Username == username && x.Password == password);
        }

    }
}
