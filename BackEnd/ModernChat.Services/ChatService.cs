using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ModernChat.Data;
using ModernChat.Domain.Entities;
using ModernChat.Domain.Entities.Enums;
using ModernChat.Models.InputModels.Chat;
using ModernChat.Models.ViewModels.Chat;
using ModernChat.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModernChat.Services
{
    public class ChatService : IChatService
    {
        private readonly IMapper mapper;
        private readonly ModernChatDbContext context;

        public ChatService(IMapper mapper, ModernChatDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<List<ChatViewModel>> GetChats(bool userChatsOnly, int currentUserId)
        {
            var chats = await context
                .Chats
                .Include(c => c.Participants)
                    .ThenInclude(p => p.User)
                .Where(c => userChatsOnly ? c.Participants.Select(u => u.UserId).Contains(currentUserId) : true)
                .ProjectTo<ChatViewModel>(mapper.ConfigurationProvider)
                .ToListAsync();

            chats.ForEach(c => c.Participants = c.Participants.Where(p => p.Id != currentUserId).ToList());

            return chats;
        }

        public async Task<List<int>> GetChatIds(bool userChatsOnly, int currentUserId)
        {
            var chatIds = await context
                .Chats
                .Where(c => userChatsOnly ? c.Participants.Select(u => u.UserId).Contains(currentUserId) : true)
                .Select(c => c.Id)
                .ToListAsync();

            return chatIds;
        }

        public async Task Create(ChatInputModel inputModel, int currentUserId)
        {
            var chat = mapper.Map<Chat>(inputModel);
            chat.Participants = new List<ChatParticipant>();

            if (chat.Type == ChatType.OneToOne)
            {
                chat.Details = null;

                if (inputModel.ParticipantIds.Length > 1)
                {
                    throw new InvalidOperationException("Invalid chat type!");
                }
            }

            chat.Participants = inputModel.ParticipantIds
                .Select(pId => new ChatParticipant
                {
                    UserId = pId
                })
                .ToList();

            chat.Participants.Add(new ChatParticipant()
            {
                UserId = currentUserId
            });

            context.Chats.Add(chat);
            await context.SaveChangesAsync();
        }
    }
}
