//// Custom converter for DateOnly
//using System.Globalization;
//using System;
//using System.Text.Json;
//using System.Text.Json.Serialization;
//using System.ComponentModel;

//public class DateOnlyJsonConverter : JsonConverter<DateOnly>
//{
//    private readonly string _serializationFormat;

//    public DateOnlyJsonConverter() : this(null)
//    {
//    }

//    public DateOnlyJsonConverter(string serializationFormat)
//    {
//        _serializationFormat = serializationFormat ?? "yyyy-MM-dd";
//    }

//    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//    {
//        return DateOnly.ParseExact(reader.GetString(), _serializationFormat, CultureInfo.InvariantCulture);
//    }

//    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
//    {
//        writer.WriteStringValue(value.ToString(_serializationFormat));
//    }
//}