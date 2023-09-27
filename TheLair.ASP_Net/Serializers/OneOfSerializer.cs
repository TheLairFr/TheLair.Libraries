using System.Text.Json;
using System.Text.Json.Serialization;

namespace TheLair.ASP_Net.Serializers;

public class OneOfSerializer : JsonConverter<OneOf>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return (typeToConvert.IsAssignableTo(typeof(OneOf)));
    }

    public override OneOf? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, OneOf value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.GetValue(), options);
    }
}