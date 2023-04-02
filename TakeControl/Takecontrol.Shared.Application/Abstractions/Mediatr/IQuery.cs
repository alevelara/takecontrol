using MediatR;

namespace Takecontrol.Shared.Application.Abstractions.Mediatr;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
