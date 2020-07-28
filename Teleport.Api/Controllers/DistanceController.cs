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
    [Authorize]
    public class DistanceController : ControllerBase
    {
        private readonly IDistanceService _distanceService; 
        public DistanceController(IDistanceService distanceService)
        {
            _distanceService = distanceService;
        }

        /// <summary>
        /// calculate two airport measure. Default return type value is mile. ReturnValueType property is optional. 
        /// you can set as km or mile or send null.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("CalculateDistance")]
        [Authorize]
        //[AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterAsync([FromBody] CalculateRequestModel rqModel)
        {
            Result result = await _distanceService.CalculateDistanceAsync(rqModel);
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }
            return Ok(result);
        }
    }
}
