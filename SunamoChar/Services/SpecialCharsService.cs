namespace SunamoChar.Services;

public class SpecialCharsService
{
    public List<char> SpecialChars { get; } = new(new[]
        { exclamation, atSign, hash, dollar, percent, caret, ampersand, asterisk, questionMark, underscore, tilde });
    public List<char> SpecialChars2 { get; } = new(new[]
    {
        leftQuote, rightQuote, dash, leftSingleQuote, rightSingleQuote,
        comma, period, colon, apostrophe, rightParenthesis, solidus, lessThan, greaterThan, leftCurlyBrace, rightCurlyBrace, leftSquareBracket, verticalBar, semicolon, plus, rightSquareBracket,
        enDash
    });
    public List<char>? SpecialCharsAll { get; set; }
    public List<char> SpecialCharsWhite { get; } = new(new[] { space });
    public List<char> SpecialCharsNotEnigma { get; } = new(new[] { nonBreakingSpace, copyright });
    private const char leftSingleQuote = '‘';
    private const char rightSingleQuote = '’';
    private const char comma = ',';
    private const char space = ' ';
    private static readonly char nonBreakingSpace = (char)160;
    private const char dollar = '$';
    private const char caret = '^';
    private const char asterisk = '*';
    private const char questionMark = '?';
    private const char tilde = '~';
    private const char period = '.';
    private const char colon = ':';
    private const char exclamation = '!';
    private const char apostrophe = '\'';
    private const char rightParenthesis = ')';
    private const char solidus = '/';
    private const char underscore = '_';
    private const char lessThan = '<';
    private const char greaterThan = '>';
    private const char ampersand = '&';
    private const char leftCurlyBrace = '{';
    private const char rightCurlyBrace = '}';
    private const char leftSquareBracket = '[';
    private const char verticalBar = '|';
    private const char semicolon = ';';
    private const char atSign = '@';
    private const char plus = '+';
    private const char rightSquareBracket = ']';
    private const char hash = '#';
    private const char percent = '%';
    private const char enDash = '–';
    private const char copyright = '©';
    private const char leftQuote = '“';
    private const char rightQuote = '”';
    private const char dash = '-';
}
