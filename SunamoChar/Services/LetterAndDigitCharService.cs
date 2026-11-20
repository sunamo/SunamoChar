namespace SunamoChar.Services;

/// <summary>
/// Represents the letter and digit char service type.
/// </summary>
public class LetterAndDigitCharService
{
    /// <summary>
    /// Gets the tab character.
    /// </summary>
    public char TabCharacter { get; } = (char)9;
    /// <summary>
    /// Gets or sets the all chars without special.
    /// </summary>
    public List<char>? AllCharsWithoutSpecial { get; set; }
    /// <summary>
    /// Gets or sets the all chars.
    /// </summary>
    public List<char>? AllChars { get; set; }
    /// <summary>
    /// Gets the numeric chars.
    /// </summary>
    public List<char> NumericChars { get; } =
        new(new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' });
    /// <summary>
    /// Gets the lower chars.
    /// </summary>
    public List<char> LowerChars { get; } = new(new[]
    {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
        'w', 'x', 'y', 'z'
    });
    /// <summary>
    /// Gets the upper chars.
    /// </summary>
    public List<char> UpperChars { get; } = new(new[]
    {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
        'W', 'X', 'Y', 'Z'
    });
    /// <summary>
    /// Initialize all chars.
    /// </summary>
    /// <param name="}">The }.</param>
    /// <returns>The initialize all chars result.</returns>
    public void InitializeAllChars()
    {
        AllCharsWithoutSpecial = new List<char>(LowerChars.Count + NumericChars.Count + UpperChars.Count);
        AllCharsWithoutSpecial.AddRange(LowerChars);
        AllCharsWithoutSpecial.AddRange(NumericChars);
        AllCharsWithoutSpecial.AddRange(UpperChars);
    }
}
