namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Uninstall a stickerset
/// Possible errors
/// Code Type Description
/// 406 STICKERSET_INVALID The provided sticker set is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.uninstallStickerSet"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UninstallStickerSetHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestUninstallStickerSet, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestUninstallStickerSet obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}