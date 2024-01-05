using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Diagnostics;

namespace Wordle.Model;

public class WordRow : ObservableObject
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
    private Letter[] letters;

    public Letter[] Letters
    {
        get { return letters; }
        set { SetProperty(ref letters, value); }
    }


    //Are letters valid Green yes. Yellow is yes but in wrong position.
    public bool Validate(char[] Word)
    {
        int count = 0;
        for (int i = 0; i < Letters.Length; i++)
        {
            var letter = Letters[i];
            char inputUpperCase = char.ToUpperInvariant(letter.Input); // Convert input to upper case
            char expectedUpperCase = char.ToUpperInvariant(Word[i]);

            Debug.WriteLine($"Input: {letter.Input}, Expected: {Word[i]}");

            if (inputUpperCase == expectedUpperCase)
            {
                letter.Color = Colors.Green;
                letter.IsCorrect = true;
                count++;
            }
            else if (Word.Contains(letter.Input) || Word.Contains(char.ToLowerInvariant(letter.Input)))
            {
                letter.Color = Colors.Yellow;
                letter.IsCorrect = false;
            }
            else
            {
                letter.Color = Colors.Gray;
                letter.IsCorrect = false;
            }
        }

        return count == 5;
    }
}

public partial class Letter : ObservableObject
{
    private bool isCorrect;

    public bool IsCorrect
    {
        get { return isCorrect; }
        set { SetProperty(ref isCorrect, value); }
    }

    private char input;

    public char Input
    {
        get { return input; }
        set { SetProperty(ref input, value); }
    }

    private Color color;

    public Color Color
    {
        get { return color; }
        set { SetProperty(ref color, value); }
    }
}
