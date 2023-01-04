using MediatR;

namespace ByteShop.Infra.CrossCutting.Bus;
public abstract class Query<Response> : IRequest<Response>
{
}
