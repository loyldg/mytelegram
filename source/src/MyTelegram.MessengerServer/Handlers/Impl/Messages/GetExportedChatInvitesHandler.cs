using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;
using IExportedChatInvite = MyTelegram.Schema.IExportedChatInvite;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetExportedChatInvitesHandler :
    RpcResultObjectHandler<RequestGetExportedChatInvites, IExportedChatInvites>,
    IGetExportedChatInvitesHandler, IProcessedHandler
{
    private readonly IAppSettingManager _appSettingManager;
    private readonly IPeerHelper _peerHelper;
    private readonly IRandomHelper _randomHelper;

    public GetExportedChatInvitesHandler(IPeerHelper peerHelper,
        IAppSettingManager appSettingManager,
        IRandomHelper randomHelper)
    {
        _peerHelper = peerHelper;
        _appSettingManager = appSettingManager;
        _randomHelper = randomHelper;
    }

    protected override Task<IExportedChatInvites> HandleCoreAsync(IRequestInput input,
        RequestGetExportedChatInvites obj)
    {
        // todo:impl get chat invites
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        var admin = _peerHelper.GetPeer(obj.AdminId, input.UserId);
        return Task.FromResult<IExportedChatInvites>(new TExportedChatInvites {
            Count = 1,
            Users = new TVector<IUser>(),
            Invites = new TVector<IExportedChatInvite> {
                new TChatInviteExported {
                    AdminId = admin.PeerId,
                    Date = CurrentDate,
                    ExpireDate = DateTime.UtcNow.AddDays(30).ToTimestamp(),
                    Link =
                        $"{_appSettingManager.GetSetting(MyTelegramServerConsts.JoinChatDomain)}/AAAAA{peer.PeerId}/{_randomHelper.GenerateRandomString(8)}",
                    Permanent = true,
                    Revoked = false,
                    StartDate = CurrentDate,
                    Usage = 0,
                    UsageLimit = 0
                }
            }
        });

        //var peer = GetPeer(obj.Peer, input.UserId);
        //var admin = GetPeer(obj.AdminId, input.UserId);
        //var invites = await QueryProcessor
        //    .ProcessAsync(new GetChatInvitesQuery(obj.Revoked,
        //            peer.PeerId,
        //            admin.PeerId,
        //            obj.OffsetDate,
        //            obj.OffsetLink,
        //            obj.Limit),
        //        CancellationToken.None).ConfigureAwait(false);

        //// todo:read invites from readmodel
        //var r = new TExportedChatInvites
        //{
        //    Count = invites.Count,
        //    Invites = new TVector<IExportedChatInvite>(ObjectMapper.Map<IReadOnlyCollection<ChatInviteReadModel>,List<TChatInviteExported>>(invites)),
        //    Users = new TVector<IUser>(),
        //};
        ////if (r.Count == 0)
        ////{
        ////    r.Invites=new TVector<IExportedChatInvite>
        ////    {
        ////        new TChatInviteExported
        ////        {

        ////        }
        ////    }
        ////}

        //return r;
    }
}
