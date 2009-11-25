using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Usefulness
{
	[Serializable]
	public class UsefulnessEntityCore : IUsefulnessEntity
	{
		public int Id { get; set; }
		public UsefulnessValue Usefulness { get; private set; }

		public UsefulnessEntityCore(IUsefulnessEntity usefulnessEntity)
		{
			Id = usefulnessEntity.Id;
			Usefulness = usefulnessEntity.Usefulness;
		}

		public UsefulnessEntityCore()
		{
			
		}
	}
}
