using Mopups.Services;
using Wordle.ViewModel;

namespace Wordle
{
    public partial class MainPage : ContentPage
    {


        public MainPage(GameViewModel viewModel)
        {

            InitializeComponent();
            BindingContext = viewModel;
            var frame = new Frame();
        }

        private void StartHere_Clicked(object sender, EventArgs e)
        {
            MopupService.Instance.PushAsync(new PopUpPage());
        }
    }
}