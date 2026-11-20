namespace SunamoChar.Services;

/// <summary>
/// Represents the special key code services type.
/// </summary>
public class SpecialKeyCodeServices
{
    /// <summary>
    /// Gets the special key codes.
    /// </summary>
    public List<int> SpecialKeyCodes { get; } =
        new(new[] { 33, 64, 35, 36, 37, 94, 38, 42, 63, 95, 126 });
}
