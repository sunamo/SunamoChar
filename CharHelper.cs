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
                    var sb = new StringBuilder();
                    foreach (var item in item2)
                        if (predicate.Invoke(item))
                        {
                            sb.Append(item);
                        }
                        else
                        {
                            if (sb.Length != 0)
                            {
                                splittedPart.Add(sb.ToString());
                                sb.Clear();
                            }
                        }
                    var splittedPartCount = splittedPart.Count();
                    if (splittedPartCount > 1)
                    {
                        splitted.RemoveAt(i);
                        for (var y = splittedPartCount - 1; y >= 0; y--) splitted.Insert(i, splittedPart[y]);
                    }
                    splitted.Add(sb.ToString());
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
    public static bool IsSpecialChar(int dx, ref string s, ref char ch, bool immediatelyRemove = false)
    {
        ch = s[dx];
        return IsSpecialChar(ch, ref s, dx, immediatelyRemove);
    }
    private static bool IsSpecialChar(char ch, ref string s, int dx = -1, bool immediatelyRemove = false)
    {
        if (ch == '(' || ch == ')') return false;
        if (ch == '\\' || ch == '{' || ch == '}') return false;
        if (ch == '-') return true;
        if (char.IsWhiteSpace(ch))
        {
            if (immediatelyRemove && s != null) s = s.Remove(dx, 1);
            return true;
        }
        if (char.IsPunctuation(ch))
        {
            if (immediatelyRemove && s != null) s = s.Remove(dx, 1);
            return true;
        }
        return false;
    }
    public static List<UnicodeChars> TypesOfUnicodeChars(string s)
    {
        var ch = new List<UnicodeChars>();
        foreach (var item in s) ch.Add(IsUnicodeChar(item));
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
    public static bool IsUnicodeChar(UnicodeChars generic, char c)
    {
        switch (generic)
        {
            case UnicodeChars.Control:
                return char.IsControl(c);
            case UnicodeChars.HighSurrogate:
                return char.IsHighSurrogate(c);
            case UnicodeChars.Lower:
                return char.IsLower(c);
            case UnicodeChars.LowSurrogate:
                return char.IsLowSurrogate(c);
            case UnicodeChars.Number:
                return char.IsNumber(c);
            case UnicodeChars.Punctaction:
                return char.IsPunctuation(c);
            case UnicodeChars.Separator:
                return char.IsSeparator(c);
            case UnicodeChars.Surrogate:
                return char.IsSurrogate(c);
            case UnicodeChars.Symbol:
                return char.IsSymbol(c);
            case UnicodeChars.Upper:
                return char.IsUpper(c);
            case UnicodeChars.WhiteSpace:
                return char.IsWhiteSpace(c);
            case UnicodeChars.Special:
                return IsSpecial(c);
            case UnicodeChars.Generic:
                return IsGeneric(c);
            default:
                ThrowEx.NotImplementedCase(generic.ToString());
                return false;
        }
    }
    public static bool IsSpecial(char c)
    {
        SpecialCharsService specialChars = new();
        var v = specialChars.specialChars.Contains(c);
        if (!v) v = specialChars.specialChars2.Contains(c);
        return v;
    }
    public static string OnlyDigits(string v)
    {
        return OnlyAccepted(v, char.IsDigit);
    }
    public static bool IsGeneric(char c)
    {
        GeneralCharService generalChar = new GeneralCharService();
        return generalChar.generalChars.Contains(c);
    }
    public static string OnlyAccepted(string v, Func<char, bool> isDigit, bool not = false)
    {
        var sb = new StringBuilder();
        var result = false;
        foreach (var item in v)
        {
            result = isDigit.Invoke(item);
            if (not) result = !result;
            if (result) sb.Append(item);
        }
        return sb.ToString();
    }
    public static string OnlyAccepted(string v, List<Func<char, bool>> isDigit, bool not = false)
    {
        var sb = new StringBuilder();
        //bool result = true;
        foreach (var item in v)
            foreach (var item2 in isDigit)
                if (item2.Invoke(item))
                {
                    sb.Append(item);
                    break;
                }
        return sb.ToString();
    }
    public static string CharWhichIsNotContained(Type typeAllChars, string item)
    {
        var v = typeAllChars.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
            .Select(x => (string)x.GetRawConstantValue())
            .ToList();
        foreach (var item2 in v)
            if (!item.Contains(item2))
                return item2;
        return null;
    }
}