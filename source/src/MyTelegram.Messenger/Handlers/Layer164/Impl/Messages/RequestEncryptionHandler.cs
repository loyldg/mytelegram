// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Sends a request to start a secret chat to the user.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 DH_G_A_INVALID g_a invalid.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.requestEncryption" />
///</summary>
internal sealed class RequestEncryptionHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestRequestEncryption, MyTelegram.Schema.IEncryptedChat>,
    Messages.IRequestEncryptionHandler
{
    protected override Task<MyTelegram.Schema.IEncryptedChat> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestRequestEncryption obj)
    {
        throw new NotImplementedException();
    }
}
