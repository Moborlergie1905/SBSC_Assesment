namespace WalletSolution.Domain.Entities;
public class UserRole : BaseEntity
{
    public int AppUserId { get; set; }
    public int RoleId { get; set; }
    public AppUser User { get; set; }
    public Role Role { get; set; }
}
