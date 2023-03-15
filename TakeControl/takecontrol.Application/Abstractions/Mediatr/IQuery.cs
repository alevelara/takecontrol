using MediatR;

namespace Takecontrol.Application.Abstractions.Mediatr;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
