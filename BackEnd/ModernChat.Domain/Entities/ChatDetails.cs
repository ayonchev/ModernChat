using ModernChat.Domain.Entities.Enums;

namespace ModernChat.Domain.Entities
{
    public class ChatDetails
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public AccessType AccessType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string JoinRequirements { get; set; }
    }
}
