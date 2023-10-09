// ReSharper disable once CheckNamespace
namespace MyTelegram;

/// <summary>
/// The request info
/// </summary>
/// <param name="ReqMsgId">Request message id</param>
/// <param name="UserId">Request userId</param>
/// <param name="AuthKeyId">Request temp/perm auth key id</param>
/// <param name="PermAuthKeyId">Request permanent auth key id</param>
/// <param name="RequestId">Request id</param>
/// <param name="Layer">Request layer</param>
/// <param name="AddRequestIdToCache">Add the request id to cache(used by InvokeAfterMsgHandler)</param>
public record RequestInfo(long ReqMsgId, long UserId, long AuthKeyId, long PermAuthKeyId, Guid RequestId, int Layer, long Date, bool AddRequestIdToCache = true);