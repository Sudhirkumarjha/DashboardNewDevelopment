using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

public static class SortHelper
{
    public static IEnumerable<T> ApplySorting<T>(this IEnumerable<T> source, string sortBy, string sortOrder)
    {
        if (string.IsNullOrEmpty(sortBy))
            return source;

        var prop = typeof(T).GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (prop == null)
            return source;

        if (sortOrder.ToLower() == "desc")
        {
            return source.OrderByDescending(x => prop.GetValue(x, null));
        }
        else
        {
            return source.OrderBy(x => prop.GetValue(x, null));
        }
    }
}