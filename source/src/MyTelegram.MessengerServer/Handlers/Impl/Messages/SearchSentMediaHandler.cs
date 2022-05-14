// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class SearchSentMediaHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSearchSentMedia, MyTelegram.Schema.Messages.IMessages>,
    Messages.ISearchSentMediaHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSearchSentMedia obj)
    {
        throw new NotImplementedException();
    }
}
