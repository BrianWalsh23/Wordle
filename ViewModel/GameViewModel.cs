using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wordle.Model;



namespace Wordle.ViewModel
{
    public partial class GameViewModel : ObservableObject
    {
        // rowIndex 0-5
        int rowIndex;

        //columnIndex 0-4
        int columnIndex;

        char[] correctAnswer;

        public char[] KeyboardRow1 { get; }
        public char[] KeyboardRow2 { get; }

        public char[] KeyboardRow3 { get; }

        [ObservableProperty]
        private WordRow[] rows;

        public GameViewModel()
        {
            Rows = new WordRow[6]
            {
                new WordRow(),
                new WordRow(),
                new WordRow(),
                new WordRow(),
                new WordRow(),
                new WordRow()
            };

            //char[] correctAnswer = new char[] { 'c', 'o', 'd', 'e', 's' };
            correctAnswer = "HELLO".ToCharArray();
            KeyboardRow1 = "QWERTYUIOP".ToCharArray();
            KeyboardRow2 = "ASDFGHJKL".ToCharArray();
            KeyboardRow3 = "<ZXCVBNM>".ToCharArray();


        }
        [RelayCommand]
        public void Enter()
        {
            if (columnIndex != 5)
                return;

            var correct = Rows[rowIndex].Validate(correctAnswer);

            if (correct)
            {
                App.Current.MainPage.DisplayAlert("You Win!", "Congratulations!", "OK");
                return;
            }
            if (rowIndex == 5)
            {
                App.Current.MainPage.DisplayAlert("Game Over!", "You are out of turns!", "OK");
            }

            if (columnIndex != 5)
                App.Current.MainPage.DisplayAlert("Please enter five letters", "Keep going!", "OK");

            else
            {
                rowIndex++;
                columnIndex = 0;
            }

        }
        [RelayCommand]
        public void EnterLetter(char letter)
        {
            if (letter == '>')
            {
                Enter();
                return;
            }

            if (letter == '<')
            {
                if (columnIndex == 0)
                    return;
                columnIndex--;
                Rows[rowIndex].Letters[columnIndex].Input = ' ';

                return;
            }

            if (columnIndex == 5)

                return;
            //Letters here
            Rows[rowIndex].Letters[columnIndex].Input = letter;
            columnIndex++;

        }

    }
}
