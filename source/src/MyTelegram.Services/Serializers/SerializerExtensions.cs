namespace MyTelegram.Services.Serializers;

public static class SerializerExtensions
{
    public static PushData? ReadPushData(this ref ReadOnlyMemory<byte> buffer)
    {
        var isNull = buffer.ReadByte() == 0;
        if (isNull)
        {
            return null;
        }
        /*
         * string LocKey, string[] LocArgs, long UserId, PushNotificationCustomData? Custom, string? Sound
         */
        var locKey = buffer.ReadString();
        var locArgsCount = buffer.ReadInt32();
        var locArgs = new string[locArgsCount];
        for (int i = 0; i < locArgsCount; i++)
        {
            locArgs[i] = buffer.ReadString();
        }
        var userId = buffer.ReadInt64();
        PushNotificationCustomData? custom = null;
        if (buffer.ReadByte() == 1)
        {
            var attachb64 = buffer.ReadString2();
            var updates = buffer.ReadString2();
            var callId = buffer.ReadNullableInt64();

            custom = new PushNotificationCustomData
            {
                Attachb64 = attachb64,
                Updates = updates,
                CallId = callId,
                CallAh = buffer.ReadNullableInt64(),
                EncryptionId = buffer.ReadNullableInt64(),
                RandomId = buffer.ReadNullableInt64(),
                ContactId = buffer.ReadNullableInt64(),
                MsgId = buffer.ReadNullableInt32(),
                ChannelId = buffer.ReadNullableInt64(),
                ChatId = buffer.ReadNullableInt64(),
                FromId = buffer.ReadNullableInt64(),
                ChatFromBroadcastId = buffer.ReadNullableInt64(),
                ChatFromGroupId = buffer.ReadNullableInt64(),
                ChatFromId = buffer.ReadNullableInt64(),
                Mention = buffer.ReadNullableBool(),
                Silent = buffer.ReadNullableBool(),
                Schedule = buffer.ReadNullableBool(),
                EditDate = buffer.ReadNullableInt32(),
                TopMsgId = buffer.ReadNullableInt32(),
                MaxId = buffer.ReadNullableInt32(),
                Dc = buffer.ReadNullableInt32(),
                Addr = buffer.ReadString2()
            };
        }
        var sound = buffer.ReadString2();
        return new PushData(locKey, locArgs, userId, custom, sound);
    }

    public static void Write(this IBufferWriter<byte> writer, PushData? value)
    {
        if (value == null)
        {
            writer.WriteByte(0);
        }
        else
        {
            /*
             * string LocKey, string[] LocArgs, long UserId, PushNotificationCustomData? Custom, string? Sound
             */
            writer.WriteByte(1);
            writer.Write(value.LocKey);
            writer.Write(value.LocArgs.Length);
            foreach (var valueLocArg in value.LocArgs)
            {
                writer.Write(valueLocArg);
            }
            writer.Write(value.UserId);
            if (value.Custom == null)
            {
                writer.WriteByte(0);
            }
            else
            {
                writer.WriteByte(1);
                writer.WriteString(value.Custom.Attachb64);
                writer.WriteString(value.Custom.Updates);
                writer.Write(value.Custom.CallId);
                writer.Write(value.Custom.CallAh);
                writer.Write(value.Custom.EncryptionId);
                writer.Write(value.Custom.RandomId);
                writer.Write(value.Custom.ContactId);
                writer.Write(value.Custom.MsgId);
                writer.Write(value.Custom.ChannelId);
                writer.Write(value.Custom.ChatId);
                writer.Write(value.Custom.FromId);
                writer.Write(value.Custom.ChatFromBroadcastId);
                writer.Write(value.Custom.ChatFromGroupId);
                writer.Write(value.Custom.ChatFromId);
                writer.WriteBool(value.Custom.Mention);
                writer.WriteBool(value.Custom.Silent);
                writer.WriteBool(value.Custom.Schedule);
                writer.Write(value.Custom.EditDate);
                writer.Write(value.Custom.TopMsgId);
                writer.Write(value.Custom.MaxId);
                writer.Write(value.Custom.Dc);
                writer.WriteString(value.Custom.Addr);
            }

            writer.WriteString(value.Sound);
        }
    }

    public static void Write(this IBufferWriter<byte> writer, IReadOnlyDictionary<string, string>? value)
    {
        writer.Write(value?.Count ?? 0);
        if (value != null)
        {
            foreach (var kv in value)
            {
                writer.Write(kv.Key);
                writer.Write(kv.Value);
            }
        }
    }

    public static Dictionary<string, string> ReadDictionary(this ref ReadOnlyMemory<byte> buffer)
    {
        var dict = new Dictionary<string, string>();
        var count = buffer.ReadInt32();
        for (int i = 0; i < count; i++)
        {
            var key = buffer.ReadString();
            var value = buffer.ReadString();
            dict.TryAdd(key, value);
        }

        return dict;
    }

    public static void WriteString(this IBufferWriter<byte> writer, string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            writer.WriteByte(0);
        }
        else
        {
            writer.WriteByte(1);
            writer.Write(value);
        }
    }

    public static string ReadString2(this ref ReadOnlyMemory<byte> buffer)
    {
        var isNull = buffer.ReadByte() == 0;
        if (isNull)
        {
            return null;
        }

        return buffer.ReadString();
    }

    public static void Write(this IBufferWriter<byte> writer, Peer value)
    {
        writer.Write((byte)value.PeerType);
        writer.Write(value.PeerId);
    }

    public static Peer ReadPeer(this ref ReadOnlyMemory<byte> buffer)
    {
        var peerType = (PeerType)buffer.ReadByte();
        var peerId = buffer.ReadInt64();

        return new Peer(peerType, peerId);
    }

    public static void Write(this IBufferWriter<byte> writer, IReadOnlyCollection<long>? value)
    {
        writer.Write(value?.Count ?? 0);
        foreach (var v in value ?? [])
        {
            writer.Write(v);
        }
    }

    public static List<long> ReadList(this ref ReadOnlyMemory<byte> buffer)
    {
        var count = buffer.ReadInt32();
        var list = new List<long>(count);
        for (int i = 0; i < count; i++)
        {
            var value = buffer.ReadInt64();
            list.Add(value);
        }
        return list;
    }

    public static RequestInfo ReadRequestInfo(this ref ReadOnlyMemory<byte> buffer)
    {
        var connectionId = buffer.ReadString();
        var sessionId = buffer.ReadInt64();
        var reqMsgId = buffer.ReadInt64();
        var userId = buffer.ReadInt64();
        var accessHashKeyId = buffer.ReadInt64();
        var authKeyId = buffer.ReadInt64();
        var permAuthKeyId = buffer.ReadInt64();
        var requestId = buffer.ReadGuid();
        var layer = buffer.ReadInt32();
        var date = buffer.ReadInt64();
        var deviceType = (DeviceType)buffer.ReadByte();
        var addRequestIdToCache = buffer.ReadBoolean();
        var isSubRequest = buffer.ReadBoolean();

        return new RequestInfo(connectionId, sessionId, reqMsgId, userId, accessHashKeyId, authKeyId, permAuthKeyId, requestId, layer, date,
            deviceType, addRequestIdToCache, isSubRequest);
    }

    public static void Write(this IBufferWriter<byte> writer, RequestInfo value)
    {
        writer.Write(value.ConnectionId);
        writer.Write(value.SessionId);
        writer.Write(value.ReqMsgId);
        writer.Write(value.UserId);
        writer.Write(value.AccessHashKeyId);
        writer.Write(value.AuthKeyId);
        writer.Write(value.PermAuthKeyId);
        writer.Write(value.RequestId);
        writer.Write(value.Layer);
        writer.Write(value.Date);
        writer.Write((byte)value.DeviceType);
        writer.WriteBool(value.AddRequestIdToCache);
        writer.WriteBool(value.IsSubRequest);
    }
}