namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get saved GIFs.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getSavedGifs"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetSavedGifsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSavedGifs, MyTelegram.Schema.Messages.ISavedGifs>
{
    protected override Task<ISavedGifs> HandleCoreAsync(IRequestInput input, RequestGetSavedGifs obj)
    {
        return Task.FromResult<ISavedGifs>(new TSavedGifs { Gifs = [] });
    }
}