// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoChar;

public class CharHelper
{
    public static List<string> SplitSpecial(string text, params char[] deli)
    {
        return SplitSpecial(StringSplitOptions.RemoveEmptyEntries, text, deli);
    }
    public static List<string> SplitSpecialNone(string text, params char[] deli)
    {
        return SplitSpecial(StringSplitOptions.None, text, deli);
    }
    /// <summary>
    ///     Use with general letters
    /// </summary>
    /// <param name="stringSplitOptions"></param>
    /// <param name="text"></param>
    /// <param name="deli"></param>
    private static List<string> SplitSpecial(StringSplitOptions stringSplitOptions, string text, params char[] deli)
    {
        if (deli == null || deli.Count() == 0) throw new Exception("NoDelimiterDetermined");
        if (deli.Length == 1 && !IsUnicodeChar(UnicodeChars.Generic, deli[0]))
            return text.Split(deli, stringSplitOptions).ToList();
        var normal = new List<char>();
        var generic = new List<char>();
        foreach (var item in deli)
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
            GeneralCharService generalChar = new GeneralCharService();
            foreach (var genericChar in generic)
            {
                predicate = generalChar.ReturnRightPredicate(genericChar);
                var splittedPart = new List<string>();
                for (var i = splitted.Count() - 1; i >= 0; i--)
                {
                    var item2 = splitted[i];
                    splittedPart.Clear();
                    var stringBuilder = new StringBuilder();
                    foreach (var item in item2)
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
                        for (var yValue = splittedPartCount - 1; yValue >= 0; yValue--) splitted.Insert(i, splittedPart[yValue]);
                    }
                    splitted.Add(stringBuilder.ToString());
                }
            }
            return splitted.ToList();
        }
        return text.Split(deli, stringSplitOptions).ToList();
    }
    /// <summary>
    ///     Return whether is whitespace or punctaction
    /// </summary>
    /// <param name="dx"></param>
    /// <param name="s"></param>
    /// <param name="ch"></param>
    public static bool IsSpecialChar(int dx, ref string text, ref char ch, bool immediatelyRemove = false)
    {
        ch = text[dx];
        return IsSpecialChar(ch, ref text, dx, immediatelyRemove);
    }
    private static bool IsSpecialChar(char ch, ref string text, int dx = -1, bool immediatelyRemove = false)
    {
        if (ch == '(' || ch == ')') return false;
        if (ch == '\\' || ch == '{' || ch == '}') return false;
        if (ch == '-') return true;
        if (char.IsWhiteSpace(ch))
        {
            if (immediatelyRemove && text != null) text = text.Remove(dx, 1);
            return true;
        }
        if (char.IsPunctuation(ch))
        {
            if (immediatelyRemove && text != null) text = text.Remove(dx, 1);
            return true;
        }
        return false;
    }
    public static List<UnicodeChars> TypesOfUnicodeChars(string text)
    {
        var ch = new List<UnicodeChars>();
        foreach (var item in text) ch.Add(IsUnicodeChar(item));
        return ch.Distinct().ToList();
    }
    public static UnicodeChars IsUnicodeChar(char ch)
    {
        if (char.IsControl(ch))
            return UnicodeChars.Control;
        if (char.IsHighSurrogate(ch))
            return UnicodeChars.HighSurrogate;
        if (char.IsLower(ch))
            return UnicodeChars.Lower;
        if (char.IsLowSurrogate(ch))
            return UnicodeChars.LowSurrogate;
        if (char.IsNumber(ch))
            return UnicodeChars.Number;
        if (char.IsPunctuation(ch))
            return UnicodeChars.Punctaction;
        if (char.IsSeparator(ch))
            return UnicodeChars.Separator;
        if (char.IsSurrogate(ch))
            return UnicodeChars.Surrogate;
        if (char.IsSymbol(ch))
            return UnicodeChars.Symbol;
        if (char.IsUpper(ch))
            return UnicodeChars.Upper;
        if (char.IsWhiteSpace(ch))
            return UnicodeChars.WhiteSpace;
        if (IsSpecial(ch))
            return UnicodeChars.Special;
        if (IsGeneric(ch)) return UnicodeChars.Generic;
        //ThrowEx.NotImplementedCase(ch);
        // Still was throwing NotImplementedCase for 㣯 => Special. not all chars catch all ifs
        return UnicodeChars.Special;
    }
    public static bool IsUnicodeChar(UnicodeChars generic, char character)
    {
        switch (generic)
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
                ThrowEx.NotImplementedCase(generic.ToString());
                return false;
        }
    }
    public static bool IsSpecial(char character)
    {
        SpecialCharsService specialChars = new();
        var value = specialChars.specialChars.Contains(character);
        if (!value) value = specialChars.specialChars2.Contains(character);
        return value;
    }
    public static string OnlyDigits(string value)
    {
        return OnlyAccepted(value, char.IsDigit);
    }
    public static bool IsGeneric(char character)
    {
        GeneralCharService generalChar = new GeneralCharService();
        return generalChar.generalChars.Contains(character);
    }
    public static string OnlyAccepted(string value, Func<char, bool> isDigit, bool not = false)
    {
        var stringBuilder = new StringBuilder();
        var result = false;
        foreach (var item in value)
        {
            result = isDigit.Invoke(item);
            if (not) result = !result;
            if (result) stringBuilder.Append(item);
        }
        return stringBuilder.ToString();
    }
    public static string OnlyAccepted(string value, List<Func<char, bool>> isDigit, bool not = false)
    {
        var stringBuilder = new StringBuilder();
        //bool result = true;
        foreach (var item in value)
            foreach (var item2 in isDigit)
            {
                var accepted = item2.Invoke(item);
                if (accepted || (!accepted && not))
                {
                    stringBuilder.Append(item);
                    break;
                }
            }
        return stringBuilder.ToString();
    }
    public static string CharWhichIsNotContained(Type typeAllChars, string item)
    {
        var value = typeAllChars.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
            .Select(x => (string)x.GetRawConstantValue())
            .ToList();
        foreach (var item2 in value)
            if (!item.Contains(item2))
                return item2;
        return null;
    }
}