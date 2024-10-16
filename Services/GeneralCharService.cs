namespace SunamoChar.Services;
using System;
using System.Collections.Generic;

internal class GeneralCharService
{
    static char notNumber = (char)9;

    internal readonly List<char> generalChars = new List<char>(new[] { notNumber });

    internal Predicate<char> ReturnRightPredicate(char genericChar)
    {
        Predicate<char> predicate = null;
        if (genericChar == notNumber)
            predicate = char.IsNumber;
        else
            ThrowEx.NotImplementedCase(generalChars);
        return predicate;
    }
}