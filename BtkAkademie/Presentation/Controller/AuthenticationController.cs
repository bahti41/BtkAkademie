using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/authentication")]
    [ApiExplorerSettings(GroupName = "v1")]

    public class AuthenticationController:ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthenticationController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody]UserForRegisterDTO userForRegisterDTO)
        {
            var result = await _service.AuthenticationService.RegisterUser(userForRegisterDTO);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);
        }

        [HttpPost("Login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody]UserForAuthenticationDTO userForAuthenticationDTO)
        {
            if(!await _service.AuthenticationService.ValidetaUser(userForAuthenticationDTO))
                return Unauthorized(); 
            var tokenDto = await _service.AuthenticationService.CreateToken(true);

            return Ok(tokenDto);
        }

        [HttpPost("Refresh")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Refresh([FromBody]TokenDTO tokenDto)
        {
            var tokenDtoReturn = await _service.AuthenticationService.RefreshToken(tokenDto);
            return Ok(tokenDtoReturn);
        }
    }
}
 