using MongoDB.Bson.Serialization.Attributes;

namespace MyTelegram.ReadModel.MongoDB;

[BsonIgnoreExtraElements]
public class AppCodeReadModel : Impl.AppCodeReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]
public class ChannelFullReadModel : Impl.ChannelFullReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]

public class ChannelMemberReadModel : Impl.ChannelMemberReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]
public class ChannelReadModel : Impl.ChannelReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]
public class ChatInviteReadModel : Impl.ChatInviteReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]
public class ChatReadModel : Impl.ChatReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]
public class DeviceReadModel : Impl.DeviceReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]
public class DialogReadModel : Impl.DialogReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]
public class DraftReadModel : Impl.DraftReadModel, IMongoDbReadModel
{
}

//public class FileReadModel : Impl.FileReadModel, IMongoDbReadModel { }

[BsonIgnoreExtraElements]
public class MessageReadModel : Impl.MessageReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]
public class PeerNotifySettingsReadModel : Impl.PeerNotifySettingsReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]
public class PtsForAuthKeyIdReadModel : Impl.PtsForAuthKeyIdReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]
public class PtsReadModel : Impl.PtsReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]
public class PushDeviceReadModel : Impl.PushDeviceReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]
public class PushUpdatesReadModel : Impl.PushUpdatesReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]
public class ReadingHistoryReadModel : Impl.ReadingHistoryReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]
public class ReplyReadModel : Impl.ReplyReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]
public class RpcResultReadModel : Impl.RpcResultReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]
public class UserNameReadModel : Impl.UserNameReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]
public class UserReadModel : Impl.UserReadModel, IMongoDbReadModel
{
}
