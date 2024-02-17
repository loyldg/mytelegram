using System.Text.Json;

namespace MyTelegram.Domain.EventFlow;

public interface ISystemTextJsonOptions
{
    void Apply(JsonSerializerOptions settings);
}