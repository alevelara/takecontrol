using MediatR;

namespace takecontrol.Application.Abstractions.Mediatr;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
