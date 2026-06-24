namespace SunamoChar.Services;

public class GeneralCharService
{
    private static readonly char tabCharacter = (char)9;
    public List<char> GeneralChars { get; } = new List<char>(new[] { tabCharacter });
    public Predicate<char> ReturnRightPredicate(char character)
    {
        if (character == tabCharacter)
            return char.IsNumber;
        else
            ThrowEx.NotImplementedCase(GeneralChars);

        throw new InvalidOperationException("Predicate not found for generic character");
    }
}
