using GithubProfiles.Presentation.Pages;
using GithubProfiles.Services;
using Xamarin.Forms;

namespace GithubProfiles
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ThemeManager.LoadTheme();
            MainPage = new NavigationPage(new ProfilePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
