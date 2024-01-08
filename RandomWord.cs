namespace Wordle
{


    public class ListWords
    {
        List<string> words = new List<string>();
        HttpClient httpClient;
        string savedfilelocation = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "words.txt");

        public async Task getWordList()
        {
            //check if the file exists
            if (File.Exists(savedfilelocation))
            {
                ReadFileIntoList();
            }
            else
            {
                //download the file if it doesn't exist
                await DownloadFile();
                ReadFileIntoList();
            }
        }

        //Read File
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

        //Download file
        public async Task DownloadFile()
        {
            //method to download the file
            using (var httpClient = new HttpClient())
            {
                var responseStream = await httpClient.GetStreamAsync("https://raw.githubusercontent.com/DonH-ITS/jsonfiles/main/words.txt");
                using (var fileStream = new FileStream(savedfilelocation, FileMode.Create))
                {
                    await responseStream.CopyToAsync(fileStream);
                }
            }
        }
        //Generate Random word
        public String GenerateRandomWord()
        {
            //Generate a random word from the list
            Random random = new Random();
            int which = random.Next(words.Count);
            return words[which];
        }

        public bool WordExists(string wordToCheck)
        {
            // Check if the given word exists in the list
            return words.Contains(wordToCheck);
        }
    }
}