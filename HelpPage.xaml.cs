using static Wordle.HomePage;

namespace Wordle;

public partial class HelpPage : ContentPage
{
	public HelpPage()
	{
		InitializeComponent();
        string storedText = UserData.EnteredText;
        StoredTextLabel.Text = storedText;
    }

}