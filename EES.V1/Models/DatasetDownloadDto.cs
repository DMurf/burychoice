using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EES.V1.Models
{
    public class DatasetDownloadDto
    {
        [JsonPropertyName("filters")]
        public List<Filter>? Filters { get; set; }

        [JsonPropertyName("indicators")]
        public List<Indicator>? Indicators { get; set; }

        [JsonPropertyName("geographicLevels")]
        public List<LevelInfo>? Geography { get; set; }

        [JsonPropertyName("locations")]
        public List<GeographyLevel>? Locations { get; set; }
    }

    public class Filter
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("column")]
        public string? Column { get; set; }

        [JsonPropertyName("label")]
        public string? Label { get; set; }

        [JsonPropertyName("hint")]
        public string? Hint { get; set; }

        [JsonPropertyName("options")]
        public List<FilterOption>? Options { get; set; }
    }

    public class FilterOption
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("label")]
        public string? Label { get; set; }
    }

    public class LocationOption
    {
        [JsonPropertyName("urn")]
        public string? Urn { get; set; }

        [JsonPropertyName("laEstab")]
        public string? LaEstab { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("oldCode")]
        public string? OldCode { get; set; }
    }

    public class Indicator
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("column")]
        public string? Column { get; set; }

        [JsonPropertyName("label")]
        public string? Label { get; set; }

        [JsonPropertyName("unit")]
        public string? Unit { get; set; }

        [JsonPropertyName("decimalPlaces")]
        public int? DecimalPlaces { get; set; }
    }

    public class GeographyLevel
    {
        [JsonPropertyName("level")]
        public LevelInfo? Level { get; set; }

        [JsonPropertyName("options")]
        public List<LocationOption>? Options { get; set; }
    }

    public class LevelInfo
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("label")]
        public string? Label { get; set; }
    }
}
