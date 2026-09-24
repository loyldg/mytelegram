using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MyTelegram.AuthServer.Extensions;

public static partial class LoggerExtensions
{
    [LoggerMessage(LogLevel.Information, "[Step1] ReqPqHandler, ConnectionId: {ConnectionId}, ReqMsgId: {ReqMsgId}", EventName = "ReqPqHandlerStep1")]
    public static partial void HandshakeStep1(this ILogger logger, string connectionId, long reqMsgId);

    [LoggerMessage(
        LogLevel.Information,
        "[Step1] ReqPqMultiHandler, ConnectionId={ConnectionId}, ReqMsgId: {ReqMsgId}, AuthKeyId: {AuthKeyId} {ElapsedMs}ms",
        EventName = "ReqPqMultiHandlerStep1")]
    public static partial void HandshakeReqMultiStep1(
        this ILogger logger,
        string connectionId,
        long reqMsgId,
        long authKeyId,
        double elapsedMs);


    [LoggerMessage(LogLevel.Information, "[Step2] ReqDhParamsHandler, ConnectionId: {ConnectionId}, ReqMsgId: {ReqMsgId}", EventName = "ReqDhParamsHandlerStep2")]
    public static partial void HandshakeStep2(this ILogger logger, string connectionId, long reqMsgId);

    [LoggerMessage(
        LogLevel.Information,
        "[Step3] [{IsPerm}] authKey created successfully, ConnectionId: {ConnectionId}, AuthKeyId: {AuthKeyId:x2}, ReqMsgId: {ReqMsgId}, MediaOnly: {MediaOnly}",
        EventName = "AuthKeyCreatedStep3")]
    public static partial void HandshakeStep3(
        this ILogger logger,
        string isPerm,
        string connectionId,
        long authKeyId,
        long reqMsgId,
        bool mediaOnly);

    [LoggerMessage(LogLevel.Warning, "The default private key is being used. The default key is publicly available in the MyTelegram open-source project. For security reasons, configure your own private key and replace the client's public key.", EventName = "DefaultPrivateKeyDetected")]
    public static partial void DefaultPrivateKeyDetected(this ILogger logger);

    [LoggerMessage(LogLevel.Warning, "PQInnerData SHA-1 hash mismatch", EventName = "PQInnerDataSha1HashMismatch")] public static partial void PqInnerDataSha1HashMismatch(this ILogger logger);

    [LoggerMessage(LogLevel.Warning, "Answer SHA-1 hash mismatch", EventName = "AnswerSha1HashMismatch")] public static partial void AnswerSha1HashMismatch(this ILogger logger);
}
