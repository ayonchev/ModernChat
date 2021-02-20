using ModernChat.Domain.Entities.Enums;

namespace ModernChat.Models.InputModels.Chat
{
    public class ChatInputModel
    {
        public ChatType Type { get; set; }
        public ChatDetailsInputModel Details { get; set; }
        public int[] ParticipantIds { get; set; }
    }
}
