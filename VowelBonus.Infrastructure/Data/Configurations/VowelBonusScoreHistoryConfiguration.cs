using VowelBonus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace VowelBonus.Infrastructure.Data.Configurations;

public class VowelBonusScoreHistoryConfiguration : IEntityTypeConfiguration<VowelBonusScoreHistory>
{
    public void Configure(EntityTypeBuilder<VowelBonusScoreHistory> builder)
    {
        builder.HasOne(v => v.User)
               .WithMany(u => u.VowelBonusScoreHistories)
               .HasForeignKey(v => v.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
