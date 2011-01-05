using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities.Usefulness
{
	[Serializable]
	public class UsefulnessEntityList : List<IUsefulnessEntity>
	{
		public Iesi.Collections.Generic.ISet<int> GetEntityIdsByEntityType(IUsefulnessEntity usefulnessEntity)
		{
			var set = new HashedSet<int>();
			foreach (var entity in this)
				if (entity.Type == usefulnessEntity.Type)
					set.Add(entity.Id);
			return set;
		}
	}
}