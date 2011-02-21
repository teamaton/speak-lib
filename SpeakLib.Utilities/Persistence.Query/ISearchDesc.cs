using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
	public interface ISearchDesc : IPager
	{
		ConditionContainer Filter { get; }
		OrderByCriteria OrderBy { get; }
	}
}