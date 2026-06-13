using MyTelegram.Services.Services;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO.Hashing;
using System.Text;

namespace MyTelegram.Messenger.Services.Impl;

public class Tokenizer(IOptionsMonitor<MyTelegramMessengerServerOptions> options) : ITokenizer, ITransientDependency
{
    private static byte[] _indexEncryptionKey = [];
    private static KeyConfig? _keyConfig = null;

    //public HashSet<long> BuildSearchTokens(
    //    ReadOnlySpan<byte> key,
    //    ReadOnlySpan<byte> utf8Message)
    //{
    //    HashSet<long> tokens = [];
    //    if (utf8Message.Length < 3)
    //    {
    //        return tokens;
    //    }

    //    //int count = 0;
    //    var seed = BinaryPrimitives.ReadInt64LittleEndian(key);
    //    for (int i = 0; i <= utf8Message.Length - 3; i++)
    //    {
    //        ReadOnlySpan<byte> gram = utf8Message.Slice(i, 3);

    //        var token = XxHash64.HashToUInt64(gram, seed);
    //        tokens.Add(token.ToInt64());
    //    }

    //    return tokens;
    //}

    public List<long>? BuildSearchTokens(string? message)
    {
        if (!options.CurrentValue.EncryptionConfig.Enabled || options.CurrentValue.EncryptionConfig.IndexKeys.Count == 0)
        {
            return null;
        }

        if (message?.Length < 3)
        {
            return null;
        }

        _keyConfig ??= options.CurrentValue.EncryptionConfig.IndexKeys[0];

        if (_indexEncryptionKey.Length == 0)
        {
            _indexEncryptionKey = Encoding.UTF8.GetBytes(_keyConfig.Key);
        }

        var maxLength = message.Length * 4;
        var tempBytes = ArrayPool<byte>.Shared.Rent(maxLength);
        HashSet<long>? tokens;
        try
        {
            var span = tempBytes.AsSpan();
            var count = Encoding.UTF8.GetBytes(message, span);
            tokens = BuildSearchTokens(_indexEncryptionKey, span.Slice(0, count));
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(tempBytes);
        }

        return tokens?.ToList();
    }

    public HashSet<long> BuildSearchTokens(ReadOnlySpan<byte> key, ReadOnlySpan<byte> utf8Message)
    {
        var tokens = new HashSet<long>();
        if (utf8Message.Length < 1)
        {
            return tokens;
        }

        var seed = BinaryPrimitives.ReadInt64LittleEndian(key);

        var start = 0;
        for (var i = 0; i < utf8Message.Length; i++)
        {
            var b = utf8Message[i];

            var isSeparator = b <= 0x20 || b >= 0x7F || !(
                b is >= (byte)'a' and <= (byte)'z' ||
                b is >= (byte)'A' and <= (byte)'Z' ||
                b is >= (byte)'0' and <= (byte)'9'
            );

            if (isSeparator)
            {
                var len = i - start;
                if (len > 0)
                {
                    var word = utf8Message.Slice(start, len);

                    var token = XxHash64.HashToUInt64(word, seed);
                    tokens.Add(token.ToInt64());
                }

                start = i + 1;
            }
        }

        if (start < utf8Message.Length)
        {
            var word = utf8Message.Slice(start);
            var token = XxHash64.HashToUInt64(word, seed);
            tokens.Add(token.ToInt64());
        }

        return tokens;
    }
}