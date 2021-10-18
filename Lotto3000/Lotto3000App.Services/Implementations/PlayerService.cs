using Lotto3000App.DataAccess.Interfaces;
using Lotto3000App.Domain.Models;
using Lotto3000App.Mappers;
using Lotto3000App.Models.Sessions;
using Lotto3000App.Models.Tickets;
using Lotto3000App.Models.Users;
using Lotto3000App.Services.Interfaces;
using Lotto3000App.Shared.CustomEntities;
using Lotto3000App.Shared.Exceptions;
using Lotto3000App.Validators;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Lotto3000App.Services.Implementations
{
    public class PlayerService : IPlayerService
    {
        private IUserRepository<Player> _playerRepository;
        private ISessionService _sessionService;
        private ITicketService _ticketService;
        private IOptions<AppSettings> _options;

        public PlayerService(IUserRepository<Player> playerRepository, ISessionService sessionService, ITicketService ticketService, IOptions<AppSettings> options)
        {
            _playerRepository = playerRepository;
            _sessionService = sessionService;
            _ticketService = ticketService;
            _options = options;
        }

        public string Login(LoginUserModel loginUserModel)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(loginUserModel.Password));
            var hashedPassword = Encoding.ASCII.GetString(md5Data);

            Player playerDb = (Player)_playerRepository.LoginUser(loginUserModel.Username, hashedPassword);
            if (playerDb == null)
                throw new NotFoundException($"No such player. Incorrect username and/or password.");

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
                        new Claim(ClaimTypes.Role, "Player"),
                        new Claim(ClaimTypes.Name, playerDb.Username),
                        new Claim(ClaimTypes.NameIdentifier, playerDb.Id.ToString()),
                        new Claim("userFullName", $"{playerDb.FirstName} {playerDb.LastName}")
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

            if (!(_playerRepository.GetUserByUsername(registerUserModel.Username) == null))
            {
                throw new UserException("A player with this username already exists!");
            }
            Player newPlayer = registerUserModel.ToPlayer();
            _playerRepository.Add(newPlayer);
        }
        public List<int> GetWinningCombination(int id)
        {
            return _sessionService.GetSessionById(id).WinningComboList;
        }

        public void SubmitTicket(TicketModel newTicketModel)
        {
            Player player = _playerRepository.GetById(newTicketModel.PlayerId);
            if (player == null)
                throw new NotFoundException($"There is no player with id {newTicketModel.PlayerId}!");

            if (_sessionService.GetAllSessions().LastOrDefault() == null)
                throw new NotFoundException("No session has been instantiated yet.");

            if (newTicketModel.SessionId != 0)
                throw new TicketException("SessionId must not be set!");


            newTicketModel.SessionId = _sessionService.GetAllSessions().LastOrDefault().Id;
            newTicketModel.GetCounter(GetWinningCombination(newTicketModel.SessionId));
            newTicketModel.GetPrize();
            

            _ticketService.AddTicket(newTicketModel);
        }

        public List<WinnerModel> GetWinners()
        {
            var sessions = _sessionService.GetAllSessions();
            if(sessions == null || sessions.Count == 0)
                throw new NotFoundException("No sessions have been initiated. Try again later.");

            if (sessions.Count == 1)
                throw new NotFoundException("First session still hasn't finished. Try again later.");

            SessionModel sessionBeforeLast = sessions[sessions.Count - 2];
            if (sessionBeforeLast == null)
                throw new NotFoundException("Please wait untill next draw.");

            List<TicketModel> winningTickets = _ticketService.GetAllTickets().Where(x => x.IsWinning && x.SessionId == sessionBeforeLast.Id).ToList();

            if (winningTickets == null || winningTickets.Count == 0)
                throw new NotFoundException("There were no winning tickets in the last session.");
            
            List<WinnerModel> winners = new List<WinnerModel>();

            foreach (TicketModel winningTicket in winningTickets)
            {
                winners.Add(new WinnerModel
                {
                    FirstName = _playerRepository.GetById(winningTicket.PlayerId).FirstName,
                    LastName = _playerRepository.GetById(winningTicket.PlayerId).LastName,
                    Username = _playerRepository.GetById(winningTicket.PlayerId).Username,
                    WinningCombination = winningTicket.EnteredCombinationList,
                    Prize = winningTicket.Prize

                }); ;
            }
            return winners;
        }
    }
}
