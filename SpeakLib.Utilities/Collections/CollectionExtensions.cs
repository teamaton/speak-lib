using System;
using System.Collections.Generic;
using System.Linq;
using Iesi.Collections.Generic;

namespace SpeakFriend.Utilities.Collections
{
	public static class CollectionExtensions
	{
		public static IEnumerable<TItem> WhereSafe<TItem>(this IEnumerable<TItem> coll, Func<TItem, bool> predicate)
		{
			if (coll.Count() <= 0)
				return coll;

			return coll.Where(predicate);
		}
	}
}