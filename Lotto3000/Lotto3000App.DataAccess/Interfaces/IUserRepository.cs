using Lotto3000App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto3000App.DataAccess.Interfaces
{
    public interface IUserRepository<T> : IRepository<T> where T : User 
    {
        User GetUserByUsername(string username);
        User LoginUser(string username, string password);
    }
}
