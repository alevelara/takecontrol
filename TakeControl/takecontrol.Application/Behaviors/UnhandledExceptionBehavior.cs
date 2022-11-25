using MediatR;
using takecontrol.Application.Contracts.Logger;

namespace takecontrol.Application.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILog _logger;

    public UnhandledExceptionBehavior(ILog logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }catch(Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            _logger.Error($"{requestName}: Application Error: {ex.Message}");
            throw;
        }
    }
}
