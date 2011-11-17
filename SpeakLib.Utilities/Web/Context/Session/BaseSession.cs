using System;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities.Web
{
	[Serializable]
	public class BaseSession
	{
		protected SessionData Data = new SessionData();
		protected RequestData RequestData = new RequestData();

		/// <summary>
		/// Calls Clear() on the encapsulated SessionData object.
		/// </summary>
		public void Clear()
		{
			Data.Clear();
		}

		public SessionData GetData()
		{
			return Data;
		}
	}
}