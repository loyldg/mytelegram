// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class GetSearchResultsCalendarHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSearchResultsCalendar, MyTelegram.Schema.Messages.ISearchResultsCalendar>,
    Messages.IGetSearchResultsCalendarHandler
{
    protected override Task<MyTelegram.Schema.Messages.ISearchResultsCalendar> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetSearchResultsCalendar obj)
    {
        throw new NotImplementedException();
    }
}
