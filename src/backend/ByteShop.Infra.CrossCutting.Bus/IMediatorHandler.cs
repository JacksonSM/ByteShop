using ByteShop.Domain.Events;
using FluentValidation.Results;

namespace ByteShop.Infra.CrossCutting.Bus;
public interface IMediatorHandler
{
    Task PublishEvent<T>(T @event) where T : Event;
    Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    Task<R> SendQuery<R>(Query<R> query);
}
