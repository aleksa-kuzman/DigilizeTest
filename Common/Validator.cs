using System;
using System.Reflection;
using DigilizeTest.Common.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DigilizeTest.Common;

public static class Validator
{
    public static List<T> ValidateAndFilter<T>(this List<T> items) where T: EntityBase
    {
       return  items.Where(m=>IsValid(m)).ToList();
    }

    private static bool IsValid<T> (T obj) where T: EntityBase
    {

        var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach(var prop in properties){

            var value  = prop.GetValue(obj);


            if (value == null || (value is string str && string.IsNullOrEmpty(str)) ||  (value is Guid guid && guid == Guid.Empty) )
            {
                return false; // Property is empty
            }
        }
        return true;
    }
}
