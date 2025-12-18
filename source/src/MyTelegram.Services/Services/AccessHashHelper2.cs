using Microsoft.Extensions.Configuration;
using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text;

namespace MyTelegram.Services.Services;

public sealed class AccessHashHelper2(
    IConfiguration configuration,
    IPeerHelper peerHelper) : IAccessHashHelper2, ISingletonDependency
{
    private byte[]? _accessHashSecretKeyBytes;

    public ValueTask<bool> IsAccessHashValidAsync(IRequestWithAccessHashKeyId request, long targetId, long accessHash,
        AccessHashType? accessHashType = null)
    {
        return IsAccessHashValidAsync(request.UserId, request.AccessHashKeyId, targetId, accessHash, accessHashType);
    }

    public async Task CheckAccessHashAsync(long currentUserId, long accessHashKeyId, long targetId, long accessHash, AccessHashType? accessHashType = null)
    {
        if (!await IsAccessHashValidAsync(currentUserId, accessHashKeyId, targetId, accessHash, accessHashType))
        {
            CreateRpcError(accessHashType).ThrowRpcError();
        }
    }

    public RpcError CreateRpcError(AccessHashType? accessHashType)
    {
        switch (accessHashType)
        {
            case AccessHashType.WallPaper:
                return RpcErrors.RpcErrors400.WallpaperInvalid;
            case AccessHashType.Theme:
                return RpcErrors.RpcErrors400.ThemeInvalid;
            case AccessHashType.GroupCall:
                return RpcErrors.RpcErrors400.GroupcallInvalid;
            case AccessHashType.StickerSet:
                return RpcErrors.RpcErrors400.StickersetInvalid;
            case AccessHashType.User:
                return RpcErrors.RpcErrors400.UserIdInvalid;
            case AccessHashType.Channel:
                return RpcErrors.RpcErrors400.ChannelIdInvalid;
            case AccessHashType.Document:
                return RpcErrors.RpcErrors400.DocumentInvalid;
            case AccessHashType.Photo:
                return RpcErrors.RpcErrors400.PhotoInvalid;
            case AccessHashType.Sticker:
                return RpcErrors.RpcErrors400.StickersetInvalid;
            case AccessHashType.BotApp:
                return RpcErrors.RpcErrors400.BotAppInvalid;
            case AccessHashType.Call:
                return RpcErrors.RpcErrors400.CallPeerInvalid;
            default:
                return RpcErrors.RpcErrors400.PeerIdInvalid;
        }
    }

    public Task CheckAccessHashAsync(IRequestWithAccessHashKeyId request, long targetId, long accessHash,
        AccessHashType? accessHashType = null)
    {
        return CheckAccessHashAsync(request.UserId, request.AccessHashKeyId, targetId, accessHash, accessHashType);
    }

    public Task CheckAccessHashAsync(long currentUserId, long accessHashKeyId, IInputPeer? inputPeer)
    {
        switch (inputPeer)
        {
            case null:
                break;
            case TInputPeerChannel inputPeerChannel:
                return CheckAccessHashAsync(currentUserId, accessHashKeyId, inputPeerChannel.ChannelId, inputPeerChannel.AccessHash, AccessHashType.Channel);

            case TInputPeerChannelFromMessage inputPeerChannelFromMessage:

                break;
            case TInputPeerSelf:
                break;
            case TInputPeerUser inputPeerUser:
                return CheckAccessHashAsync(currentUserId, accessHashKeyId, inputPeerUser.UserId, inputPeerUser.AccessHash, AccessHashType.User);

            case TInputPeerUserFromMessage inputPeerUserFromMessage:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(inputPeer));
        }

        return Task.CompletedTask;
    }

    public Task CheckAccessHashAsync(IRequestWithAccessHashKeyId request, IInputPeer? inputPeer)
    {
        return CheckAccessHashAsync(request.UserId, request.AccessHashKeyId, inputPeer);
    }

    public Task CheckAccessHashAsync(long currentUserId, long accessHashKeyId, IInputUser inputUser)
    {
        if (inputUser is TInputUser tInputUser)
        {
            return CheckAccessHashAsync(currentUserId, accessHashKeyId, tInputUser.UserId, tInputUser.AccessHash, AccessHashType.User);
        }

        return Task.CompletedTask;
    }

    public Task CheckAccessHashAsync(IRequestWithAccessHashKeyId request, IInputUser inputUser)
    {
        return CheckAccessHashAsync(request.UserId, request.AccessHashKeyId, inputUser);
    }

    public Task CheckAccessHashAsync(long currentUserId, long accessHashKeyId, IInputChannel inputChannel)
    {
        if (inputChannel is TInputChannel tInputChannel)
        {
            return CheckAccessHashAsync(currentUserId, accessHashKeyId, tInputChannel.ChannelId, tInputChannel.AccessHash, AccessHashType.Channel);
        }

        return Task.CompletedTask;
    }

    public Task CheckAccessHashAsync(IRequestWithAccessHashKeyId request, IInputChannel inputChannel)
    {
        return CheckAccessHashAsync(request.UserId, request.AccessHashKeyId, inputChannel);
    }

    public ValueTask<bool> IsAccessHashValidAsync(long currentUserId, long accessHashKeyId, long targetId, long accessHash, AccessHashType? accessHashType = AccessHashType.Unknown)
    {
        if (accessHashType == null)
        {
            var peer = peerHelper.GetPeer(targetId);
            switch (peer.PeerType)
            {
                case PeerType.Channel:
                    accessHashType = AccessHashType.Channel;
                    break;
                case PeerType.User:
                    accessHashType = AccessHashType.User;
                    break;
                case PeerType.Self:
                    return ValueTask.FromResult(true);
            }
        }

        if (accessHashType == null)
        {
            return ValueTask.FromResult(false);
        }

        var accessHashForCurrentSession = GenerateAccessHash(currentUserId, accessHashKeyId, targetId, accessHashType.Value);

        // if (accessHashForCurrentSession != accessHash)
        // {
        //     Console.WriteLine($"currentUserId:{currentUserId} accessHashKeyId:{accessHashKeyId} targetId:{targetId} {accessHashType},accessHashForCurrentSession:{accessHashForCurrentSession},accessHash:{accessHash}");
        // }

        return ValueTask.FromResult(accessHashForCurrentSession == accessHash);
    }

    private void GenerateAccessHashSecretKeyForUser(ReadOnlySpan<byte> accessHasKeyBytesOfUser, Span<byte> destination)
    {
        if (_accessHashSecretKeyBytes == null)
        {
            var masterKey = configuration.GetValue<string>("App:AccessHashSecretKey");
            if (string.IsNullOrEmpty(masterKey))
            {
                throw new InvalidOperationException("App:AccessHashSecretKey is null");
            }

            _accessHashSecretKeyBytes = Encoding.UTF8.GetBytes(masterKey);
        }

        HMACSHA256.HashData(_accessHashSecretKeyBytes, accessHasKeyBytesOfUser, destination);
    }

    public long GenerateAccessHash(long currentUserId, long accessHashKeyId, long targetId, AccessHashType accessHashType)
    {
        // accessHashType:1 + currentUserId:8 + targetId:8 + hash:32 + keyForUser:32 + accessHashKeyForUser:8 = 89 bytes
        Span<byte> bytes = stackalloc byte[89];
        bytes[0] = (byte)accessHashType;
        BinaryPrimitives.WriteInt64LittleEndian(bytes.Slice(1, 8), currentUserId);
        BinaryPrimitives.WriteInt64LittleEndian(bytes[17..], targetId);
        var dest = bytes.Slice(17, 32);
        var accessHashSecretKey = bytes.Slice(49, 32);
        var accessHashKeyBytes = bytes[..^8];
        BinaryPrimitives.WriteInt64LittleEndian(accessHashKeyBytes, accessHashKeyId);
        //accessHashKey.TryWriteBytes(accessHashKeyBytes);
        GenerateAccessHashSecretKeyForUser(accessHashKeyBytes, accessHashSecretKey);
        HMACSHA256.HashData(accessHashSecretKey, bytes[..17], dest);

        return BitConverter.ToInt64(dest);
    }
}