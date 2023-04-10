namespace WalletSolution.Domain.Entities;
public interface IEntity { }
public interface IEntity<TKey> : IEntity
{
    TKey Id { get; set; }
}
public abstract class BaseEntity<TKey> : IEntity<TKey>
{
    public TKey Id { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime? DateModified { get; set; }
    public bool Deleted { get; set; } = false;
}
public abstract class BaseEntity : BaseEntity<int> { }
