// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Returns configuration parameters for Diffie-Hellman key generation. Can also return a random sequence of bytes of required length.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 RANDOM_LENGTH_INVALID Random length invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getDhConfig" />
///</summary>
internal sealed class GetDhConfigHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetDhConfig, MyTelegram.Schema.Messages.IDhConfig>,
    Messages.IGetDhConfigHandler
{
    protected override Task<MyTelegram.Schema.Messages.IDhConfig> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetDhConfig obj)
    {
        throw new NotImplementedException();
    }
}
