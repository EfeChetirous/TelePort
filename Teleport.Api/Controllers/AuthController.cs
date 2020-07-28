using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teleport.Model.Models;
using Teleport.Model.Models.ApiResultModels;
using Teleport.Service.Interfaces;

namespace Teleport.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        
        /// <summary>
        /// Register method
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterModel user)
        {
            Result result = await _userService.RegisterAsync(user);
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(result);
        }

        /// <summary>
        /// Login method
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] UserLoginModel userLogin)
        {
            var result = await _userService.AuthenticateAsync(userLogin.UserName, userLogin.Password);
            if (!result.Success)
            {
                return Unauthorized(new { message = "invalid username and password!" });
            }
            return Ok(result);
        }
    }
}
