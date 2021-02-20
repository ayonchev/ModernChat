using System;
using System.Collections.Generic;
using System.Text;

namespace ModernChat.Domain.Entities
{
    public class ChatParticipant
    {
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
