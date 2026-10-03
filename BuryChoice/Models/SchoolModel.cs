using Newtonsoft.Json;

namespace BuryChoice.Models
{
    public class SchoolModel
    {
        public int? URN { get; set; }

        [JsonProperty("LA (code)")]
        public int? LAcode { get; set; }

        [JsonProperty("LA (name)")]
        public string LAname { get; set; }
        public int? EstablishmentNumber { get; set; }
        public string EstablishmentName { get; set; }

        [JsonProperty("ReligiousCharacter (name)")]
        public string ReligiousCharactername { get; set; }
        public int? SchoolCapacity { get; set; }
        public int? NumberOfPupils { get; set; }
        public int? NumberOfBoys { get; set; }
        public int? NumberOfGirls { get; set; }
        public double? PercentageFSM { get; set; }

        [JsonProperty("Trusts (name)")]
        public string Trustsname { get; set; }
        public string Street { get; set; }
        public string Locality { get; set; }
        public string Address3 { get; set; }
        public string Town { get; set; }

        [JsonProperty("County (name)")]
        public string Countyname { get; set; }
        public string Postcode { get; set; }
        public string SchoolWebsite { get; set; }
        public int? TelephoneNum { get; set; }

        [JsonProperty("HeadTitle (name)")]
        public string HeadTitlename { get; set; }
        public string HeadFirstName { get; set; }
        public string HeadLastName { get; set; }
        public int? Easting { get; set; }
        public int? Northing { get; set; }
        public int? FSM { get; set; }

        public PerformanceTablesModel PerformanceTables { get; set; } = new PerformanceTablesModel();

    }
}
