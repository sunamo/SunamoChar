// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoChar.Services;

public class SpecialKeyCodeServices
{
    public readonly List<int> specialKeyCodes =
        new(new[] { 33, 64, 35, 36, 37, 94, 38, 42, 63, 95, 126 });
}