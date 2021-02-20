using System;

namespace ModernChat.Models.ViewModels.Message
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string AuthorUsername { get; set; }
        public string Content { get; set; }
        public int ChatId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
