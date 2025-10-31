using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTelegram.Services.Extensions;

public static partial class LoggerExtensions
{
    [LoggerMessage(
        3000,
        LogLevel.Information,
        "User {UserId} {ReqMsgId} {Handler} {DeviceType}[{Layer}] {ElapsedMs}ms",
        EventName = "UserRequestHandled")]
    public static partial void UserRequestHandled(
        this ILogger logger,
        long userId,
        long reqMsgId,
        string handler,
        DeviceType deviceType,
        int layer,
        double elapsedMs);

    [LoggerMessage(
        3001,
        LogLevel.Debug,
        "User {UserId} {Handler} {DeviceType}[{Layer}] {ElapsedMs}ms. [{@Input}] Request data: {@Request}, response: {@Response}",
        EventName = "UserRequestDebug")]
    public static partial void UserRequestDebug(
        this ILogger logger,
        long userId,
        string handler,
        DeviceType deviceType,
        int layer,
        double elapsedMs,
        IRequestInput input,
        IObject request,
        IObject response);

}
