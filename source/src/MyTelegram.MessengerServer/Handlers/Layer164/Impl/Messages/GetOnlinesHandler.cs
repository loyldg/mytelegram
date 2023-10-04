using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetOnlinesHandler : RpcResultObjectHandler<RequestGetOnlines, IChatOnlines>,
    IGetOnlinesHandler, IProcessedHandler
{
    //private readonly IOnlineUserHelper _onlineUserHelper;
    //private readonly IPeerHelper _peerHelper;
    //public GetOnlinesHandler(IOnlineUserHelper onlineUserHelper,
    //    IPeerHelper peerHelper)
    //{
    //    _onlineUserHelper = onlineUserHelper;
    //    _peerHelper = peerHelper;
    //}

    protected override Task<IChatOnlines> HandleCoreAsync(IRequestInput input,
        RequestGetOnlines obj)
    {
        throw new NotImplementedException();

        //// todo:get onlines
        //var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        //if (peer.PeerType == PeerType.Channel)
        //{
        //    var memberList = await _onlineUserHelper.GetChannelMemberUidListAsync(peer.PeerId);
        //    return new TChatOnlines
        //    {
        //        Onlines = memberList.Count
        //    };
        //}
        //return new TChatOnlines
        //{
        //    Onlines = 1
        //};
        ////throw new NotImplementedException();
    }
}