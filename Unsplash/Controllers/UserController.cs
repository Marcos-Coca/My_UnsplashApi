using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unsplash.Models.Request;
using Unsplash.Services;

namespace Unsplash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]

        public IActionResult Authenticate([FromBody] AuthRequest model)
        {
            var userResponse = _userService.Auth(model);

            if (userResponse == null) 
                return BadRequest("Email or Password Incorrect");

            return Ok(userResponse);

        }
    }
}
