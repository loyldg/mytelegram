namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Enable or disable the <a href="https://corefork.telegram.org/api/antispam">native antispam system</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// See <a href="https://corefork.telegram.org/method/channels.toggleAntiSpam" />
///</summary>
internal sealed class ToggleAntiSpamHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleAntiSpam, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleAntiSpam obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates
        {
            Chats = [],
            Updates = [],
            Users = [],
            Date = CurrentDate
        });
    }
}
