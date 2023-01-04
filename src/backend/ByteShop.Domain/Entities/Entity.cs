using FluentValidation.Results;

namespace ByteShop.Domain.Entities;
public abstract class Entity
{
    public int Id { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public ValidationResult ValidationResult { get; protected set; } = new();

    public void AddValidationError(string propertyName, string errorMessage)
    {
        ValidationResult.Errors.Add(new ValidationFailure(propertyName, errorMessage));
    }
    public void AddValidationError(List<ValidationFailure> errors)
    {
        ValidationResult.Errors.AddRange(errors);
    }

    public virtual bool IsValid()
    {
        throw new NotImplementedException();
    }

    public override bool Equals(object obj)
    {
        Entity entity = obj as Entity;
        if ((object)this == entity)
        {
            return true;
        }

        if ((object)entity == null)
        {
            return false;
        }

        return Id.Equals(entity.Id);
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if ((object)a == null && (object)b == null)
        {
            return true;
        }

        if ((object)a == null || (object)b == null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (GetType().GetHashCode() ^ 0x5D) + Id.GetHashCode();
    }
}
