namespace SunamoChar.Services;

/// <summary>
/// Service for handling general character operations
/// </summary>
public class GeneralCharService
{
    private static readonly char tabCharacter = (char)9;
    /// <summary>
    /// List of general characters
    /// </summary>
    public List<char> GeneralChars { get; } = new List<char>(new[] { tabCharacter });
    /// <summary>
    /// Returns the appropriate predicate for a generic character
    /// </summary>
    /// <param name="character">The character to get the predicate for.</param>
    /// <returns>A predicate function for testing characters of the same type.</returns>
    public Predicate<char> ReturnRightPredicate(char character)
    {
        if (character == tabCharacter)
            return char.IsNumber;
        else
            ThrowEx.NotImplementedCase(GeneralChars);

        throw new InvalidOperationException("Predicate not found for generic character");
    }
}