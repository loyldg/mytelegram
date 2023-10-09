namespace MyTelegram.Messenger.Services.Interfaces;

public interface IOffsetHelper
{
    OffsetInfo GetOffsetInfo(GetPagedListInput input);
    OffsetInfo GetOffsetInfo(int addOffset, int offsetId, int limit, int minId, int maxId, int minDate, int maxDate);
}