using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernChat.Domain.Entities;

namespace ModernChat.Data.EntityConfigurations
{
    public class ChatDetailsConfiguration : IEntityTypeConfiguration<ChatDetails>
    {
        public void Configure(EntityTypeBuilder<ChatDetails> builder)
        {
            builder.HasKey(cd => cd.Id);

            builder
                .HasOne(cd => cd.Chat)
                .WithOne(c => c.Details)
                .HasForeignKey<ChatDetails>(cd => cd.ChatId);

            builder.Property(cd => cd.Name).IsRequired();
            builder.Property(cd => cd.AccessType).IsRequired();
        }
    }
}
