namespace SunamoChar.Services;
using System;
using System.Collections.Generic;

public class GeneralCharService
{
    static char notNumber = (char)9;

    public readonly List<char> generalChars = new List<char>(new[] { notNumber });

    public Predicate<char> ReturnRightPredicate(char genericChar)
    {
        Predicate<char> predicate = null;
        if (genericChar == notNumber)
            predicate = char.IsNumber;
        else
            ThrowEx.NotImplementedCase(generalChars);
        return predicate;
    }
}