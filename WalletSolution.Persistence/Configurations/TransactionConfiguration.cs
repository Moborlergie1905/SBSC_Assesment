using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletSolution.Domain.Entities.WalletUsers;

namespace WalletSolution.Persistence.Configurations;
public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable(nameof(Transaction));
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id);
        builder.HasIndex(x => x.WalletUserId);
    }
}
