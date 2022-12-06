using ByteShop.Application.UseCases.Commands;
using ByteShop.Application.UseCases.Results;

namespace ByteShop.Application.UseCases.Handlers;
public interface IHandler<C,R>
where C : ICommand
{
    Task<RequestResult<R>> Handle(C command);
}
