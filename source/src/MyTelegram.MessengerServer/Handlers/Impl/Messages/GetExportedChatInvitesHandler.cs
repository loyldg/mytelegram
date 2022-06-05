using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;
using IExportedChatInvite = MyTelegram.Schema.IExportedChatInvite;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetExportedChatInvitesHandler :
    RpcResultObjectHandler<RequestGetExportedChatInvites, IExportedChatInvites>,
    IGetExportedChatInvitesHandler, IProcessedHandler
{
    private readonly IPeerHelper _peerHelper;
    private readonly IRandomHelper _randomHelper;
    private readonly IOptions<MyTelegramMessengerServerOptions> _options;
    public GetExportedChatInvitesHandler(IPeerHelper peerHelper,
        IRandomHelper randomHelper,
        IOptions<MyTelegramMessengerServerOptions> options)
    {
        _peerHelper = peerHelper;
        _randomHelper = randomHelper;
        _options = options;
    }

    protected override Task<IExportedChatInvites> HandleCoreAsync(IRequestInput input,
        RequestGetExportedChatInvites obj)
    {
        // todo:impl get chat invites
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        var admin = _peerHelper.GetPeer(obj.AdminId, input.UserId);
        return Task.FromResult<IExportedChatInvites>(new TExportedChatInvites
        {
            Count = 1,
            Users = new TVector<IUser>(),
            Invites = new TVector<IExportedChatInvite> {
                new TChatInviteExported {
                    AdminId = admin.PeerId,
                    Date = CurrentDate,
                    ExpireDate = DateTime.UtcNow.AddDays(30).ToTimestamp(),
                    Link =
                        $"{_options.Value.JoinChatDomain}/AAAAA{peer.PeerId}/{_randomHelper.GenerateRandomString(8)}",
                    Permanent = true,
                    Revoked = false,
                    StartDate = CurrentDate,
                    Usage = 0,
                    UsageLimit = 0
                }
            }
        });
    }
}
