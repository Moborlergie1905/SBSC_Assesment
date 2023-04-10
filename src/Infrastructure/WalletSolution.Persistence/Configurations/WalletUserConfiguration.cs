using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletSolution.Domain.Entities.WalletUsers;

namespace WalletSolution.Persistence.Configurations;
public class WalletUserConfiguration : IEntityTypeConfiguration<WalletUser>
{
    public void Configure(EntityTypeBuilder<WalletUser> builder)
    {
        builder.ToTable(nameof(WalletUser));
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id);        
    }
}
