using System.Collections.Generic;

namespace GithubProfiles.Entities
{
    public class Profile
    {
        public string Avatar { get; set; }
        public string Username { get; set; }
        public int Followers { get; set; }
        public int RepoCount { get; set; }
        public IList<string> TopRepoList { get; set; }
    }
}
