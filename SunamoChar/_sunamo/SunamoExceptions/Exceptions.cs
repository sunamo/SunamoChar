namespace SunamoChar._sunamo.SunamoExceptions;

// © www.sunamo.cz. All Rights Reserved.
internal sealed partial class Exceptions
{
    #region Other
    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }

    internal static Tuple<string, string, string> PlaceOfException(
bool fillAlsoFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceString = stackTrace.ToString();
        var lines = stackTraceString.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        var i = 0;
        string type = string.Empty;
        string methodName = string.Empty;
        for (; i < lines.Count; i++)
        {
            var line = lines[i];
            if (fillAlsoFirstTwo)
                if (!line.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(line, out type, out methodName);
                    fillAlsoFirstTwo = false;
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
    internal static void TypeAndMethodName(string line, out string type, out string methodName)
    {
        var trimmedLine = line.Split("at ")[1].Trim();
        var methodPath = trimmedLine.Split("(")[0];
        var parts = methodPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = parts[^1];
        parts.RemoveAt(parts.Count - 1);
        type = string.Join(".", parts);
    }
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

    #region IsNullOrWhitespace
    readonly static StringBuilder additionalInfoInner = new();
    readonly static StringBuilder additionalInfo = new();
    #endregion
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