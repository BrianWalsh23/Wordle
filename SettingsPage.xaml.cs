namespace Wordle;

public partial class SettingsPage : ContentPage
{

    public SettingsPage()
    {

        InitializeComponent();
    }

    private void LightMode_Clicked(object sender, EventArgs e)
    {

        // Change the color resources to light mode colors
        Application.Current.Resources["PrimaryColor"] = Color.FromRgb(255, 255, 255);
        Application.Current.Resources["SecondaryColor"] = Color.FromRgb(100, 100, 100);
        Application.Current.Resources["ThirdColor"] = Color.FromRgb(0, 0, 0);



    }

    private void DarkMode_Clicked(object sender, EventArgs e)
    {
        BackgroundColor = Color.FromRgb(25, 25, 25);
        Application.Current.Resources["PrimaryColor"] = Color.FromRgb(25, 25, 25);


    }
}
