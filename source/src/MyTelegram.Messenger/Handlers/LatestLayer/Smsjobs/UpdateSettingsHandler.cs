namespace MyTelegram.Messenger.Handlers.LatestLayer.Smsjobs;

///<summary>
/// Update SMS job settings (official clients only).
/// <para>Possible errors</para>
/// Code Type Description
/// 400 NOT_JOINED The current user hasn't joined the Peer-to-Peer Login Program.
/// See <a href="https://corefork.telegram.org/method/smsjobs.updateSettings" />
///</summary>
internal sealed class UpdateSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Smsjobs.RequestUpdateSettings, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Smsjobs.RequestUpdateSettings obj)
    {
        throw new NotImplementedException();
    }
}
