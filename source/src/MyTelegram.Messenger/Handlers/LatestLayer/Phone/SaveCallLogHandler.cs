namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;

///<summary>
/// Save phone call debug information
/// See <a href="https://corefork.telegram.org/method/phone.saveCallLog" />
///</summary>
internal sealed class SaveCallLogHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestSaveCallLog, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestSaveCallLog obj)
    {
        throw new NotImplementedException();
    }
}
