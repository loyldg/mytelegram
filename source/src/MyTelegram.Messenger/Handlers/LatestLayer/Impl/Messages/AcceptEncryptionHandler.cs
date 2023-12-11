// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Confirms creation of a secret chat
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 ENCRYPTION_ALREADY_ACCEPTED Secret chat already accepted.
/// 400 ENCRYPTION_ALREADY_DECLINED The secret chat was already declined.
/// See <a href="https://corefork.telegram.org/method/messages.acceptEncryption" />
///</summary>
internal sealed class AcceptEncryptionHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestAcceptEncryption, MyTelegram.Schema.IEncryptedChat>,
    Messages.IAcceptEncryptionHandler
{
    protected override Task<MyTelegram.Schema.IEncryptedChat> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestAcceptEncryption obj)
    {
        throw new NotImplementedException();
    }
}
