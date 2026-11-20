namespace MyTelegram.Messenger.Services.Impl;

public class OffsetHelper : IOffsetHelper, ITransientDependency
{
    public const int DefaultLimit = 20;
    public const int MaxLimit = 100;

    public OffsetInfo GetOffsetInfo(GetPagedListInput input)
    {
        return GetOffsetInfo(input.AddOffset, input.OffsetId, input.Limit, input.MinId, input.MaxId, input.MinDate,
            input.MaxDate, input.OffsetDate);
    }

    public OffsetInfo GetOffsetInfo(int addOffset, int offsetId, int limit, int minId, int maxId, int minDate, int maxDate)
    {
        return GetOffsetInfo(addOffset, offsetId, limit, minId, maxId, minDate, maxDate, 0);
    }

    /// <summary>
    /// The result is the window [offsetFromID(offset_id) + add_offset, +limit) of the list sorted by descending id,
    /// where offsetFromID(offset_id) is the number of results with id &gt;= offset_id.
    /// The part of the window above the anchor is the newer slice (id &gt;= offset_id, ascending),
    /// the part below the anchor is the older slice (id &lt; offset_id, descending).
    /// </summary>
    public OffsetInfo GetOffsetInfo(int addOffset, int offsetId, int limit, int minId, int maxId, int minDate,
        int maxDate, int offsetDate)
    {
        limit = limit <= 0 ? DefaultLimit : Math.Min(limit, MaxLimit);
        // Clamp to avoid overflows with malicious values; a window this far away is empty anyway.
        addOffset = Math.Clamp(addOffset, -1_000_000, 1_000_000);

        var newerSkip = 0;
        var newerTake = 0;
        if (addOffset < 0)
        {
            newerSkip = Math.Max(0, -addOffset - limit);
            newerTake = Math.Min(-addOffset, limit);
        }

        var olderSkip = Math.Max(0, addOffset);
        var olderTake = Math.Max(0, limit + Math.Min(addOffset, 0));

        var hasAnchor = offsetId > 0 || offsetDate > 0;
        if (!hasAnchor)
        {
            // There is nothing newer than the top of the list.
            newerSkip = 0;
            newerTake = 0;
        }

        return new OffsetInfo
        {
            OffsetId = Math.Max(offsetId, 0),
            OffsetDate = Math.Max(offsetDate, 0),
            AddOffset = addOffset,
            Limit = limit,
            MinId = Math.Max(minId, 0),
            MaxId = Math.Max(maxId, 0),
            MinDate = Math.Max(minDate, 0),
            MaxDate = Math.Max(maxDate, 0),
            NewerSkip = newerSkip,
            NewerTake = newerTake,
            OlderSkip = olderSkip,
            OlderTake = olderTake,
            LoadType = GetLoadType(newerTake, olderTake, offsetId)
        };
    }

    private static LoadType GetLoadType(int newerTake, int olderTake, int offsetId)
    {
        if (newerTake == 0)
        {
            return LoadType.Backward;
        }

        if (olderTake == 0)
        {
            return LoadType.Forward;
        }

        return offsetId > 0 ? LoadType.AroundMessage : LoadType.AroundDate;
    }
}
