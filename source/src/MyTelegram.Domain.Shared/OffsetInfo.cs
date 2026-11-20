namespace MyTelegram;

/// <summary>
/// Pagination window computed from offset_id/offset_date/add_offset/limit.
/// See https://core.telegram.org/api/offsets: the result is the window
/// [offsetFromID(offset_id) + add_offset, +limit) over the list sorted by descending id.
/// The window is split into two slices around the anchor:
/// the newer slice (id &gt;= offset_id, read in ascending order) and
/// the older slice (id &lt; offset_id, read in descending order).
/// </summary>
public class OffsetInfo
{
    public int OffsetId { get; set; }

    /// <summary>
    /// Used as the anchor only when <see cref="OffsetId"/> is 0. Messages with date &lt;= offset_date belong to the older slice.
    /// </summary>
    public int OffsetDate { get; set; }

    public int AddOffset { get; set; }
    public int Limit { get; set; }

    /// <summary>
    /// Only return results with ID strictly greater than min_id (0 = no bound)
    /// </summary>
    public int MinId { get; set; }

    /// <summary>
    /// Only return results with ID strictly smaller than max_id (0 = no bound)
    /// </summary>
    public int MaxId { get; set; }

    /// <summary>
    /// Only return results with date strictly greater than min_date (0 = no bound)
    /// </summary>
    public int MinDate { get; set; }

    /// <summary>
    /// Only return results with date strictly smaller than max_date (0 = no bound)
    /// </summary>
    public int MaxDate { get; set; }

    public int NewerSkip { get; set; }
    public int NewerTake { get; set; }
    public int OlderSkip { get; set; }
    public int OlderTake { get; set; }

    public LoadType LoadType { get; set; }

    public bool HasIdAnchor => OffsetId > 0;
    public bool HasDateAnchor => OffsetId <= 0 && OffsetDate > 0;

    /// <summary>
    /// True when the window starts at the top of the list (the newest result is included).
    /// </summary>
    public bool StartsFromTop => !HasIdAnchor && !HasDateAnchor && AddOffset <= 0;

    public override string ToString()
    {
        return
            $"OffsetId={OffsetId},OffsetDate={OffsetDate},AddOffset={AddOffset},Limit={Limit},MinId={MinId},MaxId={MaxId},Newer={NewerSkip}+{NewerTake},Older={OlderSkip}+{OlderTake},LoadType={LoadType}";
    }
}
