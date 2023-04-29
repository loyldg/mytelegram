namespace MyTelegram.SmsSender;

public class TwilioSmsOptions
{
    public bool Enabled { get; set; }
    public string AccountSId { get; set; } = null!;

    public string AuthToken { get; set; } = null!;
    public string FromNumber { get; set; } = null!;
}
