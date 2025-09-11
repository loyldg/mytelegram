namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Report a secret chat for spam
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.reportEncryptedSpam" />
///</summary>
internal sealed class ReportEncryptedSpamHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReportEncryptedSpam, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReportEncryptedSpam obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
