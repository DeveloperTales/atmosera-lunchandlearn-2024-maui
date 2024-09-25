using System.Text.Json.Serialization;

namespace LunchAndLearn2024Models;
public class Book
{
    [JsonPropertyName("dataGroup")]
    public string DataGroup => "Book";
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("status")]
    public CurrentStatus Status { get; set; }
    [JsonPropertyName("imageUrl")]
    public string? ImageUrl { get; set; }
    [JsonPropertyName("author")]
    public string? Author { get; set; }
    [JsonPropertyName("notes")]
    public string? Notes { get; set; }
}
