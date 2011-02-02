using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SpeakFriend.Utilities;

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
		public static IList Shuffle(this IList elements)
		{
			var random = new Random();

			var n = elements.Count;

			while (n > 1)
			{
				var k = random.Next(n);
				--n;
				var temp = elements[n];
				elements[n] = elements[k];
				elements[k] = temp;
			}

			return elements;
		}

		/// <summary>
		/// Shuffles the items in the list, changes the list! 
		/// Uses a <see cref="Random"/> instance with changing seed.
		/// <br/>
		/// Strongly typed version of <see cref="Shuffle"/>.
		/// </summary>
		/// <typeparam name="TList"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static TList Shuffle<TList>(this TList list) where TList : IList
		{
			var random = new Random();

			var n = list.Count;

			while (n > 1)
			{
				var k = random.Next(n);
				--n;
				var temp = list[n];
				list[n] = list[k];
				list[k] = temp;
			}

			return list;
		}

		/// <summary>
		/// Shuffles the items in the list, changes the list! 
		/// Uses a <see cref="Random"/> instance with changing seed.
		/// <br/>
		/// Strongly typed version of <see cref="Shuffle"/>.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static IList<T> Shuffle<T>(this IList<T> list)
		{
			var random = new Random();

			var n = list.Count;

			while (n > 1)
			{
				var k = random.Next(n);
				--n;
				var temp = list[n];
				list[n] = list[k];
				list[k] = temp;
			}

			return list;
		}

		/// <summary>
		/// Returns all elements of a collection that occur more than once in the given collection.
		/// </summary>
		public static IEnumerable<T> Duplicates<T>(this IEnumerable<T> collection)
		{
			return collection.Where(item1 => collection.Count(item2 => item2.Equals(item1)) > 1);
		}

		/// <summary>
		/// Dynamically converts an IEnumerable to a generic list based on the given type.
		/// </summary>
		/// <remarks>
		/// This method is suitable when the <param name="type">type</param> parameter's value is determined at runtime.
		/// </remarks>
		/// <example>
		/// foreach (var group in Model.GroupBy(item=>item.GetType()))
		/// {
		///     Html.RenderPartial(string.Format("GeoObjectView/Structure/{0}s", group.Key.Name), group.AsListOf(group.Key));
		/// }
		/// </example>
		public static IList AsListOf(this IEnumerable coll, Type type)
		{
			var result = (IList) typeof (List<>).MakeGenericType(type).GetConstructor(Type.EmptyTypes).Invoke(null);
			foreach (var item in coll) result.Add(item);
			return result;
		}

		public static List<T> Add<T>(this List<T> list, params T[] items)
		{
			list.AddRange(items);
			return list;
		}

		/// <summary>
		/// Returns a single string, representing the ToString values of the objects in the given collection.
		/// </summary>
		public static string JoinToString<T>(this IEnumerable<T> collection)
		{
			if (collection == null)
				return "null";

			if (collection.Count() <= 0)
				return "empty";

			return collection.Select(o => o.ToString()).JoinNonEmpty(", ");
		}
	}
}