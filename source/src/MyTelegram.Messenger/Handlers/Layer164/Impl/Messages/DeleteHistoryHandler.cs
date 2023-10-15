// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Deletes communication history.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_REVOKE_DATE_UNSUPPORTED <code>min_date</code> and <code>max_date</code> are not available for using with non-user peers.
/// 400 MAX_DATE_INVALID The specified maximum date is invalid.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 MIN_DATE_INVALID The specified minimum date is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.deleteHistory" />
///</summary>
internal sealed class DeleteHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteHistory, MyTelegram.Schema.Messages.IAffectedHistory>,
    Messages.IDeleteHistoryHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IPtsHelper _ptsHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IAccessHashHelper _accessHashHelper;
    public DeleteHistoryHandler(ICommandBus commandBus,
        //IRandomHelper randomHelper,
        IPeerHelper peerHelper,
        IQueryProcessor queryProcessor,
        IPtsHelper ptsHelper,
        IAccessHashHelper accessHashHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
        _queryProcessor = queryProcessor;
        _ptsHelper = ptsHelper;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IAffectedHistory> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestDeleteHistory obj)
    {
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        var pageSize = MyTelegramServerDomainConsts.ClearHistoryDefaultPageSize;
        var messageIdList = await _queryProcessor
            .ProcessAsync(new GetMessageIdListQuery(input.UserId,
                    peer.PeerId,
                    obj.MaxId,
                    pageSize),
                default);
        if (messageIdList.Count == 0)
        {
            var cachedPts = _ptsHelper.GetCachedPts(input.UserId);
            return new TAffectedHistory { Offset = 0, Pts = cachedPts, PtsCount = 0 };
        }

        switch (peer.PeerType)
        {
            case PeerType.Chat:
                {
                    var command = new StartDeleteChatMessagesCommand(ChatId.Create(peer.PeerId),
                        input.ToRequestInfo(),
                        messageIdList,
                        obj.Revoke,
                        true,
                        Guid.NewGuid());
                    await _commandBus.PublishAsync(command, default);
                }
                break;
            case PeerType.User:
                {
                    if (obj.Peer is TInputPeerUser inputUser)
                    {
                        await _accessHashHelper.CheckAccessHashAsync(inputUser.UserId, inputUser.AccessHash);
                    }
                    var command = new StartDeleteUserMessagesCommand(DialogId.Create(input.UserId, peer),
                        input.ToRequestInfo(),
                        obj.Revoke,
                        messageIdList,
                        true,
                        Guid.NewGuid());
                    await _commandBus.PublishAsync(command, default);
                }
                break;
        }


        return null!;
    }
}
