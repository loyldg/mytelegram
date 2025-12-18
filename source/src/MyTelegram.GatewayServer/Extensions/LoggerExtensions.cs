using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MyTelegram.GatewayServer.Extensions;

public static partial class LoggerExtensions
{
    [LoggerMessage(
        2001,
        LogLevel.Warning,
        "Cannot find cached client information; skipping message sending. ConnectionId: {ConnectionId}",
        EventName = "CachedClientInfoNotFound")]
    public static partial void CachedClientInfoNotFound(this ILogger logger, string connectionId);

    [LoggerMessage(
        2002,
        LogLevel.Warning,
        "Cannot find cached client information; skipping message sending. ConnectionId: {ConnectionId}, AuthKeyId: {AuthKeyId}",
        EventName = "CachedClientInfoNotFound2")]
    public static partial void CachedClientInfoNotFound2(this ILogger logger, string connectionId, long authKeyId);

    [LoggerMessage(
        2003,
        LogLevel.Information,
        "[ConnectionId: {ConnectionId}] New client connected. DCId: {DcId}, LocalPort: {LocalPort} ({ConnectionType}), RemoteEndpoint: {RemoteEndPoint}, connection count: {ConnectionCount}.",
        EventName = "NewClientConnected")]
    public static partial void NewClientConnected(
        this ILogger logger,
        string connectionId,
        int? dcId,
        int? localPort,
        ConnectionType? connectionType,
        EndPoint? remoteEndPoint,
        int connectionCount);

    [LoggerMessage(
        2004,
        LogLevel.Information,
        "[ConnectionId: {ConnectionId}] New client connected using proxy protocol v2. DCId: {DcId}, LocalPort: {LocalPort} ({ConnectionType}), RemoteEndpoint: {RemoteEndPoint}(Proxy: {ProxyEndPoint}), connection count: {ConnectionCount}.",
        EventName = "NewClientConnectedWithProxyProtocol")]
    public static partial void NewClientConnectedWUsingProxyProtocolV2(
        this ILogger logger,
        string connectionId,
        int? dcId,
        int? localPort,
        ConnectionType? connectionType,
        EndPoint? remoteEndPoint,
        EndPoint? proxyEndPoint,
        int connectionCount);

    [LoggerMessage(
        2005,
        LogLevel.Information,
        "[ConnectionId: {ConnectionId}] Client disconnected, DcId: {DcId}, RemoteEndPoint: {RemoteEndPoint}, AuthKeyId: {AuthKeyId}",
        EventName = "ClientDisconnected")]
    public static partial void ClientDisconnected(
        this ILogger logger,
        string connectionId,
        int? dcId,
        EndPoint? remoteEndPoint,
        long authKeyId);

    [LoggerMessage(
        2006,
        LogLevel.Warning,
        "Parse proxy protocol failed, LocalEndPoint: {@LocalAddress}:{Port}, RemoteEndPoint: {@RemoteAddress}:{RemotePort}",
        EventName = "ParseProxyProtocolFailed")]
    public static partial void ParseProxyProtocolFailed(
        this ILogger logger,
        IPAddress? localAddress,
        int? port,
        IPAddress? remoteAddress,
        int? remotePort);


}
