
namespace MyTelegram.Messenger.Handlers.LatestLayer.Communities;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class CreateHandler : RpcResultObjectHandler<MyTelegram.Schema.Communities.RequestCreate, MyTelegram.Schema.IUpdates>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Communities.RequestCreate obj)
    {
        return Task.FromResult<MyTelegram.Schema.IUpdates>(new MyTelegram.Schema.TUpdates
        {
            Updates = [],
            Users = [],
            Chats = [],
            Date = CurrentDate
        });
    }
}

