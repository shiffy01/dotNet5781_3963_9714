﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using System.Reflection;
namespace DL
{
   public static class Cloning
    {

        internal static T Clone<T>(this T original) where T : new()
        {
            //public static T Clone<T>(this T original)
            // {

            T result = (T)Activator.CreateInstance(typeof(T));
            foreach (PropertyInfo item in original.GetType().GetProperties())
            {
                item.SetValue(result, item.GetValue(original, null));
            }
            return result;
            // }
        }
    }
    
}