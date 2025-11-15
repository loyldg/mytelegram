namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Edit location of geogroup, see <a href="https://corefork.telegram.org/api/nearby">here »</a> for more info on geogroups.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 MEGAGROUP_GEO_REQUIRED This method can only be invoked on a geogroup.
/// 400 MEGAGROUP_REQUIRED You can only use this method on a supergroup.
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.editLocation"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class EditLocationHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestEditLocation, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Channels.RequestEditLocation obj)
    {
        return Task.FromResult<IBool>(new MyTelegram.Schema.TBoolTrue());
    }
}