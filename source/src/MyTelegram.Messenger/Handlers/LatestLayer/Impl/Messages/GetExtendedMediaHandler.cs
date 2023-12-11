// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get information about extended media
/// See <a href="https://corefork.telegram.org/method/messages.getExtendedMedia" />
///</summary>
internal sealed class GetExtendedMediaHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetExtendedMedia, MyTelegram.Schema.IUpdates>,
    Messages.IGetExtendedMediaHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetExtendedMedia obj)
    {
        throw new NotImplementedException();
    }
}
