using System;
using System.Collections.Generic;
using System.Linq;

namespace MergeClaw3D.Scripts.Extensions
{
    public static class CustomExtensions
    {
        public static List<T> Shuffle<T>(this List<T> collection)
        {
            Random rnd = new();
            return collection.OrderBy(x => rnd.Next()).ToList();
        }
    }
}
