namespace SunamoChar.Services;

/// <summary>
/// Service for handling special characters
/// </summary>
public class SpecialCharsService
{
    /// <summary>
    /// Primary list of special characters
    /// </summary>
    public List<char> SpecialChars { get; } = new(new[]
        { excl, commat, num, dollar, percnt, hat, amp, ast, quest, lowbar, tilda });
    /// <summary>
    /// Secondary list of special characters
    /// </summary>
    public List<char> SpecialChars2 { get; } = new(new[]
    {
        lq, rq, dash, la, ra,
        comma, period, colon, apos, rpar, sol, lt, gt, lcub, rcub, lsqb, verbar, semi, plus, rsqb,
        ndash, slash
    });
    /// <summary>
    ///     Used in enigma
    /// </summary>
    public List<char>? SpecialCharsAll { get; set; }
    /// <summary>
    /// Whitespace special characters
    /// </summary>
    public List<char> SpecialCharsWhite { get; } = new(new[] { space });
    /// <summary>
    /// Special characters not used in enigma
    /// </summary>
    public List<char> SpecialCharsNotEnigma { get; } = new(new[] { nonBreakingSpace, copy });
    private const char la = '\u2018';
    private const char ra = '\u2019';
    private const char comma = ',';
    private const char space = ' ';
    private static readonly char nonBreakingSpace = (char)160;
    private const char dollar = '$';
    private const char hat = '^';
    private const char ast = '*';
    private const char quest = '?';
    private const char tilda = '~';
    private const char period = '.';
    private const char colon = ':';
    private const char excl = '!';
    private const char apos = '\'';
    private const char rpar = ')';
    private const char sol = '/';
    private const char lowbar = '_';
    private const char lt = '<';
    private const char gt = '>';
    private const char amp = '&';
    private const char lcub = '{';
    private const char rcub = '}';
    private const char lsqb = '[';
    private const char verbar = '|';
    private const char semi = ';';
    private const char commat = '@';
    private const char plus = '+';
    private const char rsqb = ']';
    private const char num = '#';
    private const char percnt = '%';
    private const char ndash = '–';
    private const char copy = '©';
    #region MyRegion
    private const char lq = '"';
    private const char rq = '"';
    #region Names here must be the same as in Consts
    private const char dash = '-';
    #endregion
    private const char slash = '/';
    #endregion
}