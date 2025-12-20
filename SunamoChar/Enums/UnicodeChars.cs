// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoChar.Enums;

public enum UnicodeChars
{
    #region char.Is*

    Control,
    HighSurrogate,
    Lower,
    LowSurrogate,
    Number,
    Punctaction,
    Separator,
    Surrogate,

    //char.IsSurrogatePair(low, right) - pair is formed by low and high
    //IsSurrogatePair,
    Symbol,
    Upper,
    WhiteSpace,

    #endregion

    Special,
    Generic
}