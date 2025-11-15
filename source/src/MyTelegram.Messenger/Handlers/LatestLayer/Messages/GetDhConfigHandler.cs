namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Returns configuration parameters for Diffie-Hellman key generation. Can also return a random sequence of bytes of required length.
/// Possible errors
/// Code Type Description
/// 400 RANDOM_LENGTH_INVALID Random length invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getDhConfig"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetDhConfigHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetDhConfig, MyTelegram.Schema.Messages.IDhConfig>
{
    protected override Task<MyTelegram.Schema.Messages.IDhConfig> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetDhConfig obj)
    {
        throw new NotImplementedException();
    }
}