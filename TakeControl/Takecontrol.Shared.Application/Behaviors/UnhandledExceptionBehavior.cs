﻿using MediatR;
using Microsoft.Extensions.Logging;
using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Shared.Application.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> _logger;

    public UnhandledExceptionBehavior(ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            LogErrorByException(ex);
            throw;
        }
    }

    private void LogErrorByException(Exception ex)
    {
        if (ex is BaseException)
        {
            _logger.LogWarning($"{ex.Source}: {ex.Message}", ex);
        }
        else
        {
            _logger.LogError($"Application error: {ex.Message}");
        }
    }
}
