using Lotto3000App.Domain.Models;
using Lotto3000App.Models.Users;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Lotto3000App.Mappers
{
    public static class AdminMapper
    {
        public static Admin ToAdmin(this RegisterUserModel registerUserModel)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(registerUserModel.Password));
            var hashedPassword = Encoding.ASCII.GetString(md5Data);
            return new Admin()
            {
                FirstName = registerUserModel.FirstName,
                LastName = registerUserModel.LastName,
                Username = registerUserModel.Username,
                Password = hashedPassword
            };
        }

    }
}
