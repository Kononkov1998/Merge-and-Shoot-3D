using UnityEngine;

namespace Utilities.Extensions
{
    public static class ArrayExtensions
    {
        public static T RandomElement<T>(this T[] array)
        {
            int randomIndex = Random.Range(0, array.Length);
            return array[randomIndex];
        }
    }
}