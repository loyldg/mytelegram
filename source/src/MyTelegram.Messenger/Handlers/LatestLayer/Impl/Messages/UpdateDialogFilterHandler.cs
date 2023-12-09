// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Update <a href="https://corefork.telegram.org/api/folders">folder</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHATLIST_EXCLUDE_INVALID &nbsp;
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 FILTER_ID_INVALID The specified filter ID is invalid.
/// 400 FILTER_INCLUDE_EMPTY The include_peers vector of the filter is empty.
/// 400 FILTER_TITLE_EMPTY The title field of the filter is empty.
/// See <a href="https://corefork.telegram.org/method/messages.updateDialogFilter" />
///</summary>
internal sealed class UpdateDialogFilterHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestUpdateDialogFilter, IBool>,
    Messages.IUpdateDialogFilterHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    public UpdateDialogFilterHandler(ICommandBus commandBus,
        IPeerHelper peerHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestUpdateDialogFilter obj)
    {
        if (obj.Filter == null)
        {
            var command = new DeleteDialogFilterCommand(DialogFilterId.Create(input.UserId, obj.Id), input.ToRequestInfo());
            await _commandBus.PublishAsync(command, default);
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
                    f.Title,
                    f.Emoticon,
                    pinnedPeers,
                    includePeers,
                    excludePeers);
                var command = new UpdateDialogFilterCommand(DialogFilterId.Create(input.UserId, obj.Id), input.ToRequestInfo(), input.UserId, filter);
                await _commandBus.PublishAsync(command, default);
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
        var peer = _peerHelper.GetPeer(inputPeer);
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
