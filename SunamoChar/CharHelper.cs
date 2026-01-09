// variables names: ok
namespace SunamoChar;

/// <summary>
/// Helper class for character operations including Unicode character detection and string manipulation
/// </summary>
public class CharHelper
{
    /// <summary>
    /// Splits text by special delimiters, removing empty entries
    /// </summary>
    public static List<string> SplitSpecial(string text, params char[] delimiters)
    {
        return SplitSpecial(StringSplitOptions.RemoveEmptyEntries, text, delimiters);
    }
    /// <summary>
    /// Splits text by special delimiters, keeping empty entries
    /// </summary>
    public static List<string> SplitSpecialNone(string text, params char[] delimiters)
    {
        return SplitSpecial(StringSplitOptions.None, text, delimiters);
    }
    /// <summary>
    ///     Use with general letters
    /// </summary>
    /// <param name="stringSplitOptions">Options for splitting the string (remove empty entries or keep them).</param>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">Characters to use as delimiters.</param>
    private static List<string> SplitSpecial(StringSplitOptions stringSplitOptions, string text, params char[] delimiters)
    {
        if (delimiters == null || delimiters.Count() == 0) throw new Exception("NoDelimiterDetermined");
        if (delimiters.Length == 1 && !IsUnicodeChar(UnicodeChars.Generic, delimiters[0]))
            return text.Split(delimiters, stringSplitOptions).ToList();
        var normal = new List<char>();
        var generic = new List<char>();
        foreach (var item in delimiters)
            if (IsUnicodeChar(UnicodeChars.Generic, item))
                generic.Add(item);
            else
                normal.Add(item);
        if (generic.Count > 0)
        {
            var splitted = new List<string>();
            if (normal.Count > 0)
                splitted.AddRange(text.Split(normal.ToArray(), stringSplitOptions).ToList());
            else
                splitted.Add(text);
            Predicate<char> predicate;
            GeneralCharService generalCharService = new GeneralCharService();
            foreach (var genericChar in generic)
            {
                predicate = generalCharService.ReturnRightPredicate(genericChar);
                var splittedPart = new List<string>();
                for (var i = splitted.Count() - 1; i >= 0; i--)
                {
                    var part = splitted[i];
                    splittedPart.Clear();
                    var stringBuilder = new StringBuilder();
                    foreach (var item in part)
                        if (predicate.Invoke(item))
                        {
                            stringBuilder.Append(item);
                        }
                        else
                        {
                            if (stringBuilder.Length != 0)
                            {
                                splittedPart.Add(stringBuilder.ToString());
                                stringBuilder.Clear();
                            }
                        }
                    var splittedPartCount = splittedPart.Count();
                    if (splittedPartCount > 1)
                    {
                        splitted.RemoveAt(i);
                        for (var partIndex = splittedPartCount - 1; partIndex >= 0; partIndex--) splitted.Insert(i, splittedPart[partIndex]);
                    }
                    splitted.Add(stringBuilder.ToString());
                }
            }
            return splitted.ToList();
        }
        return text.Split(delimiters, stringSplitOptions).ToList();
    }
    /// <summary>
    ///     Return whether is whitespace or punctaction
    /// </summary>
    /// <param name="index">Index of the character to check in the text.</param>
    /// <param name="text">The text containing the character.</param>
    /// <param name="character">Output parameter for the character at the specified index.</param>
    /// <param name="isImmediatelyRemoving">Whether to immediately remove the character from text if it is special.</param>
    public static bool IsSpecialChar(int index, ref string text, ref char character, bool isImmediatelyRemoving = false)
    {
        character = text[index];
        return IsSpecialChar(character, ref text, index, isImmediatelyRemoving);
    }
    /// <summary>
    /// Checks if a character is a special character (whitespace or punctuation, excluding certain characters)
    /// </summary>
    /// <param name="character">The character to check.</param>
    /// <param name="text">The text containing the character (for optional removal).</param>
    /// <param name="index">Index of the character in text (for optional removal).</param>
    /// <param name="isImmediatelyRemoving">Whether to immediately remove the character from text if it is special.</param>
    /// <returns>True if the character is a special character, false otherwise.</returns>
    private static bool IsSpecialChar(char character, ref string text, int index = -1, bool isImmediatelyRemoving = false)
    {
        if (character == '(' || character == ')') return false;
        if (character == '\\' || character == '{' || character == '}') return false;
        if (character == '-') return true;
        if (char.IsWhiteSpace(character))
        {
            if (isImmediatelyRemoving && text != null) text = text.Remove(index, 1);
            return true;
        }
        if (char.IsPunctuation(character))
        {
            if (isImmediatelyRemoving && text != null) text = text.Remove(index, 1);
            return true;
        }
        return false;
    }
    /// <summary>
    /// Returns list of distinct Unicode character types found in text
    /// </summary>
    public static List<UnicodeChars> TypesOfUnicodeChars(string text)
    {
        var unicodeCharTypes = new List<UnicodeChars>();
        foreach (var item in text) unicodeCharTypes.Add(IsUnicodeChar(item));
        return unicodeCharTypes.Distinct().ToList();
    }
    /// <summary>
    /// Determines the Unicode character type of a character
    /// </summary>
    public static UnicodeChars IsUnicodeChar(char character)
    {
        if (char.IsControl(character))
            return UnicodeChars.Control;
        if (char.IsHighSurrogate(character))
            return UnicodeChars.HighSurrogate;
        if (char.IsLower(character))
            return UnicodeChars.Lower;
        if (char.IsLowSurrogate(character))
            return UnicodeChars.LowSurrogate;
        if (char.IsNumber(character))
            return UnicodeChars.Number;
        if (char.IsPunctuation(character))
            return UnicodeChars.Punctaction;
        if (char.IsSeparator(character))
            return UnicodeChars.Separator;
        if (char.IsSurrogate(character))
            return UnicodeChars.Surrogate;
        if (char.IsSymbol(character))
            return UnicodeChars.Symbol;
        if (char.IsUpper(character))
            return UnicodeChars.Upper;
        if (char.IsWhiteSpace(character))
            return UnicodeChars.WhiteSpace;
        if (IsSpecial(character))
            return UnicodeChars.Special;
        if (IsGeneric(character)) return UnicodeChars.Generic;
        //ThrowEx.NotImplementedCase(ch);
        // Still was throwing NotImplementedCase for 㣯 => Special. not all chars catch all ifs
        return UnicodeChars.Special;
    }
    /// <summary>
    /// Checks if a character matches the specified Unicode character type
    /// </summary>
    /// <param name="unicodeCharType">The Unicode character type to check against.</param>
    /// <param name="character">The character to check.</param>
    /// <returns>True if the character matches the specified type, false otherwise.</returns>
    public static bool IsUnicodeChar(UnicodeChars unicodeCharType, char character)
    {
        switch (unicodeCharType)
        {
            case UnicodeChars.Control:
                return char.IsControl(character);
            case UnicodeChars.HighSurrogate:
                return char.IsHighSurrogate(character);
            case UnicodeChars.Lower:
                return char.IsLower(character);
            case UnicodeChars.LowSurrogate:
                return char.IsLowSurrogate(character);
            case UnicodeChars.Number:
                return char.IsNumber(character);
            case UnicodeChars.Punctaction:
                return char.IsPunctuation(character);
            case UnicodeChars.Separator:
                return char.IsSeparator(character);
            case UnicodeChars.Surrogate:
                return char.IsSurrogate(character);
            case UnicodeChars.Symbol:
                return char.IsSymbol(character);
            case UnicodeChars.Upper:
                return char.IsUpper(character);
            case UnicodeChars.WhiteSpace:
                return char.IsWhiteSpace(character);
            case UnicodeChars.Special:
                return IsSpecial(character);
            case UnicodeChars.Generic:
                return IsGeneric(character);
            default:
                ThrowEx.NotImplementedCase(unicodeCharType.ToString());
                return false;
        }
    }
    /// <summary>
    /// Checks if a character is a special character
    /// </summary>
    public static bool IsSpecial(char character)
    {
        SpecialCharsService specialCharsService = new();
        var isContained = specialCharsService.SpecialChars.Contains(character);
        if (!isContained) isContained = specialCharsService.SpecialChars2.Contains(character);
        return isContained;
    }
    /// <summary>
    /// Returns only digit characters from text
    /// </summary>
    public static string OnlyDigits(string text)
    {
        return OnlyAccepted(text, char.IsDigit);
    }
    /// <summary>
    /// Checks if a character is a generic character
    /// </summary>
    public static bool IsGeneric(char character)
    {
        GeneralCharService generalCharService = new GeneralCharService();
        return generalCharService.GeneralChars.Contains(character);
    }
    /// <summary>
    /// Returns only characters that match the predicate
    /// </summary>
    public static string OnlyAccepted(string text, Func<char, bool> predicate, bool isNegating = false)
    {
        var stringBuilder = new StringBuilder();
        var result = false;
        foreach (var item in text)
        {
            result = predicate.Invoke(item);
            if (isNegating) result = !result;
            if (result) stringBuilder.Append(item);
        }
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Returns only characters that match any of the predicates
    /// </summary>
    public static string OnlyAccepted(string text, List<Func<char, bool>> predicates, bool isNegating = false)
    {
        var stringBuilder = new StringBuilder();
        foreach (var item in text)
            foreach (var predicate in predicates)
            {
                var accepted = predicate.Invoke(item);
                if (accepted || (!accepted && isNegating))
                {
                    stringBuilder.Append(item);
                    break;
                }
            }
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Finds a character constant from type that is not contained in text
    /// </summary>
    public static string? CharWhichIsNotContained(Type typeAllChars, string text)
    {
        var constantValues = typeAllChars.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
            .Select(x => x.GetRawConstantValue() as string)
            .Where(x => x != null)
            .ToList();
        foreach (var constantValue in constantValues)
            if (!text.Contains(constantValue!))
                return constantValue;
        return null;
    }
}