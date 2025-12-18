namespace MyTelegram.GatewayServer.Configurations;

public class GatewayServerItem
{
    public int Id { get; set; }
    public ServerType ServerType { get; set; }
    public bool Enabled { get; set; }
    public bool Ssl { get; set; }
    public string? Ip { get; set; }
    public int Port { get; set; }
    public bool Ipv6 { get; set; }
    public string CertPemFilePath { get; set; } = string.Empty;
    public string KeyPemFilePath { get; set; } = string.Empty;
    public bool EnableProxyProtocolV2 { get; set; }
    public bool MediaOnly { get; set; }
}