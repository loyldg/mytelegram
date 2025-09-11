namespace MyTelegram.Messenger.Handlers.LatestLayer.Smsjobs;

///<summary>
/// Check if we can process SMS jobs (official clients only).
/// See <a href="https://corefork.telegram.org/method/smsjobs.isEligibleToJoin" />
///</summary>
internal sealed class IsEligibleToJoinHandler : RpcResultObjectHandler<MyTelegram.Schema.Smsjobs.RequestIsEligibleToJoin, MyTelegram.Schema.Smsjobs.IEligibilityToJoin>
{
    protected override Task<MyTelegram.Schema.Smsjobs.IEligibilityToJoin> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Smsjobs.RequestIsEligibleToJoin obj)
    {
        throw new NotImplementedException();
    }
}
