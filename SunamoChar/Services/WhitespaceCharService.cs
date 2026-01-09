// variables names: ok
namespace SunamoChar.Services;

/// <summary>
/// Service for handling whitespace characters
/// </summary>
public class WhitespaceCharService
{
    /// <summary>
    /// List of whitespace characters
    /// </summary>
    public List<char>? WhiteSpaceChars { get; set; }
    /// <summary>
    /// Whitespace character key codes
    /// </summary>
    public List<int> WhiteSpacesCodes { get; } = new(new[]
{
9, 10, 11, 12, 13, 32, 133, 160, 5760, 6158, 8192, 8193, 8194, 8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202,
8232, 8233, 8239, 8287, 12288
});
    /// <summary>
    /// Converts whitespace key codes to characters
    /// </summary>
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