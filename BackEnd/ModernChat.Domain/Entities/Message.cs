using System;

namespace ModernChat.Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        public string Content { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
