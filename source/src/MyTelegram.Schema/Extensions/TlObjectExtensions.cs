using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MyTelegram.Schema.Extensions;


public static class TlObjectExtensions
{
    public static TRpcResult ToRpcResult(this IObject result, long reqMsgId)
    {
        return new TRpcResult
        {
            ReqMsgId = reqMsgId,
            Result = result
        };
    }

    public static int SetBit(this int value, int bitIndex)
    {
        if (bitIndex >= 32)
        {
            throw new ArgumentOutOfRangeException(nameof(bitIndex));
        }

        return value | 1 << bitIndex;
    }

    public static bool IsBitSet(this int value, int bitIndex)
    {
        return ((value >> bitIndex) & 1) == 1;
    }

    public static ILayeredDocument ToNewDocument(this ILayeredDocument document)
    {
        switch (document)
        {
            case TDocument d:
                return new TDocument
                {
                    AccessHash = d.AccessHash,
                    Attributes = d.Attributes,
                    Date = d.Date,
                    DcId = d.DcId,
                    FileReference = d.FileReference,
                    Id = d.Id,
                    Flags = d.Flags,
                    MimeType = d.MimeType,
                    Size = d.Size,
                    Thumbs = d.Thumbs,
                    VideoThumbs = d.VideoThumbs,
                };
        }

        return document;
    }

    public static long? ToPeerId(this IPeer? peer)
    {
        long? ownerId = null;
        switch (peer)
        {
            case null:
                break;
            case TPeerChannel peerChannel:
                ownerId = peerChannel.ChannelId;
                break;
            case TPeerChat peerChat:
                ownerId = peerChat.ChatId;
                break;
            case TPeerUser peerUser:
                ownerId = peerUser.UserId;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(peer));
        }

        return ownerId;
    }

    public static IInputPeer ToInputPeer(this IInputUser inputUser)
    {
        return inputUser switch
        {
            TInputUser inputUser1 => new TInputPeerUser
            {
                UserId = inputUser1.UserId,
                AccessHash = inputUser1.AccessHash
            },
            _ => throw new ArgumentOutOfRangeException(nameof(inputUser))
        };
    }

    public static IInputPeer ToInputPeer(this IInputChannel inputChannel)
    {
        if (inputChannel is TInputChannel tInputChannel)
        {
            return new TInputPeerChannel
            {
                ChannelId = tInputChannel.ChannelId,
                AccessHash = tInputChannel.AccessHash
            };
        }

        throw new NotSupportedException($"Not supported channel: {inputChannel.GetType().Name}");
    }

    public static long GetReactionId(this IReaction reaction)
    {
        switch (reaction)
        {
            case TReactionCustomEmoji reactionCustomEmoji:
                return reactionCustomEmoji.DocumentId;
            case TReactionEmoji reactionEmoji:
                var bytes = Encoding.UTF8.GetBytes(reactionEmoji.Emoticon).AsSpan();
                if (bytes.Length >= 8)
                {
                    return BinaryPrimitives.ReadInt64LittleEndian(bytes);
                }
                Span<byte> newBytes = stackalloc byte[8];
                bytes.CopyTo(newBytes);

                return BinaryPrimitives.ReadInt64LittleEndian(newBytes);

            case TReactionEmpty:
                return 0;
            //return reactionEmpty.ConstructorId;

            case TReactionPaid reactionPaid:
                return reactionPaid.ConstructorId;

            default:
                throw new ArgumentOutOfRangeException(nameof(reaction));
        }
    }

    public static long GetFileId(this IInputFile? file)
    {
        switch (file)
        {
            case null:
                return 0;
            case TInputFile inputFile:
                return inputFile.Id;
            case TInputFileBig inputFileBig:
                return inputFileBig.Id;
            case TInputFileStoryDocument inputFileStoryDocument:
                switch (inputFileStoryDocument.Id)
                {
                    case TInputDocument inputDocument:
                        return inputDocument.Id;
                    case TInputDocumentEmpty:
                        return 0;
                }
                break;
        }

        return 0;
    }


    [return: NotNullIfNotNull(nameof(inputReplyTo))]
    public static int? ToReplyToMsgId(this IInputReplyTo? inputReplyTo)
    {
        switch (inputReplyTo)
        {
            case TInputReplyToMessage inputReplyToMessage:
                if (inputReplyToMessage.TopMsgId != null)
                {
                    return inputReplyToMessage.TopMsgId;
                }
                return inputReplyToMessage.ReplyToMsgId;
            case TInputReplyToStory inputReplyToStory:
                return inputReplyToStory.StoryId;
            default:
                return null;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    // ReSharper disable once InconsistentNaming
    public static int GetTLLength(this string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return 1 + 3; // len=0 + padding
        }

        int byteCount = Encoding.UTF8.GetByteCount(value);

        int header = byteCount < 254 ? 1 : 4;
        int total = header + byteCount;

        int padding = (4 - (total % 4)) & 3;

        return total + padding;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    // ReSharper disable once InconsistentNaming
    public static int GetTLLength(this byte[]? value)
    {
        if (value == null)
        {
            return 1 + 3; // len=0 + padding
        }

        var byteCount = value.Length;
        int header = byteCount < 254 ? 1 : 4;
        int total = header + byteCount;

        int padding = (4 - (total % 4)) & 3;

        return total + padding;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    // ReSharper disable once InconsistentNaming
    public static int GetTLLength(this ReadOnlyMemory<byte>? value)
    {
        if (value == null)
        {
            return 1 + 3; // len=0 + padding
        }

        var byteCount = value.Value.Length;
        int header = byteCount < 254 ? 1 : 4;
        int total = header + byteCount;

        int padding = (4 - (total % 4)) & 3;

        return total + padding;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    // ReSharper disable once InconsistentNaming
    public static int GetTLLength(this ReadOnlyMemory<byte> value)
    {
        if (value.Length == 0)
        {
            return 1 + 3; // len=0 + padding
        }

        var byteCount = value.Length;
        int header = byteCount < 254 ? 1 : 4;
        int total = header + byteCount;

        int padding = (4 - (total % 4)) & 3;

        return total + padding;
    }

    //public static int GetTlStringLength(this string data)
    //{
    //    var byteCount = Encoding.UTF8.GetByteCount(data);
    //    var length = byteCount;
    //    int padding;
    //    if (byteCount < 254)
    //    {
    //        padding = (byteCount + 1) % 4;
    //        length += 1;
    //    }
    //    else
    //    {
    //        padding = byteCount % 4;
    //        length += 4;
    //    }
    //    length += padding;

    //    return length;
    //}

    //public static int GetLength(this IObject? obj)
    //{
    //    if (obj == null)
    //    {
    //        return 0;
    //    }

    //    using var writer = new ArrayPoolBufferWriter<byte>();
    //    obj.Serialize(writer);

    //    return writer.WrittenCount;
    //}


    [return: NotNullIfNotNull(nameof(obj))]
    public static byte[]? ToBytes(this IObject? obj)
    {
        if (obj == null)
        {
            return null;
        }
        using var writer = new ArrayPoolBufferWriter<byte>();

        obj.Serialize(writer);
        var bytes = writer.WrittenSpan.ToArray();

        return bytes;
    }

    [return: NotNullIfNotNull(nameof(readOnlyMemory))]
    public static TObject? ToTObject<TObject>(this ReadOnlyMemory<byte>? readOnlyMemory) where TObject : IObject
    {
        if (readOnlyMemory?.Length > 0)
        {
            //var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(readOnlyMemory.Value));
            //return reader.Read<TObject>();
            return readOnlyMemory.Value.ToTObject<TObject>();
        }

        return default;
    }

    public static TObject ToTObject<TObject>(this ReadOnlyMemory<byte> readOnlyMemory) where TObject : IObject
    {
        if (readOnlyMemory.Length > 0)
        {
            return readOnlyMemory.Read<TObject>();
        }

        return default;
    }


    [return: NotNullIfNotNull(nameof(bytes))]
    public static TObject? ToTObject<TObject>(this byte[]? bytes) where TObject : IObject
    {
        return ToTObject<TObject>(readOnlyMemory: bytes);
    }

    public static TObject ToTObject<TObject>(this Memory<byte> memory) where TObject : IObject
    {
        return ToTObject<TObject>(readOnlyMemory: memory);
    }
}