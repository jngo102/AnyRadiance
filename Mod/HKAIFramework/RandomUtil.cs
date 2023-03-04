using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine;

namespace HKAIFramework
{
    public static class RandomUtil
    {
        private static Dictionary<object, int> _trackedItems = new();
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
            
            Debug.Log("item count: " + items.Count + ", item index: " + itemIndex);

            T chosenItem = items[itemIndex];
            foreach (T item in _trackedItems.Select(x => (T)x.Key).ToList())
            {
                _trackedItems[item] = item.Equals(chosenItem) ? _trackedItems[item] + 1 : 0;
            }

            return chosenItem;
        }
    }
}