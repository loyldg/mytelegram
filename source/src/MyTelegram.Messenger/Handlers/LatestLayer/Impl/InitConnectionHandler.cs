// ReSharper disable All

namespace MyTelegram.Handlers;

///<summary>
/// Initialize connection
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CONNECTION_LAYER_INVALID Layer invalid.
/// See <a href="https://corefork.telegram.org/method/initConnection" />
///</summary>
internal sealed class InitConnectionHandler : BaseObjectHandler<MyTelegram.Schema.RequestInitConnection, IObject>,
    IInitConnectionHandler
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.RequestInitConnection obj)
    {
        throw new NotImplementedException();
    }
}
