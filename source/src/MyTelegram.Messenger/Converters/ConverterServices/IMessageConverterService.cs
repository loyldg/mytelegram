namespace MyTelegram.Messenger.Converters.ConverterServices;

public interface IMessageConverterService
{
    IMessage ToMessage(long selfUserId, IMessageReadModel readModel,
        IPollReadModel? pollReadModel = null,
        List<string>? chosenOptions = null,
        List<IUserReactionReadModel>? userReactionReadModels = null);

    List<IMessage> ToMessageList(long selfUserId,
        IReadOnlyCollection<IMessageReadModel> messageReadModels,
        IReadOnlyCollection<IPollReadModel>? pollReadModels,
        IReadOnlyCollection<IPollAnswerVoterReadModel>? pollAnswerVoterReadModels,
        IReadOnlyCollection<IUserReactionReadModel>? userReactionReadModels
    );

    IMessage ToMessage(
        long selfUserId,
        MessageItem item,
        List<long>? userReactionIds = null,
        bool mentioned = false
    );

    string DecryptMessage(long ownerPeerId, int messageId, ReadOnlyMemory<byte>? encryptedData);
}