namespace MyTelegram.GatewayServer.NativeAot;

public static class NativeAotExtensions
{
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MyTelegramGatewayServerOption))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(List<GatewayServerItem>))]

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MTProto.EncryptedMessage))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MTProto.EncryptedMessageResponse))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MTProto.UnencryptedMessage))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MTProto.UnencryptedMessageResponse))]

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MyTelegram.Core.EncryptedMessage))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MyTelegram.Core.EncryptedMessageResponse))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MyTelegram.Core.UnencryptedMessage))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MyTelegram.Core.UnencryptedMessageResponse))]

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(AuthKeyNotFoundEvent))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MyTelegram.Core.ClientDisconnectedEvent))]
    public static void FixNativeAotIssues(this IServiceCollection services)
    {

    }
}