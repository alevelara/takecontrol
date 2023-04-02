using MediatR;

namespace Takecontrol.Shared.Application.Abstractions.Mediatr;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
