using MediatR;

namespace takecontrol.Application.Abstractions.Mediatr;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
