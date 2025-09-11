namespace MyTelegram.Messenger.Handlers.LatestLayer.Smsjobs;

///<summary>
/// Get SMS jobs status (official clients only).
/// See <a href="https://corefork.telegram.org/method/smsjobs.getStatus" />
///</summary>
internal sealed class GetStatusHandler : RpcResultObjectHandler<MyTelegram.Schema.Smsjobs.RequestGetStatus, MyTelegram.Schema.Smsjobs.IStatus>
{
    protected override Task<MyTelegram.Schema.Smsjobs.IStatus> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Smsjobs.RequestGetStatus obj)
    {
        throw new NotImplementedException();
    }
}
