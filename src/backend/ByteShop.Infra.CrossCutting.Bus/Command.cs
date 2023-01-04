using FluentValidation.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace ByteShop.Infra.CrossCutting.Bus;
public abstract class Command : IRequest<ValidationResult>, IBaseRequest
{
    [JsonIgnore]
    public ValidationResult ValidationResult { get; protected set; } = new();

    public void AddValidationError(string propertyName, string errorMessage)
    {
        ValidationResult.Errors.Add(new ValidationFailure(propertyName, errorMessage));
    }

    public virtual bool IsValid()
    {
        throw new NotImplementedException();
    }
}
