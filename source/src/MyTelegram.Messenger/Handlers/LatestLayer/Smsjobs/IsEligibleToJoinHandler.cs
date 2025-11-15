namespace MyTelegram.Messenger.Handlers.LatestLayer.Smsjobs;
/// <summary>
/// Check if we can process SMS jobs (official clients only).
/// <para><c>See <a href="https://corefork.telegram.org/method/smsjobs.isEligibleToJoin"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class IsEligibleToJoinHandler : RpcResultObjectHandler<MyTelegram.Schema.Smsjobs.RequestIsEligibleToJoin, MyTelegram.Schema.Smsjobs.IEligibilityToJoin>
{
    protected override Task<MyTelegram.Schema.Smsjobs.IEligibilityToJoin> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Smsjobs.RequestIsEligibleToJoin obj)
    {
        throw new NotImplementedException();
    }
}