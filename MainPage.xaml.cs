using Wordle.ViewModel;
using static Wordle.HomePage;

namespace Wordle
{
    
    public partial class MainPage : ContentPage
    {

        

        public MainPage(GameViewModel viewModel)
        {

            InitializeComponent();;
            BindingContext = viewModel;
            var frame = new Frame();
            //string storedText = UserData.EnteredText;
           // StoredTextLabel.Text = storedText;

        }

        private async void Settings_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new SettingsPage());
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HelpPage());
        }
    }

}