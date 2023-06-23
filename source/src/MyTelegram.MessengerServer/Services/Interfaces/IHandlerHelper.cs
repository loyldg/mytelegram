namespace MyTelegram.MessengerServer.Services.Interfaces;

public interface IHandlerHelper
{
    void InitAllHandlers(Assembly assembly);

    bool TryGetHandler(uint objectId,
        [NotNullWhen(true)] out IObjectHandler? handler);

    bool TryGetHandlerName(uint objectId,
        [NotNullWhen(true)] out string? handlerName);

    bool TryGetHandlerShortName(uint objectId,
        [NotNullWhen(true)] out string? handlerShortName);
}