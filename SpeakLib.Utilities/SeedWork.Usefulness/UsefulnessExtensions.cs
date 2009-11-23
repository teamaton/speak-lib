using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Usefulness
{
	public static class UsefulnessExtensions
	{
		public static void Add(this UsefulnessValue entity, IUsefulnessCreator rater)
		{
			
		}

		public static void Usefulness(this IUsefulnessEntity entity, UsefulnessService service)
		{
			var usefulnessValue = service.GetByEntity(entity);
			entity.Usefulness = usefulnessValue;
		}
	}
}
