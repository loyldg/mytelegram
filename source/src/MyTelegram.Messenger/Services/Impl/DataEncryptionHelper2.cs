using System.Buffers.Binary;

namespace MyTelegram.Messenger.Services.Impl;

public class DataEncryptionHelper2(IDataEncryptionHelper dataEncryptionHelper,
    ILogger<DataEncryptionHelper2> logger,
    IOptionsMonitor<MyTelegramMessengerServerOptions> options) : IDataEncryptionHelper2, ITransientDependency
{
    private static Dictionary<int, byte[]> _keys = [];
    private static byte[] _firstKey = [];
    private static int _firstKeyId = 1;
    public int Encrypt(long ownerPeerId, string plainText, Span<byte> encryptedData)
    {
        InitKeys();
        return dataEncryptionHelper.Encrypt(_firstKeyId, _firstKey, ownerPeerId, plainText, encryptedData);
    }

    public string DecryptMessage(long ownerPeerId, ReadOnlyMemory<byte>? encryptedData)
    {
        if (encryptedData == null || encryptedData.Value.IsEmpty)
        {
            return string.Empty;
        }

        var tempBytes = ArrayPool<byte>.Shared.Rent(encryptedData.Value.Length);

        try
        {
            var span = tempBytes.AsSpan(0, encryptedData.Value.Length);
            var count = DecryptMessage(ownerPeerId, encryptedData, span);

            return Encoding.UTF8.GetString(span[..count]);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(tempBytes);
        }
    }

    public int DecryptMessage(long ownerPeerId, ReadOnlyMemory<byte>? encryptedData, Span<byte> decryptedData)
    {
        if (encryptedData == null || encryptedData.Value.IsEmpty)
        {
            return 0;
        }

        InitKeys();
        var keyId = BinaryPrimitives.ReadInt32LittleEndian(encryptedData.Value.Span);
        if (!_keys.TryGetValue(keyId, out var masterKey))
        {
            logger.LogWarning("Message encryption master key not exists, keyId: {KeyId}", keyId);
            return 0;
        }
        var tempBytes = ArrayPool<byte>.Shared.Rent(encryptedData.Value.Length);
        try
        {
            return dataEncryptionHelper.Decrypt(masterKey, ownerPeerId, encryptedData.Value.Span.Slice(4), decryptedData);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(tempBytes);
        }
    }

    private void InitKeys()
    {
        if (_keys.Count == 0)
        {
            _keys = options.CurrentValue.EncryptionConfig.MessageKeys.ToDictionary(k => k.Id,
                v => Encoding.UTF8.GetBytes(v.Key));
            if (_keys.Count == 0)
            {
                throw new InvalidOperationException("Message encryption keys not exists.");
            }
            var firstItem = _keys.First();
            _firstKeyId = firstItem.Key;
            _firstKey = firstItem.Value;
        }
    }
}