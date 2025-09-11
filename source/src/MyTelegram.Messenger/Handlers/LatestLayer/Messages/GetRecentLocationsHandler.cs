namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Get live location history of a certain user
/// See <a href="https://corefork.telegram.org/method/messages.getRecentLocations" />
///</summary>
internal sealed class GetRecentLocationsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetRecentLocations, MyTelegram.Schema.Messages.IMessages>
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetRecentLocations obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IMessages>(new TMessages
        {
            Chats = [],
            Messages = [],
            Users = [],
        });
    }
}
