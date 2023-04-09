namespace WalletSolution.Domain.Entities;
public class AppUser : BaseEntity
{
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<UserRole> Roles { get; set; }
}
