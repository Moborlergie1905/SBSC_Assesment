using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletSolution.Domain.Entities;

namespace WalletSolution.Persistence.Configurations;
public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable(nameof(AppUser));
        builder.HasKey(x => x.Id);        
    }
}
