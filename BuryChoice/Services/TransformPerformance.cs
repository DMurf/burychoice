using BuryChoice.Models;
using EES.V1.Models;
using EES.V1.Models.EESResponse;
using EES.V1.Repositories;

namespace BuryChoice.Services
{
    public class TransformPerformance
    {
        public List<PerformanceTablesModel> Map(EESResponse ees)
        {
            var eesRep = new EESRepository();
            var returnList = new List<PerformanceTablesModel>();

            var schoolIds = ees.results.Select(x => x.locations.SCH).Distinct().ToList();
            foreach(var schoolId in schoolIds)
            {
                var thisItem = new PerformanceTablesModel();

                var urn = eesRep.GetUrnFromLookup(Guid.Parse("19e39901-a96c-be76-b9c2-6af54ae076d2"), schoolId);
                

                thisItem.SchoolId = urn;

                var schoolResults = ees.results.Where(x => x.locations.SCH == schoolId).ToList();
                foreach(var result in schoolResults)
                {
                    if (result.timePeriod.period == "2024/2025")
                    {
                        thisItem.Attainment8Score_25 = CanParseWithResult(result, "kgVhs");
                        thisItem.Progress8Score_25 = CanParseWithResult(result, "Pwoeb"); 
                        thisItem.Grade4EnglishMaths_25 = CanParseWithResult(result, "hCRyW"); 
                        thisItem.Grade5EnglishMaths_25 = CanParseWithResult(result, "dDo0Z");
                    }
                    else if (result.timePeriod.period == "2023/2024")
                    {
                        thisItem.Attainment8Score_24 = CanParseWithResult(result, "kgVhs");  
                        thisItem.Progress8Score_24 = CanParseWithResult(result, "Pwoeb"); 
                        thisItem.Grade4EnglishMaths_24 = CanParseWithResult(result, "hCRyW"); 
                        thisItem.Grade5EnglishMaths_24 = CanParseWithResult(result, "dDo0Z");
                    }
                    else if (result.timePeriod.period == "2022/2023")
                    {
                        thisItem.Attainment8Score_23 = CanParseWithResult(result, "kgVhs"); 
                        thisItem.Progress8Score_23 = CanParseWithResult(result, "Pwoeb"); 
                        thisItem.Grade4EnglishMaths_23 = CanParseWithResult(result, "hCRyW");
                        thisItem.Grade5EnglishMaths_23 = CanParseWithResult(result, "dDo0Z");
                    }
                }
                returnList.Add(thisItem);

            }
            return returnList;
            
        }

        public List<PerformanceTablesModel> Map_Geo_LA(EESResponse ees)
        {
            var eesRep = new EESRepository();
            var returnList = new List<PerformanceTablesModel>();

            var schoolIds = ees.results.Select(x => x.geographicLevel == "LA").Distinct().ToList();
            foreach (var schoolId in schoolIds)
            {
                var thisItem = new PerformanceTablesModel();

                thisItem.SchoolId = "";

                var schoolResults = ees.results.Where(x => x.locations.LA == "p5PSo").ToList();
                foreach (var result in schoolResults)
                {
                    if (result.timePeriod.period == "2024/2025")
                    {
                        thisItem.Attainment8Score_25 = CanParseWithResult(result, "S9YVx");
                        thisItem.Progress8Score_25 = CanParseWithResult(result, "OvpCL");
                        thisItem.Grade4EnglishMaths_25 = CanParseWithResult(result, "HPhzL");
                        thisItem.Grade5EnglishMaths_25 = CanParseWithResult(result, "kxGhs");
                    }
                    else if (result.timePeriod.period == "2023/2024")
                    {
                        thisItem.Attainment8Score_24 = CanParseWithResult(result, "S9YVx");
                        thisItem.Progress8Score_24 = CanParseWithResult(result, "OvpCL");
                        thisItem.Grade4EnglishMaths_24 = CanParseWithResult(result, "HPhzL");
                        thisItem.Grade5EnglishMaths_24 = CanParseWithResult(result, "kxGhs");
                    }
                    else if (result.timePeriod.period == "2022/2023")
                    {
                        thisItem.Attainment8Score_23 = CanParseWithResult(result, "S9YVx");
                        thisItem.Progress8Score_23 = CanParseWithResult(result, "OvpCL");
                        thisItem.Grade4EnglishMaths_23 = CanParseWithResult(result, "HPhzL");
                        thisItem.Grade5EnglishMaths_23 = CanParseWithResult(result, "kxGhs");
                    }
                }
                returnList.Add(thisItem);

            }
            return returnList;

        }

        public List<PerformanceTablesModel> Map_Geo_NAT(EESResponse ees)
        {
            var eesRep = new EESRepository();
            var returnList = new List<PerformanceTablesModel>();

            var schoolIds = ees.results.Select(x => x.geographicLevel == "NAT").Distinct().ToList();
            foreach (var schoolId in schoolIds)
            {
                var thisItem = new PerformanceTablesModel();

                thisItem.SchoolId = "";

                var schoolResults = ees.results.Where(x => x.geographicLevel == "NAT").Where(x => x.locations.NAT == "dP0Zw").ToList();
                foreach (var result in schoolResults)
                {
                    if (result.timePeriod.period == "2024/2025")
                    {
                        thisItem.Attainment8Score_25 = CanParseWithResult(result, "S9YVx");
                        thisItem.Progress8Score_25 = CanParseWithResult(result, "OvpCL");
                        thisItem.Grade4EnglishMaths_25 = CanParseWithResult(result, "HPhzL");
                        thisItem.Grade5EnglishMaths_25 = CanParseWithResult(result, "kxGhs");
                    }
                    else if (result.timePeriod.period == "2023/2024")
                    {
                        thisItem.Attainment8Score_24 = CanParseWithResult(result, "S9YVx");
                        thisItem.Progress8Score_24 = CanParseWithResult(result, "OvpCL");
                        thisItem.Grade4EnglishMaths_24 = CanParseWithResult(result, "HPhzL");
                        thisItem.Grade5EnglishMaths_24 = CanParseWithResult(result, "kxGhs");
                    }
                    else if (result.timePeriod.period == "2022/2023")
                    {
                        thisItem.Attainment8Score_23 = CanParseWithResult(result, "S9YVx");
                        thisItem.Progress8Score_23 = CanParseWithResult(result, "OvpCL");
                        thisItem.Grade4EnglishMaths_23 = CanParseWithResult(result, "HPhzL");
                        thisItem.Grade5EnglishMaths_23 = CanParseWithResult(result, "kxGhs");
                    }
                }
                returnList.Add(thisItem);

            }
            return returnList;

        }

        private double? CanParseWithResult(Result results, string key)
        {

            if (double.TryParse(results.Values.Where(x => x.Key == key).FirstOrDefault().Value, out double result)){
                return result;
            }
            return null;
        }
    }
}
