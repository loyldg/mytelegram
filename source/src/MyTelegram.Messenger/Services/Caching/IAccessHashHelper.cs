namespace MyTelegram.Messenger.Services.Caching;

public interface IAccessHashHelper
{
    Task<bool> IsAccessHashValidAsync(long id,
        long accessHash);

    Task CheckAccessHashAsync(long id,
        long accessHash);

    Task CheckAccessHashAsync(IInputPeer? inputPeer);
    Task CheckAccessHashAsync(IInputUser inputUser);
    Task CheckAccessHashAsync(IInputChannel inputChannel);
    Task CheckAccessHashAsync(Peer peer);
    void AddAccessHash(long id, long accessHash);
}