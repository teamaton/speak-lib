using System;

namespace SpeakFriend.Utilities.Usefulness
{
	public class UsefulnessEntry : IMutablePersistable
	{
		public int Id { get; set; }
		private int? _positiveValue;
		private int? _negativeValue;
		public int Value
		{
			get { return _positiveValue.HasValue ? _positiveValue.Value : _negativeValue.Value; }
			set
			{
				if (value == 0)
					throw new ArgumentException("Value must be positive or negative!", "value");
				if (value > 0)
					_positiveValue = value;
				else
					_negativeValue = value;
			}
		}

		public int EntityId { get; set; }
		public string EntityType { get; set; }

		public int CreatorId { get; set; }
		public string CreatorType { get; set; }

		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }

		public UsefulnessEntry(){}

		public UsefulnessEntry(IUsefulnessEntity usefulEntity, int value)
			: this(usefulEntity, value, new UsefulnessCreatorAnonymous())
		{
		}

		public UsefulnessEntry(IUsefulnessEntity usefulEntity, int value, IUsefulnessCreator creator)
		{
			Value = value;
			EntityId = usefulEntity.Id;
			EntityType = usefulEntity.Type;
			CreatorId = creator.Id;
			CreatorType = creator.Type;
		}
	}
}