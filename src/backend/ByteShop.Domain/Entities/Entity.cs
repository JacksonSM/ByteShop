namespace ByteShop.Domain.Entities;
public abstract class Entity
{
    public int Id { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

}
