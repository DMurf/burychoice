using BuryChoice.Helpers;

namespace BuryChoice.Models
{
    public class OfferDetails
    {
        public int URN { get; set; }
        public string Name { get; set; }
        public int IntakeYear { get; set; }
        public string PAN { get; set; }
        public int Total { get; set; }
        public string Distance { get; set; }
        public double? DistanceMiles => this.Distance != null ? double.Parse(this.Distance) : (double?)null;
    }

    public class OfferDetailsSummary
    {
        public int URN { get; set; }
        public string Name { get; set; }
        public string PAN26 { get; set; }
        public int Total26 { get; set; }
        public double? Distance26 { get; set; }

        public string PAN25 { get; set; }
        public int Total25 { get; set; }
        public double? Distance25 { get; set; }

        public string PAN24 { get; set; }
        public int Total24 { get; set; }
        public double? Distance24 { get; set; }

        public static List<OfferDetailsSummary> Map(List<OfferDetails> offerDetails, Dictionary<int?, string> schoolDict)
        {
            var offerDetailsSummary = new List<OfferDetailsSummary>();
            foreach(var urn in schoolDict.Keys)
            {
                var details = offerDetails.Where(o => o.URN == urn).ToList();
                if (details.Count > 0)
                {
                    var summary = new OfferDetailsSummary();
                    summary.URN = urn.Value;
                    summary.Name = schoolDict[urn.Value];
                    foreach (var detail in details)
                    {
                        switch (detail.IntakeYear)
                        {
                            case 2026:
                                summary.PAN26 = detail.PAN;
                                summary.Total26 = detail.Total;
                                summary.Distance26 = detail.DistanceMiles != null ? detail.DistanceMiles.Value : (double?)null;
                                break;
                            case 2025:
                                summary.PAN25 = detail.PAN;
                                summary.Total25 = detail.Total;
                                summary.Distance25 = detail.DistanceMiles != null ? detail.DistanceMiles.Value : (double?)null;
                                break;
                            case 2024:
                                summary.PAN24 = detail.PAN;
                                summary.Total24 = detail.Total;
                                summary.Distance24 = detail.DistanceMiles != null ? detail.DistanceMiles.Value : (double?)null;
                                break;
                        }
                    }
                    offerDetailsSummary.Add(summary);
                }
            }
            return offerDetailsSummary;
        }
    }
}
