namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Disable ads on the specified channel, for all users.Available only after reaching at least the <a href="https://corefork.telegram.org/api/boost">boost level »</a> specified in the <a href="https://corefork.telegram.org/api/config#channel-restrict-sponsored-level-min"><code>channel_restrict_sponsored_level_min</code> »</a> config parameter.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.restrictSponsoredMessages" />
///</summary>
internal sealed class RestrictSponsoredMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestRestrictSponsoredMessages, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestRestrictSponsoredMessages obj)
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
