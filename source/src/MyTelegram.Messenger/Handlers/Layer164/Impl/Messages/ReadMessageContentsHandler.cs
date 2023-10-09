// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Notifies the sender about the recipient having listened a voice message or watched a video.
/// See <a href="https://corefork.telegram.org/method/messages.readMessageContents" />
///</summary>
internal sealed class ReadMessageContentsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReadMessageContents, MyTelegram.Schema.Messages.IAffectedMessages>,
    Messages.IReadMessageContentsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReadMessageContents obj)
    {
        throw new NotImplementedException();
    }
}
