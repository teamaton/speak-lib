using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Usefulness
{
	public class UsefulnessValue
	{
		private readonly UsefulnessService _usefulnessService;
		private readonly IUsefulnessEntity _entity;

		public delegate UsefulnessValue Factory(IUsefulnessEntity entity);

		private int? _positive;
		public int Positive
		{
			get
			{
				if (!_positive.HasValue) 
					InitializeValues();

				return _positive.Value;
			}
			private set { _positive = value; }
		}

		private int? _negative;
		public int Negative
		{
			get
			{
				if (!_negative.HasValue)
					InitializeValues();

				return _negative.Value;
			}
			private set { _negative = value; }
		}

		public int Count { get { return Convert.ToInt32(Positive + Math.Abs(Negative)); } }

		private void InitializeValues()
		{
			var usefulnessValue = _usefulnessService.GetUsefulnessValueByEntity(_entity);
			_positive = usefulnessValue.Positive;
			_negative = usefulnessValue.Negative;
		}

		public UsefulnessValue()
		{
			
		}

		public UsefulnessValue(IUsefulnessEntity entity, UsefulnessService usefulnessService)
		{
			_entity = entity;
			_usefulnessService = usefulnessService;
		}

		public UsefulnessValue(int initialPositive, int initialNegative)
		{
			Positive = initialPositive;
			Negative = initialNegative;
		}
	}
}
