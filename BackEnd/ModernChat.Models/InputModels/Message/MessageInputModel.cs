using System;
using System.Collections.Generic;
using System.Text;

namespace ModernChat.Models.InputModels.Message
{
    public class MessageInputModel
    {
        public int AuthorId { get; set; }
        public string Content { get; set; }
        public int ChatId { get; set; }
    }
}
