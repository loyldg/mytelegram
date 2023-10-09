using MyTelegram.Messenger.Services.Interfaces;

namespace MyTelegram.Messenger.Services.Impl;

public class PeerSettingsAppService : IPeerSettingsAppService //, ISingletonDependency
{
    private readonly IQueryProcessor _queryProcessor;

    public PeerSettingsAppService(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    public async Task<PeerSettings> GetAsync(long userId,
        Peer peer)
    {
        var peerSettingsReadModel = await _queryProcessor.ProcessAsync(new GetPeerSettingsQuery(userId, peer.PeerId), default);
        if (peerSettingsReadModel != null)
        {
            if (peerSettingsReadModel.HiddenPeerSettingsBar)
            {
                return new PeerSettings();
            }

            if (peerSettingsReadModel.PeerSettings != null)
            {
                return new PeerSettings
                {
                    AddContact = peerSettingsReadModel.PeerSettings.AddContact,
                    BlockContact = peerSettingsReadModel.PeerSettings.BlockContact,
                    NeedContactsException = peerSettingsReadModel.PeerSettings.NeedContactsException,
                    ReportGeo = peerSettingsReadModel.PeerSettings.ReportGeo,
                    ReportSpam = peerSettingsReadModel.PeerSettings.ReportSpam,
                    ShareContact = peerSettingsReadModel.PeerSettings.ShareContact,
                };
            }

            return new PeerSettings
            {

            };
        }

        return new PeerSettings();
    }

    public Task<IPeerSettingsReadModel?> GetPeerSettingsAsync(long userId, long peerId)
    {
        return _queryProcessor.ProcessAsync(new GetPeerSettingsQuery(userId, peerId), default);
    }

    public Task<List<PeerSettings>> GetPeerSettingsListAsync(GetPeerSettingsListInput input)
    {
        return Task.FromResult(new List<PeerSettings>());
    }
}