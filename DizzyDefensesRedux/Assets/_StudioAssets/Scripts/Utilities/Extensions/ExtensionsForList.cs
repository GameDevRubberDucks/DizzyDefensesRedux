/*
 * ExtensionsForList.cs
 * 
 * Description:
 * - Adds extensions to the basic List class
 * 
 * Author(s): 
 * - Dan
*/

using System.Collections.Generic;

using UnityEngine;

namespace RubberDucks.Utilities.Extensions
{
    public static class ExtensionsForList
    {
        public static T GetRandomElement<T>(this List<T> list)
        {
            int randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }

        public static T GetLastElement<T>(this List<T> list)
        {
            int lastIndex = list.Count - 1;
            return list[lastIndex];
        }

        public static void AddFromEnd<T>(this List<T> list, T element, int offsetFromEnd)
        {
            list.Insert(list.Count - offsetFromEnd, element);
        }

        // Found here: https://stackoverflow.com/questions/28833373/javascript-splice-in-c-sharp
        // Answer from: Selman Genç
        public static List<T> Splice<T>(this List<T> source, int index, int count)
        {
            var items = source.GetRange(index, count);
            source.RemoveRange(index, count);
            return items;
        }
		
		// Found here: https://stackoverflow.com/questions/273313/randomize-a-listt
        // Answer from: grenade
        public static void Shuffle<T>(this List<T> list)
        {
            System.Random rng = new System.Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static List<T> ShuffleAndCopy<T>(this List<T> list)
        {
            List<T> newList = new List<T>(list);
            newList.Shuffle();

            return newList;
        }
		
		 // A derangement is a permutation where NONE of the elements are in their original places
        // Ex: ABCD -> BCAD. BCAD is NOT a derangement since D is in its original spot
        // TODO: Write a MUCH better algorithm than this
        public static List<T> GetDerangement<T>(this List<T> list)
        {
			// If the list has only one element, just return the list as is otherwise we will hit an infinite loop!
            if (list.Count == 1)
            {
                return list;
            }
			
            List<T> derangement = null;
            bool foundDerangement = false;
            int numCycles = 0;

            while(!foundDerangement)
            {
                derangement = list.ShuffleAndCopy();

                for (int i = 0; i < derangement.Count; ++i)
                {
                    if (derangement[i].Equals(list[i]))
                    {
                        break;
                    }

                    // If we got here, it means we have reached the last element AND that element is correct
                    // In that case, we found a derangement properly
                    if (i == derangement.Count - 1)
                    {
                        foundDerangement = true;
                    }
                }

                numCycles++;
            }

            return derangement;
        }
    }
}