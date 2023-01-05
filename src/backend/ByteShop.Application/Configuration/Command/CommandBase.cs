using FluentValidation.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace ByteShop.Application.Configuration.Command;
public abstract class CommandBase<TResult> : IRequest<TResult>, IBaseRequest
{
    [JsonIgnore]
    public ValidationResult Errors { get; protected set; } = new();

    public void AddValidationError(string propertyName, string errorMessage)
    {
        Errors.Errors.Add(new ValidationFailure(propertyName, errorMessage));
    }

    public virtual bool IsValid()
    {
        throw new NotImplementedException();
    }
}