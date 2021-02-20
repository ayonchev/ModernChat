using ModernChat.Domain.Entities.Enums;

namespace ModernChat.Models.ViewModels.Chat
{
    public class ChatDetailsViewModel
    {
        public int Id { get; set; }
        public AccessType AccessType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string JoinRequirements { get; set; }
    }
}
