namespace MyTelegram.Domain.ValueObjects;

public class PeerSettings : ValueObject
{
    public bool AddContact { get; set; }
    public bool BlockContact { get; set; }
    public bool NeedContactsException { get; set; }

    public bool ReportGeo { get; set; }
    //public long UserId { get; set; }

    public bool ReportSpam { get; set; }
    public bool ShareContact { get; set; }
}
