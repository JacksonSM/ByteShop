using Microsoft.Extensions.Logging;
using Moq;

namespace Utilities.Services;
public class LoggerBuilder<T>
{
    private static LoggerBuilder<T>? _instance;
    private readonly Mock<Logger<T>> _uow;

    private LoggerBuilder()
    {
        if (_uow is null)
        {
            _uow = new Mock<Logger<T>>();
        }
    }

    public static LoggerBuilder<T> Instance()
    {
        _instance = new LoggerBuilder<T>();
        return _instance;
    }

    public Logger<T> Build()
    {
        return _uow.Object;
    }
}
