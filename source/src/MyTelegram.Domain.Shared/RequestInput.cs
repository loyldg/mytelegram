// ReSharper disable once CheckNamespace

namespace MyTelegram;

public record RequestInput(
    string ConnectionId,
    Guid RequestId,
    uint ObjectId,
    long ReqMsgId,
    long UserId,
    long AuthKeyId,
    long PermAuthKeyId,
    int Layer,
    long Date
) : IRequestInput;