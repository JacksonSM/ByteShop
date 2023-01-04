using ByteShop.Domain.Events;
using FluentValidation.Results;
using MediatR;

namespace ByteShop.Infra.CrossCutting.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T @event) where T : Event
        {
            await _mediator.Publish(@event);
        }

        public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }

        public async Task<R> SendQuery<R>(Query<R> query)
        {
            return await _mediator.Send(query);
        }
    }
}