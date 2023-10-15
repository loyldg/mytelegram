// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Associate a group to a channel as <a href="https://corefork.telegram.org/api/discussion">discussion group</a> for that channel
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BROADCAST_ID_INVALID Broadcast ID invalid.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 LINK_NOT_MODIFIED Discussion link not modified.
/// 400 MEGAGROUP_ID_INVALID Invalid supergroup ID.
/// 400 MEGAGROUP_PREHISTORY_HIDDEN Group with hidden history for new members can't be set as discussion groups.
/// See <a href="https://corefork.telegram.org/method/channels.setDiscussionGroup" />
///</summary>
internal sealed class SetDiscussionGroupHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestSetDiscussionGroup, IBool>,
    Channels.ISetDiscussionGroupHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IAccessHashHelper _accessHashHelper;
    public SetDiscussionGroupHandler(ICommandBus commandBus,
        IAccessHashHelper accessHashHelper)
    {
        _commandBus = commandBus;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetDiscussionGroup obj)
    {
        if (obj.Broadcast is TInputChannel broadcastChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(broadcastChannel.ChannelId, broadcastChannel.AccessHash);
        }
        else
        {
            throw new NotImplementedException();
        }

        long? groupId = null;

        if (obj.Group is TInputChannel groupChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(groupChannel.ChannelId, groupChannel.AccessHash);
        }

        switch (obj.Group)
        {
            case TInputChannel inputChannel:
                groupId = inputChannel.ChannelId;
                break;

            case TInputChannelEmpty _:
                break;

            case TInputChannelFromMessage _:
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        var command = new SetDiscussionGroupCommand(ChannelId.Create(broadcastChannel.ChannelId),
            input.ToRequestInfo(),
            input.UserId,
            broadcastChannel.ChannelId,
            groupId);
        await _commandBus.PublishAsync(command, CancellationToken.None);
        return null!;
    }
}
