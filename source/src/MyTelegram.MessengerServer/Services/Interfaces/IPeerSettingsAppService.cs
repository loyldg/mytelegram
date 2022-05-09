namespace MyTelegram.MessengerServer.Services.Interfaces;

public interface IPeerSettingsAppService
{
    Task<PeerSettings> GetAsync(long userId,
        Peer peer);

    Task<List<PeerSettings>> GetPeerSettingsAsync(GetPeerSettingsListInput input);
}