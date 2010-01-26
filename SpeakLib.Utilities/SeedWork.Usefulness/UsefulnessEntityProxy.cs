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

		public UsefulnessValue Usefulness { get; protected internal set; }

		public UsefulnessEntityProxy(UsefulnessEntry usefulnessEntry)
		{
			Id = usefulnessEntry.EntityId;
			Type = usefulnessEntry.EntityType;
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
