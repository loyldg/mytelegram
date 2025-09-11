namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Edit/create a <a href="https://corefork.telegram.org/api/factcheck">fact-check</a> on a message.Can only be used by independent fact-checkers as specified by the <a href="https://corefork.telegram.org/api/config#can-edit-factcheck">appConfig.can_edit_factcheck</a> configuration flag.
/// <para>Possible errors</para>
/// Code Type Description
/// 403 CHAT_ACTION_FORBIDDEN You cannot execute this action.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.editFactCheck" />
///</summary>
internal sealed class EditFactCheckHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestEditFactCheck, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestEditFactCheck obj)
    {
        throw new NotImplementedException();
    }
}
