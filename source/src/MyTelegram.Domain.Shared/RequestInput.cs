// ReSharper disable once CheckNamespace

namespace MyTelegram;

public record RequestInput(uint ObjectId,
    long ReqMsgId,
    long UserId,
    long AuthKeyId,
    long PermAuthKeyId) : IRequestInput;
