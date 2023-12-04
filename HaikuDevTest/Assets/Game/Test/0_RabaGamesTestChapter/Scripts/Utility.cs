using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility {

	public static T[] ShuffleArray<T>(T[] array, int seed)
	{
		System.Random rand = new System.Random(seed);

		for (int i = 0;i<array.Length-1 ;i++)
		{
			int randomIndex=rand.Next(i, array.Length);
			T tmp = array[randomIndex];
			array[randomIndex] = array[i];
			array[i] = tmp;
		}

		return array;
	}
}
