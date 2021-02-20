using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernChat.Services.Interfaces;

namespace ModernChat.API.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpGet("{chatId}")]
        public async Task<IActionResult> Get(int chatId)
        {
            var messages = await messageService.Get(chatId);

            return Ok(messages);
        }
    }
}
