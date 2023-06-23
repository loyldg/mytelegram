using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class CheckUsernameHandler : RpcResultObjectHandler<RequestCheckUsername, IBool>,
    ICheckUsernameHandler, IProcessedHandler
{
    private readonly IQueryProcessor _queryProcessor;

    public CheckUsernameHandler(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestCheckUsername obj)
    {
        var item = await _queryProcessor
            .ProcessAsync(new GetUserNameByIdQuery(obj.Username),
                CancellationToken.None);
        if (item == null) return new TBoolTrue();

        return new TBoolFalse();

        //if (obj.Channel is TInputChannel inputChannel)
        //{
        //    //var command = new CheckUserNameCommand(
        //    //    UserNameId.Create(PeerType.Channel, inputChannel.ChannelId, obj.Username),
        //    //    input.ReqMsgId,
        //    //    obj.Username);
        //    //await CommandBus.PublishAsync(command, CancellationToken.None);
        //    var item = await QueryProcessor
        //        .ProcessAsync(new GetUserNameByIdQuery(PeerType.Channel, inputChannel.ChannelId, obj.Username),
        //            CancellationToken.None);
        //    if (item == null)
        //    {
        //        return new TBoolTrue();
        //    }

        //    return new TBoolFalse();
        //}

        //return new TBoolFalse();
    }
}