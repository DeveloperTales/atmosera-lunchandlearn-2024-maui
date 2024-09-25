using System.Text.Json.Serialization;

namespace LunchAndLearn2024Models;
public record Quote
{
    [JsonPropertyName("dataGroup")]
    public string DataGroup => "Quote";
    [JsonPropertyName("id")]
    public string? Id { get; init; }
    [JsonPropertyName("title")]
    public string? Title { get; init; }
    [JsonPropertyName("description")]
    public string? Description { get; init; }
    [JsonPropertyName("author")]
    public string? Author { get; init; }
}
