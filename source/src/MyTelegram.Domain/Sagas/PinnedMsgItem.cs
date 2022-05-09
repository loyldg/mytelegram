namespace MyTelegram.Domain.Sagas;

public record PinnedMsgItem(long OwnerPeerId,
    int MessageId,
    long ToPeerId);
