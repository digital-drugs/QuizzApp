using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Shuffle
{
    private static readonly System.Random rng = new();

    public static void ShuffleList<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }
}
