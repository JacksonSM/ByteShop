using FluentValidation.Results;
using MediatR;

namespace ByteShop.Infra.CrossCutting.Bus;
public abstract class Command : IRequest<ValidationResult>, IBaseRequest
{
}
