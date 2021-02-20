using ModernChat.Domain.Entities.Enums;
using ModernChat.Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernChat.Models.ViewModels.Chat
{
    public class ChatViewModel
    {
        public int Id { get; set; }
        public ChatType Type { get; set; }
        public ChatDetailsViewModel Details { get; set; }
        public List<UserViewModel> Participants { get; set; }
    }
}
