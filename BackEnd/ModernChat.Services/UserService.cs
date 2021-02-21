using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ModernChat.Data;
using ModernChat.Models.ViewModels.User;
using ModernChat.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModernChat.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly ModernChatDbContext context;
        private readonly HubStoreService hubStoreService;

        public UserService(IMapper mapper, ModernChatDbContext context, HubStoreService hubStoreService)
        {
            this.mapper = mapper;
            this.context = context;
            this.hubStoreService = hubStoreService;
        }

        public async Task<List<UserViewModel>> GetUsers(int currentUserId)
        {
            var users = await context
                .Users
                .Where(u => u.Id != currentUserId)
                .ProjectTo<UserViewModel>(mapper.ConfigurationProvider)
                .ToListAsync();

            var activeUserIds = hubStoreService
                .ActiveUserConnections
                .Where(kvp => kvp.Value > 0)
                .Select(kvp => kvp.Key);

            users.ForEach(u => u.IsActive = activeUserIds.Contains(u.Id));

            return users;
        }
    }
}
