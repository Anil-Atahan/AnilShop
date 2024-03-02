using System.Diagnostics;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnilShop.SharedKernel;

public class LoggingBehaviour<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new NullReferenceException();
        }

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Handling {RequestName}", typeof(TRequest).Name);

            Type myType = request.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (var prop in props)
            {
                object? propValue = prop?.GetValue(request, null);
                _logger.LogInformation("Property {Property} : {@Value}", prop?.Name, propValue);
            }
        }
        
        var sw = Stopwatch.StartNew();

        var response = await next();

        _logger.LogInformation("Handled {RequestName} with {Response} in {ms} ms",
            typeof(TRequest).Name,
            response,
            sw.ElapsedMilliseconds);
        sw.Stop();
        return response;
    }
}