using Microsoft.Azure.Cosmos;
using System.Text.Json;

namespace LunchAndLearn2024API;

public class CosmosNetSerializer : CosmosSerializer
{
    private readonly JsonSerializerOptions? _serializerOptions;

    public CosmosNetSerializer() => _serializerOptions = null;

    public CosmosNetSerializer(JsonSerializerOptions serializerOptions) =>
       this._serializerOptions = serializerOptions;

    public override T FromStream<T>(Stream stream)
    {
        using (stream)
        {
            if (typeof(Stream).IsAssignableFrom(typeof(T)))
            {
                return (T)(object)stream;
            }

#pragma warning disable CS8603 // Possible null reference return.
            return JsonSerializer.DeserializeAsync<T>(stream, _serializerOptions).GetAwaiter().GetResult();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }

    public override Stream ToStream<T>(T input)
    {
        var outputStream = new MemoryStream();


        // A BREAKPOINT HERE SHOWS THAT 'input' has all the fields, including Id

        JsonSerializer.SerializeAsync<T>(outputStream, input, _serializerOptions).GetAwaiter().GetResult();

        // A BREAKPOINT HERE AND A LOOK INSIDE 'outputStream' shows that ALL the fields have been serialized EXCEPT 'Id' or 'id.

        outputStream.Position = 0;
        return outputStream;
    }
}
