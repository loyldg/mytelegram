// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Report a <a href="https://corefork.telegram.org/api/reactions">message reaction</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.reportReaction" />
///</summary>
internal sealed class ReportReactionHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReportReaction, IBool>,
    Messages.IReportReactionHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReportReaction obj)
    {
        throw new NotImplementedException();
    }
}
