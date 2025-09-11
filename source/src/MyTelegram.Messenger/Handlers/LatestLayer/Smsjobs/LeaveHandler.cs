namespace MyTelegram.Messenger.Handlers.LatestLayer.Smsjobs;

///<summary>
/// Disable SMS jobs (official clients only).
/// See <a href="https://corefork.telegram.org/method/smsjobs.leave" />
///</summary>
internal sealed class LeaveHandler : RpcResultObjectHandler<MyTelegram.Schema.Smsjobs.RequestLeave, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Smsjobs.RequestLeave obj)
    {
        throw new NotImplementedException();
    }
}
