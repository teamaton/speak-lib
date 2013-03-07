using System;

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
		[Obsolete(
			"Replace this by setting a prefix and removing all data with " +
			"keys matching that prefix. Use Data.Clear(dataPrefixes).")]
		public void Clear()
		{
			Data.Clear();
		}

		public void Clear(params string[] dataPrefixes)
		{
			Data.Clear(dataPrefixes);
		}

		public SessionData GetData()
		{
			return Data;
		}
	}
}