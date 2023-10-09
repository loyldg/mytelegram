namespace MyTelegram.Messenger.Services.Interfaces;

public interface IPeerSettingsAppService
{
    Task<PeerSettings> GetAsync(long userId,
        Peer peer);

    Task<IPeerSettingsReadModel?> GetPeerSettingsAsync(long userId, long peerId);

    Task<List<PeerSettings>> GetPeerSettingsListAsync(GetPeerSettingsListInput input);
}