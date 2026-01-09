// variables names: ok
namespace SunamoChar._sunamo.SunamoExceptions;

// © www.sunamo.cz. All Rights Reserved.
internal sealed partial class Exceptions
{
    #region Other
    /// <summary>
    /// Checks and formats the 'before' string to be used as a prefix in exception messages.
    /// </summary>
    /// <param name="before">The prefix string to check and format.</param>
    /// <returns>Empty string if before is null or whitespace, otherwise before followed by ": ".</returns>
    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }

    /// <summary>
    /// Gets information about the place where an exception occurred from the stack trace.
    /// </summary>
    /// <param name="isFillFirstTwo">Whether to fill the first two items (type and method name) from the stack trace.</param>
    /// <returns>A tuple containing type name, method name, and formatted stack trace string.</returns>
    internal static Tuple<string, string, string> PlaceOfException(
bool isFillFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceString = stackTrace.ToString();
        var lines = stackTraceString.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        string type = string.Empty;
        string methodName = string.Empty;
        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            if (isFillFirstTwo)
                if (!line.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(line, out type, out methodName);
                    isFillFirstTwo = false;
                }
            if (line.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(type, methodName, string.Join(Environment.NewLine, lines));
    }
    /// <summary>
    /// Extracts type name and method name from a stack trace line.
    /// </summary>
    /// <param name="line">A single line from the stack trace.</param>
    /// <param name="type">Output parameter for the extracted type name.</param>
    /// <param name="methodName">Output parameter for the extracted method name.</param>
    internal static void TypeAndMethodName(string line, out string type, out string methodName)
    {
        var trimmedLine = line.Split("at ")[1].Trim();
        var methodPath = trimmedLine.Split("(")[0];
        var parts = methodPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = parts[^1];
        parts.RemoveAt(parts.Count - 1);
        type = string.Join(".", parts);
    }
    /// <summary>
    /// Gets the name of the calling method from the stack trace.
    /// </summary>
    /// <param name="frameIndex">The stack frame index to examine (default is 1).</param>
    /// <returns>The name of the calling method, or an error message if it cannot be retrieved.</returns>
    internal static string CallingMethod(int frameIndex = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(frameIndex)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }
    #endregion
    /// <summary>
    /// Creates a "not implemented case" exception message with contextual information.
    /// </summary>
    /// <param name="before">Prefix text to prepend to the exception message.</param>
    /// <param name="notImplementedName">The object or type that is not implemented.</param>
    /// <returns>A formatted exception message indicating a not implemented case.</returns>
    internal static string? NotImplementedCase(string before, object notImplementedName)
    {
        var formattedInfo = string.Empty;
        if (notImplementedName != null)
        {
            formattedInfo = " for ";
            if (notImplementedName.GetType() == typeof(Type))
                formattedInfo += ((Type)notImplementedName).FullName;
            else
                formattedInfo += notImplementedName.ToString();
        }
        return CheckBefore(before) + "Not implemented case" + formattedInfo + " . internal program error. Please contact developer" +
        ".";
    }
}