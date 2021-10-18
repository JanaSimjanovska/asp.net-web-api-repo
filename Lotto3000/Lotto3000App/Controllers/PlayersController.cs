using Lotto3000App.Domain.Models;
using Lotto3000App.Models.Tickets;
using Lotto3000App.Models.Users;
using Lotto3000App.Services.Interfaces;
using Lotto3000App.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lotto3000App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegisterPlayer([FromBody] RegisterUserModel registerUserModel)
        {
            try
            {
                _playerService.Register(registerUserModel);
                return StatusCode(StatusCodes.Status201Created, "New player successfully registered.");
            }
            catch (UserException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<string> LoginPlayer([FromBody] LoginUserModel loginUserModel)
        {
            try
            {
                string token = _playerService.Login(loginUserModel);
                return StatusCode(StatusCodes.Status200OK, token);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Player")]
        [HttpPost("submit-ticket")]
        public IActionResult SubmitTicket([FromBody] TicketModel ticketModel)
        { 
            
            try
            {
                _playerService.SubmitTicket(ticketModel);

                return StatusCode(StatusCodes.Status201Created, $"New ticket successfully submitted. Enter the following address in the url bar \"http://localhost:24260/api/players/winners\" to see the winners of the last draw. The session id of your ticket is {ticketModel.SessionId}, if the board of winners shows tickets from previous session, wait for the draw, and try the link again later to see if you have won a prize.");
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);

            }
            catch (TicketException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException);

            }
        }

        [AllowAnonymous]
        [HttpGet("winners")]
        public ActionResult<List<WinnerModel>> GetWinners()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _playerService.GetWinners());
            }
            catch(NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

       

    }
}
