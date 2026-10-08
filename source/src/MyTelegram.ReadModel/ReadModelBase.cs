using MyTelegram.EventFlow.ReadStores;

namespace MyTelegram.ReadModel;

public abstract class ReadModelBase : IMyReadModel
{
    public bool IsDeleted { get; set; }
    public long? CreatedBy { get; set; }
    public long CreatedAt { get; set; }
    public long? LastUpdatedAt { get; set; }
    public long? LastUpdatedBy { get; set; }
    public long? DeletedAt { get; set; }
    public long? DeletedBy { get; set; }
}