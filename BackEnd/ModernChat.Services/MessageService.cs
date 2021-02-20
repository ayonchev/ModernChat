using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ModernChat.Data;
using ModernChat.Domain.Entities;
using ModernChat.Models.InputModels.Message;
using ModernChat.Models.ViewModels.Message;
using ModernChat.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernChat.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper mapper;
        private readonly ModernChatDbContext context;

        public MessageService(IMapper mapper, ModernChatDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<List<MessageViewModel>> Get(int chatId)
        {
            var messages = await context
                .Messages
                .Where(m => m.ChatId == chatId)
                .Include(m => m.Author)
                .OrderBy(m => m.CreatedOn)
                .ProjectTo<MessageViewModel>(mapper.ConfigurationProvider)
                .ToListAsync();

            return messages;
        }

        public async Task<MessageViewModel> Create(MessageInputModel inputModel)
        {
            var message = mapper.Map<Message>(inputModel);
            message.CreatedOn = DateTime.Now;

            context.Messages.Add(message);

            await context.SaveChangesAsync();

            message = await context
                .Messages
                .Include(m => m.Author)
                .FirstOrDefaultAsync(m => m.Id == message.Id);

            var viewModel = mapper.Map<MessageViewModel>(message);

            return viewModel;
        }

        public async Task Delete(int id)
        {
            var message = await context.Messages.FindAsync(id);

            context.Messages.Remove(message);
            await context.SaveChangesAsync();
        }
    }
}
