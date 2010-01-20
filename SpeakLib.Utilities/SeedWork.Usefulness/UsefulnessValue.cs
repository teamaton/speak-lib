using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeakFriend.Utilities.SeedWork.Usefulness;

namespace SpeakFriend.Utilities.Usefulness
{
	[Serializable]
	public class UsefulnessValue
	{
		[NonSerialized]
		private Func<UsefulnessService> _usefulnessService;
		[NonSerialized]
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

		public double Percentage { get { return Count > 0 ? Positive / (double) Count : 0; } }

		private void InitializeValues()
		{
			var usefulnessServiceTemp = _usefulnessService();
			try
			{
				var usefulnessValue = usefulnessServiceTemp.GetUsefulnessValueByEntity(_entity);
				_positive = usefulnessValue.Positive;
				_negative = usefulnessValue.Negative;
			}
			catch (InvalidOperationException ioe)
			{
				usefulnessServiceTemp.Session.Connection.Close();
				usefulnessServiceTemp.Session.Connection.Dispose();

				throw new UsefulnessException(
					"Exception while reading Usefulness values from DB! " +
					"Manually closed and disposed the ADO.NET Connection.", ioe);
			}
		}

		public UsefulnessValue()
		{
			
		}

		public UsefulnessValue(IUsefulnessEntity entity, Func<UsefulnessService> usefulnessService)
		{
			_entity = entity;
			_usefulnessService = usefulnessService;
		}

		public UsefulnessValue(int initialPositive, int initialNegative)
		{
			Positive = initialPositive;
			Negative = initialNegative;
		}

		#region Unused but might be useful?!

		public bool HasService
		{
			get { return _usefulnessService != null; }
		}

		public void SetService(Func<UsefulnessService> usefulnessService)
		{
			_usefulnessService = usefulnessService;
		}

		#endregion
	}
}
