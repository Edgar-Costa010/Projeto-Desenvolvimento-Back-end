using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

// Conversor personalizado para DateTime? que suporta múltiplos formatos de data na desserialização, para evitar conflito de modelos com diferentes formatos no banco de dados.

public class DateTimeConverter : JsonConverter<DateTime?>
{
    private const string Format = "dd/MM/yyyy";

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (string.IsNullOrWhiteSpace(value)) return null;

        // suporta os seguintes formatos
        var formats = new[] { "dd/MM/yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "dd-MM-yyyy", "o" };
        if (DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var date))
            return date;

        if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out date))
            return date;

        throw new JsonException($"Formato de data inválido. Use {formats[0]} or ISO-8601");
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.Value.ToString(Format));
        else
            writer.WriteNullValue();
    }
}