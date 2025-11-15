namespace MyTelegram.Messenger.Handlers.LatestLayer.Smsjobs;
/// <summary>
/// Update SMS job settings (official clients only).
/// Possible errors
/// Code Type Description
/// 400 NOT_JOINED The current user hasn't joined the Peer-to-Peer Login Program.
/// <para><c>See <a href="https://corefork.telegram.org/method/smsjobs.updateSettings"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Smsjobs.RequestUpdateSettings, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Smsjobs.RequestUpdateSettings obj)
    {
        throw new NotImplementedException();
    }
}