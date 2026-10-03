using EES.V1.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EES.V1.Repositories
{
    public class EESRepository
    {
        private RestClient _client;
        public EESRepository()
        {
            _client = new RestClient("https://api.education.gov.uk/statistics/v1"); // Replace with actual base URL
        }
        public async void GetMetadataDownload(Guid dataSetId)
        {
            var request = new RestRequest($"/data-sets/{dataSetId}/meta", Method.Get);
            request.AddHeader("Accept", "application/json");
            
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var content = response.Content;
                File.WriteAllText($"C:\\Development\\BuryChoice\\EES.V1\\DatasetDownload\\{dataSetId}.json", JsonConvert.SerializeObject(JsonConvert.DeserializeObject(content), Formatting.Indented));


                var getFiltersFromJson = JsonConvert.DeserializeObject<DatasetDownloadDto>(content);

                var a = getFiltersFromJson.Locations.Where(x => x.Level.Code == "SCH").First().Options;

                File.WriteAllText($"C:\\Development\\BuryChoice\\EES.V1\\DatasetDownload\\{dataSetId}-urns.json", JsonConvert.SerializeObject(a, Formatting.Indented));

            }
            else
            {
                throw new Exception($"Error fetching school data: {response.StatusCode} - {response.Content}");
            }
        }

        public string GetIdFromLookup(Guid dataSetId, string urn)
        {
            var lookup = JsonConvert.DeserializeObject<List<LocationOption>>(File.ReadAllText($"C:\\Development\\BuryChoice\\EES.V1\\DatasetDownload\\{dataSetId}-urns.json"));
            return lookup.Where(x => x.Urn == urn).FirstOrDefault()?.Id;
        }

        public string GetUrnFromLookup(Guid dataSetId, string id)
        {
            var lookup = JsonConvert.DeserializeObject<List<LocationOption>>(File.ReadAllText($"C:\\Development\\BuryChoice\\EES.V1\\DatasetDownload\\{dataSetId}-urns.json"));
            return lookup.Where(x => x.Id == id).FirstOrDefault()?.Urn;
        }


        public async Task<EES.V1.Models.EESResponse.EESResponse> GetData()
        {
            var request = new RestRequest($"/data-sets/19e39901-a96c-be76-b9c2-6af54ae076d2/query", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            var body = @"{" + "\n" +
@"    ""indicators"": [" + "\n" +
@"        ""kgVhs""" + "\n" +
@"    ]," + "\n" +
@"    ""criteria"": {" + "\n" +
@"        ""and"": [" + "\n" +
@"            {" + "\n" +
@"                ""geographicLevels"": {" + "\n" +
@"                    ""in"":[ " + "\n" +
@"                        ""SCH""," + "\n" +
@"                        ""LA""" + "\n" +
@"                    ]" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"            {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""5Kydi""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"            {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""mws9K""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"            {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""WCb2b""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"            {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""9b64v""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"            {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""TaYuP""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"            {" + "\n" +
@"                ""locations"": {" + "\n" +
@"                    ""in"": [" + "\n" +
@"                        {" + "\n" +
@"                            ""level"": ""SCH""," + "\n" +
@"                            ""urn"": ""146529""" + "\n" +
@"                        }," + "\n" +
@"                        {" + "\n" +
@"                            ""level"": ""SCH""," + "\n" +
@"                            ""urn"": ""105354""" + "\n" +
@"                        }," + "\n" +
@"                        {" + "\n" +
@"                        ""level"": ""LA""," + "\n" +
@"                        ""oldCode"": ""351""" + "\n" +
@"                    }" + "\n" +
@"                    ]," + "\n" +
@"                    ""eq"": {" + "\n" +
@"                        ""level"": ""LA""," + "\n" +
@"                        ""oldCode"": ""351""" + "\n" +
@"                    }" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"            {" + "\n" +
@"                ""timePeriods"": {" + "\n" +
@"                    ""gte"": {" + "\n" +
@"                        ""period"": ""2022/2023""," + "\n" +
@"                        ""code"": ""AY""" + "\n" +
@"                    }," + "\n" +
@"                    ""lte"": {" + "\n" +
@"                        ""period"": ""2024/2025""," + "\n" +
@"                        ""code"": ""AY""" + "\n" +
@"                    }" + "\n" +
@"                }" + "\n" +
@"            }" + "\n" +
@"        ]" + "\n" +
@"    }," + "\n" +
@"    ""debug"": false," + "\n" +
@"    ""page"": 1," + "\n" +
@"    ""pageSize"": 1000" + "\n" +
@"}";
            request.AddStringBody(body, DataFormat.Json);


            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var content = response.Content;

                var eesResponse = System.Text.Json.JsonSerializer.Deserialize<EES.V1.Models.EESResponse.EESResponse>(content);

                return eesResponse;
            }
            else
            {
                throw new Exception($"Error fetching school data: {response.StatusCode} - {response.Content}");
            }
        }

        public async Task<EES.V1.Models.EESResponse.EESResponse> GetKS4PerformanceData_Estab()
        {
            var request = new RestRequest($"/data-sets/19e39901-a96c-be76-b9c2-6af54ae076d2/query", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            var body = @"{" + "\n" +
@"    ""indicators"": [" + "\n" +
@"        ""kgVhs""," + "\n" +
@"        ""Pwoeb""," + "\n" +
@"        ""hCRyW""," + "\n" +
@"        ""dDo0Z""," + "\n" +
@"        ""IL3Bz""" + "\n" +
@"    ]," + "\n" +
@"    ""criteria"": {" + "\n" +
@"        ""and"": [" + "\n" +
@"            {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""5Kydi""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"            {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""mws9K""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"            {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""WCb2b""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"            {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""9b64v""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"            {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""TaYuP""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"            {" + "\n" +
@"                ""locations"": {" + "\n" +
@"                    ""in"": [" + "\n" +
@"                        {" + "\n" +
@"                            ""level"": ""SCH""," + "\n" +
@"                            ""urn"": ""146529""" + "\n" +
@"                        }," + "\n" +
@"                        {" + "\n" +
@"                            ""level"": ""SCH""," + "\n" +
@"                            ""urn"": ""105354""" + "\n" +
@"                        }," + "\n" +
@"                        {" + "\n" +
@"                            ""level"": ""SCH""," + "\n" +
@"                            ""urn"": ""148097""" + "\n" +
@"                        }" + "\n" +
@"                    ]" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"            {" + "\n" +
@"                ""timePeriods"": {" + "\n" +
@"                    ""gte"": {" + "\n" +
@"                        ""period"": ""2022/2023""," + "\n" +
@"                        ""code"": ""AY""" + "\n" +
@"                    }" + "\n" +
@"                }" + "\n" +
@"            }" + "\n" +
@"        ]" + "\n" +
@"    }," + "\n" +
@"    ""debug"": false," + "\n" +
@"    ""page"": 1," + "\n" +
@"    ""pageSize"": 1000" + "\n" +
@"}";
            request.AddStringBody(body, DataFormat.Json);


            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var content = response.Content;

                var eesResponse = System.Text.Json.JsonSerializer.Deserialize<EES.V1.Models.EESResponse.EESResponse>(content);

                return eesResponse;
            }
            else
            {
                throw new Exception($"Error fetching school data: {response.StatusCode} - {response.Content}");
            }
        }

        public async Task<EES.V1.Models.EESResponse.EESResponse> GetKS4PerformanceData_LANAT()
        {
            var request = new RestRequest($"/data-sets/b3e19901-5d2b-b676-bb4c-e60937d74725/query", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            var body = @"{" + "\n" +
@"    ""indicators"": [" + "\n" +
@"        ""S9YVx""," + "\n" +
@"        ""OvpCL""," + "\n" +
@"        ""uWzo4""," + "\n" +
@"        ""HPhzL""," + "\n" +
@"        ""kxGhs""" + "\n" +
@"    ]," + "\n" +
@"    ""criteria"": {" + "\n" +
@"        ""and"": [" + "\n" +
@"            {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""bVOtT""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"            {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""pcsSo""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"            {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""bBiet""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"            {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""Cm2Id""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"            {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""aqzLP""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"                        {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""mrV9K""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"                        {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""V8F5X""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"                        {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""uiPo4""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"                        {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""rvQNj""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"                        {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""4Q8UZ""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"                        {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""WGD2b""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"                        {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""iFV6X""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"                        {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""qTajG""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"                                    {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""W1UF2""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"                                    {" + "\n" +
@"                ""filters"": {" + "\n" +
@"                    ""eq"": ""BUx7J""" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"" + "\n" +
@"            {" + "\n" +
@"                ""locations"": {" + "\n" +
@"                    ""in"": [" + "\n" +
@"                        {" + "\n" +
@"                            ""level"": ""LA""," + "\n" +
@"                            ""oldCode"": ""351""" + "\n" +
@"                        }," + "\n" +
@"                        {" + "\n" +
@"                            ""level"": ""NAT""," + "\n" +
@"                            ""code"": ""E92000001""" + "\n" +
@"                        }" + "\n" +
@"                    ]" + "\n" +
@"                }" + "\n" +
@"            }," + "\n" +
@"            {" + "\n" +
@"                ""timePeriods"": {" + "\n" +
@"                    ""gte"": {" + "\n" +
@"                        ""period"": ""2022/2023""," + "\n" +
@"                        ""code"": ""AY""" + "\n" +
@"                    }" + "\n" +
@"                }" + "\n" +
@"            }" + "\n" +
@"        ]" + "\n" +
@"    }," + "\n" +
@"    ""debug"": false," + "\n" +
@"    ""page"": 1," + "\n" +
@"    ""pageSize"": 1000" + "\n" +
@"}";
            request.AddStringBody(body, DataFormat.Json);


            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var content = response.Content;

                var eesResponse = System.Text.Json.JsonSerializer.Deserialize<EES.V1.Models.EESResponse.EESResponse>(content);

                return eesResponse;
            }
            else
            {
                throw new Exception($"Error fetching school data: {response.StatusCode} - {response.Content}");
            }
        }

        //public async Task<EES.V1.Models.EESResponse.EESResponse> GetDataQuery()
        //{
        //    var request = new RestRequest($"/data-sets/19e39901-a96c-be76-b9c2-6af54ae076d2/query", Method.Post);
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddHeader("Accept", "application/json");




        //    request.AddStringBody(JsonConvert.SerializeObject(eesQuery), DataFormat.Json);


        //    var response = await _client.ExecuteAsync(request);
        //    if (response.IsSuccessful)
        //    {
        //        var content = response.Content;

        //        var eesResponse = System.Text.Json.JsonSerializer.Deserialize<EES.V1.Models.EESResponse.EESResponse>(content);

        //        return eesResponse;
        //    }
        //    else
        //    {
        //        throw new Exception($"Error fetching school data: {response.StatusCode} - {response.Content}");
        //    }
        //}

    }
}
