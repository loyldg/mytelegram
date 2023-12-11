// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Sends a service message to a secret chat.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 DATA_INVALID Encrypted data invalid.
/// 400 ENCRYPTION_DECLINED The secret chat was declined.
/// 400 ENCRYPTION_ID_INVALID The provided secret chat ID is invalid.
/// 500 MSG_WAIT_FAILED A waiting call returned an error.
/// 403 USER_DELETED You can't send this secret message because the other participant deleted their account.
/// 403 USER_IS_BLOCKED You were blocked by this user.
/// See <a href="https://corefork.telegram.org/method/messages.sendEncryptedService" />
///</summary>
internal sealed class SendEncryptedServiceHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendEncryptedService, MyTelegram.Schema.Messages.ISentEncryptedMessage>,
    Messages.ISendEncryptedServiceHandler
{
    protected override Task<MyTelegram.Schema.Messages.ISentEncryptedMessage> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSendEncryptedService obj)
    {
        throw new NotImplementedException();
    }
}
