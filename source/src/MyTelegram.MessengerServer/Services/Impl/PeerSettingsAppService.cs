namespace MyTelegram.MessengerServer.Services.Impl;

public class PeerSettingsAppService : IPeerSettingsAppService //, ISingletonDependency
{
    public Task<PeerSettings> GetAsync(long userId,
        Peer peer)
    {
        var setting = new PeerSettings();

        return Task.FromResult(setting);
    }

    public Task<List<PeerSettings>> GetPeerSettingsAsync(GetPeerSettingsListInput input)
    {
        return Task.FromResult(new List<PeerSettings>());
    }
}