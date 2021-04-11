using System;
using System.Collections.Generic;
using System.Numerics;
using Utils.ScriptableObjects.Audio;

namespace Utils
{
    public static class RandomUtils
    {
        private static readonly Random _random = new Random();
        
        public static T Choice<T>(this T[] items)
        {
            if (items.Length == 0)
            {
                return default;
            }
            return items[_random.Next(0, items.Length)];
        }
		
        public static T ChoiceFrom<T>(params T[] items)
        {
            return items.Choice();
        }
        
        public static T Choice<T>(this IReadOnlyList<T> collection)
        {
            if (collection.Count == 0)
            {
                return default;
            }
            var idx = _random.Next(0, collection.Count);
            return collection[idx];
        }

        public static float RandomWithin(this Vector2 range)
        {
            return UnityEngine.Random.Range(range.X, range.Y);
        }

        public static float Random(this RangedFloat rangedFloat)
        {
            return UnityEngine.Random.Range(rangedFloat.minValue, rangedFloat.maxValue);
        }
    }
}