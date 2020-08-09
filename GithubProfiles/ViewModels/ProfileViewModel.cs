using System.Collections.Generic;
using System.Windows.Input;
using GithubProfiles.Entities;
using Xamarin.Forms;

namespace GithubProfiles.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public ICommand SearchProfileCommand { get; private set; }

        private Profile _profile;
        public Profile Profile
        {
            get { return _profile; }
            set { SetProperty(ref _profile, value); }
        }
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public ProfileViewModel()
        {
            SearchProfileCommand = new Command(SearchProfileCommandAction);
        }

        private async void SearchProfileCommandAction()
        {
            IsBusy = true;

            Profile = new Profile
            {
                Username = "André",
                Avatar = "someurl",
                Followers = 5,
                RepoCount = 15,
                TopRepoList = new List<string>
               {
                   "Test 1",
                   "Test 2",
                   "Test 3",
                   "Test 4",
               }
            };

            IsBusy = false;
        }
    }
}
