namespace MyTelegram;

/// <summary>
///     The request info
/// </summary>
/// <param name="ReqMsgId">Request message id</param>
/// <param name="UserId">Request userId</param>
/// <param name="AuthKeyId">Request temp/perm auth key id</param>
/// <param name="PermAuthKeyId">Request permanent auth key id</param>
/// <param name="RequestId">Request id</param>
/// <param name="Layer">Request layer</param>
/// <param name="Date">Request date</param>
/// <param name="DeviceType">Client device type</param>
/// <param name="AddRequestIdToCache">Add the request id to cache(used by InvokeAfterMsgHandler)</param>
public record RequestInfo(
    long ReqMsgId,
    long UserId,
    long AccessHashKeyId,
    long AuthKeyId,
    long PermAuthKeyId,
    Guid RequestId,
    int Layer,
    long Date,
    DeviceType DeviceType = DeviceType.Desktop,
    bool AddRequestIdToCache = true,
    bool IsSubRequest = false
    ) : IRequestWithAccessHashKeyId
{
    public static RequestInfo Empty { get; } = new(0, 0, 0, 0, 0, Guid.Empty, 0, 0);

    /// <summary>Request layer</summary>
    public int Layer { get; set; } = Layer;
}