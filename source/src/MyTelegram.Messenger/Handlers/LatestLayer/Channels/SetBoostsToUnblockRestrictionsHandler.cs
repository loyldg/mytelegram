namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Admins with <a href="https://corefork.telegram.org/constructor/chatAdminRights">ban_users admin rights »</a> may allow users that apply a certain number of <a href="https://corefork.telegram.org/api/boost">booosts »</a> to the group to bypass <a href="https://corefork.telegram.org/method/channels.toggleSlowMode">slow mode »</a> and <a href="https://corefork.telegram.org/api/rights#default-rights">other »</a> supergroup restrictions, see <a href="https://corefork.telegram.org/api/boost#bypass-slowmode-and-chat-restrictions">here »</a> for more info.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.setBoostsToUnblockRestrictions" />
///</summary>
internal sealed class SetBoostsToUnblockRestrictionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestSetBoostsToUnblockRestrictions, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestSetBoostsToUnblockRestrictions obj)
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
