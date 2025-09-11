namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Update <a href="https://corefork.telegram.org/api/folders">folder</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHATLIST_EXCLUDE_INVALID The specified <code>exclude_peers</code> are invalid.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 FILTER_ID_INVALID The specified filter ID is invalid.
/// 400 FILTER_INCLUDE_EMPTY The include_peers vector of the filter is empty.
/// 400 FILTER_TITLE_EMPTY The title field of the filter is empty.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.updateDialogFilter" />
///</summary>
internal sealed class UpdateDialogFilterHandler(
    ICommandBus commandBus,
    IPeerHelper peerHelper) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestUpdateDialogFilter, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestUpdateDialogFilter obj)
    {
        if (obj.Filter == null)
        {
            var command = new DeleteDialogFilterCommand(DialogFilterId.Create(input.UserId, obj.Id), input.ToRequestInfo());
            await commandBus.PublishAsync(command, default);
        }
        else
        {
            if (obj.Filter is TDialogFilter f)
            {
                var pinnedPeers = f.PinnedPeers.Select(GetInputPeer).ToList();
                var includePeers = f.IncludePeers.Select(GetInputPeer).ToList();
                var excludePeers = f.ExcludePeers.Select(GetInputPeer).ToList();
                var filter = new DialogFilter(obj.Id,
                    f.Contacts,
                    f.NonContacts,
                    f.Groups,
                    f.Broadcasts,
                    f.Bots,
                    f.ExcludeMuted,
                    f.ExcludeRead,
                    f.ExcludeArchived,
                    f.TitleNoanimate,
                    f.Title,
                    f.Emoticon,
                    f.Color,
                    pinnedPeers,
                    includePeers,
                    excludePeers, false);
                var command = new UpdateDialogFilterCommand(DialogFilterId.Create(input.UserId, obj.Id), input.ToRequestInfo(), input.UserId, filter.Id, filter);
                await commandBus.PublishAsync(command, default);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        return new TBoolTrue();
    }

    private InputPeer GetInputPeer(IInputPeer inputPeer)
    {
        var peer = peerHelper.GetPeer(inputPeer);
        long accessHash = 0;
        switch (inputPeer)
        {
            case TInputPeerChannel inputPeerChannel:
                accessHash = inputPeerChannel.AccessHash;
                break;
            case TInputPeerChat:
                break;
            case TInputPeerEmpty:
                break;
            case TInputPeerSelf:
                break;
            case TInputPeerUser inputPeerUser:
                accessHash = inputPeerUser.AccessHash;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(peer));
        }

        return new InputPeer(peer, accessHash);
    }
}
