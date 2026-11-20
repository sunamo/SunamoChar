namespace SunamoChar.Services;

/// <summary>
/// Represents the general char service type.
/// </summary>
public class GeneralCharService
{
    private static readonly char tabCharacter = (char)9;
    /// <summary>
    /// Gets the general chars.
    /// </summary>
    public List<char> GeneralChars { get; } = new List<char>(new[] { tabCharacter });
    /// <summary>
    /// Return right predicate.
    /// </summary>
    /// <typeparam name="char">The type char.</typeparam>
    /// <param name="}">The }.</param>
    /// <returns>The return right predicate result.</returns>
    public Predicate<char> ReturnRightPredicate(char character)
    {
        if (character == tabCharacter)
            return char.IsNumber;
        else
            ThrowEx.NotImplementedCase(GeneralChars);

        throw new InvalidOperationException("Predicate not found for generic character");
    }
}
