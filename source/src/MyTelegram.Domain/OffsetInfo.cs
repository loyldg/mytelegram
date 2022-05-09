namespace MyTelegram.Domain;

public class OffsetInfo
{
    public int AddOffset { get; set; }

    public int FromId { get; set; }

    public LoadType LoadType { get; set; }

    //public int OffsetId { get; set; }
    //public int SkipCountFrom0 { get; set; }
    public int MaxId { get; set; }

    //public int OffsetId { get; set; }
    public int StartOffsetId { get; set; }

    public override string ToString()
    {
        return
            $"MaxId={MaxId},FromId={FromId},LoadType={LoadType},StartOffsetId={StartOffsetId},AddOffset={AddOffset}";
    }
}
