using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using Wordle.Model;

namespace Wordle.ViewModel
{
    public partial class GameViewModel : ObservableObject
    {
        ListWords list;

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
            list = new ListWords();
            rows = new WordRow[6]
                {
                new WordRow(),
                new WordRow(),
                new WordRow(),
                new WordRow(),
                new WordRow(),
                new WordRow()
                };


            KeyboardRow1 = "QWERTYUIOP".ToCharArray();
            KeyboardRow2 = "ASDFGHJKL".ToCharArray();
            KeyboardRow3 = "<ZXCVBNM>".ToCharArray();


            InitializeAsync();


        }

        private async void InitializeAsync()
        {
            await list.getWordList();
            GenerateWord();
            await App.Current.MainPage.DisplayAlert("Generated Word", $"The generated word is: {new string(Word)}", "OK");
        }

        public bool newWord = true;
        public char[] Word;

        public async void GenerateWord()
        {
            Word = list.GenerateRandomWord().ToCharArray();

        }

        public void Enter()
        {
            if (columnIndex != 5)
                return;

            // Join the letters to form the entered word
            var enteredWord = new string(Rows[rowIndex].Letters.Select(l => char.ToLowerInvariant(l.Input)).ToArray()).Trim();

            // Debug information
            Debug.WriteLine($"Entered Word: {enteredWord}, Expected Word: {new string(Word)}");

            // Check if the entered word exists in the list
            if (list.WordExists(enteredWord))
            {
                var answer = Rows[rowIndex].Validate(Word);


                var expectedWord = new string(Word).ToLowerInvariant();


                if (answer)
                {
                    HandleGameEnd("Congratulations! You Win!", "Click okay to play again!");
                    return;
                }

                if (rowIndex == 5)
                {
                    HandleGameEnd("Game Over!", "Out of Turns. Click okay to try again!");
                }
                else
                {
                    rowIndex++;
                    columnIndex = 0;
                }
            }
            else
            {
                // Display an alert if the entered word is not in the list
                App.Current.MainPage.DisplayAlert("Invalid Word", "The entered word is not in the list.", "OK");
                ClearCurrentLine();
            }
        }

        private void ClearCurrentLine()
        {
            // Clear the current line by setting the Input of each letter to ' '
            foreach (var letter in Rows[rowIndex].Letters)
            {
                letter.Input = ' ';
            }

            // Optionally, reset the columnsIndex to 0 if needed
            columnIndex = 0;
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

        private void ResetGame()
        {
            rowIndex = 0;
            columnIndex = 0;
            newWord = true;

            // Reset the Letters in each row
            foreach (var row in Rows)
            {
                for (int i = 0; i < row.Letters.Length; i++)
                {
                    row.Letters[i].Input = ' ';
                    row.Letters[i].Color = Colors.White; // Set to the default color (you may have a specific color for default state)
                    row.Letters[i].IsCorrect = false;
                }
            }

            GenerateWord();

            // Notify UI that properties have changed
            OnPropertyChanged(nameof(Rows));
            OnPropertyChanged(nameof(Word));
        }

        private void HandleGameEnd(string title, string message)
        {
            App.Current.MainPage.DisplayAlert(title, message, "OK");
            ResetGame();
        }

    }
}
