namespace MyTelegram.GatewayServer.Services;

public interface IClientManager
{
    void AddClient(string connectionId,
        ClientData clientData);

    void RemoveClient(string connectionId);

    bool TryGetClientData(string connectionId,
        [NotNullWhen(true)] out ClientData? clientData);

    bool TryRemoveClient(string connectionId,[NotNullWhen(true)]out ClientData? clientData);

    int GetOnlineCount();
}