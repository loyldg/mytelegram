// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Edit an inline bot message
/// <para>Possible errors</para>
/// Code Type Description
/// 400 BUTTON_DATA_INVALID The data of one or more of the buttons you provided is invalid.
/// 400 ENTITY_BOUNDS_INVALID A specified <a href="https://corefork.telegram.org/api/entities#entity-length">entity offset or length</a> is invalid, see <a href="https://corefork.telegram.org/api/entities#entity-length">here »</a> for info on how to properly compute the entity offset/length.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 MESSAGE_NOT_MODIFIED The provided message data is identical to the previous message data, the message wasn't modified.
/// See <a href="https://corefork.telegram.org/method/messages.editInlineBotMessage" />
///</summary>
internal sealed class EditInlineBotMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestEditInlineBotMessage, IBool>,
    Messages.IEditInlineBotMessageHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestEditInlineBotMessage obj)
    {
        throw new NotImplementedException();
    }
}
