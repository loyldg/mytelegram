namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Reports some messages from a user in a supergroup as spam; requires administrator rights in the supergroup
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.reportSpam" />
///</summary>
internal sealed class ReportSpamHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestReportSpam, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestReportSpam obj)
    {
        return System.Threading.Tasks.Task.FromResult<IBool>(new TBoolTrue());
    }
}
