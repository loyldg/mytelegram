namespace MyTelegram.Domain.Aggregates.Document;

[EnableAutoGeneration]
public class DocumentAggregate(DocumentId id) : AggregateRoot<DocumentAggregate, DocumentId>(id);