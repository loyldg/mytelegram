using IChatFull = MyTelegram.Schema.IChatFull;

namespace MyTelegram.Messenger.Converters.ConverterServices;

public interface IChatConverterService
{
    Task<IChat> GetChannelAsync(IRequestWithAccessHashKeyId request,
        long channelId,
        bool checkChannelMember,
        bool? channelMemberIsLeft,
        int layer = 0,
        bool throwIfNotFound = true
    );

    Task<List<IChat>> GetChannelListAsync(IRequestWithAccessHashKeyId request,
        List<long> channelIds,
        IReadOnlyCollection<IChannelMemberReadModel>? channelMemberReadModels = null,
        int layer = 0);

    Task<IChatFull> GetChannelFullAsync(IRequestWithAccessHashKeyId request,
        long channelId,
        IPeerNotifySettingsReadModel? peerNotifySettingsReadModel = null,
        IChatInviteReadModel? chatInviteReadModel = null,
        int layer = 0);

    Schema.Channels.IChannelParticipant ToChannelParticipant(
        IRequestWithAccessHashKeyId request,
        IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        IChannelMemberReadModel channelMemberReadModel,
        //IChatPhoto chatPhoto,
        IUser user,
        int layer = 0
    );

    IChat ToChannel(IRequestWithAccessHashKeyId request, IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        IChannelMemberReadModel? channelMemberReadModel,
        bool? channelMemberIsLeft,
        int layer);

    List<IChat> ToChannelList(IRequestWithAccessHashKeyId request, IReadOnlyCollection<IChannelReadModel> channelReadModels,
        IReadOnlyCollection<IPhotoReadModel> photoReadModels,
        IReadOnlyCollection<IChannelMemberReadModel>? channelMemberReadModels,
        IReadOnlyCollection<long>? joinedChannelIds = null,
        int layer = 0);

    IChannelParticipants ToChannelParticipants(
        IRequestWithAccessHashKeyId request,
        IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        //IReadOnlyCollection<IChatAdminReadModel>? chatAdminReadModels,
        int participantCount,
        IReadOnlyCollection<IChannelMemberReadModel> channelMemberReadModels,
        IEnumerable<IUser> users,
        DeviceType deviceType,
        bool forceNotLeft,
        int layer
    );

    Schema.Messages.IChatFull ToChannelFull(
        IRequestWithAccessHashKeyId request,
        IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        IChannelFullReadModel channelFullReadModel,
        IChannelMemberReadModel? channelMemberReadModel,
        IPeerNotifySettingsReadModel peerNotifySettingsReadModel,
        IChatInviteReadModel? chatInviteReadModel = null,
        int layer = 0
    );
}