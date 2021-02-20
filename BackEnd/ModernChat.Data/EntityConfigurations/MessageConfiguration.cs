using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernChat.Data.EntityConfigurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Id);

            builder
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId)
                .IsRequired();

            builder
                .HasOne(m => m.Author)
                .WithMany(a => a.Messages)
                .HasForeignKey(m => m.AuthorId)
                .IsRequired();
        }
    }
}
