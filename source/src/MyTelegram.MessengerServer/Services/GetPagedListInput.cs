namespace MyTelegram.MessengerServer.Services;

public class GetPagedListInput
{
    public int AddOffset { get; set; }
    public int OffsetId { get; set; }
    public int Limit { get; set; }

    /// <summary>
    ///     Can be used to only return results with ID strictly smaller than max_id (e.g. message ID)
    /// </summary>
    public int MaxId { get; set; }

    /// <summary>
    ///     Can be used to only return results with ID strictly greater than min_id(e.g. message ID)
    /// </summary>
    public int MinId { get; set; }

    /// <summary>
    ///     Can be used to only return results that are older than max_date
    /// </summary>
    public int MaxDate { get; set; }

    /// <summary>
    ///     Can be used to only return results with are newer than min_date
    /// </summary>
    public int MinDate { get; set; }
}
