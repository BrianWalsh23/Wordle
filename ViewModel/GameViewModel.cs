using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui;
using System.Collections.Generic;
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

            var correct = Rows[rowIndex].Validate(Word);

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
