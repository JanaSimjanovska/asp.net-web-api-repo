using Lotto3000App.Domain.Models;
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
    public class AdminsController : ControllerBase
    {
        private IUserService<Admin> _adminService;

        public AdminsController(IUserService<Admin> adminService)
        {
            _adminService = adminService;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public IActionResult RegisterAdmin([FromBody] RegisterUserModel registerUserModel)
        {
            try
            {
                _adminService.Register(registerUserModel);
                return StatusCode(StatusCodes.Status200OK, "New admin successfully registered.");
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
        public ActionResult<string> LoginAdmin([FromBody] LoginUserModel loginUserModel)
        {
            try
            {
                string token = _adminService.Login(loginUserModel);
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
    }
}
