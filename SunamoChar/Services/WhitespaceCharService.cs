namespace SunamoChar.Services;

/// <summary>
/// Represents the whitespace char service type.
/// </summary>
public class WhitespaceCharService
{
    /// <summary>
    /// Gets or sets the white space chars.
    /// </summary>
    public List<char>? WhiteSpaceChars { get; set; }
    /// <summary>
    /// Gets the white spaces codes.
    /// </summary>
    public List<int> WhiteSpacesCodes { get; } = new(new[]
    {
        9, 10, 11, 12, 13, 32, 133, 160, 5760, 6158, 8192, 8193, 8194, 8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202,
        8232, 8233, 8239, 8287, 12288
    });
    /// <summary>
    /// Convert white space codes to chars.
    /// </summary>
    /// <param name="}">The }.</param>
    /// <returns>The convert white space codes to chars result.</returns>
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
