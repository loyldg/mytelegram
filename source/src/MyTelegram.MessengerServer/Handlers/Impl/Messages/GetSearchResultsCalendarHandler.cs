// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class GetSearchResultsCalendarHandler :
    RpcResultObjectHandler<RequestGetSearchResultsCalendar, ISearchResultsCalendar>,
    Messages.IGetSearchResultsCalendarHandler
{
    protected override Task<ISearchResultsCalendar> HandleCoreAsync(IRequestInput input,
        RequestGetSearchResultsCalendar obj)
    {
        throw new NotImplementedException();
    }
}
