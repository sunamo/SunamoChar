namespace SunamoChar.Services;

public class SpecialKeyCodeServices
{
    public List<int> SpecialKeyCodes { get; } =
        new(new[] { 33, 64, 35, 36, 37, 94, 38, 42, 63, 95, 126 });
}
