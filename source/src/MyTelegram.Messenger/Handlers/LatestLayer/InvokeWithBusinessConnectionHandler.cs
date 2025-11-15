namespace MyTelegram.Messenger.Handlers;
/// <summary>
/// Invoke a method using a <a href="https://corefork.telegram.org/api/bots/connected-business-bots">Telegram Business Bot connection, see here » for more info, including a list of the methods that can be wrapped in this constructor</a>.Make sure to always send queries wrapped in a <code>invokeWithBusinessConnection</code> to the datacenter ID, specified in the <code>dc_id</code> field of the <a href="https://corefork.telegram.org/constructor/botBusinessConnection">botBusinessConnection</a> that is being used.
/// <para><c>See <a href="https://corefork.telegram.org/method/invokeWithBusinessConnection"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class InvokeWithBusinessConnectionHandler : RpcResultObjectHandler<MyTelegram.Schema.RequestInvokeWithBusinessConnection, IObject>
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.RequestInvokeWithBusinessConnection obj)
    {
        throw new NotImplementedException();
    }
}