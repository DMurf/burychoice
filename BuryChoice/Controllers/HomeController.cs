using BuryChoice.Models;
using BuryChoice.Services;
using EES.V1.Models;
using EES.V1.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;

namespace BuryChoice.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJsonFileReader _jsonReader;

        public HomeController(IJsonFileReader jsonFileReader)
        {
            _jsonReader = jsonFileReader;
        }
        public async Task<IActionResult> Index(string urns)
        {
            urns = "105354,146529,148097";

            var schoolsToInclude = urns.Split(",").ToList();

            var schools = new List<SchoolModel>();

            schools = await _jsonReader.ReadJsonFileAsync<List<SchoolModel>>("data/schools.json", fromWebRoot: true) ?? new List<SchoolModel>();

            if (!string.IsNullOrWhiteSpace(urns))
            {
                schools = schools.Where(x => schoolsToInclude.Contains(x.URN.ToString())).ToList();
            }
            

            var offerDetails = await _jsonReader.ReadJsonFileAsync<List<OfferDetails>>("data/offerdetails.json", fromWebRoot: true) ?? new List<OfferDetails>();

            var model = new MainViewModel(schools);
            model.OfferDetails = OfferDetailsSummary.Map(offerDetails, schoolDict: schools.ToDictionary(s => s.URN, s => s.EstablishmentName));

            var dicto = await _jsonReader.ReadJsonFileAsync<DatasetDownloadDto>("data/19e39901-a96c-be76-b9c2-6af54ae076d2.json", fromWebRoot: true) ?? new DatasetDownloadDto();
            var locationOpts = await _jsonReader.ReadJsonFileAsync<List<LocationOption>>("data/19e39901-a96c-be76-b9c2-6af54ae076d2-urns.json", fromWebRoot: true) ?? new List<LocationOption>();

            var ees = new EESRepository();
            var transformer = new TransformPerformance();
            var result = await ees.GetKS4PerformanceData_Estab();
            var laNat = await ees.GetKS4PerformanceData_LANAT();
            var transformedResults = transformer.Map(result, locationOpts);
            foreach(var school in model.Schools)
            {
                school.PerformanceTables = transformedResults.Where(x => x.SchoolId == school.URN.ToString()).FirstOrDefault();
            }

            model.BuryPerformance = transformer.Map_Geo_LA(laNat).First();
            model.EnglandPerformance = transformer.Map_Geo_NAT(laNat).First();


            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("api")]
        public async Task<IActionResult> API()
        {
            var ees = new EESRepository();
            var result = await ees.GetKS4PerformanceData_Estab();
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(result, Formatting.Indented));
        }

        [Route("updoot")]
        public async Task<IActionResult> Updoot()
        {
            var ees = new EESRepository();
            ees.GetMetadataDownload(Guid.Parse("19e39901-a96c-be76-b9c2-6af54ae076d2"));
            return RedirectToAction("Index");   
        }
    }
}
