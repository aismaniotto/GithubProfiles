using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GithubProfiles.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace GithubProfiles.Services.Api
{
    /**
     * Inspired by 
     * https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/Mobile/eShopOnContainers/eShopOnContainers.Core/Services/RequestProvider/RequestProvider.cs
     */

    public class GithubService
    {
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly string _baseUri = "https://api.github.com/users/";

        public GithubService()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };
            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public async Task<Profile> GetGithubProfile(string username)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var uri = _baseUri + username;
            HttpResponseMessage response = await httpClient.GetAsync(uri);

            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            Profile result = await Task.Run(() =>
                JsonConvert.DeserializeObject<Profile>(serialized, _serializerSettings));
            result.TopRepoList = await GetGithubTopRepos(username);
            return result;
        }

        private async Task<IList<string>> GetGithubTopRepos(string username)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var uri = _baseUri + username + "/repos";
            HttpResponseMessage response = await httpClient.GetAsync(uri);

            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            var result = await Task.Run(() =>
                JsonConvert.DeserializeObject<List<IDictionary<string, object>>>(serialized, _serializerSettings));

            List<string> topRepos = new List<string>();

            result.GetRange(0, result.Count < 4 ? result.Count : 4).ForEach((repo) => topRepos.Add((string)repo["name"]));

            return topRepos;
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                throw new HttpRequestException(content);
            }
        }
    }
}
