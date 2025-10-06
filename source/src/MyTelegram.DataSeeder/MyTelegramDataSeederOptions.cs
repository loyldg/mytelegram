namespace MyTelegram.DataSeeder;

public class MyTelegramDataSeederOptions
{
    public bool UploadNewDocumentFiles { get; set; }
    public MyTelegramBotOptions MyTelegramBotOptions { get; set; } = null!;
    public bool CreateTestUsers { get; set; }
}