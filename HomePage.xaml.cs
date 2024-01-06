namespace Wordle;

public partial class HomePage : ContentPage
{
    public HomePage()
	{
        InitializeComponent();
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage", true);
        UserData.EnteredText = UsernameEntry.Text;
        
    }

    public static class UserData
    {
        public static string EnteredText { get; set; }
    }

}