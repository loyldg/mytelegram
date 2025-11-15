namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Add GIF to saved gifs list
/// Possible errors
/// Code Type Description
/// 400 GIF_ID_INVALID The provided GIF ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.saveGif"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SaveGifHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSaveGif, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSaveGif obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}