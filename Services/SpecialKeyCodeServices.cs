namespace SunamoChar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class SpecialKeyCodeServices
{
    public readonly List<int> specialKeyCodes =
        new(new[] { 33, 64, 35, 36, 37, 94, 38, 42, 63, 95, 126 });
}