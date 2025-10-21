// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoChar.Services;

public class LetterAndDigitCharService
{
    public char NotNumber { get; } = (char)9;
    public List<char> allCharsWithoutSpecial;
    public List<char> allChars;
    public readonly List<char> numericChars =
        new(new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' });
    public readonly List<char> lowerChars = new(new[]
    {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
        'w', 'x', 'y', 'z'
    });
    public readonly List<char> upperChars = new(new[]
    {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
        'W', 'X', 'Y', 'Z'
    });
    void Add()
    {
        allCharsWithoutSpecial = new List<char>(lowerChars.Count + numericChars.Count + upperChars.Count);
        allCharsWithoutSpecial.AddRange(lowerChars);
        allCharsWithoutSpecial.AddRange(numericChars);
        allCharsWithoutSpecial.AddRange(upperChars);
    }
}