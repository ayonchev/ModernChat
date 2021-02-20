using System.Collections.Generic;
using ModernChat.Domain.Entities.Enums;

namespace ModernChat.Domain.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public ChatType Type { get; set; }
        public int? ChatDetailsId { get; set; }
        public ChatDetails Details { get; set; }
        public List<Message> Messages { get; set; }
        public List<ChatParticipant> Participants { get; set; }
    }
}
