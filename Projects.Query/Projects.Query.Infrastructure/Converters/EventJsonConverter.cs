using CQRS.Core.Events;
using System.Text.Json.Serialization;
using System.Text.Json;
using Projects.Common.Events;

namespace Projects.Query.Infrastructure.Converters
{
    public class EventJsonConverter : JsonConverter<BaseEvent>
    {
        public override bool CanConvert(Type type)
        {
            return type.IsAssignableFrom(typeof(BaseEvent));
        }

        public override BaseEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!JsonDocument.TryParseValue(ref reader, out var doc))
            {
                throw new JsonException($"Failed to parse {nameof(JsonDocument)}");
            }

            if (!doc.RootElement.TryGetProperty("Type", out var type))
            {
                throw new JsonException("Could not detect the Type discriminator property!");
            }

            var typeDiscriminator = type.GetString();
            var json = doc.RootElement.GetRawText();

            return typeDiscriminator switch
            {
                nameof(ProjectCreatedEvent) => JsonSerializer.Deserialize<ProjectCreatedEvent>(json, options),
                nameof(ProjectEditedEvent) => JsonSerializer.Deserialize<ProjectEditedEvent>(json, options),
                nameof(ProjectDeletedEvent) => JsonSerializer.Deserialize<ProjectDeletedEvent>(json, options),
                nameof(ProjectPermanentlyDeletedEvent) => JsonSerializer.Deserialize<ProjectPermanentlyDeletedEvent>(json, options),
                _ => throw new JsonException($"{typeDiscriminator} is not supported yet!")
            };
        }

        public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}