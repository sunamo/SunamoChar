namespace SunamoChar._sunamo;
internal class SHSE
{
    public static string TrimEnd(string name, string ext)
    {
        while (name.EndsWith(ext)) return name.Substring(0, name.Length - ext.Length);

        return name;
    }
    internal static List<string> SplitChar(string v1, params char[] v2)
    {
        return v1.Split(v2).ToList();
    }

    public static List<string> Split(string s, params string[] dot)
    {
        return s.Split(dot, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public static string NullToStringOrDefault(object n)
    {
        //return NullToStringOrDefault(n, null);
        return n == null ? " " + Consts.nulled : AllStrings.space + n;
    }
    public static string JoinNL(List<string> l)
    {
        StringBuilder sb = new();
        foreach (var item in l) sb.AppendLine(item);
        var r = string.Empty;
        r = sb.ToString();
        return r;
    }
}
