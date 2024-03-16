using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.victorafael.randomList
{
	public abstract class RandomListData<T> : ScriptableObject
	{
		[System.Serializable]
		public class RandomListDataItem
		{
			public T item;
			public float startChance = 5;
			public float scoreMultiplier = 0.2f;
			public float minChance = 0;
			public float maxChance = 100;

			public float GetChance(float score)
			{
				return Mathf.Clamp(
					startChance + score * scoreMultiplier,
					minChance, maxChance
				);
			}
		}
		public List<RandomListDataItem> items = new List<RandomListDataItem>();

		public float[] GetChances(float score, out float total)
		{
			float[] chances = new float[items.Count];
			total = 0;

			float c;
			for (int i = 0; i < items.Count; i++)
			{
				c = items[i].GetChance(score);
				chances[i] = c;
				total += c;
			}
			return chances;
		}

		public T GetRandom(float score, System.Random systemRandom)
		{
			var chances = GetChances(score, out float sum);

			double random = systemRandom.NextDouble() * sum;
			for (int i = 0; i < items.Count; i++)
			{
				if (random < chances[i])
				{
					return Take(i);
				}
				else
				{
					random -= chances[i];
				}
			}
			return default;
		}

		public T GetRandom(float score = 0)
		{
			var chances = GetChances(score, out float sum);

			float random = Random.value * sum;
			for (int i = 0; i < items.Count; i++)
			{
				if (random < chances[i])
				{
					return Take(i);
				}
				else
				{
					random -= chances[i];
				}
			}
			return default;
		}

		public T GetRandom(float score, T[] source)
		{
			var chances = GetChances(score, out float sum);
			float random = Random.value * sum;
			for (int i = 0; i < items.Count; i++)
			{
				if (random < chances[i])
				{
					return source[i];
				}
				else
				{
					random -= chances[i];
				}
			}
			return default;
		}

		public virtual T Take(int index)
		{
			return items[index].item;
		}
	}
}