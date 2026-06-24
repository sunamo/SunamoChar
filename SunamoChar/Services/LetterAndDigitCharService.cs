namespace SunamoChar.Services;

public class LetterAndDigitCharService
{
    public char TabCharacter { get; } = (char)9;
    public List<char>? AllCharsWithoutSpecial { get; set; }
    public List<char>? AllChars { get; set; }
    public List<char> NumericChars { get; } =
        new(new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' });
    public List<char> LowerChars { get; } = new(new[]
    {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
        'w', 'x', 'y', 'z'
    });
    public List<char> UpperChars { get; } = new(new[]
    {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
        'W', 'X', 'Y', 'Z'
    });
    public void InitializeAllChars()
    {
        AllCharsWithoutSpecial = new List<char>(LowerChars.Count + NumericChars.Count + UpperChars.Count);
        AllCharsWithoutSpecial.AddRange(LowerChars);
        AllCharsWithoutSpecial.AddRange(NumericChars);
        AllCharsWithoutSpecial.AddRange(UpperChars);
    }
}
