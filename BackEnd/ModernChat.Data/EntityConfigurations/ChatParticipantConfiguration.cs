using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernChat.Domain.Entities;

namespace ModernChat.Data.EntityConfigurations
{
    public class ChatParticipantConfiguration : IEntityTypeConfiguration<ChatParticipant>
    {
        public void Configure(EntityTypeBuilder<ChatParticipant> builder)
        {
            builder.HasKey(cp => new { cp.ChatId, cp.UserId });

            builder
                .HasOne(cp => cp.Chat)
                .WithMany(c => c.Participants)
                .HasForeignKey(cp => cp.ChatId);

            builder
                .HasOne(cp => cp.User)
                .WithMany(u => u.Chats)
                .HasForeignKey(cp => cp.UserId);
        }
    }
}
