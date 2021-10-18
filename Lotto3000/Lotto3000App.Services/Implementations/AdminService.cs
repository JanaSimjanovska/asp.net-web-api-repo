using Lotto3000App.DataAccess.Interfaces;
using Lotto3000App.Domain.Models;
using Lotto3000App.Models.Users;
using Lotto3000App.Services.Interfaces;
using Lotto3000App.Shared.CustomEntities;
using Lotto3000App.Shared.Exceptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Lotto3000App.Mappers;
using Lotto3000App.Validators;

namespace Lotto3000App.Services.Implementations
{
    public class AdminService : IUserService<Admin>
    {
        private IUserRepository<Admin> _adminRepository;
        private IOptions<AppSettings> _options;

        public AdminService(IUserRepository<Admin> adminRepository, IOptions<AppSettings> options)
        {
            _adminRepository = adminRepository;
            _options = options;
        }
        public string Login(LoginUserModel loginUserModel)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(loginUserModel.Password));
            var hashedPassword = Encoding.ASCII.GetString(md5Data);

            Admin adminDb = (Admin)_adminRepository.LoginUser(loginUserModel.Username, hashedPassword);
            if (adminDb == null)
                throw new NotFoundException($"No such admin! Username and/or password incorrect!");

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_options.Value.SecretKey);
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim(ClaimTypes.Name, adminDb.Username),
                        new Claim(ClaimTypes.NameIdentifier, adminDb.Id.ToString()),
                        new Claim("userFullName", $"{adminDb.FirstName} {adminDb.LastName}"),
                    }
                )
            };
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string tokenString = jwtSecurityTokenHandler.WriteToken(token);
            return tokenString;
        }

        public void Register(RegisterUserModel registerUserModel)
        {
            UserValidation.ValidateUser(registerUserModel);

            if (!(_adminRepository.GetUserByUsername(registerUserModel.Username) == null))
            {
                throw new UserException("An admin with this username already exists!");
            }

            Admin newAdmin = registerUserModel.ToAdmin();
            _adminRepository.Add(newAdmin);

        }
    }
}
