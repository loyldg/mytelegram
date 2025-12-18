namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Changes the default value of the Time-To-Live setting, applied to all new chats.
/// Possible errors
/// Code Type Description
/// 400 TTL_PERIOD_INVALID The specified TTL period is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.setDefaultHistoryTTL"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SetDefaultHistoryTTLHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetDefaultHistoryTTL, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSetDefaultHistoryTTL obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}