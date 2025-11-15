namespace MyTelegram.Messenger.Handlers.LatestLayer.Smsjobs;
/// <summary>
/// Get SMS jobs status (official clients only).
/// <para><c>See <a href="https://corefork.telegram.org/method/smsjobs.getStatus"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetStatusHandler : RpcResultObjectHandler<MyTelegram.Schema.Smsjobs.RequestGetStatus, MyTelegram.Schema.Smsjobs.IStatus>
{
    protected override Task<MyTelegram.Schema.Smsjobs.IStatus> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Smsjobs.RequestGetStatus obj)
    {
        throw new NotImplementedException();
    }
}