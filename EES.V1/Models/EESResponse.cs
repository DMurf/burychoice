using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;

namespace EES.V1.Models.EESResponse
{
    public class EESResponse
    {
        public List<Warning> warnings { get; set; }
        public Paging paging { get; set; }
        public List<Result> results { get; set; }

    }
    public class Detail
    {
        public List<string> items { get; set; }
    }

    public class Filters
    {
        public string pPmSo { get; set; }
        public string ibG6X { get; set; }
        public string ETvqF { get; set; }
        public string LZ6Wj { get; set; }
        public string IzpBz { get; set; }
    }

    public class Locations
    {
        public string LA { get; set; }
        public string NAT { get; set; }
        public string SCH { get; set; }
    }

    public class Paging
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int totalResults { get; set; }
        public int totalPages { get; set; }
    }

    public class Result
    {
        public TimePeriod timePeriod { get; set; }
        public string geographicLevel { get; set; }
        public Locations locations { get; set; }
        //public Filters filters { get; set; }
        [JsonPropertyName("filters")]
        public Dictionary<string, string>? _filters { get; set; }

        public List<ReferencedFilter> Filters = new List<ReferencedFilter>();

        [JsonPropertyName("values")]
        public Dictionary<string, string>? _values { get; set; }
        public List<ReferencedValues> Values = new List<ReferencedValues>();

        public static Result Map(Result unMapped, DatasetDownloadDto lookup)
        {
            var returnResult = unMapped;
            returnResult.Filters = unMapped._filters?.Select(f => new ReferencedFilter(f.Key, f.Value, "filter", lookup)).ToList() ?? new List<ReferencedFilter>();
            returnResult.Values = unMapped._values?.Select(v => new ReferencedValues(v.Key, v.Value, "values", lookup)).ToList() ?? new List<ReferencedValues>();
            return returnResult;
        }
    }


    public class TimePeriod
    {
        public string code { get; set; }
        public string period { get; set; }
    }

    public class Values
    {
        [JsonProperty("kgVhs")]
        public string kgVhs { get; set; }
    }

    public class Warning
    {
        public string message { get; set; }
        public string path { get; set; }
        public string code { get; set; }
        public Detail detail { get; set; }
    }

    public class ReferencedFilter
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string KeyMatched { get; set; }
        public string ValueMatched { get; set; }
        public ReferencedFilter(string key, string value, string type, DatasetDownloadDto referenceFile)
        {
            Key = key;
            Value = value;

            try
            {
                KeyMatched = referenceFile.Filters.Where(x => x.Id == key).FirstOrDefault()?.Label;
                ValueMatched = referenceFile.Filters.Where(x => x.Id == key).First()?.Options.Where(x => x.Id == value).FirstOrDefault()?.Label;
            }
            catch (Exception e)
            {

            }
        }
    }

    public class ReferencedValues
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string KeyMatched { get; set; }
        public ReferencedValues(string key, string value, string type, DatasetDownloadDto referenceFile)
        {
            Key = key;
            Value = value;
            try
            {
                var matchedAttempt = referenceFile.Indicators.Where(x => x.Id == key).FirstOrDefault();

                KeyMatched = matchedAttempt != null ? matchedAttempt.Label : string.Empty;
            }
            catch ( Exception e)
            {

            }
        }
    }
}
