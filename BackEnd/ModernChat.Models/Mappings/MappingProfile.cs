using AutoMapper;
using ModernChat.Domain.Entities;
using ModernChat.Models.InputModels.Chat;
using ModernChat.Models.InputModels.Message;
using ModernChat.Models.ViewModels.Chat;
using ModernChat.Models.ViewModels.Message;
using ModernChat.Models.ViewModels.User;
using System.Linq;

namespace ModernChat.Models.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MessageInputModel, Message>();
            CreateMap<Message, MessageViewModel>()
                .ForMember(mvm => mvm.AuthorUsername, config => config.MapFrom(m => m.Author.UserName));

            CreateMap<ChatDetailsInputModel, ChatDetails>();
            CreateMap<ChatDetails, ChatDetailsViewModel>();

            CreateMap<ChatParticipant, UserViewModel>()
                .ForMember(uvm => uvm.Id, config => config.MapFrom(cp => cp.UserId))
                .ForMember(uvm => uvm.Email, config => config.MapFrom(cp => cp.User.Email))
                .ForMember(uvm => uvm.Username, config => config.MapFrom(cp => cp.User.UserName));

            CreateMap<ChatInputModel, Chat>();
            //.ForMember(c => c.Participants, config => config.MapFrom())
            //.ForPath(c => c.Participants, config => config.)
            CreateMap<Chat, ChatViewModel>();

            CreateMap<ApplicationUser, UserViewModel>();
        }
    }
}
