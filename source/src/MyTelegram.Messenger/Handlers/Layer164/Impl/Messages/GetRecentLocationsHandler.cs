// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get live location history of a certain user
/// See <a href="https://corefork.telegram.org/method/messages.getRecentLocations" />
///</summary>
internal sealed class GetRecentLocationsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetRecentLocations, MyTelegram.Schema.Messages.IMessages>,
    Messages.IGetRecentLocationsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetRecentLocations obj)
    {
        throw new NotImplementedException();
    }
}
