namespace SunamoChar;

public class CharHelper
{
    public static List<string> SplitSpecial(string text, params char[] delimiters)
    {
        return SplitSpecial(StringSplitOptions.RemoveEmptyEntries, text, delimiters);
    }

    public static List<string> SplitSpecialNone(string text, params char[] delimiters)
    {
        return SplitSpecial(StringSplitOptions.None, text, delimiters);
    }

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
            GeneralCharService generalCharService = new();
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

    public static bool IsSpecialChar(int index, ref string text, ref char character, bool isImmediatelyRemoving = false)
    {
        character = text[index];
        return IsSpecialChar(character, ref text, index, isImmediatelyRemoving);
    }

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

    public static List<UnicodeChars> TypesOfUnicodeChars(string text)
    {
        var unicodeCharTypes = new List<UnicodeChars>();
        foreach (var item in text) unicodeCharTypes.Add(IsUnicodeChar(item));
        return unicodeCharTypes.Distinct().ToList();
    }

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

    public static bool IsSpecial(char character)
    {
        SpecialCharsService specialCharsService = new();
        var isContained = specialCharsService.SpecialChars.Contains(character);
        if (!isContained) isContained = specialCharsService.SpecialChars2.Contains(character);
        return isContained;
    }

    public static string OnlyDigits(string text)
    {
        return OnlyAccepted(text, char.IsDigit);
    }

    public static bool IsGeneric(char character)
    {
        GeneralCharService generalCharService = new();
        return generalCharService.GeneralChars.Contains(character);
    }

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
