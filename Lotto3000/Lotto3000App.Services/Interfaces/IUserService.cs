using Lotto3000App.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto3000App.Services.Interfaces
{
    public interface IUserService<T>
    {
        void Register(RegisterUserModel registerUserModel);
        string Login(LoginUserModel loginUserModel);
    }
}
