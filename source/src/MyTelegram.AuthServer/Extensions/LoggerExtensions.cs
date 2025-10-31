using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MyTelegram.AuthServer.Extensions;

public static partial class LoggerExtensions
{
    [LoggerMessage(1001, LogLevel.Information, "[Step1] ReqPqHandler, ConnectionId: {ConnectionId}, ReqMsgId: {ReqMsgId}", EventName = "ReqPqHandlerStep1")]
    public static partial void HandshakeStep1(this ILogger logger, string connectionId, long reqMsgId);

    [LoggerMessage(
        1002,
        LogLevel.Information,
        "[Step1] ReqPqMultiHandler, ConnectionId={ConnectionId}, ReqMsgId: {ReqMsgId}, AuthKeyId: {AuthKeyId} {ElapsedMs}ms",
        EventName = "ReqPqMultiHandlerStep1")]
    public static partial void HandshakeReqMultiStep1(
        this ILogger logger,
        string connectionId,
        long reqMsgId,
        long authKeyId,
        double elapsedMs);


    [LoggerMessage(1003, LogLevel.Information, "[Step2] ReqDhParamsHandler, ConnectionId: {ConnectionId}, ReqMsgId: {ReqMsgId}", EventName = "ReqDhParamsHandlerStep2")]
    public static partial void HandshakeStep2(this ILogger logger, string connectionId, long reqMsgId);

    [LoggerMessage(
        1004,
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


}
