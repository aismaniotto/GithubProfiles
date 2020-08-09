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
        }
    }
}
