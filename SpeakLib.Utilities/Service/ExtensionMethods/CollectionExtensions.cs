using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
	public static class CollectionExtensions
	{
		/// <summary>
		/// Shuffles the items in the list, changes the list! Uses a Random instance with changing seed.
		/// </summary>
		/// <remarks>
		/// User Fisher-Yates (see http://en.wikipedia.org/wiki/Fisher-Yates_shuffle)
		/// recommended here: http://nunos.zi-yu.com/2008/03/shuffle-a-collection/
		/// </remarks>
		public static void Shuffle(this IList elements)
		{
			var random = new Random(DateTime.Now.Millisecond);
			int n = elements.Count;
			while (n > 1)
			{
				int k = random.Next(n);
				--n;
				var temp = elements[n];
				elements[n] = elements[k];
				elements[k] = temp;
			}
		}
	}
}
