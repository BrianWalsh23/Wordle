using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace Wordle.Model;

public class WordRow
{
    public WordRow()
    {
        Letters = new Letter[5]
        {
            new Letter(),
            new Letter(),
            new Letter(),
            new Letter(),
            new Letter(),
        };
    }
    public Letter[] Letters { get; set; }

    //Are letters valid Green yes. Yellow is yes but in wrong position.
    public bool Validate(char[] correctAnswer)
    {
        int count = 0;

        for (int i = 0; i < Letters.Length; i++)
        {
            var letter = Letters[i];
            if (letter.Input == correctAnswer[i])
            {
                letter.Color = Colors.Green;
                count++;
            }
            else if (correctAnswer.Contains(letter.Input))
            {
                letter.Color = Colors.Yellow;
            }
            else
            {
                letter.Color = Colors.Gray;
            }
        }
        return count == 5;
    }
}

public partial class Letter : ObservableObject
{
    public Letter()
    {
        Color = Colors.Black;
    }

    [ObservableProperty]
    private char input;

    [ObservableProperty]
    private Color color;


}
