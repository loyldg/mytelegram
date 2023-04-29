namespace MyTelegram.GatewayServer.Configurations;

public class MyTelegramGatewayServerOption
{
    public int ThisDcId { get; set; }
    public List<GatewayServerItem> Servers { get; set; } = new();
}
