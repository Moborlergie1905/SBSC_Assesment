using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace WalletSolution.Common.Behaviours;
public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        _logger.LogInformation("Request: {Name} {@Request}", requestName, request);
    }
}
