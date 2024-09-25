using Newtonsoft.Json;
using System;
using System.Globalization;

public class DateTimeConverter : JsonConverter
{
    private const string DateFormat = "dd/MM/yyyy";

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var date = (DateTime)value;
        writer.WriteValue(date.ToString(DateFormat, CultureInfo.InvariantCulture));
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var dateString = (string)reader.Value;

        if (DateTime.TryParseExact(dateString, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
        {
            return date;
        }

        throw new JsonSerializationException($"Unable to parse '{dateString}' to DateTime with format {DateFormat}.");
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(DateTime);
    }
}
