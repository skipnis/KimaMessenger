using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserChatConfiguration : IEntityTypeConfiguration<UserChat>
{
    public void Configure(EntityTypeBuilder<UserChat> builder)
    {
        builder.ToTable("UserChats");
        
        builder.HasKey(x => new {x.UserId, x.ChatId });
    }
}