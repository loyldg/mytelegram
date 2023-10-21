namespace MyTelegram.Messenger.Services.Impl;

public class OffsetHelper : IOffsetHelper
{
    public OffsetInfo GetOffsetInfo(GetPagedListInput input)
    {
        return GetOffsetInfo(input.AddOffset, input.OffsetId, input.Limit, input.MinId, input.MaxId, input.MinDate,
            input.MaxDate);
    }

    public OffsetInfo GetOffsetInfo(int addOffset, int offsetId, int limit, int minId, int maxId, int minDate, int maxDate)
    {
        var loadType = GetOffsetLoadType(limit, addOffset, maxId);
        //var offsetId = 20;
        var newMaxId = 0;
        var fromId = 0;
        switch (loadType)
        {
            case LoadType.Backward:
                fromId = offsetId - limit;
                newMaxId = offsetId;
                break;

            case LoadType.Forward:
                fromId = offsetId;
                //maxId = input.OffsetId + input.Limit;
                break;

            case LoadType.FirstUnread:
                break;

            case LoadType.AroundMessage:
                fromId = offsetId + addOffset;
                //maxId = input.OffsetId + input.AddOffset + input.Limit;
                break;

            case LoadType.AroundDate:
                break;

            case LoadType.LimitIs1:

                break;

            default:
                throw new ArgumentOutOfRangeException($"unsuporrted load type:{loadType}");
        }

        //Logger.LogDebug($"input:AddOffset={input.AddOffset} OffsetId={input.OffsetId} Limit={input.Limit} MaxId={input.MaxId} {loadType} fromId:{fromId}  maxId:{maxId} limit:{input.Limit}");
        return new OffsetInfo { MaxId = newMaxId, FromId = fromId, LoadType = loadType };
    }

    private static LoadType GetOffsetLoadType(int limit, int addOffset, int maxId)
    {
        if (limit == 1)
        {
            //return LoadType.LimitIs1;
            return LoadType.Backward;
        }

        if (addOffset == -1)
        {
            return LoadType.Backward;
        }

        //if (input.AddOffset == 0 && input.OffsetId > 0)
        //{
        //    return LoadType.Forward;
        //}

        if (addOffset == 0)
        {
            return LoadType.Backward;
        }

        if (addOffset == -limit + 5)
        {
            return LoadType.AroundDate;
        }

        if (addOffset == -limit / 2)
        {
            return LoadType.AroundMessage;
        }

        // WebZ:LoadMoreDirection.Around: 
        // addOffset = -(Math.round(MESSAGE_LIST_SLICE / 2) + 1);
        if (addOffset == -(limit / 2 + 1))
        {
            return LoadType.AroundMessage;
        }

        // WebZ:LoadMoreDirection.Forwards:
        //  addOffset = -(MESSAGE_LIST_SLICE + 1);
        if (addOffset == -(limit + 1))
        {
            return LoadType.Backward;
        }

        if (addOffset == -limit + 6 && maxId != 0)
        {
            return LoadType.AroundDate;
        }

        return LoadType.Forward;
    }
}