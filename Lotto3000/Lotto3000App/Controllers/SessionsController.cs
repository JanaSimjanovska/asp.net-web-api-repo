using Lotto3000App.Models.Sessions;
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
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private ISessionService _sessionService;
        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost("initiate")]
        public IActionResult InitiateSession()
        {
            try
            {
                _sessionService.InitiateSession();
                return StatusCode(StatusCodes.Status201Created, "New session successfully created.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("draw")]
        public IActionResult InitiateDraw()
        {
            try
            {
                _sessionService.InitiateDraw();
                return StatusCode(StatusCodes.Status201Created, "Successful draw. Old session ended. New session open.");
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
       
    }
}
