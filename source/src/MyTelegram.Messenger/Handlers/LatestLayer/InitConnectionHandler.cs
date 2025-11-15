namespace MyTelegram.Messenger.Handlers;
/// <summary>
/// Initialize connection
/// Possible errors
/// Code Type Description
/// 400 CONNECTION_LAYER_INVALID Layer invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/initConnection"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✔]
/// </remarks>
internal sealed class InitConnectionHandler : BaseObjectHandler<MyTelegram.Schema.RequestInitConnection, IObject>
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.RequestInitConnection obj)
    {
        throw new NotImplementedException();
    }
}