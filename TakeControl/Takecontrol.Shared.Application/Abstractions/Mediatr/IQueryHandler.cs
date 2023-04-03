using MediatR;

namespace Takecontrol.Shared.Application.Abstractions.Mediatr;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}
