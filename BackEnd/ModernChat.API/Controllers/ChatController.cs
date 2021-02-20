using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernChat.Models.InputModels.Chat;
using ModernChat.Services;
using ModernChat.Services.Interfaces;

namespace ModernChat.API.Controllers
{
    [Route("api/chats")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService chatService;
        private readonly CurrentUserService currentUser;

        public ChatController(IChatService chatService, CurrentUserService currentUser)
        {
            this.chatService = chatService;
            this.currentUser = currentUser;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool userChatsOnly)
        {
            var chats = await chatService.GetChats(userChatsOnly, currentUser.GetId());

            return Ok(chats);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChatInputModel inputModel)
        {
            await chatService.Create(inputModel, currentUser.GetId());

            return Ok();
        }
    }
}
