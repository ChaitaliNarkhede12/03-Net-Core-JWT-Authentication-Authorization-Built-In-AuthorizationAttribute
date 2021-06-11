using ApplicationLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreJWTAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenAppService _tokenAppService;
        public AuthenticationController(ITokenAppService tokenAppService)
        {
            _tokenAppService = tokenAppService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var token = await _tokenAppService.Login(loginViewModel.UserName, loginViewModel.Password);
            return Ok(token);
        }
    }
}
