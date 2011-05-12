using System;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities.Web
{
	[Serializable]
	public class BaseSession
	{
		protected SessionData Data = new SessionData();

		/// <summary>
		/// Calls Clear() on the encapsulated SessionData object.
		/// </summary>
		public void Clear()
		{
			Data.Clear();
		}
	}
}