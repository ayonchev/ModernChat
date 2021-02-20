using System;
using System.Collections.Generic;
using System.Text;

namespace ModernChat.Services
{
    public class HubStoreService
    {
        public Dictionary<int, string> ChatGroupIds { get; } = new Dictionary<int, string>();
        public Dictionary<int, int> ActiveUserConnections { get; set; } = new Dictionary<int, int>();
    }
}
