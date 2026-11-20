namespace SunamoChar.Enums;

/// <summary>
/// Represents the unicode chars type.
/// </summary>
public enum UnicodeChars
{
    #region char.Is*
    /// <summary>
    /// The control value.
    /// </summary>
    Control,
    /// <summary>
    /// The high surrogate value.
    /// </summary>
    HighSurrogate,
    /// <summary>
    /// The lower value.
    /// </summary>
    Lower,
    /// <summary>
    /// The low surrogate value.
    /// </summary>
    LowSurrogate,
    /// <summary>
    /// The number value.
    /// </summary>
    Number,
    /// <summary>
    /// The punctuation value.
    /// </summary>
    Punctuation,
    /// <summary>
    /// The separator value.
    /// </summary>
    Separator,
    /// <summary>
    /// The surrogate value.
    /// </summary>
    Surrogate,
    /// <summary>
    /// The symbol value.
    /// </summary>
    Symbol,
    /// <summary>
    /// The upper value.
    /// </summary>
    Upper,
    /// <summary>
    /// The white space value.
    /// </summary>
    WhiteSpace,

    #endregion
    /// <summary>
    /// The special value.
    /// </summary>
    Special,
    /// <summary>
    /// The generic value.
    /// </summary>
    Generic
}
