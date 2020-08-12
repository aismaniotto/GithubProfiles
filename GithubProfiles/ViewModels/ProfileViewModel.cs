using System;
using System.Net;
using System.Net.Http;
using System.Windows.Input;
using GithubProfiles.Entities;
using GithubProfiles.Services.Api;
using Xamarin.Forms;
using Xamarin.Essentials;
using GithubProfiles.Services;

namespace GithubProfiles.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public ICommand SearchProfileCommand { get; private set; }
        public ICommand ChangeThemeCommand { get; private set; }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
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
        private bool _isEmpty = true;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set { SetProperty(ref _isEmpty, value); }
        }
        public bool IsDarkTheme
        {
            get
            {
                return Preferences.Get("dark_theme", false);
            }
        }

        private GithubService _githubService;

        public ProfileViewModel()
        {
            _githubService = new GithubService();
            SearchProfileCommand = new Command(SearchProfileCommandAction);
            ChangeThemeCommand = new Command(ChangeThemeCommandAction);
        }

        private async void SearchProfileCommandAction()
        {

            try
            {
                IsBusy = true;
                IsEmpty = true;
                Profile = await _githubService.GetGithubProfile(Username);
                IsEmpty = false;
            }
            catch (HttpRequestException)
            {
                IsEmpty = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Not found",
                    "The informed usernames wasn't found",
                    "Ok");

            }
            catch (WebException)
            {
                IsEmpty = true;
                await Application.Current.MainPage.DisplayAlert(
                    "No internet",
                    "Not possible to connect on internet. Please try again",
                    "Ok");
            }
            catch (Exception)
            {
                IsEmpty = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Something wrong",
                    "Some unnexpectable error happens :(. Please try again",
                    "Ok");
            }
            finally
            {
                IsBusy = false;
            }

        }

        private void ChangeThemeCommandAction()
        {
            ThemeManager.SetTheme(!IsDarkTheme);
        }
    }
}
