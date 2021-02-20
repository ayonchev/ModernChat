using ModernChat.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernChat.Models.InputModels.Chat
{
    public class ChatDetailsInputModel
    {
        public AccessType AccessType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string JoinRequirements { get; set; }
    }
}
