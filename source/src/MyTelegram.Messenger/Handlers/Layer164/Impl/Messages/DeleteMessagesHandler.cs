// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Deletes messages by their identifiers.
/// <para>Possible errors</para>
/// Code Type Description
/// 403 MESSAGE_DELETE_FORBIDDEN You can't delete one of the messages you tried to delete, most likely because it is a service message.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.deleteMessages" />
///</summary>
internal sealed class DeleteMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteMessages, MyTelegram.Schema.Messages.IAffectedMessages>,
    Messages.IDeleteMessagesHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestDeleteMessages obj)
    {
        throw new NotImplementedException();
    }
}
