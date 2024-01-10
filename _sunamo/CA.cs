namespace SunamoChar._sunamo;

//internal delegate bool IsEqualToAnyElementDelegate<T>(T t, List<Char>);

internal class CA
{
    //internal static IsEqualToAnyElementDelegate<T> IsEqualToAnyElement;

    /// <summary>
    /// ContainsAnyFromElement - Contains string elements of list. Return List<string>
    ///IsEqualToAnyElement - same as ContainsElement, only have switched elements. return bool
    ///IsEqualToAllElement - takes two generic list. return bool
    ///ContainsElement - at least one element must be equaled. generic. bool
    ///IsSomethingTheSame - only for string. as List.Contains. bool
    ///IsAllTheSame() - takes element and list.generic. bool
    ///IndexesWithValue() - element and list.generic. return list<int>
    ///ReturnWhichContainsIndexes() - takes two list or element and list. return List<int>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="p"></param>
    /// <param name="list"></param>
    internal static bool IsEqualToAnyElement<T>(T p, IList<T> list)
    {
        foreach (T item in list)
        {
            if (EqualityComparer<T>.Default.Equals(p, item))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// CA.ContainsAnyFromElement - Contains string elements of list. Return List<string>
    /// CA.IsEqualToAnyElement - same as ContainsElement, only have switched elements. return bool
    /// CA.IsEqualToAllElement - takes two generic list. return bool
    /// CA.ContainsElement - at least one element must be equaled. generic. bool
    /// CA.IsSomethingTheSame - only for string. as List.Contains. bool
    /// CA.IsAllTheSame() - takes element and list.generic. bool
    /// CA.IndexesWithValue() - element and list.generic. return list<int>
    /// CA.ReturnWhichContainsIndexes() - takes two list or element and list. return List<int>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="p"></param>
    /// <param name="prvky"></param>
    /// <returns></returns>
    internal static bool IsEqualToAnyElement<T>(T p, params T[] prvky)
    {
        return IsEqualToAnyElement(p, prvky.ToList());
    }
}
