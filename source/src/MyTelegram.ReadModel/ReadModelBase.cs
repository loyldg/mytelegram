using MyTelegram.EventFlow.ReadStores;

namespace MyTelegram.ReadModel;

public abstract class ReadModelBase : IMyReadModel
{
    public bool IsDeleted { get; set; }
    public long? CreatedBy { get; set; }
    public long CreatedAt { get; set; }
    public long? LastModificationTime { get; set; }
    public long? LastModifiedBy { get; set; }
    public long? DeletionTime { get; set; }
    public long? DeletedBy { get; set; }
}