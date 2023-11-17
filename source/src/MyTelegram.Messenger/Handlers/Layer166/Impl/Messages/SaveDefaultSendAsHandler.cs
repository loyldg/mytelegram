// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Change the default peer that should be used when sending messages to a specific group
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 SEND_AS_PEER_INVALID You can't send messages as the specified peer.
/// See <a href="https://corefork.telegram.org/method/messages.saveDefaultSendAs" />
///</summary>
internal sealed class SaveDefaultSendAsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSaveDefaultSendAs, IBool>,
    Messages.ISaveDefaultSendAsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSaveDefaultSendAs obj)
    {
        throw new NotImplementedException();
    }
}
