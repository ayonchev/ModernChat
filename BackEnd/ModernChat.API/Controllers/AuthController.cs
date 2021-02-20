using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernChat.Models.InputModels.Auth;
using ModernChat.Services.Interfaces;

namespace ModernChat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterIM inputModel)
        {
            string token = await authService.Register(inputModel);

            return Ok(new { Token = token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginIM inputModel)
        {
            string token = await authService.Login(inputModel);

            return Ok(new { Token = token });
        }
    }
}
