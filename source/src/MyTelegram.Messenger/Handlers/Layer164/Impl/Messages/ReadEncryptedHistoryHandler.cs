// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Marks message history within a secret chat as read.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MSG_WAIT_FAILED A waiting call returned an error.
/// See <a href="https://corefork.telegram.org/method/messages.readEncryptedHistory" />
///</summary>
internal sealed class ReadEncryptedHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReadEncryptedHistory, IBool>,
    Messages.IReadEncryptedHistoryHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReadEncryptedHistory obj)
    {
        throw new NotImplementedException();
    }
}
