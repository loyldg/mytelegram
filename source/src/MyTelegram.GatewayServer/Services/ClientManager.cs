namespace MyTelegram.GatewayServer.Services;

public class ClientManager : IClientManager
{
    private readonly ConcurrentDictionary<string, ClientData> _clients = new();
    //private readonly ConcurrentDictionary<string, ConnectionContext> _connections = new();
    public void AddClient(string connectionId, ClientData clientData)
    {
        _clients.TryAdd(connectionId, clientData);
    }

    public void RemoveClient(string connectionId)
    {
        _clients.TryRemove(connectionId, out _);
    }

    public bool TryGetClientData(string connectionId, [NotNullWhen(true)] out ClientData? clientData)
    {
        if (_clients.TryGetValue(connectionId, out var d))
        {
            clientData = d;
            return true;
        }

        clientData = default;
        return false;
    }
}