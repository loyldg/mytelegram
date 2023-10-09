namespace MyTelegram.Services.Services;

public record SessionRequestInput(
    string ConnectionId,
    Guid RequestId,
    uint ObjectId,
    long ReqMsgId,
    long UserId,
    long AuthKeyId,
    long PermAuthKeyId,
    string ClientIp,
    long RequestSessionId,
    int SeqNumber,
    bool IsAuthKeyActive,
    byte[] AuthKeyData,
    byte[] ServerSalt,
    int Layer,
    long Date
) : RequestInput(
    ConnectionId,
    RequestId,
    ObjectId,
    ReqMsgId,
    UserId,
    AuthKeyId,
    PermAuthKeyId,
    Layer,
    Date
);