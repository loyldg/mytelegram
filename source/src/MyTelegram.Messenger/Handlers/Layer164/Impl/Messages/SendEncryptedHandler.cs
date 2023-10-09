// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Sends a text message to a secret chat.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 DATA_INVALID Encrypted data invalid.
/// 400 DATA_TOO_LONG Data too long.
/// 400 ENCRYPTION_DECLINED The secret chat was declined.
/// 400 MSG_WAIT_FAILED A waiting call returned an error.
/// 403 USER_IS_BLOCKED You were blocked by this user.
/// See <a href="https://corefork.telegram.org/method/messages.sendEncrypted" />
///</summary>
internal sealed class SendEncryptedHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendEncrypted, MyTelegram.Schema.Messages.ISentEncryptedMessage>,
    Messages.ISendEncryptedHandler
{
    protected override Task<MyTelegram.Schema.Messages.ISentEncryptedMessage> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSendEncrypted obj)
    {
        throw new NotImplementedException();
    }
}
