namespace SunamoChar.Services;

public class GeneralCharService
{
    static char notNumber = (char)9;
    public readonly List<char> GeneralChars = new List<char>(new[] { notNumber });
    public Predicate<char> ReturnRightPredicate(char genericChar)
    {
        Predicate<char> predicate = null;
        if (genericChar == notNumber)
            predicate = char.IsNumber;
        else
            ThrowEx.NotImplementedCase(GeneralChars);
        return predicate;
    }
}