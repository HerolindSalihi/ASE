using System;
using System.Collections.Generic;
using System.Linq;

public static class NullHandlingExtensions
{
    public static T NonNull<T>(this T? value) where T : class
    {
        if (value == null)
        {
            throw new NullReferenceException($"The value of type {typeof(T)} is null.");
        }

        return value;
    }
}