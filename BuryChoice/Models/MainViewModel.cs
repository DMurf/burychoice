using BuryChoice.Helpers;
using Newtonsoft.Json;

namespace BuryChoice.Models
{
    public class MainViewModel
    {
        public List<SchoolModel> Schools { get; set; }
        public PerformanceTablesModel BuryPerformance { get; set; } = new PerformanceTablesModel();
        public PerformanceTablesModel EnglandPerformance { get; set; } = new PerformanceTablesModel();


        public List<OfferDetailsSummary> OfferDetails { get; set; } = new List<OfferDetailsSummary>();
        public List<SchoolLocation> SchoolLocations => Schools?.Select(s => new SchoolLocation
        {
            title = s.EstablishmentName,
            lat = MappingHelper.ConvertToLatLon(s.Easting.ToString(), s.Northing.ToString())?.Latitude ?? 0,
            lng = MappingHelper.ConvertToLatLon(s.Easting.ToString(), s.Northing.ToString())?.Longitude ?? 0,
            furthestOffer = OfferDetails.Where(x => x.URN == s.URN)?.Select(x => x.Distance26).First() ?? 0
        }).ToList();

        public string LocationDataJson => JsonConvert.SerializeObject(SchoolLocations);

        public MainViewModel(List<SchoolModel> schools)
        {
            Schools = schools;
        }
    }

    public class SchoolLocation
    {
        public string title { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public double? furthestOffer { get; set; }
    }
}
