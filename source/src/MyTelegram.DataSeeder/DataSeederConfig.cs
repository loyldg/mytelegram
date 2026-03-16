namespace MyTelegram.DataSeeder;

public record DataSeederConfig
{
    public bool IsUserCreated { get; set; }
    public bool IsReactionCreated { get; set; }
    public bool IsEffectCreated { get; set; }
    public bool IsWallPaperCreated { get; set; }
    public bool IsThemeCreated { get; set; }
    public List<long> CreatedStickerSetIds { get; set; } = [];
    public List<long> CreatedDocumentIds { get; set; } = [];
    public List<long> CreatedBotUserIds { get; set; } = [];
    public bool IsDialogFilterMigrated { get; set; }

    public bool IsUserNameIdMigrated { get; set; }
    public bool IsChatAdminMigrated { get; set; }
    public bool IsServiceNotificationAccountBioUpdated { get; set; }
}