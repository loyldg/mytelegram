namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.getPreparedInlineMessage" />
///</summary>
internal sealed class GetPreparedInlineMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPreparedInlineMessage, MyTelegram.Schema.Messages.IPreparedInlineMessage>
{
    protected override Task<MyTelegram.Schema.Messages.IPreparedInlineMessage> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetPreparedInlineMessage obj)
    {
        throw new NotImplementedException();
    }
}
