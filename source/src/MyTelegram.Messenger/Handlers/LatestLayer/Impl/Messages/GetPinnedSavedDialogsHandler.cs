// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.getPinnedSavedDialogs" />
///</summary>
internal sealed class GetPinnedSavedDialogsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPinnedSavedDialogs, MyTelegram.Schema.Messages.ISavedDialogs>,
    Messages.IGetPinnedSavedDialogsHandler
{
    protected override Task<MyTelegram.Schema.Messages.ISavedDialogs> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetPinnedSavedDialogs obj)
    {
        throw new NotImplementedException();
    }
}
