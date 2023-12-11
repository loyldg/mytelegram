// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Export an invite link for a chat
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 EXPIRE_DATE_INVALID The specified expiration date is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USAGE_LIMIT_INVALID The specified usage limit is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.exportChatInvite" />
///</summary>
internal sealed class ExportChatInviteHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestExportChatInvite, MyTelegram.Schema.IExportedChatInvite>,
    Messages.IExportChatInviteHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IRandomHelper _randomHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IIdGenerator _idGenerator;
    public ExportChatInviteHandler(ICommandBus commandBus,
        IRandomHelper randomHelper,
        IAccessHashHelper accessHashHelper, IIdGenerator idGenerator)
    {
        _commandBus = commandBus;
        _randomHelper = randomHelper;
        _accessHashHelper = accessHashHelper;
        _idGenerator = idGenerator;
    }

    protected override async Task<MyTelegram.Schema.IExportedChatInvite> HandleCoreAsync(IRequestInput input,
        RequestExportChatInvite obj)
    {
        if (obj.Peer is TInputPeerChannel inputPeerChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputPeerChannel.ChannelId, inputPeerChannel.AccessHash);

            //var inviteUrl = _randomHelper.GenerateRandomString(16);

            var chatInviteId = await _idGenerator.NextLongIdAsync(IdType.InviteId, inputPeerChannel.ChannelId);
            var bytes = new byte[12];
            _randomHelper.NextBytes(bytes);
            var inviteHash = $"{Convert.ToBase64String(bytes)
                .Replace($"+", "/")
                .Replace("=", string.Empty)}";

            var command = new ExportChatInviteCommand(ChannelId.Create(inputPeerChannel.ChannelId),
                input.ToRequestInfo(),
                input.UserId,
                chatInviteId,
                obj.Title,
                obj.RequestNeeded,
                obj.ExpireDate,
                obj.UsageLimit,
                obj.LegacyRevokePermanent,
                inviteHash,
                CurrentDate);
            await _commandBus.PublishAsync(command, CancellationToken.None);
            return null!;
        }

        throw new NotImplementedException();
    }
}
