using hrmSolution.Models.System;
using hrmSolution.Services.SystemServices.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hrmSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var accessToken = await _authenticationService.Login(request);
            if (string.IsNullOrEmpty(accessToken))
            {
                return BadRequest("Thông tin đăng nhập không chính xác");
            }
            return Ok(new { accessToken = accessToken });
        }
    }
}
