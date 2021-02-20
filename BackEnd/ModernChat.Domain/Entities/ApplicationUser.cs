using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernChat.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public List<Message> Messages { get; set; }
        public List<ChatParticipant> Chats { get; set; }
    }
}
