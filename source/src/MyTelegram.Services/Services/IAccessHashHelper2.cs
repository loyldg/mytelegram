namespace MyTelegram.Services.Services;

public interface IAccessHashHelper2
{
    ValueTask<bool> IsAccessHashValidAsync(long currentUserId, long accessHashKeyId, long targetId, long accessHash, AccessHashType? accessHashType = null);
    ValueTask<bool> IsAccessHashValidAsync(IRequestWithAccessHashKeyId request, long targetId, long accessHash, AccessHashType? accessHashType = null);

    Task CheckAccessHashAsync(long currentUserId, long accessHashKeyId, long targetId, long accessHash, AccessHashType? accessHashType = null);
    Task CheckAccessHashAsync(IRequestWithAccessHashKeyId request, long targetId, long accessHash, AccessHashType? accessHashType = null);
    Task CheckAccessHashAsync(long currentUserId, long accessHashKeyId, IInputPeer? inputPeer);
    Task CheckAccessHashAsync(IRequestWithAccessHashKeyId request, IInputPeer? inputPeer);
    Task CheckAccessHashAsync(long currentUserId, long accessHashKeyId, IInputUser inputUser);
    Task CheckAccessHashAsync(IRequestWithAccessHashKeyId request, IInputUser inputUser);
    Task CheckAccessHashAsync(long currentUserId, long accessHashKeyId, IInputChannel inputChannel);
    Task CheckAccessHashAsync(IRequestWithAccessHashKeyId request, IInputChannel inputChannel);

    /// <summary>
    /// Generates an access hash for a user based on session
    /// </summary>
    /// <param name="fromUserId"></param>
    /// <param name="accessHashKeyId">Permanent auth key based access hash key id</param>
    /// <param name="targetId"></param>
    /// <param name="accessHashType"></param>
    /// <returns></returns>
    long GenerateAccessHash(long fromUserId, long accessHashKeyId, long targetId, AccessHashType accessHashType);

    RpcError CreateRpcError(AccessHashType? accessHashType);
}