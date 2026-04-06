namespace SunamoChar;

/// <summary>
/// Helper class for character operations including Unicode character detection and string manipulation
/// </summary>
public class CharHelper
{
    /// <summary>
    /// Splits text by special delimiters, removing empty entries.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">Characters to use as delimiters.</param>
    /// <returns>List of string parts after splitting.</returns>
    public static List<string> SplitSpecial(string text, params char[] delimiters)
    {
        return SplitSpecial(StringSplitOptions.RemoveEmptyEntries, text, delimiters);
    }
    /// <summary>
    /// Splits text by special delimiters, keeping empty entries.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">Characters to use as delimiters.</param>
    /// <returns>List of string parts after splitting, including empty entries.</returns>
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
        if (delimiters == null || delimiters.Length == 0) throw new Exception("NoDelimiterDetermined");
        if (delimiters.Length == 1 && !IsUnicodeChar(UnicodeChars.Generic, delimiters[0]))
            return text.Split(delimiters, stringSplitOptions).ToList();
        var normalCharacters = new List<char>();
        var genericCharacters = new List<char>();
        foreach (var item in delimiters)
            if (IsUnicodeChar(UnicodeChars.Generic, item))
                genericCharacters.Add(item);
            else
                normalCharacters.Add(item);
        if (genericCharacters.Count > 0)
        {
            var splitParts = new List<string>();
            if (normalCharacters.Count > 0)
                splitParts.AddRange(text.Split(normalCharacters.ToArray(), stringSplitOptions).ToList());
            else
                splitParts.Add(text);
            Predicate<char> predicate;
            GeneralCharService generalCharService = new GeneralCharService();
            foreach (var genericChar in genericCharacters)
            {
                predicate = generalCharService.ReturnRightPredicate(genericChar);
                var currentParts = new List<string>();
                for (var i = splitParts.Count - 1; i >= 0; i--)
                {
                    var part = splitParts[i];
                    currentParts.Clear();
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
                                currentParts.Add(stringBuilder.ToString());
                                stringBuilder.Clear();
                            }
                        }
                    var currentPartsCount = currentParts.Count;
                    if (currentPartsCount > 1)
                    {
                        splitParts.RemoveAt(i);
                        for (var partIndex = currentPartsCount - 1; partIndex >= 0; partIndex--) splitParts.Insert(i, currentParts[partIndex]);
                    }
                    splitParts.Add(stringBuilder.ToString());
                }
            }
            return splitParts.ToList();
        }
        return text.Split(delimiters, stringSplitOptions).ToList();
    }
    /// <summary>
    /// Returns whether the character is whitespace or punctuation.
    /// </summary>
    /// <param name="index">Index of the character to check in the text.</param>
    /// <param name="text">The text containing the character.</param>
    /// <param name="character">Output parameter for the character at the specified index.</param>
    /// <param name="isImmediatelyRemoving">Whether to immediately remove the character from text if it is special.</param>
    /// <returns>True if the character at the specified index is a special character, false otherwise.</returns>
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
    /// Returns list of distinct Unicode character types found in text.
    /// </summary>
    /// <param name="text">The text to analyze for Unicode character types.</param>
    /// <returns>List of distinct Unicode character type categories found in the text.</returns>
    public static List<UnicodeChars> TypesOfUnicodeChars(string text)
    {
        var unicodeCharTypes = new List<UnicodeChars>();
        foreach (var item in text) unicodeCharTypes.Add(IsUnicodeChar(item));
        return unicodeCharTypes.Distinct().ToList();
    }
    /// <summary>
    /// Determines the Unicode character type of a character.
    /// </summary>
    /// <param name="character">The character to classify.</param>
    /// <returns>The Unicode character type category of the specified character.</returns>
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
            return UnicodeChars.Punctuation;
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
            case UnicodeChars.Punctuation:
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
    /// Checks if a character is a special character.
    /// </summary>
    /// <param name="character">The character to check.</param>
    /// <returns>True if the character is a special character, false otherwise.</returns>
    public static bool IsSpecial(char character)
    {
        SpecialCharsService specialCharsService = new();
        var isContained = specialCharsService.SpecialChars.Contains(character);
        if (!isContained) isContained = specialCharsService.SpecialChars2.Contains(character);
        return isContained;
    }
    /// <summary>
    /// Returns only digit characters from text.
    /// </summary>
    /// <param name="text">The text to extract digits from.</param>
    /// <returns>String containing only the digit characters from the input text.</returns>
    public static string OnlyDigits(string text)
    {
        return OnlyAccepted(text, char.IsDigit);
    }
    /// <summary>
    /// Checks if a character is a generic character.
    /// </summary>
    /// <param name="character">The character to check.</param>
    /// <returns>True if the character is a generic character, false otherwise.</returns>
    public static bool IsGeneric(char character)
    {
        GeneralCharService generalCharService = new GeneralCharService();
        return generalCharService.GeneralChars.Contains(character);
    }
    /// <summary>
    /// Returns only characters that match the predicate.
    /// </summary>
    /// <param name="text">The text to filter.</param>
    /// <param name="predicate">Function to test each character.</param>
    /// <param name="isNegating">Whether to negate the predicate result.</param>
    /// <returns>String containing only the characters that match the predicate.</returns>
    public static string OnlyAccepted(string text, Func<char, bool> predicate, bool isNegating = false)
    {
        var stringBuilder = new StringBuilder();
        var isMatching = false;
        foreach (var item in text)
        {
            isMatching = predicate.Invoke(item);
            if (isNegating) isMatching = !isMatching;
            if (isMatching) stringBuilder.Append(item);
        }
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Returns only characters that match any of the predicates.
    /// </summary>
    /// <param name="text">The text to filter.</param>
    /// <param name="predicates">List of functions to test each character.</param>
    /// <param name="isNegating">Whether to negate the predicate results.</param>
    /// <returns>String containing only the characters that match any of the predicates.</returns>
    public static string OnlyAccepted(string text, List<Func<char, bool>> predicates, bool isNegating = false)
    {
        var stringBuilder = new StringBuilder();
        foreach (var item in text)
            foreach (var predicate in predicates)
            {
                var isAccepted = predicate.Invoke(item);
                if (isAccepted || (!isAccepted && isNegating))
                {
                    stringBuilder.Append(item);
                    break;
                }
            }
        return stringBuilder.ToString();
    }
    /// <summary>
    /// Finds a character constant from type that is not contained in text.
    /// </summary>
    /// <param name="typeAllChars">The type containing character constants to search.</param>
    /// <param name="text">The text to check for character containment.</param>
    /// <returns>The first character constant not found in the text, or null if all are contained.</returns>
    public static string? CharWhichIsNotContained(Type typeAllChars, string text)
    {
        var constantValues = typeAllChars.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fieldInfo => fieldInfo.IsLiteral && !fieldInfo.IsInitOnly && fieldInfo.FieldType == typeof(string))
            .Select(fieldInfo => fieldInfo.GetRawConstantValue() as string)
            .Where(constantValue => constantValue != null)
            .ToList();
        foreach (var constantValue in constantValues)
            if (!text.Contains(constantValue!))
                return constantValue;
        return null;
    }
}