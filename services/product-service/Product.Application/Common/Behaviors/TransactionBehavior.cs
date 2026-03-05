using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Common.Interfaces;

namespace Product.Application.Common.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

    public TransactionBehavior(IUnitOfWork unitOfWork, ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Query'ler için transaction açma
        var requestName = typeof(TRequest).Name;
        if (!requestName.EndsWith("Command"))
            return await next();

        _logger.LogInformation("İşlem başlatıldı: {RequestName}", requestName);

        try
        {
            var response = await next();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("İşlem başarıyla tamamlandı (commit edildi): {RequestName}", requestName);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "İşlem sırasında hata oluştu: {RequestName}", requestName);
            throw;
        }
    }
}
