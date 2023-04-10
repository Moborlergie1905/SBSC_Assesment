using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletSolution.Domain.Entities.WalletUsers;

namespace WalletSolution.Persistence.Configurations;
public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable(nameof(Wallet));
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id);
    }
}
