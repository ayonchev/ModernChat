using System;
using System.Collections.Generic;
using System.Text;

namespace ModernChat.Models.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
    }
}
