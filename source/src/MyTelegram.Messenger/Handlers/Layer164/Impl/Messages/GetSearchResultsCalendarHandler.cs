// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Returns information about the next messages of the specified type in the chat split by days.Returns the results in reverse chronological order.<br>
/// Can return partial results for the last returned day.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 FILTER_NOT_SUPPORTED The specified filter cannot be used in this context.
/// See <a href="https://corefork.telegram.org/method/messages.getSearchResultsCalendar" />
///</summary>
internal sealed class GetSearchResultsCalendarHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSearchResultsCalendar, MyTelegram.Schema.Messages.ISearchResultsCalendar>,
    Messages.IGetSearchResultsCalendarHandler
{
    protected override Task<MyTelegram.Schema.Messages.ISearchResultsCalendar> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetSearchResultsCalendar obj)
    {
        throw new NotImplementedException();
    }
}
