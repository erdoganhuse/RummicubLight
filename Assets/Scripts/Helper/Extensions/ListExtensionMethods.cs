using System;
using System.Collections.Generic;
using System.Linq;

namespace Helper.Extensions
{
    public static class ListExtensionMethods
    {
        private static readonly Random Random = new Random();

        public static T RemoveFirst<T>(this IList<T> source)
        {
            T removedItem = source.First();
            source.RemoveAt(0);
            return removedItem;
        }

        public static T RemoveLast<T>(this IList<T> source)
        {
            T removedItem = source.Last();
            source.RemoveAt(source.Count - 1);
            return removedItem;
        }
        
        public static T RemoveRandom<T>(this IList<T> source)
        {
            T removedItem = source[UnityEngine.Random.Range(0, source.Count())];
            source.Remove(removedItem);
            return removedItem;
        }
        
        public static void Shuffle<T>(this IList<T> source)
        {
            int count = source.Count;  
            while (count > 1) {  
                count--;  
                int randomIndex = Random.Next(count + 1);  
                T value = source[randomIndex];  
                source[randomIndex] = source[count];  
                source[count] = value;  
            }  
        }
    }
}