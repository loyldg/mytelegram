namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Sends a request to start a secret chat to the user.
/// Possible errors
/// Code Type Description
/// 400 DH_G_A_INVALID g_a invalid.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// 403 USER_IS_BLOCKED You were blocked by this user.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.requestEncryption"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class RequestEncryptionHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestRequestEncryption, MyTelegram.Schema.IEncryptedChat>
{
    protected override Task<MyTelegram.Schema.IEncryptedChat> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestRequestEncryption obj)
    {
        throw new NotImplementedException();
    }
}