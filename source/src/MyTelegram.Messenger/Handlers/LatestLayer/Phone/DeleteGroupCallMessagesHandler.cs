
namespace MyTelegram.Messenger.Handlers.Phone;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class DeleteGroupCallMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestDeleteGroupCallMessages, MyTelegram.Schema.IUpdates>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestDeleteGroupCallMessages obj)
    {
        throw new NotImplementedException();
    }
}

