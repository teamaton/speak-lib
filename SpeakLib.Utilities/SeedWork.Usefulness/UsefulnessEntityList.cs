using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Usefulness
{
	[Serializable]
	public class UsefulnessEntityList : List<IUsefulnessEntity>
	{
		public ISet<int> GetEntityIdsByEntityType(IUsefulnessEntity usefulnessEntity)
		{
			var set = new HashSet<int>();
			foreach (var entity in this)
				if (entity.Type == usefulnessEntity.Type)
					set.Add(entity.Id);
			return set;
		}
	}
}
