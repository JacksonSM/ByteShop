using FluentValidation.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace ByteShop.Application.Configuration.Command;
public abstract class CommandBase<TResult> : IRequest<TResult>, IBaseRequest
{
    [JsonIgnore]
    public ValidationResult ValidationResult { get; protected set; } = new();

    public void AddValidationError(string propertyName, string errorMessage)
    {
        ValidationResult.Errors.Add(new ValidationFailure(propertyName, errorMessage));
    }
}