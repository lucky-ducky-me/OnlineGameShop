using Microsoft.SqlServer.Server;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineGameShopApi
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string FormatWrite;

        private readonly string FormatRead;

        public CustomDateTimeConverter(string format)
        {
            FormatWrite = format;
            FormatRead = format;
        }

        public CustomDateTimeConverter(string formatWrite, string formatRead)
        {
            FormatWrite = formatWrite;
            FormatRead = formatRead;
        }
        public override void Write(Utf8JsonWriter writer, DateTime date, JsonSerializerOptions options)
        {
            writer.WriteStringValue(date.ToString(FormatWrite));
        }
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), FormatRead, null);
        }
    }
}
