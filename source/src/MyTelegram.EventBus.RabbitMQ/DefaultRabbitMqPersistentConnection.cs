using Microsoft.Extensions.Options;

namespace MyTelegram.EventBus.RabbitMQ;

// taken from https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/BuildingBlocks/EventBus/EventBusRabbitMQ/DefaultRabbitMQPersistentConnection.cs
public class DefaultRabbitMqPersistentConnection
    : IRabbitMqPersistentConnection
{
    private IConnectionFactory? _connectionFactory;
    private readonly ILogger<DefaultRabbitMqPersistentConnection> _logger;
    private readonly IOptions<RabbitMqOptions> _options;
    private IConnection? _connection;
    private bool _disposed;
    private int _retryCount = 5;

    private readonly object _syncRoot = new();

    public DefaultRabbitMqPersistentConnection(ILogger<DefaultRabbitMqPersistentConnection> logger,
        IOptions<RabbitMqOptions> options)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options;
    }

    public bool IsConnected => _connection is { IsOpen: true } && !_disposed;

    public IModel CreateModel()
    {
        if (!IsConnected)
        {
            throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
        }

        //try
        //{
        //    return _connection.CreateModel();
        //}
        //catch (TimeoutException)
        //{
        //    Dispose();
        //}

        //if (TryConnect())
        //{

        //}
        return _connection.CreateModel();
    }

    public void Dispose()
    {
        if (_disposed) return;

        _disposed = true;

        try
        {
            if (_connection == null)
            {
                return;
            }

            _connection.ConnectionShutdown -= OnConnectionShutdown;
            _connection.CallbackException -= OnCallbackException;
            _connection.ConnectionBlocked -= OnConnectionBlocked;
            _connection.Dispose();
        }
        catch (IOException ex)
        {
            _logger.LogCritical(ex.ToString());
        }
    }

    public bool TryConnect()
    {
        _logger.LogInformation("RabbitMQ Client is trying to connect");

        lock (_syncRoot)
        {
            var policy = Policy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex, "RabbitMQ Client could not connect after {TimeOut}s ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
                }
                );

            policy.Execute(() =>
            {
                _connection = CreateConnectionFactory()
                    .CreateConnection();
            });

            if (IsConnected)
            {
                _connection!.ConnectionShutdown += OnConnectionShutdown;
                _connection.CallbackException += OnCallbackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;

                _logger.LogInformation("RabbitMQ Client acquired a persistent connection to '{HostName}' and is subscribed to failure events", _connection.Endpoint.HostName);

                return true;
            }

            _logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened");

            return false;
        }
    }

    private void OnConnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
    {
        if (_disposed) return;

        _logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect...");

        TryConnect();
    }

    private void OnCallbackException(object? sender, CallbackExceptionEventArgs e)
    {
        if (_disposed) return;

        _logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");

        TryConnect();
    }

    private void OnConnectionShutdown(object? sender, ShutdownEventArgs reason)
    {
        if (_disposed) return;

        _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");

        TryConnect();
    }

    private IConnectionFactory CreateConnectionFactory()
    {
        if (_connectionFactory == null)
        {
            var factory = new ConnectionFactory
            {
                HostName = _options.Value.HostName,
                Port = _options.Value.Port,
                UserName = _options.Value.UserName,
                Password = _options.Value.Password,
                DispatchConsumersAsync = true
            };

            _connectionFactory = factory;
        }

        return _connectionFactory;
    }
}