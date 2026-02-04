namespace SunamoChar.Enums;

/// <summary>
/// Unicode character type categories
/// </summary>
public enum UnicodeChars
{
    #region char.Is*
    /// <summary>Control character</summary>
    Control,
    /// <summary>High surrogate character</summary>
    HighSurrogate,
    /// <summary>Lowercase letter</summary>
    Lower,
    /// <summary>Low surrogate character</summary>
    LowSurrogate,
    /// <summary>Numeric character</summary>
    Number,
    /// <summary>Punctuation character</summary>
    Punctaction,
    /// <summary>Separator character</summary>
    Separator,
    /// <summary>Surrogate character</summary>
    Surrogate,

    //char.IsSurrogatePair(low, right) - pair is formed by low and high
    //IsSurrogatePair,
    /// <summary>Symbol character</summary>
    Symbol,
    /// <summary>Uppercase letter</summary>
    Upper,
    /// <summary>Whitespace character</summary>
    WhiteSpace,

    #endregion
    /// <summary>Special character</summary>
    Special,
    /// <summary>Generic character</summary>
    Generic
}