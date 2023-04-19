using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine;

namespace HKAIFramework
{
    public static class RandomUtil
    {
        /// <summary>
        /// Keeps track of the number of consecutively returned items from WeightedRandom.
        /// </summary>
        private static Dictionary<object, int> _trackedItems = new();
        
        /// <summary>
        /// Provide a random item based on a provide list of tuples each containing
        /// an item's weight, or chance that it will be returned, and its maximum number
        /// of times that it can be consecutively returned.
        /// </summary>
        /// <typeparam name="T">Type of the item</typeparam>
        /// <param name="list">List containing items and their weights and max number of repeats</param>
        /// <returns></returns>
        public static T WeightedRandom<T>(Tuple<T, float, int>[] list)
        {
            var items = new List<T>();
            foreach ((T item, float weight, int maxRepeats) in list)
            {
                if (!_trackedItems.ContainsKey(item)) _trackedItems.Add(item, 0);
                for (int i = 0; i < (int)(weight * 100); i++)
                {
                    if (_trackedItems[item] < maxRepeats) items.Add(item);
                }
            }

            int itemIndex = Random.Range(0, items.Count - 1);

            T chosenItem = items[itemIndex];
            foreach (T item in _trackedItems.Select(x => (T)x.Key).ToList())
            {
                _trackedItems[item] = item.Equals(chosenItem) ? _trackedItems[item] + 1 : 0;
            }

            return chosenItem;
        }
    }
}