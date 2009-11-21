using System;

namespace SpeakFriend.Utilities.Usefulness
{
	public class UsefulnessEntry : IMutablePersistable
	{
		public int Id { get; set; }
		public int Value { get; set; }

		public int UsefulEntityId { get; set; }
		public string UsefulEntityType { get; set; }

		public int RatingEntityId { get; set; }
		public string RatingEntityType { get; set; }

		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }

		public UsefulnessEntry(ICanBeUseful usefulEntity, int value)
		{
			UsefulEntityId = usefulEntity.Id;
			Value = value;
		}

		public UsefulnessEntry(ICanBeUseful usefulEntity, int value, ICanRateUsefulness rater)
		{
			Value = value;
			UsefulEntityId = usefulEntity.Id;
			UsefulEntityType = usefulEntity.GetType().Name;
			RatingEntityId = rater.Id;
			RatingEntityType = rater.GetType().Name;
		}
	}
}