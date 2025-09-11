namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Edit location of geogroup
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 MEGAGROUP_REQUIRED You can only use this method on a supergroup.
/// See <a href="https://corefork.telegram.org/method/channels.editLocation" />
///</summary>
internal sealed class EditLocationHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestEditLocation, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestEditLocation obj)
    {
        return Task.FromResult<IBool>(new MyTelegram.Schema.TBoolTrue());
    }
}
