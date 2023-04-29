namespace MyTelegram.MessengerServer.Services;

public record SessionRequestInput(uint ObjectId,
    long ReqMsgId,
    long UserId,
    long AuthKeyId,
    long PermAuthKeyId,
    string ClientIp,
    string ConnectionId,
    long RequestSessionId,
    int SeqNumber,
    bool IsAuthKeyActive,
    byte[] AuthKeyData,
    byte[] ServerSalt) : RequestInput(ObjectId,
    ReqMsgId,
    UserId,
    AuthKeyId,
    PermAuthKeyId);
