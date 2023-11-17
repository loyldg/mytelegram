// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get saved GIFs
/// See <a href="https://corefork.telegram.org/method/messages.getSavedGifs" />
///</summary>
internal sealed class GetSavedGifsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSavedGifs, MyTelegram.Schema.Messages.ISavedGifs>,
    Messages.IGetSavedGifsHandler
{
    protected override Task<MyTelegram.Schema.Messages.ISavedGifs> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetSavedGifs obj)
    {
        return Task.FromResult<ISavedGifs>(new TSavedGifs { Gifs = new TVector<IDocument>() });
    }
}
