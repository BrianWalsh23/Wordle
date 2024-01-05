using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wordle.Model;

namespace Wordle.ViewModel
{
    public partial class GameViewModel : ObservableObject
    {
        List<string> words = new List<string>();
        HttpClient httpClient;
        string savedfilelocation = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "RandomWords.txt");


        public async Task getWordList()
        {
            if (File.Exists(savedfilelocation))
            {
                ReadFileIntoList();
            }
            else
            {
                await DownloadFile();
                ReadFileIntoList();
            }
        }

        public void ReadFileIntoList()
        {
            StreamReader sr = new StreamReader(savedfilelocation);
            string word = "";
            while ((word = sr.ReadLine()) != null)
            {
                words.Add(word);
            }
            sr.Close();
        }
        public async Task DownloadFile()
        {

            using (var httpClient = new HttpClient())
            {
                var responseStream = await httpClient.GetStreamAsync("https://raw.githubusercontent.com/DonH-ITS/jsonfiles/main/words.txt");
                using var fileStream = new FileStream(savedfilelocation, FileMode.Create);
                responseStream.CopyToAsync(fileStream);
            }
        }


        public String GenerateRandomWord()
        {
            if (words.Count > 0)
            {
                Random random = new Random();
                int which = random.Next(words.Count);
                return words[which];
            }

            else
            {
                throw new InvalidOperationException("Words list is empty or not initialized.");

            }
        }

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
            string randomWord = GenerateRandomWord();
            correctAnswer = randomWord.ToCharArray();

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
            correctAnswer = randomWord.ToCharArray();
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
