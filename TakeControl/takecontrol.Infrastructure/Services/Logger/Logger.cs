using Microsoft.Extensions.Logging;
using takecontrol.Application.Contracts.Logger;

namespace takecontrol.Infrastructure.Services.Logger;

public class Logger : ILog 
{
    private readonly ILogger _logger;

    public Logger(ILogger logger)
    {
        _logger = logger;
    }

    public void Error(string message)
    {
        _logger.Log(LogLevel.Error, message);
    }

    public void Info(string message)
    {
        _logger.Log(LogLevel.Information, message);
    }

    public void Warn(string message)
    {
        _logger.Log(LogLevel.Warning, message);
    }
}
