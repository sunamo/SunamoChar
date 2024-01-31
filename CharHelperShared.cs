namespace SunamoChar;

public partial class CharHelper
{
    public static string CharWhichIsNotContained(string item)
    {
        var v = typeof(AllStrings).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(String))
            .Select(x => (String)x.GetRawConstantValue())
            .ToList();
        foreach (var item2 in v)
        {
            if (!item.Contains(item2))
            {
                return item2;
            }
        }
        return null;
    }
}
