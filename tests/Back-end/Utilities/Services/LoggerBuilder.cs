using Microsoft.Extensions.Logging;
using Moq;

namespace Utilities.Services;
public class LoggerBuilder<T>
{
    private static LoggerBuilder<T>? _instance;
    private readonly Mock<ILogger<T>> _logger;

    private LoggerBuilder()
    {
        if (_logger is null)
        {
            _logger = new Mock<ILogger<T>>();
        }
    }

    public static LoggerBuilder<T> Instance()
    {
        _instance = new LoggerBuilder<T>();
        return _instance;
    }

    public ILogger<T> Build()
    {
        return _logger.Object;
    }
}
