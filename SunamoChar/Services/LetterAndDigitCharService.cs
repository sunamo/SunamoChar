// variables names: ok
namespace SunamoChar.Services;

/// <summary>
/// Service for handling letters and digits
/// </summary>
public class LetterAndDigitCharService
{
    /// <summary>
    /// Tab character
    /// </summary>
    public char TabCharacter { get; } = (char)9;
    /// <summary>
    /// All characters without special characters
    /// </summary>
    public List<char>? AllCharsWithoutSpecial { get; set; }
    /// <summary>
    /// All characters
    /// </summary>
    public List<char>? AllChars { get; set; }
    /// <summary>
    /// Numeric characters (0-9)
    /// </summary>
    public List<char> NumericChars { get; } =
        new(new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' });
    /// <summary>
    /// Lowercase letter characters (a-z)
    /// </summary>
    public List<char> LowerChars { get; } = new(new[]
    {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
        'w', 'x', 'y', 'z'
    });
    /// <summary>
    /// Uppercase letter characters (A-Z)
    /// </summary>
    public List<char> UpperChars { get; } = new(new[]
    {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
        'W', 'X', 'Y', 'Z'
    });
    /// <summary>
    /// Initializes all character lists
    /// </summary>
    public void InitializeAllChars()
    {
        AllCharsWithoutSpecial = new List<char>(LowerChars.Count + NumericChars.Count + UpperChars.Count);
        AllCharsWithoutSpecial.AddRange(LowerChars);
        AllCharsWithoutSpecial.AddRange(NumericChars);
        AllCharsWithoutSpecial.AddRange(UpperChars);
    }
}