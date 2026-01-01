namespace SunamoChar.Services;

/// <summary>
/// Service for handling special character key codes
/// </summary>
public class SpecialKeyCodeServices
{
    /// <summary>
    /// List of special character key codes
    /// </summary>
    public List<int> SpecialKeyCodes { get; } =
        new(new[] { 33, 64, 35, 36, 37, 94, 38, 42, 63, 95, 126 });
}