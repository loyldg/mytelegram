namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get dialogs manually marked as unread
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getDialogUnreadMarks"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetDialogUnreadMarksHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetDialogUnreadMarks, TVector<MyTelegram.Schema.IDialogPeer>>
{
    protected override Task<TVector<MyTelegram.Schema.IDialogPeer>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetDialogUnreadMarks obj)
    {
        return Task.FromResult<TVector<IDialogPeer>>([]);
    }
}