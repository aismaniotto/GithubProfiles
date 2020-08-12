using GithubProfiles.ViewModels;
using Xamarin.Forms;

namespace GithubProfiles.Presentation.Pages
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel();

            // declarin here to avoid triggering on first time set up
            switchDarkTheme.Toggled += SwitchDarkTheme_Toggled;
        }

        private void SwitchDarkTheme_Toggled(object sender, ToggledEventArgs e)
        {
            // Switch doesnt have command property
            ((ProfileViewModel)this.BindingContext).ChangeThemeCommand.Execute(null);
        }
    }
}
