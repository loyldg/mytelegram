using MongoDB.Bson.Serialization.Attributes;

namespace MyTelegram.ReadModel.MongoDB;

public class AppCodeReadModel : Impl.AppCodeReadModel, IMongoDbReadModel
{
}

[BsonIgnoreExtraElements]
public class ChannelReadModel : Impl.ChannelReadModel, IMongoDbReadModel
{
}

public class ChannelFullReadModel : Impl.ChannelFullReadModel, IMongoDbReadModel
{
}

public class ChannelMemberReadModel : Impl.ChannelMemberReadModel, IMongoDbReadModel
{
}

public class ChatInviteReadModel : Impl.ChatInviteReadModel, IMongoDbReadModel
{
}

public class ChatReadModel : Impl.ChatReadModel, IMongoDbReadModel
{
}

public class DeviceReadModel : Impl.DeviceReadModel, IMongoDbReadModel
{
}

public class DialogReadModel : Impl.DialogReadModel, IMongoDbReadModel
{
}

public class DraftReadModel : Impl.DraftReadModel, IMongoDbReadModel
{
}

//public class FileReadModel : Impl.FileReadModel, IMongoDbReadModel { }
public class MessageReadModel : Impl.MessageReadModel, IMongoDbReadModel
{
}

public class ReadingHistoryReadModel : Impl.ReadingHistoryReadModel, IMongoDbReadModel
{
}

public class PeerNotifySettingsReadModel : Impl.PeerNotifySettingsReadModel, IMongoDbReadModel
{
}


public class PtsReadModel : Impl.PtsReadModel, IMongoDbReadModel
{
}

public class PtsForAuthKeyIdReadModel : Impl.PtsForAuthKeyIdReadModel, IMongoDbReadModel
{
}

public class PushUpdatesReadModel : Impl.PushUpdatesReadModel, IMongoDbReadModel
{
}

public class PushDeviceReadModel : Impl.PushDeviceReadModel, IMongoDbReadModel
{
}

public class RpcResultReadModel : Impl.RpcResultReadModel, IMongoDbReadModel
{
}

public class UserNameReadModel : Impl.UserNameReadModel, IMongoDbReadModel
{
}


public class UserReadModel : Impl.UserReadModel, IMongoDbReadModel
{
}
