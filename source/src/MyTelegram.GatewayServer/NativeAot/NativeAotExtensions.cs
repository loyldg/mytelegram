namespace MyTelegram.GatewayServer.NativeAot;

public static class NativeAotExtensions
{
    [DynamicDependency(DynamicallyAccessedMemberTypes.All,typeof(MyTelegramGatewayServerOption))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All,typeof(List<GatewayServerItem>))]
    public static void FixNativeAotIssues(this IServiceCollection services)
    {
        services.AddTransient<IRabbitMqSerializer, NativeAotUtf8JsonRabbitMqSerializer>();
        services.AddTransient<IJsonContextProvider, GatewayServerJsonContextProvider>();
    }
}