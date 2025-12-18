namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Enable or disable the <a href="https://corefork.telegram.org/api/antispam">native antispam system</a>.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.toggleAntiSpam"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleAntiSpamHandler(IChannelAdminRightsChecker channelAdminRightsChecker) : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleAntiSpam, MyTelegram.Schema.IUpdates>
{
    protected override async Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Channels.RequestToggleAntiSpam obj)
    {
        await channelAdminRightsChecker.ThrowIfNotChannelOwnerAsync(obj.Channel, input.UserId);
        return new TUpdates
        {
            Chats = [],
            Updates = [],
            Users = [],
            Date = CurrentDate
        };
    }
}