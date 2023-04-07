using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletSolution.Domain.Entities.Admins;

namespace WalletSolution.Persistence.Configurations;
public class CurrencyTypeConfiguration : IEntityTypeConfiguration<CurrencyType>
{
    public void Configure(EntityTypeBuilder<CurrencyType> builder)
    {
        builder.ToTable(nameof(CurrencyType));
        builder.HasKey(x => x.Id);
    }
}
