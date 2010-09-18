using System;

namespace SpeakFriend.Utilities
{
	public interface IPersistable
	{
		int Id { get; set; }

		DateTime DateCreated { get; set; }
	}
}