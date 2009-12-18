using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Usefulness
{
	[Serializable]
	public class UsefulnessEntityProxy : IUsefulnessEntity
	{
		public int Id { get; set; }
		public string Type { get; private set; }

		public UsefulnessValue Usefulness { get; private set; }

		public UsefulnessEntityProxy(IUsefulnessEntity usefulnessEntity)
		{
			Id = usefulnessEntity.Id;
			Type = usefulnessEntity.Type;
			Usefulness = usefulnessEntity.Usefulness;
		}

		public UsefulnessEntityProxy(UsefulnessEntry usefulnessEntry)
		{
			Id = usefulnessEntry.EntityId;
			Type = usefulnessEntry.EntityType;
//			Usefulness = new UsefulnessValue(usefulnessEntry.Value > 0 ? usefulnessEntry.Value : 0,
//			                                 usefulnessEntry.Value < 0 ? usefulnessEntry.Value : 0);
		}

		public UsefulnessEntityProxy()
		{
			
		}

		public UsefulnessEntityProxy(string entityType, int entitiyId)
		{
			Id = entitiyId;
			Type = entityType;
		}
	}
}
