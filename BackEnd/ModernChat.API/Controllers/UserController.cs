using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernChat.Services;
using ModernChat.Services.Interfaces;

namespace ModernChat.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly CurrentUserService currentUser;

        public UserController(IUserService userService, CurrentUserService currentUser)
        {
            this.userService = userService;
            this.currentUser = currentUser;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await userService.GetUsers(currentUser.GetId());

            return Ok(users);
        }
    }
}
