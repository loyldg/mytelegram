namespace MyTelegram.MessengerServer.Services;

public abstract class BaseAppService
{
    protected static int CurrentDate => (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();

    protected virtual OffsetInfo GetOffset(GetPagedListInput input)
    {
        var loadType = GetOffsetLoadType(input);
        //var offsetId = 20;
        var maxId = 0;
        var fromId = 0;
        switch (loadType)
        {
            case LoadType.Backward:
                fromId = input.OffsetId - input.Limit;
                maxId = input.OffsetId;
                break;

            case LoadType.Forward:
                fromId = input.OffsetId;
                //maxId = input.OffsetId + input.Limit;
                break;

            case LoadType.FirstUnread:
                break;

            case LoadType.AroundMessage:
                fromId = input.OffsetId + input.AddOffset;
                //maxId = input.OffsetId + input.AddOffset + input.Limit;
                break;

            case LoadType.AroundDate:
                break;

            case LoadType.LimitIs1:

                break;

            default:
                throw new ArgumentOutOfRangeException($"Unsuporrted load type:{loadType}");
        }

        return new OffsetInfo { MaxId = maxId, FromId = fromId, LoadType = loadType };
    }
    private static LoadType GetOffsetLoadType(GetPagedListInput input)
    {
        if (input.Limit == 1)
        {
            //return LoadType.LimitIs1;
            return LoadType.Backward;
        }

        if (input.AddOffset == -1)
        {
            return LoadType.Backward;
        }

        //if (input.AddOffset == 0 && input.OffsetId > 0)
        //{
        //    return LoadType.Forward;
        //}

        if (input.AddOffset == 0)
        {
            return LoadType.Backward;
        }

        if (input.AddOffset == -input.Limit + 5)
        {
            return LoadType.AroundDate;
        }

        if (input.AddOffset == -input.Limit / 2)
        {
            return LoadType.AroundMessage;
        }

        if (input.AddOffset == -input.Limit + 6 && input.MaxId != 0)
        {
            return LoadType.AroundDate;
        }

        return LoadType.Forward;
    }
}