namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Delete the entire phone call history.
/// See <a href="https://corefork.telegram.org/method/messages.deletePhoneCallHistory" />
///</summary>
internal sealed class DeletePhoneCallHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeletePhoneCallHistory, MyTelegram.Schema.Messages.IAffectedFoundMessages>
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedFoundMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestDeletePhoneCallHistory obj)
    {
        throw new NotImplementedException();
    }
}
