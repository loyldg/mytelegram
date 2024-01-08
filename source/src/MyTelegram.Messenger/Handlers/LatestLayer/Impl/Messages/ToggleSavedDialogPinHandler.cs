// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.toggleSavedDialogPin" />
///</summary>
internal sealed class ToggleSavedDialogPinHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleSavedDialogPin, IBool>,
    Messages.IToggleSavedDialogPinHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestToggleSavedDialogPin obj)
    {
        throw new NotImplementedException();
    }
}
