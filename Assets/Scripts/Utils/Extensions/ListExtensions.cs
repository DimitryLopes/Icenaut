using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static T GetRandomItem<T>(this IList<T> list)
    {
        if (list.Count > 0)
        {
            return list[Random.Range(0, list.Count)];
        }
        else
        {
            throw new System.IndexOutOfRangeException("The list is empty!");
        }
    }
}