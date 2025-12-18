namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Install a stickerset
/// Possible errors
/// Code Type Description
/// 406 STICKERSET_INVALID The provided sticker set is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.installStickerSet"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class InstallStickerSetHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestInstallStickerSet, MyTelegram.Schema.Messages.IStickerSetInstallResult>
{
    protected override Task<MyTelegram.Schema.Messages.IStickerSetInstallResult> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestInstallStickerSet obj)
    {
        throw new NotImplementedException();
    }
}