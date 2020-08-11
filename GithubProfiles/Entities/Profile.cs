using System.Collections.Generic;
using Newtonsoft.Json;

namespace GithubProfiles.Entities
{
    public class Profile
    {
        [JsonProperty("avatar_url")]
        public string Avatar { get; set; }
        [JsonProperty("login")]
        public string Username { get; set; }
        public int Followers { get; set; }
        [JsonProperty("public_repos")]
        public int RepoCount { get; set; }
        public IList<string> TopRepoList { get; set; }
    }
}
