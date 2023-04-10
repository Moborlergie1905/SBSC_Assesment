using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletSolution.Domain.Entities.Admins;

namespace WalletSolution.Persistence.Configurations;
public class CurrencyTypeConfiguration : IEntityTypeConfiguration<CurrencyType>
{
    public void Configure(EntityTypeBuilder<CurrencyType> builder)
    {
        builder.ToTable("CurrencyTypes"); //nameof(CurrencyType)
        builder.HasKey(x => x.Id);
        builder.HasData(
            new CurrencyType { Currency = "US Dollar", CurrencyCode = "USD" },
            new CurrencyType { Currency = "Great Britain Pounds", CurrencyCode = "GBP" },
            new CurrencyType { Currency = "Naira", CurrencyCode = "NGN" });
    }
}
