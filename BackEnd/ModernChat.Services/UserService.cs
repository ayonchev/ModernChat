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

        public UserService(IMapper mapper, ModernChatDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<List<UserViewModel>> GetUsers(int currentUserId)
        {
            var users = await context
                .Users
                .Where(u => u.Id != currentUserId)
                .ProjectTo<UserViewModel>(mapper.ConfigurationProvider)
                .ToListAsync();

            return users;
        }
    }
}
