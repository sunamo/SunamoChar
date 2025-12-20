// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoChar.Services;

public class WhitespaceCharService
{
    public List<char> WhiteSpaceChars;
    public readonly List<int> WhiteSpacesCodes = new(new[]
{
9, 10, 11, 12, 13, 32, 133, 160, 5760, 6158, 8192, 8193, 8194, 8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202,
8232, 8233, 8239, 8287, 12288
});
    public void ConvertWhiteSpaceCodesToChars()
    {
        WhiteSpaceChars = new List<char>(WhiteSpacesCodes.Count);
        foreach (var item in WhiteSpacesCodes)
        {
            var text = char.ConvertFromUtf32(item);
            var character = Convert.ToChar(text);
            WhiteSpaceChars.Add(character);
        }
    }
}