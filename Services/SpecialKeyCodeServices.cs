namespace SunamoChar.Services;
using System.Collections.Generic;

internal class SpecialKeyCodeServices
{
    public readonly List<int> specialKeyCodes =
        new(new[] { 33, 64, 35, 36, 37, 94, 38, 42, 63, 95, 126 });
}