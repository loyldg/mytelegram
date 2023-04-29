using System.Reflection;
using EventFlow.ReadStores;

namespace MyTelegram.Domain.EventFlow;

public class MyReadModelFactory<TReadModel> : IReadModelFactory<TReadModel>
    where TReadModel : IReadModel
{
    private static Func<TReadModel>? _createReadModelFunc;
    private readonly ILogger<MyReadModelFactory<TReadModel>> _logger;

    static MyReadModelFactory()
    {
        var type = typeof(TReadModel).GetTypeInfo();

        var emptyConstructor = type
            .GetConstructors()
            .Where(c => !c.GetParameters().Any())
            .ToList();

        if (!emptyConstructor.Any())
        {
            throw new ArgumentException(
                $"Read model type '{typeof(TReadModel).PrettyPrint()}' doesn't have an empty " +
                $"constructor. Please create a custom '{typeof(IReadModelFactory<TReadModel>).PrettyPrint()}' " +
                "implementation.");
        }
    }

    public MyReadModelFactory(
        ILogger<MyReadModelFactory<TReadModel>> logger)
    {
        _logger = logger;
    }

    public Task<TReadModel> CreateAsync(string id,
        CancellationToken cancellationToken)
    {
        if (_logger.IsEnabled(LogLevel.Trace))
        {
            _logger.LogTrace(
                "Creating new instance of read model type {ReadModelType} with ID {Id}",
                typeof(TReadModel).PrettyPrint(),
                id);
        }

        _createReadModelFunc ??= MyReflectionHelper.CompileConstructor<TReadModel>();

        return Task.FromResult(_createReadModelFunc());
    }
}
