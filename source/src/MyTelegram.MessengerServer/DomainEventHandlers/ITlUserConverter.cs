namespace MyTelegram.MessengerServer.DomainEventHandlers;

public interface ITlUserConverter
{
    IUser ToUser(IUserReadModel user, long selfUserId);

    Task<IUserFull> ToUserFullAsync(IUserReadModel user,
        long selfUserId,
        IPeerNotifySettingsReadModel? peerNotifySettingsReadModel
    );

    IList<IUser> ToUserList(IReadOnlyCollection<IUserReadModel> userList,
        long selfUserId);
}