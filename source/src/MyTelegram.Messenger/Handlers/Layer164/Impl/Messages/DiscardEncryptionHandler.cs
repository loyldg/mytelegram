// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Cancels a request for creation and/or delete info on secret chat.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ID_EMPTY The provided chat ID is empty.
/// 400 ENCRYPTION_ALREADY_DECLINED The secret chat was already declined.
/// 400 ENCRYPTION_ID_INVALID The provided secret chat ID is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.discardEncryption" />
///</summary>
internal sealed class DiscardEncryptionHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDiscardEncryption, IBool>,
    Messages.IDiscardEncryptionHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestDiscardEncryption obj)
    {
        throw new NotImplementedException();
    }
}
