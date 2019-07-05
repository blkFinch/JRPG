using System.Collections.Generic;

//Allows a safe check if a list is empty
public static class IEnumerableExtension
{       
    public static IEnumerable<T> Safe<T>(this IEnumerable<T> source)
    {
        if (source == null)
        {
            yield break;
        }

        foreach (var item in source)
        {
            yield return item;
        }
    }
}